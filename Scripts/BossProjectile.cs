using UnityEngine;
public class BossProjectile : MonoBehaviour
{
    // Public
    public float BossfireRate = .25f;
    public GameObject Player;
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public AudioSource audioSource;
    public Animator animator;

    // Private
    private float FirstFireDelay = 1.0f;
    private GameObject bullet;

    void FixedUpdate()
    {
        // Check to see if Player exists
        Player = GameObject.FindWithTag("Player");
        if (Player != null)
        {
            // If time > FirstFireDelay play AttackAnim and Shoot
            if (Time.time > FirstFireDelay)
            {
                animator.SetBool("isShooting", true);
                FirstFireDelay = Time.time + BossfireRate;
                Shoot();
            }
        }
    }

    // Play Sound and Create Bullet object
    void Shoot()
    {
        // Call Firing Sound
        FireSound();

        // Instantiate Bullet object with Rigidbody2D, Force, and Direction
        bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }

    // Plays Fire Sound at reduced volume
    void FireSound()
    {
        audioSource.time = .3f;
        audioSource.volume = .25f;
        audioSource.Play();
    }
}