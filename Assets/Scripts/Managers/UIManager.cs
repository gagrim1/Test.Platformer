using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameData gameData;
    public Image healthBar;
    public Text scoreText;

    public void RedrawHealthBar()
    {
        PlayerData playerData = gameData.playerData;
        healthBar.fillAmount = playerData.healthPoints / playerData.maxHealthPoints;
    }
}
