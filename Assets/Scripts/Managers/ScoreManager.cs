using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public PlayerData playerData;
    UnityEvent scoreChangedEvent;
    UIManager _ui;

    void Start()
    {
        _ui = GameObject.FindWithTag("LevelScene").GetComponent<UIManager>();
        playerData.coinScore = 0;
        if (scoreChangedEvent == null) scoreChangedEvent = new UnityEvent();
        scoreChangedEvent.AddListener(_ui.RedrawCoinScore);
    }

    public void GetCoin()
    {
        playerData.coinScore++;
        scoreChangedEvent.Invoke();
    }
}
