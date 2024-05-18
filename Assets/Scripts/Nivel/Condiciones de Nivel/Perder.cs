using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Perder : MonoBehaviour
{
    public void IrAlMenu(string Menu)
    {
        if (string.IsNullOrEmpty(Menu))
        {
            Debug.LogError("El nombre de la escena no puede estar vacío o ser nulo.");
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(Menu))
        {
            Debug.LogError("La escena '" + Menu + "' no existe o no está incluida en la lista de escenas de construcción.");
            return;
        }

        try
        {
            SceneManager.LoadScene(Menu);
            Debug.Log("Cargando la escena: " + Menu);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al cargar la escena '" + Menu + "': " + e.Message);
        }
        GameManager.Instancia.IrAlMenu("Menu");
    }

    public void VolverALv1()
    {
        try
        {
            SceneManager.LoadScene("Pruebas");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al cargar el nivel 1: " + e.Message);
        }
    }
}