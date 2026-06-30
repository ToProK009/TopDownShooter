using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public static float globalSpeedBonus = 0f;
    public static AudioClip killSound;

    void Start()
    {
        speed += globalSpeedBonus;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x < -0.1f || viewPos.x > 1.1f || viewPos.y < -0.1f || viewPos.y > 1.1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            Meteorite m = other.GetComponent<Meteorite>();
            if (m != null)
            {
                GameManager.Instance.AddCoins(m.coinValue);
                GameManager.Instance.AddScore(m.scoreValue);
            }
            if (killSound != null)
                AudioSource.PlayClipAtPoint(killSound, other.transform.position);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
