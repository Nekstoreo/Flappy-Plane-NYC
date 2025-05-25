using UnityEngine;

public class TowerMovement : MonoBehaviour
{
    // Velocidad a la que se moverá el par de torres hacia la izquierda.
    // Pública para poder ajustarla desde el Inspector del Prefab.
    public float speed = 3f; // Empecemos con una velocidad moderada, luego la ajustas.

    // Límite en el eje X para destruir las torres cuando salgan de la pantalla.
    // Pública para ajustarla si es necesario.
    public float despawnBoundaryX = -10f; // Ajusta este valor según el ancho de tu pantalla de juego.

    // Update se llama una vez por cada frame.
    void Update()
    {
        // 1. Mover el par de torres hacia la izquierda.
        // transform.Translate mueve el objeto en la dirección y distancia especificadas.
        // Vector2.left es una forma corta de escribir new Vector2(-1, 0).
        // Multiplicamos por 'speed' para controlar la velocidad.
        // Multiplicamos por 'Time.deltaTime' para que el movimiento sea suave e independiente
        // de la tasa de frames (fps) del juego.
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // 2. Verificar si el par de torres ha salido de la pantalla para destruirlo.
        // Comparamos la posición X actual del transform de este GameObject (el TowerPair)
        // con nuestro límite de destrucción.
        if (transform.position.x < despawnBoundaryX)
        {
            // Si la torre ha pasado el límite, la destruimos.
            // Destroy(gameObject) destruye el GameObject al que este script está adjunto (o sea, el TowerPair completo).
            Destroy(gameObject);
        }
    }
}