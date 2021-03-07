using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @author yuriromanov
 */
public static class Loader
{
    public enum Scene
    {
        MainMenu,
        TestLevel
    }
    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}
