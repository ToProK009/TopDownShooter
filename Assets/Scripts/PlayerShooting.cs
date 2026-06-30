using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float fireRate = 0.15f;

    private float nextFireTime;

    void Start()
    {
        if (shootingPoint == null)
            shootingPoint = transform.Find("ShootingPoint");
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        }
    }
}
