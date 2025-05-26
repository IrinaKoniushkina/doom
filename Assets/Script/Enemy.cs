using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 10f;
    public GameObject projectile;  
    public Transform player;
    public float shootCooldown = 2f;
    public float projectileSpeed = 10f; // Добавим отдельную переменную для скорости
    private float lastShootTime;
    public ParticleSystem bloodEffect;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time > lastShootTime + shootCooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        if (projectile == null) 
        {
            Debug.LogError("Projectile not assigned!");
            return;
        }

        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        
        Vector3 direction = (player.position - transform.position).normalized;
        

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = direction * projectileSpeed;
        }
        else
        {
            Debug.LogError("Projectile has no Rigidbody component!");
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        
        if (bloodEffect != null)
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
        
        if (health <= 0f) 
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}