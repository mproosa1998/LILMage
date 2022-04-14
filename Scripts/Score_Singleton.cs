using UnityEngine;
public class Score_Singleton : MonoBehaviour
{
    public static Score_Singleton Instance;
    public UI ui;
    public static int score;

    // Keep track of Score between Scenes
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
            Destroy(this);
        
        else
            Instance = this;
    }
    // Called by PlayerBullet upon colliding with MR_Enemy
    public static void ScoreIncrease()
    {
        ++score;
    }
}
