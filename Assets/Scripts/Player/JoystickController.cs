using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    public PlayerController player;
    public float speed = 15.0f;

    public Transform circle;
    public Transform outerCircle;

    private Vector2 startingPoint;
    private int leftTouch = 99;

    public GameObject mytext;
    public Camera myCamera;

    private Vector3 respawnPosition;



    void Start()
    {
        myCamera = Camera.main;
        myCamera.GetComponent<CameraController>().SetPlayerGameObject(this.gameObject);

        respawnPosition = transform.position;
        //mytext.GetComponent<Text>().text = "QQQ";
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (player.isGrounded)
        {
            mytext.GetComponent<Text>().text = "isGrounded";
        }
        else
        {
            mytext.GetComponent<Text>().text = "isNotGrounded";
        }
        */

        //player.transform.eulerAngles = new Vector3(0, 0, 0);

        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = getTouchPosition(t.position) * -1; // * -1 for perspective cameras
            //Debug.Log(touchPos.x);
            //Debug.Log(touchPos.y);

            //circle.position = new Vector3(touchPos.x*(-1), touchPos.y*(-1), 0);
            //outerCircle.position = new Vector3(touchPos.x*(-1), touchPos.y*(-1), 0);

            
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    //mytext.GetComponent<Text>().text = "Jump";
                    playerJump();
                }
                else
                {
                    //mytext.GetComponent<Text>().text = "Left";
                    leftTouch = t.fingerId;
                    startingPoint = touchPos;
                }
            }
            else if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector3 offset = new Vector3(startingPoint.x - touchPos.x, 0, startingPoint.y - touchPos.y);
                //mytext.GetComponent<Text>().text = "a" + offset.x;

                Vector3 direction = Vector3.ClampMagnitude(offset, 0.5f);

                moveCharacter(direction);

                //outerCircle.transform.position = new Vector3(touchPos.x * (-1), touchPos.y * (-1), 0);
                //circle.transform.position = new Vector2(outerCircle.transform.position.x + direction.x, outerCircle.transform.position.y + direction.y);

            }
            else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 99;
                //mytext.GetComponent<Text>().text = "a" + myCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z)).x;
                //circle.transform.position = new Vector2(outerCircle.transform.position.x, outerCircle.transform.position.y);
            }

            ++i;
        }

    }

    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        //mytext.GetComponent<Text>().text = "a" + myCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z)).x;
        return myCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }

    void moveCharacter(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void playerJump()
    {
        /*
        if (player.isGrounded)
        {
            Vector3 movement = new Vector3(0, 70f, 0);
            player.GetComponent<Rigidbody>().AddForce(movement * 5);
        }
        */
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Zone")
        {
            Debug.Log("ZONE!!!!!!");
            transform.position = respawnPosition;
        }
    }
    */
}
