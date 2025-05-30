using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int score { get; private set; }

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalScoreText;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public GameObject creditsPanel;

    [Header("Audio")]
    public AudioClip pointSoundClip;
    public AudioClip collisionSoundClip;
    public AudioSource audioSource; // Asigna el AudioSource para SFX

    [Header("Game Elements to Control")]
    public GameObject playerPlane;
    public GameObject towerSpawner;
    public GameObject cloudsContainer;

    public enum GameState
    {
        MainMenu,
        Playing,
        GameOver
        // Paused (lo implementaremos después si quieres)
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

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("GameManager: AudioSource no asignado/encontrado. SFX no sonarán.");
            }
        }
    }

    void Start()
    {
        // El juego siempre inicia en el estado MainMenu.
        // Llamamos a GoToMainMenu para configurar todo correctamente desde el inicio.
        GoToMainMenu(true); // true indica que es el arranque inicial del juego.
    }

    // Inicia una nueva sesión de juego (llamado por Play desde MainMenu o por RestartGame)
    public void StartNewGameSession()
    {
        currentState = GameState.Playing;
        Time.timeScale = 1f; // Asegura que el juego corra
        score = 0;

        SetupForCurrentState(); // Activa/desactiva GameObjects y UI para el estado Playing

        if (scoreText != null) scoreText.text = "Score: " + score;

        ResetPlayerState();
        ResetAndStartTowerSpawner();

        Debug.Log("New Game Session Started. State: Playing");
    }

    public void AddScore(int pointsToAdd)
    {
        if (currentState == GameState.Playing)
        {
            score += pointsToAdd;
            if (scoreText != null) scoreText.text = "Score: " + score;
            PlaySound(pointSoundClip);
        }
    }

    public void TriggerGameOver()
    {
        if (currentState == GameState.Playing)
        {
            currentState = GameState.GameOver;
            Time.timeScale = 0f; // Congela el juego
            Debug.Log("GameManager: GAME OVER triggered!");

            PlaySound(collisionSoundClip);
            
            // Iniciar corrutina para mostrar el GameOver panel con delay
            StartCoroutine(ShowGameOverWithDelay());
        }
    }

    private IEnumerator ShowGameOverWithDelay()
    {
        // Esperar 2 segundos usando tiempo real (no afectado por timeScale)
        yield return new WaitForSecondsRealtime(2.0f);
        
        SetupForCurrentState(); // Muestra GameOverPanel, oculta elementos de juego

        if (finalScoreText != null)
        {
            finalScoreText.text = "Puntuación Final: " + score;
        }
    }

    // Llamado por el botón "Reiniciar" del panel de Game Over
    public void RestartGame()
    {
        Debug.Log("RestartGame Button Clicked");
        Time.timeScale = 1f; // Restaurar la escala de tiempo ANTES de hacer cualquier otra cosa
        CleanUpOldGameElements(); // Limpiar elementos de la partida anterior
        StartNewGameSession();    // Iniciar una nueva sesión de juego
    }

    // Llamado por "Menú Principal" en GameOver o "Volver" en Credits
    public void GoToMainMenu(bool isInitialStart = false)
    {
        Debug.Log("GoToMainMenu Called. isInitialStart: " + isInitialStart);
        Time.timeScale = 1f;
        currentState = GameState.MainMenu;
        score = 0; // Reiniciar la puntuación al volver al menú

        if (!isInitialStart) // No limpiar si es el primer arranque del juego desde Start()
        {
            CleanUpOldGameElements();
        }

        SetupForCurrentState(); // Configura para MainMenu (activa panel menú, desactiva otros)

        if (scoreText != null) scoreText.text = "Score: " + score; // Resetear visualmente el score
        Debug.Log("Switched to Main Menu. State: MainMenu");
    }

    public void ShowCredits()
    {
        if (currentState == GameState.MainMenu)
        {
            if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
            if (creditsPanel != null) creditsPanel.SetActive(true);
            Debug.Log("Mostrando Panel de Créditos.");
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void CleanUpOldGameElements()
    {
        Debug.Log("Cleaning up old game elements...");
        // Limpiar obstáculos existentes, pero excluyendo elementos permanentes como FloorBoundary
        // **ASEGÚRATE de que tus prefabs TowerPair tengan el Tag "Obstacle"**
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        int towersDestroyed = 0;
        foreach (GameObject obstacle in obstacles)
        {
            // Solo destruir objetos que NO sean elementos permanentes del nivel
            // FloorBoundary y otros elementos fijos deben permanecer
            if (obstacle.name != "FloorBoundary" &&
                !obstacle.name.Contains("Boundary") &&
                !obstacle.name.Contains("Floor") &&
                !obstacle.name.Contains("Ceiling") &&
                !obstacle.name.Contains("Wall"))
            {
                Destroy(obstacle);
                towersDestroyed++;
            }
        }
        Debug.Log(towersDestroyed + " dynamic towers destroyed. Permanent boundaries preserved.");

        // Detener el TowerSpawner
        if (towerSpawner != null)
        {
            TowerSpawnerController spawnerController = towerSpawner.GetComponent<TowerSpawnerController>();
            if (spawnerController != null)
            {
                spawnerController.StopSpawning(); // Usar el nuevo método
                Debug.Log("Tower Spawner stopped via StopSpawning method.");
            }
        }

        GameObject[] explosions = GameObject.FindGameObjectsWithTag("ExplosionEffectTag"); // Usa el Tag que le diste al prefab
        foreach (GameObject explosion in explosions)
        {
            Destroy(explosion);
        }
        if (explosions.Length > 0) Debug.Log(explosions.Length + " explosions destroyed.");
    }

    private void ResetPlayerState()
    {
        if (playerPlane != null)
        {
            Debug.Log("Resetting Player State...");
            if (!playerPlane.activeSelf) playerPlane.SetActive(true);

            playerPlane.transform.position = new Vector3(-2f, 0f, 0f); // Ajusta esta posición
            playerPlane.transform.rotation = Quaternion.identity;

            Rigidbody2D rbPlayer = playerPlane.GetComponent<Rigidbody2D>();
            if (rbPlayer != null)
            {
                rbPlayer.velocity = Vector2.zero;
                rbPlayer.angularVelocity = 0f;
            }

            PlaneController pc = playerPlane.GetComponent<PlaneController>();
            if (pc != null)
            {
                pc.enabled = true;
                // Si PlaneController tiene un método Reset interno, llámalo. Ej: pc.Initialize();
            }
        }
        else
        {
            Debug.LogError("PlayerPlane reference is null in GameManager. Cannot reset player state.");
        }
    }

    private void ResetAndStartTowerSpawner()
    {
        if (towerSpawner != null)
        {
            Debug.Log("Resetting and Starting Tower Spawner...");
            if (!towerSpawner.activeSelf) towerSpawner.SetActive(true);

            TowerSpawnerController spawnerController = towerSpawner.GetComponent<TowerSpawnerController>();
            if (spawnerController != null)
            {
                spawnerController.StartSpawning(); // Usar el nuevo método
                Debug.Log("Tower Spawner started via StartSpawning method.");
            }
            else
            {
                Debug.LogError("TowerSpawnerController component not found on towerSpawner GameObject.");
            }
        }
        else
        {
            Debug.LogError("TowerSpawner reference is null in GameManager. Cannot reset spawner.");
        }
    }

    void SetupForCurrentState()
    {
        Debug.Log("SetupForCurrentState called. Current State: " + currentState);
        // Paneles de UI
        if (mainMenuPanel != null) mainMenuPanel.SetActive(currentState == GameState.MainMenu);
        if (gameOverPanel != null) gameOverPanel.SetActive(currentState == GameState.GameOver);
        if (creditsPanel != null) creditsPanel.SetActive(false); // Créditos se maneja explícitamente por ShowCredits/GoToMainMenu

        // Visibilidad de la UI del Score durante el juego
        if (scoreText != null) scoreText.gameObject.SetActive(currentState == GameState.Playing);

        // Elementos del Juego
        bool isPlaying = (currentState == GameState.Playing);
        if (playerPlane != null) playerPlane.SetActive(isPlaying);
        if (towerSpawner != null) towerSpawner.SetActive(isPlaying);

        // Las nubes permanecen siempre visibles ya que son parte del escenario
        if (cloudsContainer != null) cloudsContainer.SetActive(true);

        // Lógica específica adicional por estado
        if (currentState == GameState.GameOver)
        {
            if (towerSpawner != null) towerSpawner.SetActive(false); // Redundante si isPlaying ya lo controla, pero seguro.
        }
        else if (currentState == GameState.MainMenu)
        {
            if (towerSpawner != null)
            { // Asegurar que spawner esté detenido en menú
                TowerSpawnerController spawnerController = towerSpawner.GetComponent<TowerSpawnerController>();
                if (spawnerController != null)
                {
                    spawnerController.StopSpawning(); // Usar el nuevo método
                    Debug.Log("Tower Spawner explicitly stopped for MainMenu in SetupForCurrentState.");
                }
            }
        }
    }
}