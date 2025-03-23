using UnityEngine;

public class StaticEnemy : MonoBehaviour
{
    public int health = 3; // Vidas del enemigo
    private Renderer enemyRenderer; // Renderer para cambiar el color

    void Start()
    {
        // Obtener el componente Renderer del objeto
        enemyRenderer = GetComponent<Renderer>();

        // Verificar si el objeto tiene un Renderer
        if (enemyRenderer == null)
        {
            Debug.LogError("El objeto no tiene un Renderer asignado.");
            return;
        }

        // Crear una instancia del material para evitar modificar el original
        enemyRenderer.material = new Material(enemyRenderer.material);

        // Actualizar el color inicial
        UpdateColor();
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        // Reducir la salud
        health -= damage;

        // Actualizar el color según las vidas restantes
        UpdateColor();

        // Destruir el enemigo si no tiene vidas
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Actualizar el color según las vidas
    void UpdateColor()
    {
        switch (health)
        {
            case 3:
                enemyRenderer.material.color = Color.green;
                break;
            case 2:
                enemyRenderer.material.color = Color.yellow;
                break;
            case 1:
                enemyRenderer.material.color = Color.red;
                break;
            default:
                enemyRenderer.material.color = Color.white; // Color por defecto
                break;
        }
    }

    // Detectar colisiones con proyectiles
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto que colisionó es un proyectil
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Aplicar daño al enemigo
            TakeDamage(1);

            // Destruir el proyectil
            Destroy(collision.gameObject);
        }
    }
}