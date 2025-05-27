using UnityEngine;

public class ProjectileForPlayer : MonoBehaviour
{
    public float damage = 10f;
    public GameObject impactEffect;
    
    void OnCollisionEnter(Collision collision)
    {
        // При попадании во врага
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }

        // Создаем эффект попадания
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        // Уничтожаем пулю
        Destroy(gameObject);
    }
}