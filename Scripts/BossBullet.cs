using System.Collections;
using UnityEngine;
public class BossBullet : MonoBehaviour
{
    // Set up Audio effects for Collision
    public AudioClip hitClip;
    public AudioSource hitSource;

    // Handles Collision Event
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check to see if Collision with Player Projectile to prevent Audio Overlap
        if (!collision.gameObject.tag.Equals("MR_PlayerProjectile"))
        {
            // Call hitSound IEnumerator
            StartCoroutine(hitSound());

            // Upon colliding with Player -> Destroy Player
            if (collision.gameObject.tag.Equals("Player"))
                Destroy(collision.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Plays a sound on collision
    IEnumerator hitSound()
    {
        // Change position of object offscreen while it plays hit sound
        transform.position = new Vector2(1000, 1000);

        // Lower default volume of on hit sound
        hitSource.volume = .25f;
        // Call to play sound
        hitSource.PlayOneShot(hitClip);

        // Wait for sound to finish playing before destroying self
        yield return new WaitForSeconds(hitClip.length);
        Destroy(gameObject);
    }
}
