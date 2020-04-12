using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int tutorialProcess;
    private bool firstGrab = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector2(GameObject.Find("Stick1").transform.position.x, GameObject.Find("Stick1").transform.position.y + 4.5f));
        transform.GetChild(1).position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector2(GameObject.Find("Stick2").transform.position.x, GameObject.Find("Stick2").transform.position.y + 4.5f));
        transform.GetChild(2).position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector2(GameObject.Find("Stick3").transform.position.x, GameObject.Find("Stick3").transform.position.y + 4.5f));
        tutorialProcess = 1;
    }

    // Update is called once per frame
    void Update()
    {
        showTutorial(tutorialProcess);
        if(Input.GetMouseButtonDown(0) && firstGrab && tutorialProcess == 1)
        {
            tutorialProcess++;
            firstGrab = false;
        }
        if(Input.GetMouseButtonUp(0) && tutorialProcess == 2)
        {
            tutorialProcess++;
        }
    }

    public void showTutorial(int index)
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            if (i + 1 == index)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }

    public void errorBlink() // show the error message when player drops a plate on a smaller one
    {
        transform.GetChild(3).GetComponent<Animator>().SetTrigger("cantDrop");
    }

    public void setFirstGrab()
    {
        firstGrab = true;
    }
}
