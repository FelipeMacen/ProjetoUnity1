using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
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

        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.EnemyDefeated();
        };
    }

    public void Initialize(Transform target, float movementSpeed)
    {
       player = target;
       speed =  movementSpeed;
    }
}
