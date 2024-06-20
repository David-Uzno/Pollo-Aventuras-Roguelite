using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] GameObject _prefabDefault;

    void Start()
    {
        GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");

        if (existingPlayer != null)
        {
            Debug.LogWarning("Ya existe un GameObject con tag 'Player'. No se instanciará otro.");
            transform.SetParent(existingPlayer.transform);
            return;
        }

        int _indexPlayer = PlayerPrefs.GetInt("PlayerIndex");
        GameObject newParent;

        if (CharacterManager.Instance != null && _indexPlayer < CharacterManager.Instance.CharacterIndex.Count && _indexPlayer > 0)
        {
            newParent = Instantiate(CharacterManager.Instance.CharacterIndex[_indexPlayer]._player);
        }
        else
        {
            newParent = Instantiate(_prefabDefault);

            if (CharacterManager.Instance == null)
            {
                Debug.LogWarning($"CharacterManager no está en la escena. Usando prefab por defecto para el jugador.");
            }
            else if (_indexPlayer < 0 || _indexPlayer > CharacterManager.Instance.CharacterIndex.Count)
            {
                Debug.LogWarning($"Índice fuera de rango. Usando prefab por defecto para el jugador.");
            }
            else
            {
                Debug.LogWarning($"Error. Usando prefab por defecto para el jugador.");
            }
        }

        transform.SetParent(newParent.transform);
    }
}