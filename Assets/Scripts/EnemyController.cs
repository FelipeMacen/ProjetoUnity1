using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 3f;

    private Rigidbody2D rb;

    private bool isDefeated;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    } 

    private void FixedUpdate()
    {
        if (player == null)
        {
            return;
        }

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;

        rb.MovePosition(
            rb.position + direction * speed * Time.fixedDeltaTime
        );
    }

    public void TakeHit()
    {
        if (isDefeated)
        {
            return;
        }

        isDefeated = true;

        Debug.Log("Inimigo morto");

        Destroy(gameObject);
    }
}
