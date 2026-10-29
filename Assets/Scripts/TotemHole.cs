using UnityEngine;
using System.Threading.Tasks;


public class TotemHole : MonoBehaviour
{
    [SerializeField] private Color hitcolor = Color.green;

    private SpriteRenderer spriteRenderer;
    private bool hasBeenHit;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public async void RegisterHit()
    {
        if (hasBeenHit)
        {
            return;
        }

        hasBeenHit = true;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = hitcolor;
            await Task.Delay(100);
            Destroy(this.transform.parent.gameObject);
            TotemManager.Instance.totensRemainig--;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RegisterTotemHit();
        }

        Debug.Log("Acertou o buraco do totem!");
    }
}
