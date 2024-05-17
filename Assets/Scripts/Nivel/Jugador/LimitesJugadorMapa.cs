using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitesJugadorMapa : MonoBehaviour
{
    #region Variables
    private Camera _camaraPrincipal;
    private Vector2[] _limites;
    #endregion

    #region Funciones de Actualización
    void Start()
    {
        _camaraPrincipal = Camera.main;

        // Inicializa los límites del área visible de la cámara
        _limites = ActualizarLimitesCamara(_camaraPrincipal);

        Camera.onPreCull += ActualizarLimites;
    }

    // Calcula la nueva posición restringida del jugador
    void LateUpdate()
    {
        Vector3 nuevaPosicion = RestringirPosicion(transform.position, _limites[0], _limites[1]);
        transform.position = nuevaPosicion;
    }

    // Calcula los límites de la cámara con Vector2 para su uso como parámetro en RestringirPosicion()
    Vector2[] ActualizarLimitesCamara(Camera camara)
    {
        float mitadAltura = camara.orthographicSize;
        float mitadAncho = camara.aspect * mitadAltura;

        Vector2 limitesMinimos = (Vector2)camara.transform.position - new Vector2(mitadAncho, mitadAltura);
        Vector2 limitesMaximos = (Vector2)camara.transform.position + new Vector2(mitadAncho, mitadAltura);

        return new Vector2[] { limitesMinimos, limitesMaximos };
    }
    #endregion

    #region Métodos Auxiliares
    void ActualizarLimites(Camera camara)
    {
        if (camara == _camaraPrincipal)
        {
            _limites = ActualizarLimitesCamara(camara);
        }
    }

    // Restringe la posición del jugador dentro de los límites del área visible de la cámara
    Vector3 RestringirPosicion(Vector3 posicion, Vector2 limitesMinimos, Vector2 limitesMaximos)
    {
        posicion.x = Mathf.Clamp(posicion.x, limitesMinimos.x, limitesMaximos.x);
        posicion.y = Mathf.Clamp(posicion.y, limitesMinimos.y, limitesMaximos.y);

        return posicion;
    }
    #endregion
}