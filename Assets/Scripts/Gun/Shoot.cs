using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform _shootingControl;
    [SerializeField] Transform _bullet;

    // Variables - Mouse
    Vector3 _objetive;
    Camera _mainCamera;

    // Variables - Tiempo de Disparo
    [SerializeField] float _shotCooldown = 0.25f;
    float _shotTime = 0;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        Fire();
        Mouse();
    }

    private void Mouse()
    {
        // Posición Actual del Mouse
        _objetive = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Grados Entre GameObject y Mouse
        float AnguleDegress = Mathf.Atan2(_objetive.y - transform.position.y, _objetive.x - transform.position.x) * Mathf.Rad2Deg - 90;
        // Rotación
        transform.rotation = Quaternion.Euler(0, 0, AnguleDegress);
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            //Condición de Cooldown
            if (Time.time > _shotTime)
            {
                // Instancia de la Bala
                Instantiate(_bullet, _shootingControl.position, _shootingControl.rotation);

                //Tiempo de Cooldown
                _shotTime = Time.time + _shotCooldown;
            }
        }
    }
}