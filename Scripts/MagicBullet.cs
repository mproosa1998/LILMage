using System.Collections;
using UnityEngine;
public class MagicBullet : MonoBehaviour
{
    // Public 
    public int bounceCount = 0;
    public int speed = 1;
    public AudioClip bounceClip;
    public AudioClip hitClip;
    public AudioSource bounceSource;
    public AudioSource hitSource;

    // Private
    private Rigidbody2D rb;

    // On Start assign RigidBody to Bullet instance
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // On Collision with object
    void OnCollisionEnter2D(Collision2D collision)
    {
        // If Collision was with Wall and times bounced is <2
        if (collision.gameObject.tag.Equals("MR_Wall") == true)
        {
            if (bounceCount <2)
            {
                // Play Bounce sound effect and increase Bounce Count
                bounceSource.time = .5f;
                bounceSource.PlayOneShot(bounceClip);
                bounceCount++;
            }

            // Else Play Hit sound effect and destroy Self
            else
            {
                StartCoroutine(hitSound());
            }
        }
        else
        {
            // Plays Hit sound effect
            StartCoroutine(hitSound());

            // If collided with Enemy call EnemyController.Dead() and add point to score
            if (collision.gameObject.tag.Equals("MR_Enemy"))
            {
                collision.gameObject.GetComponent<EnemyController>().Dead();
                Score_Singleton.ScoreIncrease();
            }

            // If collided with Enemy call BossController.Dead()
            else if (collision.gameObject.tag.Equals("MR_Boss"))
            {
                collision.gameObject.GetComponent<BossController>().Dead();
            }

            // If Collided with Player destroy Player
            else if (collision.gameObject.tag.Equals("Player"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    // On Collision with ForceField trigger destroy self and ForceField
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("MR_BossField"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(hitSound());
        }        
    }

    // On hit move off screen and play Sound before destroying Self
    IEnumerator hitSound()
    {
        transform.position = new Vector2(1000, 1000);
        hitSource.PlayOneShot(hitClip);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
