using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject chasingEnemyPrefab; // Prefab del enemigo
    public Transform player; // Referencia al jugador
    public float spawnInterval = 5f; // Tiempo entre spawns
    public int maxEnemies = 10; // Máximo de enemigos simultáneos

    [Header("Área de Spawn")]
    public Vector3 spawnCenter = Vector3.zero; // Centro del cubo
    public Vector3 spawnSize = new Vector3(10f, 0f, 10f); // Tamaño del cubo

    private int currentEnemies = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemies < maxEnemies)
            {
                // Calcular posición aleatoria dentro del cubo
                Vector3 spawnPosition = new Vector3(
                    spawnCenter.x + Random.Range(-spawnSize.x / 2, spawnSize.x / 2),
                    spawnCenter.y,
                    spawnCenter.z + Random.Range(-spawnSize.z / 2, spawnSize.z / 2)
                );

                // Instanciar enemigo
                GameObject newEnemy = Instantiate(chasingEnemyPrefab, spawnPosition, Quaternion.identity);
                currentEnemies++;

                // Configurar referencia al jugador
                ChasingEnemy enemyScript = newEnemy.GetComponent<ChasingEnemy>();
                if (enemyScript != null)
                {
                    enemyScript.player = player;
                }

                // Configurar destrucción
                EnemyHealth enemyHealth = newEnemy.GetComponent<EnemyHealth>();
                if (enemyHealth == null)
                {
                    enemyHealth = newEnemy.AddComponent<EnemyHealth>();
                }
                enemyHealth.spawner = this;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Método para reducir el contador de enemigos
    public void EnemyDestroyed()
    {
        currentEnemies--;
    }

    // Dibujar Gizmo para ver el área de spawn en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(spawnCenter, spawnSize);
    }
}
