using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author yuriromanov
 */
public class MainMenuButtonManager : MonoBehaviour
{
    public void StartGame()
    {
        Loader.Load(Loader.Scene.TestLevel);
    }
}
