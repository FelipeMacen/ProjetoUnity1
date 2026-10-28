using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.2f);
    }
    private void OnTriggerEnter2D(Collider2D Other)
    {
        if (CompareTag("TotemFrame"))
        {
            return;
        }

        if (Other.TryGetComponent<EnemyController>(out EnemyController enemy))
        {
            enemy.TakeHit();

            Destroy(gameObject);
            return;
        }

        if (Other.TryGetComponent<TotemHole>(out TotemHole hole))
        {
            hole.RegisterHit();
        }

        Destroy(gameObject);
    }
}
