using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de origen del disparo
    public float bulletForce = 20f; // Fuerza del disparo

    void Update()
    {
        // Disparar al presionar el botón izquierdo del ratón
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Aplicar fuerza al proyectil
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

        // Destruir el proyectil después de 2 segundos (opcional)
        Destroy(bullet, 2f);
    }
}