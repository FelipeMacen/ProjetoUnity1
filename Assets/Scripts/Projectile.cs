using UnityEngine;

public class Projectile : MonoBehaviour
{
      private void OnTriggerEnter2D(Collider2D Other)
    {
        if (CompareTag("TotemFrame"))
        {
            return;
        }

        if (Other.TryGetComponent<TotemHole>(out TotemHole hole))
        {
            hole.RegisterHit();
        }

        Destroy(gameObject);
    }
}
