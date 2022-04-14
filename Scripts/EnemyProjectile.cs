using System.Collections;
using UnityEngine;
public class EnemyProjectile : MonoBehaviour
{
    // Public
    public float EnemyfireRate = 2f;
    public GameObject Player;
    public EnemyController controller;
    public Transform firepoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public AudioSource audioSource;
    public RaycastHit2D hit;
    public Vector3 startPos;
    public Vector3 endPos;

    // Private
    private GameObject bullet;
    private float nextFire = 2.0f;
    private bool canShoot = false;

    void Update()
    {
        // While Player tag is not Null
        Player = GameObject.FindWithTag("Player");
        if(Player != null)
        {

            // Draws a Raycast between Self and Player
            startPos = transform.position;
            endPos = transform.up * 10;
            Debug.DrawRay(startPos, endPos, Color.red);

            // If the first Raycast Collision is Player And Time until nextFire is surpassed
            hit = Physics2D.Raycast(startPos, endPos, 100);
            if (hit.collider.tag == Player.tag)
            {
                if (Time.time > nextFire)
                {
                    // Play Shoot animation and Shoot
                    StartCoroutine(waitToShoot());
                    if (canShoot)
                    {
                        nextFire = Time.time + EnemyfireRate;
                        Shoot();
                        canShoot = false;
                    }
                }
            }
        }
    }

    // Play Sound and Create Bullet object
    void Shoot()
    {
        audioSource.time = .3f;
        audioSource.Play();

        bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }

    // Play Shooting Animation before allowing Enemy to Shoot
    IEnumerator waitToShoot()
    {
        controller.animator.SetBool("isShooting", true);
        yield return new WaitForSeconds(controller.animator.GetCurrentAnimatorStateInfo(0).length + 
            controller.animator.GetCurrentAnimatorStateInfo(0).normalizedTime - .5f);
        canShoot = true;
        controller.animator.SetBool("isShooting", false);
    }
}
