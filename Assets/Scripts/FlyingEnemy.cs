using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento
    private List<Vector3> waypoints; // Lista de coordenadas de waypoints
    private int currentWaypointIndex = 0; // Índice del waypoint actual

    void Start()
    {
        // Generar waypoints automáticamente al inicio
        GenerateWaypoints();

        // Verificar si se generaron waypoints
        if (waypoints == null || waypoints.Count == 0)
        {
            Debug.LogError("No se generaron waypoints.");
            return;
        }
    }

    void Update()
    {
        // Mover hacia el siguiente waypoint
        MoveToWaypoint();
    }

    // Generar waypoints automáticamente usando coordenadas
    void GenerateWaypoints()
    {
        waypoints = new List<Vector3>
        {
            new Vector3(0f, 5f, 0f),   // Waypoint 1
            new Vector3(10f, 5f, 10f),  // Waypoint 2
            new Vector3(-10f, 5f, 10f), // Waypoint 3
            new Vector3(0f, 5f, 20f)    // Waypoint 4
        };

        // Opcional: Dibujar la ruta en el editor para visualización
        for (int i = 0; i < waypoints.Count; i++)
        {
            int nextIndex = (i + 1) % waypoints.Count;
            Debug.DrawLine(waypoints[i], waypoints[nextIndex], Color.red, 10f);
        }
    }

    // Mover al enemigo hacia el waypoint actual
    void MoveToWaypoint()
    {
        // Obtener el waypoint actual
        Vector3 targetWaypoint = waypoints[currentWaypointIndex];

        // Mover hacia el waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);

        // Cambiar al siguiente waypoint si está suficientemente cerca
        if (Vector3.Distance(transform.position, targetWaypoint) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}