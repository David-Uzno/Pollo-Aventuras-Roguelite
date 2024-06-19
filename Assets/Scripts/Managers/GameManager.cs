using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Header("Lifes")]
    [SerializeField] Image[] _heartImages;
    [SerializeField] Sprite[] _heartStatuses;
    [SerializeField] int _currentHeartCount;
    [SerializeField] int _playerLifes = 6;
    static int _minHeartCount = 3;
    static int _maxHeartCount = 5;
    private int _maxLifeMultiplier = 2;
    #endregion

    #region Singleton
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("¡Más de un GameManager en la escena!");
            Destroy(gameObject);
        }

        CheckScene();
    }
    #endregion

    #region Unity Methods
    private void OnEnable()
    {
        Heart.OnHeartCollected += UpdateHeartsCurrents;
    }

    private void OnDisable()
    {
        Heart.OnHeartCollected -= UpdateHeartsCurrents;
    }

    private void Start()
    {
        // Asegura que _currentHeartCount y _playerLifes estén dentro de los límites establecidos
        _currentHeartCount = Mathf.Clamp(_currentHeartCount, _minHeartCount, _maxHeartCount);
        _playerLifes = Mathf.Clamp(_playerLifes, 1, _currentHeartCount * 2);

        // Asignación de maxLifeMultiplier al número de elementos de _heartStatuses
        _maxLifeMultiplier = Mathf.Max(0, _heartStatuses.Length - 1);

        // Actualización de corazones en el HUD
        UpdateHeartsCurrents();
    }
    #endregion

    #region Scenes
    public void CheckScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Menu" || currentSceneName == "Winner" || currentSceneName == "GameOver")
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region Life
    public int GetMaxLife()
    {
        return _currentHeartCount * _maxLifeMultiplier;
    }

    private void UpdateHeartsCurrents()
    {
        int lifePointsRemaining = _playerLifes;

        for (int i = 0; i < _maxHeartCount; i++)
        {
            if (i < _currentHeartCount)
            {
                // Actualiza el sprite del corazón según la vida restante
                _heartImages[i].enabled = true;
                _heartImages[i].sprite = GetStatusHearts(lifePointsRemaining);
                // Reducción para el próximo corazón
                lifePointsRemaining -= _maxLifeMultiplier;
            }
            else
            {
                // Desactiva corazones adicionales
                _heartImages[i].enabled = false;
            }
        }
    }

    private Sprite GetStatusHearts(int lifePoints)
    {
        // Limita el índice para evitar desbordamientos.
        int statusIndex = Mathf.Clamp(lifePoints, 0, _heartStatuses.Length - 1);
        // Devuelve el sprite correspondiente al estado actual de los corazones.
        return _heartStatuses[statusIndex];
    }

    public void LoseLife()
    {
        _playerLifes = Mathf.Max(0, _playerLifes - 1);
        UpdateHeartsCurrents();
    }

    public void RecoverLife(int amount)
    {
        int maxLife = GetMaxLife();

        _playerLifes = Mathf.Min(_playerLifes + amount, maxLife);
        UpdateHeartsCurrents();
    }
    #endregion
}