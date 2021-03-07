using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject main;
    [SerializeField]
    private GameObject setting;
    [SerializeField]
    private GameObject pauseField;

    // Start is called before the first frame update
    void Start()
    {
        main.SetActive(true);
        setting.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseResumeButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseMainButton()
    {
        main.SetActive(true);
        setting.SetActive(false);
    }

    public void PauseSettingButton()
    {
        main.SetActive(false);
        setting.SetActive(true);
    }

    public void PauseBackButton()
    {
        main.SetActive(true);
        setting.SetActive(false);
    }

    public void PauseMainMenuButton()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
