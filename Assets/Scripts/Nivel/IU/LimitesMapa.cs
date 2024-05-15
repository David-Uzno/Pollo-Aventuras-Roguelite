using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitesMapa : MonoBehaviour

{
    private Camera mainCamera;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float halfWidth;
    private float halfHeight;

    void Start()
    {
        mainCamera = Camera.main;
        UpdateCameraBounds();
    }

    void Update()
    {
        UpdateCameraBounds();
        ConstrainPosition();
    }

    void UpdateCameraBounds()
    {
        // Calcula los límites de la cámara
        halfHeight = mainCamera.orthographicSize;
        halfWidth = mainCamera.aspect * halfHeight;

        minBounds = mainCamera.transform.position - new Vector3(halfWidth, halfHeight, 0);
        maxBounds = mainCamera.transform.position + new Vector3(halfWidth, halfHeight, 0);
    }

    void ConstrainPosition()
    {
        // Obtiene la posición actual del personaje
        Vector3 position = transform.position;

        // Restringe la posición del personaje dentro de los límites de la cámara
        position.x = Mathf.Clamp(position.x, minBounds.x, maxBounds.x);
        position.y = Mathf.Clamp(position.y, minBounds.y, maxBounds.y);

        // Actualiza la posición del personaje
        transform.position = position;
    }
}