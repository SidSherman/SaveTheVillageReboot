using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
   [SerializeField] private AudioSource _music;
   [SerializeField] private string[] _dialogeLines;
   [SerializeField] private TextMeshProUGUI _text;
   [SerializeField] private GameObject _gamePanel;


   public void PlayLine(int line)
   {
      if (_text && line >=0 && line < _dialogeLines.Length)
         
         _text.text = _dialogeLines[line];
   }
   
   public void Clear()
   {
      if (_text)
         _text.text = "";
   }
   public void ShowUI()
   {
      if (_gamePanel)
         _gamePanel.SetActive(true);
   }


   public void PlayMusic()
   {
      if(_music)
      _music.Play();
   }
}
