using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Variables
    public List<Characters> _characters;
    #endregion

    #region Singleton
    public static CharacterManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("¡Más de un CharacterManager en la escena!");
            Destroy(gameObject);
        }
    }
    #endregion
}
