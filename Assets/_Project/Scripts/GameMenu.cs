using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : Menu
{
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private TextMeshProUGUI _arrowCount;
    [SerializeField] private TextMeshProUGUI _coinCount;
    [SerializeField] private TextMeshProUGUI _toolTipText;
    [SerializeField] private Image _healthValue;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _DeathPanel;
    [SerializeField] private TextMeshProUGUI _finishInfo;
    [SerializeField] private Player _player;
    [SerializeField] private GameManager _gameManager;
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
        
    }
    
    public void ResumeGame()
    {
        _gamePanel.SetActive(true);
        _menuPanel.SetActive(false);
    }
    
    public void FinishGame()
    {
        Time.timeScale = 0f;
        if (!_finishPanel)
            return;
        
        _finishPanel.SetActive(true);
        
        if (!_finishInfo)
            return;

       
        _finishInfo.text = $"Монеты: {GameManager.Score}\n" +
                           $"Враги: {GameManager.EnemyKilled}\n" +
                           $"Время: {GameManager.LevelTime.ToString("F1")}";

    }
    public void LoseGame()
    {
        
        _DeathPanel.SetActive(true);
    }
    
    public override void OpenLevelSelector()
    {
        base.OpenLevelSelector();
        _DeathPanel.SetActive(false);
        _gamePanel.SetActive(false);
        
    }
    
    public override void CloseLevelSelector()
    {
        base.CloseLevelSelector();
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
