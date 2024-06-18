using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayer", menuName = "ScriptablesObjects/Characters")]
public class Characters : ScriptableObject
{
    [SerializeField] GameObject _player;
    public Sprite Image;
}