using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @author yuriromanov
 */
public class PauseManager : MonoBehaviour
{
    public float timer = 1f;
    public bool inPause = false;
    public bool guiPause;

    [SerializeField]
    private GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(inPause);
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();   
        }

        
    }

    public void PauseGame()
    {
        inPause = !inPause;
        pauseMenu.SetActive(inPause);
        if (inPause)
        {
            timer = 0;
            guiPause = true;
        }
        else
        {
            timer = 1f;
            guiPause = false;
        }
    }

    public void OnGUI()
    {
        if (guiPause)
        {
            Cursor.visible = true;
        } 
        else
        {
            Cursor.visible = false;
        }
    }
}
