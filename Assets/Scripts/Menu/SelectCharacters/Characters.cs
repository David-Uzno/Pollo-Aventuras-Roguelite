using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptablesObjects/Characters")]
public class Characters : ScriptableObject
{
    public GameObject _player;
    public Sprite Image;
}