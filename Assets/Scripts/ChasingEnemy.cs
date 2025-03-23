using UnityEngine;

public class ChasingEnemy : MonoBehaviour
{
    // Atributos originales de persecución (sin modificar)
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseDistance = 3f;

    // Nuevos atributos del StaticEnemy
    public int health = 3;
    private Renderer[] enemyRenderers;

    void Start()
    {
        // Inicialización de Renderers (nuevo)
        enemyRenderers = GetComponentsInChildren<Renderer>();
        
        if (enemyRenderers == null || enemyRenderers.Length == 0)
        {
            Debug.LogError("El objeto no tiene Renderers asignados.");
            return;
        }

        foreach (Renderer rend in enemyRenderers)
        {
            rend.material = new Material(rend.material);
        }
        
        UpdateColor();
    }

    void Update()
    {
        // Lógica original de persecución (sin cambios)
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= chaseDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // Métodos nuevos del StaticEnemy
    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateColor();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateColor()
    {
        if (enemyRenderers == null) return;

        foreach (Renderer rend in enemyRenderers)
        {
            switch (health)
            {
                case 3:
                    rend.material.color = Color.green;
                    break;
                case 2:
                    rend.material.color = Color.yellow;
                    break;
                case 1:
                    rend.material.color = Color.red;
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}