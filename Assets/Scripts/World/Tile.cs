using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer myRenderer;

    private int id;
    GridGenerator gridGenerator;

    public void Initialize(int _id, GridGenerator _gridGenerator)
    {
        id = _id;
        gridGenerator = _gridGenerator;
    }

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = this.GetComponent<Renderer>();
        myRenderer.material.SetColor("_Color", Color.gray);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTileHealth(int health)
    {
        if (health == 75)
        {
            myRenderer.material.SetColor("_Color", Color.green);
        }
        else if (health == 50)
        {
            myRenderer.material.SetColor("_Color", Color.blue);
        }
        else if (health == 25)
        {
            myRenderer.material.SetColor("_Color", Color.red);
        }
        else if (health == 0)
        {
            //Destroy(this.gameObject);
            this.gameObject.SetActive(false);
            myRenderer.material.SetColor("_Color", Color.gray);
        }
    }

}
