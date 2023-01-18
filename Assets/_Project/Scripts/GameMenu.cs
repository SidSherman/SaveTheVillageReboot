using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _arrowCount;
    [SerializeField] private TextMeshProUGUI _coinCount;
    [SerializeField] private Image _healthValue;
    
    [SerializeField] private Player _player;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _player = player.GetComponent<Player>();
        _player.HealthComponent.onHealthChange += UpdateHealth;
        _player.onArrowChange += UpdateArrow;
    }

    public void PauseGame()
    {
        _gamePanel.SetActive(false);
        _menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void ResumeGame()
    {
        _gamePanel.SetActive(true);
        _menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void UpdateMessage(string value)
    {
        if(_messageText)
            _messageText.text = value.ToString();
    }
    
    public void UpdateCoins(int value)
    {
        if(_coinCount)
            _coinCount.text = "Coins: "+ value.ToString();
    }
    
    public void UpdateArrow(int value)
    {
        if(_arrowCount)
            _arrowCount.text = "Arrows: " + value.ToString();
    }
    public void UpdateHealth(float value)
    {
        _healthValue.fillAmount = value / 100;
    }
}
