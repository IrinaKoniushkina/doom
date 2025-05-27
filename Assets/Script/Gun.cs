using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;
    public float rotationSpeed = 5f;
    
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 30f;
    public float bulletLifetime = 3f;

    void Update()
    {
        // Поворот оружия за курсором
        RotateGunToMouse();

        // Стрельба пулей по ПКМ
        if (Input.GetMouseButtonDown(1)) // 1 - правая кнопка мыши
        {
            ShootBullet();
        }
    }

    void RotateGunToMouse()
    {
        Ray ray = fpsCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, range))
        {
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0; // Оставляем только горизонтальное вращение
            
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ShootBullet()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null)
        {
            Debug.LogError("Bullet prefab or spawn point not assigned!");
            return;
        }

        // Эффекты выстрела
        if (muzzleFlash != null) muzzleFlash.Play();
        if (shootSound != null) shootSound.Play();

        // Создание пули
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        
        // Настройка пули
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
        }
        else
        {
            Debug.LogError("Bullet prefab has no Rigidbody component!");
        }

        // Уничтожение пули через время
        Destroy(bullet, bulletLifetime);
    }
}