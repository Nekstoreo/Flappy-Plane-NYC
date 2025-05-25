using UnityEngine; // Necesario para casi todo en Unity


public class PlaneController : MonoBehaviour
{
    // Variable pública para la fuerza del "flap" o impulso hacia arriba.
    // Al ser pública, podemos ajustarla desde el Inspector de Unity sin cambiar el código.
    public float flapForce = 5f;

    // Variable privada para guardar la referencia al componente Rigidbody2D de nuestro avión.
    private Rigidbody2D rb;

    // Variable para controlar la rotación del avión (opcional, para más estilo)
    public float rotationSpeedFactor = 5f; // Multiplicador para la velocidad de rotación
    public float maxUpAngle = 45f;       // Ángulo máximo hacia arriba
    public float maxDownAngle = -90f;    // Ángulo máximo hacia abajo
    public float topBoundary = 5f; // Límite superior para la posición Y del avión

    // --- NUEVA VARIABLE ---
    private int score = 0; // <--- NUEVA VARIABLE

    // Start se llama una vez, justo antes de que se actualice el primer frame.
    // Es ideal para inicializar cosas.
    void Start()
    {
        // Obtenemos el componente Rigidbody2D que está en el mismo GameObject que este script
        // y lo guardamos en nuestra variable 'rb' para poder usarlo después.
        rb = GetComponent<Rigidbody2D>();
    }

    // Update se llama una vez por cada frame.
    // Aquí pondremos la lógica que necesita verificarse constantemente, como la entrada del jugador.
    void Update()
    {
        // Verificamos si el jugador ha presionado el botón izquierdo del ratón (o ha tocado la pantalla en móvil).
        if (Input.GetMouseButtonDown(0)) // El '0' se refiere al botón izquierdo del ratón o al primer toque.
        {
            // Si se detecta el input, aplicamos el "flap":
            // 1. Reiniciamos la velocidad vertical actual a cero (opcional, pero ayuda a que cada flap se sienta igual).
            // rb.velocity = new Vector2(rb.velocity.x, 0); // Descomenta esta línea si quieres este efecto.

            // 2. Aplicamos una velocidad vertical instantánea hacia arriba.
            rb.velocity = Vector2.up * flapForce;
            // Alternativamente, podrías usar AddForce:
            // rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
            // Ambas funcionan, rb.velocity da un control más directo e inmediato para este tipo de juego.
        }

        // Rotación del avión basada en su velocidad vertical (opcional)
        // Calcula el ángulo deseado. Si la velocidad Y es positiva (subiendo), inclina hacia arriba.
        // Si es negativa (cayendo), inclina hacia abajo.
        float targetAngle = Mathf.Clamp(rb.velocity.y * rotationSpeedFactor, maxDownAngle, maxUpAngle);

        // Aplicamos la rotación de forma suave (opcional, podrías usar asignación directa si prefieres)
        // Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        // O de forma directa:
        transform.rotation = Quaternion.Euler(0, 0, targetAngle);
    }
    void LateUpdate()
    {
        // Obtenemos la posición actual del avión
        Vector3 currentPosition = transform.position;

        // Verificamos si la posición Y ha superado el límite superior
        if (currentPosition.y > topBoundary)
        {
            // Si es así, la ajustamos para que sea exactamente el límite superior
            transform.position = new Vector3(currentPosition.x, topBoundary, currentPosition.z);

            // Opcional: Si queremos que el avión pierda su velocidad ascendente al tocar el techo
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }

        // Podríamos añadir una lógica similar para un bottomBoundary (límite inferior) aquí
        // si quisiéramos que el juego termine si cae por debajo de cierto punto,
        // pero por ahora, la gravedad y la ausencia de suelo harán que siga cayendo.
        // Más adelante, un "suelo" invisible podría ser un objeto con un collider que cause Game Over.
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ScorePoint"))
        {
            score++;
            Debug.Log("Score: " + score);
        }
    }
}