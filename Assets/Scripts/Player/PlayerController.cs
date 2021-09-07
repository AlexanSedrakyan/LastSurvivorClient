using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera ourMainCamera;
    private Vector2 startingPoint;
    private int leftTouch = 99;
    private int rightTouch = 99;

    private bool moveUp = false;
    private bool moveDown = false;
    private bool moveRight = false;
    private bool moveLeft = false;
    private bool jumpPressed = false;

    public Transform joystickCircleOuter;
    public Transform joystickCircleInner;

    void Start()
    {
        ourMainCamera = Camera.main;
        ourMainCamera.GetComponent<CameraController>().SetPlayerGameObject(this.gameObject);
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //ClientSend.PlayerShoot(camTransform.forward);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //ClientSend.PlayerThrowItem(camTransform.forward);
            }
        }

        HandleTouch();
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    /// <summary>Sends player input to the server.</summary>
    private void SendInputToServer()
    {
        bool[] _inputs;
        //if (Application.platform == RuntimePlatform.Android)
        if (true)
        {
            //Debug.Log("moveUp " + moveUp);
            //Debug.Log("moveDown " + moveDown);
            //Debug.Log("moveLeft " + moveLeft);
            //Debug.Log("moveRight " + moveRight);
            //Debug.Log("moveUp " + jumpPressed);
            _inputs = new bool[]
            {
                moveUp,
                moveDown,
                moveLeft,
                moveRight,
                jumpPressed
            };
        }
        else
        {
            _inputs = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.D),
                Input.GetKey(KeyCode.Space)
            };
        }


        ClientSend.PlayerMovement(_inputs);
    }

    void HandleTouch()
    {
        // for the touch movement
        if (Input.touchCount > 0)
        {
            Debug.Log("Left touch" + leftTouch);
            Debug.Log("Right touch" + rightTouch);
            foreach (Touch touchIndex in Input.touches)
            {
                //Touch theTouch = Input.GetTouch(0);
                Touch theTouch = touchIndex;

                //Debug.Log(Input.touchCount);
                //Debug.Log(theTouch.position);
                //Vector2 touchPos = getTouchPosition(theTouch.position) * -1;

                if (theTouch.phase == TouchPhase.Began)
                {
                    if (theTouch.position.x > Screen.width / 2)
                    {
                        if (theTouch.position.y < Screen.height / 2)
                        {
                            jumpPressed = true;
                            rightTouch = theTouch.fingerId;
                        }
                    }
                    else
                    {
                        //mytext.GetComponent<Text>().text = "Left";
                        leftTouch = theTouch.fingerId;
                        //startingPoint = touchPos;
                        startingPoint = theTouch.position;
                    }
                }
                else if (theTouch.phase == TouchPhase.Moved)
                {
                    if (leftTouch == theTouch.fingerId)
                    {
                        //Vector3 offset = new Vector3(startingPoint.x - touchPos.x, 0, startingPoint.y - touchPos.y);
                        //Vector3 direction = Vector3.ClampMagnitude(offset, 0.5f);
                        //transform.Translate(direction * speed * Time.deltaTime);

                        /*
                        Debug.Log("startingPoint.x " + startingPoint.x);
                        Debug.Log("startingPoint.y " + startingPoint.y);
                        Debug.Log("touchPos.x " + touchPos.x);
                        Debug.Log("touchPos.y " + touchPos.y);
                        */

                        float touchDiffThreshold = 10f;
                        if (startingPoint.x > theTouch.position.x + touchDiffThreshold)
                        {
                            moveLeft = true;
                            moveRight = false;
                        }
                        else if (startingPoint.x + touchDiffThreshold < theTouch.position.x)
                        {
                            moveLeft = false;
                            moveRight = true;
                        }
                        else
                        {
                            moveLeft = false;
                            moveRight = false;
                        }

                        if (startingPoint.y > theTouch.position.y + touchDiffThreshold)
                        {
                            moveDown = true;
                            moveUp = false;
                        }
                        else if (startingPoint.y + touchDiffThreshold < theTouch.position.y)
                        {
                            moveDown = false;
                            moveUp = true;
                        }
                        else
                        {
                            moveDown = false;
                            moveUp = false;
                        }

                        Vector2 offset = theTouch.position - startingPoint;
                        Vector2 direction = Vector2.ClampMagnitude(offset, 20.0f);

                        UIManager.instance.joystickCircleInner.transform.position = new Vector2(UIManager.instance.joystickCircleOuter.transform.position.x + direction.x,
                            UIManager.instance.joystickCircleOuter.transform.position.y + direction.y);
                    }
                    else
                    {

                    }
                }
                else if (theTouch.phase == TouchPhase.Ended)
                {
                    if (leftTouch == theTouch.fingerId)
                    {
                        leftTouch = 99;
                        moveUp = false;
                        moveDown = false;
                        moveRight = false;
                        moveLeft = false;

                        UIManager.instance.joystickCircleInner.transform.position = UIManager.instance.joystickCircleOuter.transform.position;
                    }
                    else if (rightTouch == theTouch.fingerId)
                    {
                        rightTouch = 99;
                        jumpPressed = false;
                    }
                }
            }
        }
    }

    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return ourMainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
}
