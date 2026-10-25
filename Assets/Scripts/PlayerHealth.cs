using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float damageInterval = 1f;

    [SerializeField] private TMP_Text livesText;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Color hitFlashColor = Color.white;
    [SerializeField] private float flashDuration = 0.6f;
    [SerializeField] private float flashInterval = 0.01f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Coroutine flashCoroutine;

    private int currentLives;
    private float nextDamageTime;

    private PlayerController playerController;

    private void Awake()
    {
        currentLives = maxLives;
        playerController = GetComponent<PlayerController>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        UpdateLivesText();
    }

    private void StartDamageFlash()
    {
        if (spriteRenderer == null)
        {
            return;
        }

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(flashOnDamage());
    }

    private IEnumerator flashOnDamage()
    {
        float elapsed = 0f;
        bool showHitColor = true;

        while (elapsed < flashDuration)
        {
            spriteRenderer.color = showHitColor
            ? hitFlashColor
            : originalColor;

            showHitColor = !showHitColor;
            yield return new WaitForSecondsRealtime(flashInterval);

            elapsed += flashInterval;
        }

        spriteRenderer.color = originalColor;
        flashCoroutine = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentLives <= 0)
        {
            return;
        }

        EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

        if (enemy == null)
        {
            return;
        }

        if (Time.time < nextDamageTime)
        {
            return;
        }

        nextDamageTime = Time.time + damageInterval;

        currentLives = Mathf.Max(currentLives - 1, 0);

        UpdateLivesText();

        StartDamageFlash();

        Debug.Log("Vidas Restantes: " + currentLives);

        if (currentLives <= 0)
        {
            if (playerController != null)
            {
                playerController.enabled = false;
            }

            if (gameManager != null)
            {
                gameManager.GameOver();
            }
            else
            {
                Debug.LogError("Deu ruim pae");
            }
        }
    }

    private void UpdateLivesText()
    {   if (livesText != null)
        {
            livesText.text = $"Vidas: {currentLives}/{maxLives}";
        }
    }
}
