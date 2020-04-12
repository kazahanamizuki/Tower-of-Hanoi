using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioSource errorSound;
    public GameObject canvas;  // this is the level clear UI
    public Button next, exit;  // buttons for next level or exit game(go to top page when builds in WebGL)
    public List<Transform> plates;

    // these two variables contains the sticks
    public Transform sticksParentObject;
    public List<Transform> sticks;

    private string topPage = "http://www.8th-destroyers.net/";
    [DllImport("__Internal")]
    private static extern void toTopPage(string str);

    // Start is called before the first frame update
    void Start()
    {
        canvas.SetActive(false);
        // add all sticks to "sticks" variable, so it does not need to set stick variables in Unity editor
        sticks = getAllChildren(sticksParentObject);
    }

    // Update is called once per frame
    void Update()
    {
       
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

    public void playErrorSound()
    {
        errorSound.Play();
    }

    public bool checkIfLevelisClear()
    {
        foreach (var plate in plates)
        {
            if(plate.parent.name != "Stick3")
            {
                return false;
            }
        }
        return true;
    }

    public void levelClear()
    {
        canvas.SetActive(true);

        // disable tutorials(only be active in level 1)
        if(GameObject.Find("Tutorial") != null)
        {
            GameObject.Find("Tutorial").SetActive(false);
        }

        // diable controlls on sticks (TODO: use a raycast block UI may be better)
        foreach (var stick in sticks)
        {
            stick.GetComponent<Stick>().enabled = false;
        }   
        
        // if this is the last scene, disable next button and move exit button to the center of screen
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            Destroy(next.gameObject);
            exit.transform.localPosition = new Vector2(0f, exit.transform.localPosition.y);
        }
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void reloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void exitGame()
    {
        toTopPage(topPage);
    }
}
