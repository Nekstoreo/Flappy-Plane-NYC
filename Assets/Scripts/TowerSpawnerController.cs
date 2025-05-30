using UnityEngine;

public class TowerSpawnerController : MonoBehaviour
{
    // Variable pública para arrastrar nuestro Prefab de TowerPair desde el editor.
    public GameObject towerPrefab;

    // Tiempo (en segundos) entre la aparición de cada par de torres.
    public float spawnRate = 2.5f; // Puedes ajustar esto para cambiar la dificultad.

    // Retraso inicial antes de que comience a aparecer el primer par de torres.
    public float initialDelay = 1.0f;

    // Rango de variación vertical para la posición de las torres.
    // Un valor de 2.5f significa que las torres pueden aparecer hasta 2.5 unidades
    // por encima o por debajo de la posición Y del Spawner.
    public float heightOffset = 2.5f;

    // Start se llama una vez, justo antes de que se actualice el primer frame.
    void Start()
    {
        // Verificamos que el prefab de la torre haya sido asignado en el Inspector.
        if (towerPrefab == null)
        {
            Debug.LogError("Tower Prefab no asignado en el TowerSpawnerController. Por favor, asigna el prefab en el Inspector.");
            return; // No continuamos si no hay prefab.
        }

        // NO iniciamos InvokeRepeating aquí, dejamos que el GameManager controle cuándo empezar
        // El GameManager llamará a StartSpawning() cuando sea apropiado
        Debug.Log("TowerSpawnerController initialized. Waiting for GameManager to start spawning.");
    }

    // Método público para que el GameManager pueda iniciar el spawning
    public void StartSpawning()
    {
        CancelInvoke("SpawnTower"); // Cancelar cualquier invoke anterior por seguridad
        InvokeRepeating("SpawnTower", initialDelay, spawnRate);
        Debug.Log("Tower spawning started by GameManager.");
    }

    // Método público para detener el spawning
    public void StopSpawning()
    {
        CancelInvoke("SpawnTower");
        Debug.Log("Tower spawning stopped.");
    }

    // Función que se encarga de crear (instanciar) un nuevo par de torres.
    void SpawnTower()
    {
        // Creamos una nueva instancia del prefab de la torre.
        GameObject newTower = Instantiate(towerPrefab);

        // Calculamos una posición Y aleatoria para el nuevo par de torres.
        // Random.Range(min, max) nos da un número flotante aleatorio entre min (inclusivo) y max (inclusivo).
        float randomY = Random.Range(-heightOffset, heightOffset);

        // Establecemos la posición del nuevo par de torres.
        // Su posición X y Z será la misma que la del Spawner (este GameObject).
        // Su posición Y será la del Spawner más el desplazamiento aleatorio 'randomY'.
        newTower.transform.position = transform.position + new Vector3(0, randomY, 0);

        // Las torres instanciadas ya tienen el script TowerMovement que las hará moverse y autodestruirse.
    }
}