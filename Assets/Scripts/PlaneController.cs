using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneController : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource playerAudioSource;
    public AudioClip flapSoundClip;
    public GameObject explosionPrefab;
    public float flapForce = 5f;
    private Rigidbody2D rb;
    public float rotationSpeedFactor = 5f;
    public float maxUpAngle = 45f;
    public float maxDownAngle = -90f;
    public float topBoundary = 5f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (playerAudioSource == null)
        {
            Debug.LogError("PlayerPlane no tiene un AudioSource asignado en el inspector.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.up * flapForce;
            if (playerAudioSource != null && flapSoundClip != null)
            {
                playerAudioSource.PlayOneShot(flapSoundClip);
            }
            else // Bloque else para debug
            {
                if (playerAudioSource == null) Debug.LogError("FLAP ERROR: playerAudioSource es null!");
                if (flapSoundClip == null) Debug.LogError("FLAP ERROR: flapSoundClip no está asignado en el Inspector!");
            }
        }

        float targetAngle = Mathf.Clamp(rb.velocity.y * rotationSpeedFactor, maxDownAngle, maxUpAngle);

        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }
    void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        if (currentPosition.y > topBoundary)
        {
            transform.position = new Vector3(currentPosition.x, topBoundary, currentPosition.z);
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScorePoint"))
        {
            GameManager.Instance.AddScore(1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // 1. Instanciar la explosión en la posición del avión
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            // 2. Desactivar el GameObject del avión para que desaparezca
            gameObject.SetActive(false);
            GameManager.Instance.TriggerGameOver();
            // enabled = false;
        }
    }
}