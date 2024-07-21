using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    #region Variables
    [SerializeField] int _limitFPS = 120;
    [SerializeField] FPSCounter _FPSCounter;
    #endregion

    #region Singleton
    public static FPSManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("¡Más de un FPSManager en la escena!");
            Destroy(gameObject);
        }
    }
    #endregion

    #region Unity Methods
    private void Start()
    {
        // Establece límite de FPS
        Application.targetFrameRate = _limitFPS;
    }

    private void Update()
    {
        HandleFPSInput();
    }
    #endregion

    #region FPS
    private void HandleFPSInput()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            _FPSCounter.Show();
        }
    }
    #endregion
}