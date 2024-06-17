using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadScene(string sceneName)
    {
        // Usar el SceneManager de Unity para cargar la escena
        SceneManager.LoadScene(sceneName);
    }

    private void OnEnable()
    {
        // Suscribirse al evento de escena cargada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Desuscribirse del evento de escena cargada
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Notificar al GameManager para que verifique si debe destruirse
        GameManager.Instance?.CheckScene();
    }
}