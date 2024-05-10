using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _textoPuntos;
    [SerializeField] private GameObject[] _iconosVida;

    private void Update()
    {
        _textoPuntos.text = _gameManager.PuntosTotales.ToString();
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        _textoPuntos.text = puntosTotales.ToString();
    }

    public void DesactivarVida(int indice)
    {
        _iconosVida[indice].SetActive(false);
    }

    public void ActivarVida(int indice)
    {
        _iconosVida[indice].SetActive(true);
    }
}