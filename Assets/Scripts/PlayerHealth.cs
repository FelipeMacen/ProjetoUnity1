using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxLives = 3;
    [SerializeField] private float damageInterval = 1f;

    [SerializeField] private TMP_Text livesText;
    [SerializeField] private GameManager gameManager;

    private int currentLives;
    private float nextDamageTime;

    private PlayerController playerController;

    private void Awake()
    {
        currentLives = maxLives;
        playerController = GetComponent<PlayerController>();

        UpdateLivesText();
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
