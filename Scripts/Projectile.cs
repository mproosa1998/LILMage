using UnityEngine;
public class Projectile : MonoBehaviour
{
    // Public
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public int maxBullet = 3;
    public AudioSource audioSource;
    public PlayerController controller;

    // Private
    private GameObject bullet;
    private int bulletCount = 0;

    // Updates every frame
    public void Update()
    {
        // if Current Number of Bullets is less than 3 and Fire1 pressed Shoot
        bulletCount = GameObject.FindGameObjectsWithTag("MR_PlayerProjectile").Length;
        if (Input.GetButtonDown("Fire1") && bulletCount < 3 && !SceneChanger.gameIsPaused)
        {
            Shoot();
        }
    }

    // Call ShootSound and create Bullet Instance
    public void Shoot()
    {
        ShootSound();
        bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }

    // Play Shoot sound effect
    public void ShootSound()
    {
        audioSource.time = .3f;
        audioSource.Play();
    }
}
