using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    // Public
    public Animator transition;
    public float transitionTime = 1;
    public int EnemyCount;
    public int BossCount;
    public int PlayerCount;
    public static bool gameIsPaused;

    // Updates Every Frame
    public void Update()
    {
        // Puse game on Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }

        // Updates how many characters are currently active in the scene
        EnemyCount = GameObject.FindGameObjectsWithTag("MR_Enemy").Length;
        PlayerCount = GameObject.FindGameObjectsWithTag("Player").Length;
        BossCount = GameObject.FindGameObjectsWithTag("MR_Boss").Length;

        // If the Current Scene is not a Menu Page
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lil\'MageMenu")
            && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("GameOver")
            && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Victory"))
        {
            // If Enemy and Boss count reach Zero call NextLevel
            if (EnemyCount == 0 && BossCount == 0)
            {
                StartCoroutine(SceneTransition());
                NextLevel();
            }

            // If Player count reaches Zero call GameOver
            else if (PlayerCount == 0)
            {
                StartCoroutine(SceneTransition());
                GameOver();
            }
        }
    }

    // Begins game at Level1 scene and resets Score to 0
    public void StartGame()
    {
        StartCoroutine(SceneTransition());
        SceneManager.LoadScene("Level1");
        Score_Singleton.score = 0;
    }

    // Calls Main Menu Scene
    public void MainMenu()
    {
        StartCoroutine(SceneTransition());
        SceneManager.LoadScene("Lil\'MageMenu");
        Score_Singleton.score = 0;
    }

    // Calls GameOver Scene
    public void GameOver()
    {
        StartCoroutine(SceneTransition());
        SceneManager.LoadScene("GameOver");
    }

    // Calls next Scene in Build Index
    public void NextLevel()
    {
        StartCoroutine(SceneTransition());
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene+1);
    }

    // Quits the Game Window and returns to the Classic Arcade Main Menu
    public void Quit()
    {
        Debug.Log("Exit Game");
        StartCoroutine(SceneTransition());
        // Resets Cursor to defult on exit
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        MusicPlayer.Instance.StopMusic();
        Application.Quit();
    }

    // Plays Transition Display between Scenes
    IEnumerator SceneTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }

    // Handles Pause Game
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
