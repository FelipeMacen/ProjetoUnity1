using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private GameObject hitEffectPrefab;

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
            SpawnHitEffect(transform.position);

            enemy.TakeHit();

            Destroy(gameObject);
            return;
        }

        if (Other.TryGetComponent<TotemHole>(out TotemHole hole))
        {
            hole.RegisterHit();

            SpawnHitEffect(transform.position);
        }

        Destroy(gameObject);
    }

    private void SpawnHitEffect(Vector3 position)
    {
        Instantiate(hitEffectPrefab, position, Quaternion.identity);
    }
}
