using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Combat Settings")]
    public float health = 50f;
    public float attackRange = 15f;
    public float shootCooldown = 2f;
    public float damagePerShot = 10f;
    
    [Header("Projectile Settings")]
    public GameObject enemyProjectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed = 20f;
    
    [Header("Effects")]
    public ParticleSystem deathEffect;
    public AudioClip shootSound;
    
    private Transform player;
    private float lastShootTime;
    private AudioSource audioSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null) return;

        // Проверяем расстояние до игрока
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        // Если игрок в радиусе атаки и прошло время перезарядки
        if (distanceToPlayer <= attackRange && Time.time > lastShootTime + shootCooldown)
        {
            ShootAtPlayer();
            lastShootTime = Time.time;
        }

        // Поворачиваемся к игроку
        if (distanceToPlayer <= attackRange * 1.5f)
        {
            FacePlayer();
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Не наклоняем врага по вертикали
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ShootAtPlayer()
    {
        if (enemyProjectilePrefab == null || projectileSpawnPoint == null) return;

        // Создаем снаряд
        GameObject projectile = Instantiate(enemyProjectilePrefab, 
                                         projectileSpawnPoint.position, 
                                         projectileSpawnPoint.rotation);
        
        // Направляем снаряд в игрока
        Vector3 shootDirection = (player.position - projectileSpawnPoint.position).normalized;
        
        // Настройка снаряда
        EnemyProjectile enemyProjectile = projectile.GetComponent<EnemyProjectile>();
        if (enemyProjectile != null)
        {
            enemyProjectile.SetDamage(damagePerShot);
        }

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * projectileSpeed;
        }

        // Эффекты
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
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
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}