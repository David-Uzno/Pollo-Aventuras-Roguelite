using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    #region Variables
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] Transform _shootingControl;
    [SerializeField] Transform _bullet;
    [SerializeField] RectTransform _virtualCursor;

    [Header("Mouse")]
    Vector3 _objetive;
    Camera _mainCamera;

    [Header("Time of Shoot")]
    [SerializeField] float _shotCooldown = 0.25f;
    float _shotTime = 0;
    #endregion

    #region Unity Methods
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        UpdateAim();
        Fire();
    }
    #endregion

    #region Mouse
    private void UpdateAim()
    {
        if (_virtualCursor != null)
        {
            // Usar la posición del Virtual Cursor
            Vector2 cursorScreenPosition = _virtualCursor.position;
            _objetive = _mainCamera.ScreenToWorldPoint(new Vector3(cursorScreenPosition.x, cursorScreenPosition.y, _mainCamera.nearClipPlane));
        }
        else
        {
            // Posición del Mouse
            Vector2 mousePosition = _playerInput.actions["Aim"].ReadValue<Vector2>();

            // Convertir la Posición del Mouse a Coordenadas del Mundo
            _objetive = _mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane));

            // Posición Actual del Mouse
            _objetive = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        // Grados Entre GameObject y Objetivo
        float angleDegrees = Mathf.Atan2(_objetive.y - transform.position.y, _objetive.x - transform.position.x) * Mathf.Rad2Deg - 90;
        // Rotación
        transform.rotation = Quaternion.Euler(0, 0, angleDegrees);
    }
    #endregion

    #region Shoot
    private void Fire()
    {
        if (_playerInput.actions["Shoot"].ReadValue<float>() > 0)
        {
            // Condición de Cooldown
            if (Time.time > _shotTime)
            {
                // Instancia de la Bala
                Instantiate(_bullet, _shootingControl.position, _shootingControl.rotation);

                // Tiempo de Cooldown
                _shotTime = Time.time + _shotCooldown;
            }
        }
    }
    #endregion
}