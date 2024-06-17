using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    [SerializeField] private Color _invincibleColor = Color.cyan;
    [SerializeField] private float _invincibilityDuration = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.tag = "Untagged";

            SpriteRenderer playerSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer != null)
            {
                // Cambiar el Color del Jugador
                playerSpriteRenderer.color = _invincibleColor;
            }


            // Crear GameObject Temporal
            GameObject tempObject = new GameObject("TempObject");
            TempObjectScript tempScript = tempObject.AddComponent<TempObjectScript>();
            tempScript.Initialize(collision.gameObject, _invincibilityDuration, _invincibleColor);

            // Destruir el GameObject Invincibility
            Destroy(gameObject);
        }
    }
}

public class TempObjectScript : MonoBehaviour
{
    private GameObject _player;
    private float _duration;
    private Color _originalColor;

    public void Initialize(GameObject player, float duration, Color invincibleColor)
    {
        _player = player;
        _duration = duration;
        _originalColor = player.GetComponent<SpriteRenderer>().color;

        // Cambiar el Color del Jugador
        player.GetComponent<SpriteRenderer>().color = invincibleColor;

        StartCoroutine(RestorePlayerColorAfterDelay());
    }

    private IEnumerator RestorePlayerColorAfterDelay()
    {
        yield return new WaitForSeconds(_duration);

        if (_player != null)
        {
            // Restaurar el Color Original del Jugador
            _player.GetComponent<SpriteRenderer>().color = _originalColor;
            // Restaurar el Tag Original del Jugador
            _player.tag = "Player";
        }

        // Destruir el GameObject Temporal
        Destroy(gameObject);
    }
}