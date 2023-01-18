using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private GameMenu _gameMenu;
    public int Score { get => _score; set => _score = value; }

    

    private void Start()
    {
        Time.timeScale = 1f;
    }
    
    public void LoadFinishMessage()
    {
        if(_gameMenu)
            _gameMenu.UpdateMessage("Finish");
    }
}
