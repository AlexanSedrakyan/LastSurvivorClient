using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //public string serverIP = "109.75.46.138"; // real IP
    //public string serverIP = "192.168.0.106"; // work IP
    public string serverIP = "192.168.5.53"; // home IP

    public GameObject startMenu;
    public InputField usernameField;
    public InputField IPField;
    public GameObject world;
    public Button restartButton;

    public Transform joystickCircleOuter;
    public Transform joystickCircleInner;
    public Transform jumpButton;

    public Text pingStatus;
    public Text debugText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // canvas items
            startMenu.SetActive(true);
            usernameField.interactable = true;
            IPField.interactable = true;

            restartButton.gameObject.SetActive(false);
            joystickCircleOuter.gameObject.SetActive(false);
            joystickCircleInner.gameObject.SetActive(false);
            jumpButton.gameObject.SetActive(false);

        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }


    // TODO: ping doesn't work correctly
    /*
    IEnumerator StartPing(string ip)
    {
        //WaitForSeconds f = new WaitForSeconds(0.05f);
        Ping p = new Ping(ip);
        
        while (p.isDone == false)
        {
            //yield return f;
            yield return 0;
        }
        Debug.Log(p.time);
        yield return p.time;
        //PingFinished(p);
    }

    public void PingFinished(Ping p)
    {
        // stuff when the Ping p has finshed....
        
        pingStatus.text = p.time.ToString();
    }
    */

    /// <summary>Attempts to connect to the server.</summary>
    public void ConnectToServer()
    {
        if (IPField.text == "")
        {
            IPField.text = serverIP; // work IP
        }
        Client.instance.ConnectToServer(IPField.text);

        //StartCoroutine(StartPing(serverIP));
    }

    public void DisableConnectMenu()
    {
        startMenu.SetActive(false);
        restartButton.gameObject.SetActive(true);
        usernameField.interactable = false;
        IPField.interactable = false;

        joystickCircleOuter.gameObject.SetActive(true);
        joystickCircleInner.gameObject.SetActive(true);
        jumpButton.gameObject.SetActive(true);

        world.GetComponent<GridGenerator>().CreateGridMap();
    }

    public void RequireRestart()
    {
        ClientSend.ResetWorld();
    }
}
