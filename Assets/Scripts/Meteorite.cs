using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public float minSpeed = 1f;
    public float maxSpeed = 3f;
    [HideInInspector] public int coinValue;
    [HideInInspector] public int scoreValue;
    [HideInInspector] public float speedMultiplier = 1f;
    private float speed;

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed) * speedMultiplier;
        coinValue = Mathf.Clamp(6 - Mathf.RoundToInt(transform.localScale.x * 2f), 1, 5);
        scoreValue = Mathf.RoundToInt(Mathf.Lerp(50f, 10f, (transform.localScale.x - 0.5f) / 2f));

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.isTrigger = true;

        Vector3 targetPos = Vector3.zero;
        if (Camera.main != null)
            targetPos = Camera.main.transform.position;

        Vector2 moveDir = (targetPos - transform.position).normalized;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x < -0.5f || viewPos.x > 1.5f || viewPos.y < -0.5f || viewPos.y > 1.5f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameManager.Instance.GameOver();
        }
    }
}
