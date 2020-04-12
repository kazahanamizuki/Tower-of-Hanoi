using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    // set to true when player grabs a plate
    private bool isGrabed;

    // these two variables contains the sticks
    public Transform sticksParentObject;
    public List<Transform> sticks;

    private Camera mainCamera;
    public GameObject gameManager;

    // these variables are used to locate (localPosition of) plates
    private float standardPlateLocalPosition = -0.55f;
    private float plateHeight = 0.1f;

    // this contains the stick where the plate go back to when it can't drop to new parent stick (because a smaller plate exists)
    private Transform originalParentStick;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        isGrabed = false;

        // add all sticks to "sticks" variable, so it does not need to set stick variables in Unity editor
        sticks = getAllChildren(sticksParentObject);
        originalParentStick = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (isGrabed)
            {
                Drop();
            }
        }

        if (isGrabed)
        {
            movePlateToMouse();
        }
    }

    // will be used in Stick.cs
    public void Grab()
    {
        transform.position = transform.parent.GetChild(0).transform.position;
        originalParentStick = transform.parent;
        isGrabed = true;
    }

    public void Drop()
    {
        if (!droppingPlateIsSmaller(transform))
        {
            // set parent back to originalParentStick and play some Error SE, when the plate can't drop to new parent stick (because a smaller plate exists)
            gameManager.GetComponent<GameManager>().playErrorSound();
            if (GameObject.Find("Tutorial") != null)
            {
                GameObject.Find("Tutorial").GetComponent<Tutorial>().errorBlink();
            }
            transform.parent = originalParentStick;
        }
        isGrabed = false;
        // set the plate to the top of the stick
        var topY = standardPlateLocalPosition + (transform.parent.childCount - 1) * plateHeight;
        transform.parent.GetChild(transform.parent.childCount - 1).localPosition = new Vector2(0f, topY);

        if (gameManager.GetComponent<GameManager>().checkIfLevelisClear())
        {
            gameManager.GetComponent<GameManager>().levelClear();
        }

    }

    private bool droppingPlateIsSmaller(Transform t)
    {
        if(t.parent.childCount <= 2)
        {
            return true;
        }
        else
        {
            var topPlateOfStick = t.parent.GetChild(t.parent.childCount - 2);
            if(gameObject.GetComponent<PlateSize>().getSize() <= topPlateOfStick.gameObject.GetComponent<PlateSize>().getSize())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // only called when isGrabed is true, move the plate with the mouse cursor
    public void movePlateToMouse()
    {
        Transform nearestStick = transform.parent;
        foreach (var stick in sticks)
        {
            Vector2 plateToStick = stick.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 plateToNearestStick = nearestStick.position - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            if (plateToStick.sqrMagnitude < plateToNearestStick.sqrMagnitude)
            {
                nearestStick = stick;
            }
            transform.parent = nearestStick;
            transform.position = transform.parent.GetChild(0).transform.position;
        }
    }

    List<Transform> getAllChildren(Transform t)
    {
        if (t.childCount == 0)
        {
            return null;
        }
        else
        {
            List<Transform> list = new List<Transform>();
            for (int i = 0; i < t.childCount; i++)
            {
                list.Add(t.GetChild(i));
            }
            return list;
        }
    }
}
