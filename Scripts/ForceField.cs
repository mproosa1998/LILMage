using UnityEngine;
public class ForceField : MonoBehaviour
{
    // Ignore Enemy Projectile Collisions with ForceField
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("MR_EnemyProjectile"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }
    }
}
