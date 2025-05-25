using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public GameObject projectile;
    public Transform player;
    public float shootCooldown = 2f;
    private float lastShootTime;

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

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().linearVelocity = (player.position - transform.position).normalized * 10f;
    }
}