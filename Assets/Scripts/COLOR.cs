using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Renderer objectRenderer; // Referencia al Renderer del objeto

    void Start()
    {
        // Obtener el componente Renderer del objeto
        objectRenderer = GetComponent<Renderer>();

        // Cambiar el color a rojo
        objectRenderer.material.color = Color.red;
    }

    void Update()
    {
        // Cambiar el color dinámicamente (opcional)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            objectRenderer.material.color = Color.blue;
        }
    }
}