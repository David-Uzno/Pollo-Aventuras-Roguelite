using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    private int _currentIndex;
    [SerializeField] private Image _characterImage;
    private CharacterManager _characterManager;

    void Start()
    {
        _characterManager = CharacterManager.Instance;

        /*_currentIndex = PlayerPrefs.GetInt("PlayerIndex", 0);*/

        UpdateCharacterDisplay();
    }

    void UpdateCharacterDisplay()
    {
        /*PlayerPrefs.SetInt("PlayerIndex", _currentIndex);*/
        _characterImage.sprite = _characterManager.CharacterIndex[_currentIndex].Image;
    }

    public void NextCharacter()
    {
        // Circular al siguiente índice
        if (_currentIndex == _characterManager.CharacterIndex.Count - 1)
        {
            _currentIndex = 0;
        }
        else
        {
            _currentIndex += 1;
        }

        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        // Circular al anterior índice
        if (_currentIndex == 0)
        {
            _currentIndex = _characterManager.CharacterIndex.Count - 1;
        }
        else
        {
            _currentIndex -= 1;
        }
         
        UpdateCharacterDisplay();
    }

    public void StarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}