using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    // determine how close the player click a stick will active the plate
    private float clickRange = 2f;
    public Camera mainCamera;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inClickRange())
        {
            if(this.transform.childCount <= 1)
            {
                // play some error SE when there is no plate in the clicked stick
                gameManager.playErrorSound();
            }
            else
            {
                // call the Grab function in Plate.cs
                this.transform.GetChild(this.transform.childCount - 1).GetComponent<Plate>().Grab();
                if(GameObject.Find("Tutorial") != null)
                {
                    GameObject.Find("Tutorial").GetComponent<Tutorial>().setFirstGrab();
                }                    
            }
        }
    }

    bool inClickRange() // check the mouse cursor position and determine whick stick is under controll)
    {
        var mouseX = mainCamera.ScreenToWorldPoint(Input.mousePosition).x;
        if (this.transform.position.x - clickRange < mouseX && mouseX < this.transform.position.x + clickRange)
            return true;
        return false;
    }
}
