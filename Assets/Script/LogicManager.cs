using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{

    public int playerHealth;
    public Text healthText;
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    private SoundFXManager soundFXManager;
    public Text finalScore;

    void Start()
    {
        soundFXManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<SoundFXManager>();
    }

    [ContextMenu("Increase Player Score")]
    public void scoreCount()
    {
        playerScore++;
        scoreText.text = "Score: " + playerScore.ToString();
        if (playerScore % 10 == 0)
        {
            soundFXManager.playSFX(soundFXManager.getPoints);
        }
    }

    public void displayScore()
    {
        finalScore.text = "Your Final Score Is: " + playerScore + "\n";
        if (playerScore >= 50)
        {
            finalScore.text += "Wow, that's amazing!";
        }
        else if (playerScore < 10)
        {
            finalScore.text += "Maybe next time.";
        }
        else
        {
            finalScore.text += "Good work!";
        }

    }

    public void healthCount()
    {
        playerHealth--;
        healthText.text = "Health: " + playerHealth;
        soundFXManager.playSFX(soundFXManager.looseHealth);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        Cursor.visible = true;
        soundFXManager.playSFX(soundFXManager.gameOver);
        int milliseconds = 500;
        Thread.Sleep(milliseconds);
        soundFXManager.stopSFX();
        displayScore();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("GameStart");
        Cursor.visible = true;
    }
}
