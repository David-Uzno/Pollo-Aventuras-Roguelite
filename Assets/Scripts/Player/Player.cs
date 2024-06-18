using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Life")]
    [SerializeField] private int _life = 3;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _damageFlashColor = Color.red;
    [SerializeField] private int _damageFlashCount = 3;
    [SerializeField] private float _damageFlashDuration = 0.75f;
    private Coroutine _flashCoroutine;
    private Color _originalColor;

    [Header("Movement")]
    [SerializeField] private Rigidbody2D _RB;
    [SerializeField] private float _speed = 5f;

    [Header("Other Components")]
    [SerializeField] private Animator _animator;
    #endregion

    #region Unity Methods
    private void Start()
    {
        if (_spriteRenderer == null)
        {
            Debug.LogError("¡SpriteRenderer no está asignado!");
        }
        else
        {
            _originalColor = _spriteRenderer.color;
        }

        if (_animator != null)
        {
            _animator.SetBool("Walk", false);
        }
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player") && collision.CompareTag("Enemy"))
        {
            LoseLife();
        }
    }
    #endregion

    #region Movement
    private void HandleMovement()
    {
        float movementHorizontal = Input.GetAxis("Horizontal");
        float movementVertical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(movementHorizontal, movementVertical);

        // Move the Player
        _RB.velocity = movement * _speed;
        HandleRotation(movementHorizontal);

        // Handle Animations
        if (_animator != null)
        {
            UpdateAnimations(movementHorizontal, movementVertical);
        }
    }

    private void HandleRotation(float movementHorizontal)
    {
        if (movementHorizontal < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (movementHorizontal > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    #endregion

    #region Animation
    private void UpdateAnimations(float movementHorizontal, float movementVertical)
    {
        bool isWalking = movementHorizontal != 0 || movementVertical != 0;
        _animator.SetBool("Walk", isWalking);
    }
    #endregion

    #region Life

    public void RecoverLife(int amount)
    {
        int maxLife = GameManager.Instance.GetMaxLife();

        if (_life < maxLife)
        {
            _life = Mathf.Min(_life + amount, maxLife);
            GameManager.Instance.RecoverLife(amount);
        }
    }

    public void LoseLife()
    {
        _life--;
        if (_flashCoroutine != null)
        {
            StopCoroutine(_flashCoroutine);
        }
        GameManager.Instance.LoseLife();

        _flashCoroutine = StartCoroutine(FlashSprite());

        if (_life <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private IEnumerator FlashSprite()
    {
        for (int i = 0; i < _damageFlashCount; i++)
        {
            _spriteRenderer.color = _damageFlashColor;
            yield return new WaitForSeconds(_damageFlashDuration / 2);

            _spriteRenderer.color = _originalColor;
            yield return new WaitForSeconds(_damageFlashDuration / 2);
        }

        _spriteRenderer.color = _originalColor;
    }
    #endregion
}