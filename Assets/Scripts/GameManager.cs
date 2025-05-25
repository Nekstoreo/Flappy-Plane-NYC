using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score { get; private set; }
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    [Header("Audio Clips")]
    public AudioClip pointSoundClip;
    public AudioClip collisionSoundClip;
    public AudioSource audioSource;
    public enum GameState
    {
        Playing,
        GameOver
    }
    public GameState currentState { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Imprimir todos los componentes adjuntos a este GameObject
        Component[] allComponents = GetComponents<Component>();
        Debug.Log($"Componentes en GameManager:");
        foreach (Component comp in allComponents)
        {
            Debug.Log($"- {comp.GetType().Name}");
        }

        if (audioSource == null)
        {
            Debug.LogWarning("GameManager no tiene un AudioSource asignado en el inspector, los SFX no sonarán.");
        }
    }

    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        score = 0;
        currentState = GameState.Playing;
        Time.timeScale = 1f; Debug.Log("Game Initialized. State: Playing, Score: 0");
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }


    public void AddScore(int pointsToAdd)
    {
        if (currentState == GameState.Playing)
        {
            score += pointsToAdd;
            if (scoreText != null)
            {
                scoreText.text = "Score: " + score;
            }
            if (audioSource != null && pointSoundClip != null)
            {
                audioSource.PlayOneShot(pointSoundClip);
            }
        }
    }

    public void TriggerGameOver()
    {
        if (currentState == GameState.Playing)
        {
            currentState = GameState.GameOver;
            Time.timeScale = 0f; Debug.Log("GameManager: GAME OVER triggered!");

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
            }
            if (finalScoreText != null)
            {
                finalScoreText.text = "Puntuación Final: " + score;
            }
            // Reproducir sonido de colisión si está asignado
            if (audioSource != null && collisionSoundClip != null)
            {
                audioSource.PlayOneShot(collisionSoundClip);
            }

        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}