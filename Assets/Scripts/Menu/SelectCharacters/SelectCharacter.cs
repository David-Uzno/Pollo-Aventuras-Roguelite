using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectCharacter : MonoBehaviour
{
    private int _index;
    [SerializeField] private Image _imageCharacter;
    private CharacterManager _characterManager;

    void Start()
    {
        _characterManager = CharacterManager.Instance;

        _index = PlayerPrefs.GetInt("PlayerIndex");
        ChangeScreen();

        if (_index > _characterManager._characters.Count - 1)
        {
            _index = 0;
        }
    }

    void ChangeScreen()
    {
        PlayerPrefs.SetInt("PlayerIndex", _index);
        _imageCharacter.sprite = _characterManager._characters[_index]._image;
    }

    public void NextCharacter()
    {
        if(_index == _characterManager._characters.Count - 1)
        {
            _index = 0;
        }
        else
        {
            _index += 1;
        }
        ChangeScreen();
    }

    public void PreviousCharacter()
    {
        if(_index == 0)
        {
            _index = _characterManager._characters.Count - 1;
        }
        else
        {
            _index -= 1;
        }
        ChangeScreen();
    }

    public void StarGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}