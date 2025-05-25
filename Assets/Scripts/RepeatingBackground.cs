using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{
    public float scrollSpeed = 1f;

    // Un punto X en el mundo. Cuando el pivote del sprite cruce este punto
    // hacia la izquierda, se considerará que debe reposicionarse.
    // Necesitarás ajustar este valor en el Inspector.
    // Debería ser un valor a la izquierda de la cámara, donde el sprite ya no es visible.
    public float leftLoopTriggerX = -15f;

    private float spriteWidth;

    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        if (sRenderer != null)
        {
            spriteWidth = sRenderer.bounds.size.x;
            if (spriteWidth == 0) {
                // Si bounds.size.x es 0 (puede pasar si el sprite no es visible o la escala es 0 al inicio)
                // intenta calcularlo desde el sprite asset.
                if (sRenderer.sprite != null) {
                    spriteWidth = sRenderer.sprite.rect.width / sRenderer.sprite.pixelsPerUnit;
                } else {
                    Debug.LogError("Sprite no asignado en el SpriteRenderer de: " + gameObject.name);
                }
            }
        }
        else
        {
            Debug.LogError("RepeatingBackground script necesita un SpriteRenderer en el GameObject: " + gameObject.name);
            enabled = false; // Desactivar el script si no hay SpriteRenderer
            return;
        }

        if (spriteWidth == 0) {
            Debug.LogError("No se pudo determinar el ancho del sprite para: " + gameObject.name + ". El scroll no funcionará correctamente.");
            enabled = false; // Desactivar el script si el ancho es 0
        }
    }

    void Update()
    {
        if (spriteWidth == 0) return; // No hacer nada si el ancho del sprite no es válido

        // Mover el fondo hacia la izquierda.
        transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);

        // Comprobar si el pivote (centro) del sprite ha cruzado el punto de bucle izquierdo.
        if (transform.position.x < leftLoopTriggerX)
        {
            // Si ha pasado el umbral, lo reposicionamos.
            // Lo movemos hacia la derecha una distancia igual al ancho de DOS sprites.
            transform.position += new Vector3(spriteWidth * 2f, 0, 0);
        }
    }
}