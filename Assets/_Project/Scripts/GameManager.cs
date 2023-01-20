using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
   
    [SerializeField] private GameMenu _gameMenu;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;
    private PlayerControlls _input;
    private int _gameState = 2;
    private static int _score = 0;
    private static int _enemyKilled = 0;
    private static float _levelTime = 0.0f;

    private int PAUSE_STATE = 1;
    private int GAME_STATE = 2;
    private int CUTSCENE_STATE = 3;
    public static int EnemyKilled { get => _enemyKilled; set => _enemyKilled = value; }
    public static float LevelTime => _levelTime;
    public static int Score { get => _score; set => _score = value; }

    public static GameManager GameManagerInstance;
    private void Start()
    {

        _gameState = GAME_STATE;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if(_gameState == GAME_STATE)
            _levelTime += Time.deltaTime;
    }

    public void TogglePauseInput(InputAction.CallbackContext context)
    {
        TogglePause();
    }
    
    public void TogglePause()
    {
        Debug.Log("TogglePause");
        if (_gameState == GAME_STATE)
        {
            Pause();
        }
        else if (_gameState == PAUSE_STATE)
        {
            Play();
        }
    }
    public void Pause()
    {
        Debug.Log("Pause");
        if(_gameMenu)
            _gameMenu.PauseGame();
        Time.timeScale = 0f;
        _gameState = PAUSE_STATE;
     
    }

    public void Play()
    {
        Debug.Log("Play");
        if(_gameMenu)
            _gameMenu.ResumeGame();
        Time.timeScale = 1f;
        _gameState = GAME_STATE;
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        if (_gameMenu)
        {
            _gameMenu.LoseGame();
        }
        ClearStaticValues();
        _soundManager.PlaySound(_loseSound);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        if(_gameMenu)
            _gameMenu.FinishGame();
        ClearStaticValues();
        _soundManager.PlaySound(_winSound);
    }
    
    public void ClearStaticValues()
    {
        _score = 0;
        _enemyKilled = 0;
        _levelTime = 0.0f;
    }
    
    public void UpdateScore(int value)
    {
        _score +=value;
        if(_gameMenu)
            _gameMenu.UpdateCoins(_score);
    }
    
    public void UnsetCameraFollowObject()
    {
        _camera.Follow = null;
    }

   
}
