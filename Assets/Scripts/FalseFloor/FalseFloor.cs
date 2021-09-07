using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseFloor : MonoBehaviour
{
    //[SerializeField] FalseFloor[] group;
    Renderer myRend;
    Rigidbody myRB;

    //[SerializeField] bool revealPath;

    public bool isFalse;

    private void OnEnable()
    {
        myRend = GetComponent<Renderer>();
        myRB = GetComponent<Rigidbody>();

        if (!isFalse)
        {
            Destroy(myRB);
            myRB = null;
        }
    }

    // Start is called before the first frame update
    /*
    void Start()
    {
        if (group.Length == 0)
            return;

        int pathIndex = Random.Range(0, group.Length);
        for (int i=0; i < group.Length; i++)
        {
            if (pathIndex == i)
            {
                group[i].SetPath();
            }
            else
            {
                group[i].SetFalsePath();
            }
        }
    }

    public void SetPath()
    {
        isFalse = false;
        myRB.isKinematic = true;
        if (revealPath)
        {
            myRend.material.SetColor("_Color", Color.green);
        }
    }

    public void SetFalsePath()
    {
        isFalse = true;
        myRB.isKinematic = false;
        if (revealPath)
        {
            myRend.material.SetColor("_Color", Color.red);
        }
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (isFalse)
            {
                if (myRB)
                    myRB.isKinematic = false;
                myRend.material.SetColor("_Color", Color.red);
            }
            else
            {
                if (myRB)
                    myRB.isKinematic = true;
                myRend.material.SetColor("_Color", Color.green);
            }
        }

        if (collision.transform.tag == "Zone")
        {
            Destroy(gameObject);
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
