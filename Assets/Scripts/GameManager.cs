using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena más adelante

public class GameManager : MonoBehaviour
{
    // --- Singleton Pattern ---
    // Una instancia estática de GameManager que permite que sea accesible desde cualquier otro script.
    public static GameManager Instance;

    // --- Score Management ---
    public int score { get; private set; } // Puntuación actual, solo modificable dentro de esta clase

    // --- Game State ---
    public enum GameState
    {
        Playing,
        GameOver
    }
    public GameState currentState { get; private set; }

    // Awake se llama antes que cualquier función Start.
    // Ideal para configurar el Singleton.
    void Awake()
    {
        // Implementación del Singleton
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Descomenta si tu juego tiene múltiples escenas y quieres que el GameManager persista. Para un juego de una sola escena como Flappy Bird, no es estrictamente necesario.
        }
        else if (Instance != this) // Si ya existe una instancia y no soy yo, me destruyo.
        {
            Destroy(gameObject);
            return; // Salir para evitar ejecutar el resto del Awake/Start en la instancia duplicada.
        }

        // Inicializar estado aquí si no dependes de otras cosas en Start
        // o si necesitas que esté listo antes que otros Start()
    }

    // Start se llama una vez, justo antes de que se actualice el primer frame.
    void Start()
    {
        InitializeGame();
    }

    void InitializeGame()
    {
        score = 0;
        currentState = GameState.Playing;
        Time.timeScale = 1f; // Asegura que el juego esté corriendo a velocidad normal
        Debug.Log("Game Initialized. State: Playing, Score: 0");
    }

    // --- Métodos Públicos ---

    public void AddScore(int pointsToAdd)
    {
        if (currentState == GameState.Playing) // Solo sumar puntos si se está jugando
        {
            score += pointsToAdd;
            Debug.Log("GameManager Score: " + score);
            // Aquí, más adelante, actualizaremos la UI de la puntuación
        }
    }

    public void TriggerGameOver()
    {
        if (currentState == GameState.Playing) // Solo activar Game Over si se estaba jugando
        {
            currentState = GameState.GameOver;
            Time.timeScale = 0f; // Congelar el juego
            Debug.Log("GameManager: GAME OVER triggered!");

            // Más adelante aquí:
            // - Mostrar panel de Game Over en la UI.
            // - Detener Spawners y movimiento de otros objetos si es necesario de forma más selectiva.
        }
    }

    // Método para reiniciar el juego (lo usaremos más adelante con la UI)
    public void RestartGame()
    {
        // Time.timeScale debe ser 1 ANTES de cargar la escena,
        // o la nueva escena podría empezar pausada.
        Time.timeScale = 1f;
        // Carga la escena actual de nuevo. Asegúrate de que tu escena esté en Build Settings.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}