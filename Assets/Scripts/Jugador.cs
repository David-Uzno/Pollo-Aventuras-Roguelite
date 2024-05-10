using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del jugador
    public int vidas = 3; // Cantidad inicial de vidas

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private TrailRenderer tr;

    void Update()
    {
        // Obtener las entradas de teclado para el movimiento
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcular el vector de movimiento basado en las entradas
        Vector3 movimiento = new Vector3(movimientoHorizontal, movimientoVertical, 0f) * velocidad * Time.deltaTime;

        // Aplicar el movimiento al jugador
        transform.Translate(movimiento);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            PerderVida(); // Llama al método PerderVida
            Debug.Log("¡Te han destruido!");
            // Aquí puedes agregar más acciones como reiniciar el nivel o mostrar un mensaje de Game Over
        }
    }

    // Método para perder una vida
    void PerderVida()
    {
        vidas--; // Disminuye la cantidad de vidas
        Debug.Log("Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            // Si las vidas son igual o menor a cero, destruye el jugador
            Destroy(gameObject);
            Debug.Log("¡Has perdido todas tus vidas! Game Over.");
            // Aquí puedes agregar más acciones como reiniciar el nivel o mostrar un mensaje de Game Over
        }
    }
    
}









