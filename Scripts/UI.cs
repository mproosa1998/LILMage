using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI : MonoBehaviour
{
    
    // Public 
    public TMP_Text scoreboard;
    public Image enemy;
    public static int score;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public AudioClip music;

    // Sets Cursor and Initializes score on Start
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        score = 0;
        
        MusicPlayer.Instance.PlayMusic(music);
    }

    // Update Score on UI using Score_Singleton
    void Update()
    {
        scoreboard.text = "x" + Score_Singleton.score.ToString();
    }
}