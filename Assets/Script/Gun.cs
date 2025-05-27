using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 30f;
    public float bulletLifetime = 3f;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;

    [Header("Aiming")]
    public float rotationSpeed = 5f;
    public float maxAngle = 60f; 
    private Vector3 initialForward;

    void Start()
    {
        initialForward = transform.forward;
    }

    void Update()
    {
        AimWithMouse();
        
        if (Input.GetMouseButtonDown(1)) 
        {
            ShootBullet();
        }
    }

    void AimWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Vector3 targetDirection = hit.point - transform.position;
            targetDirection.y = 0;
            
            // Ограничиваем угол поворота относительно начального направления
            float angle = Vector3.Angle(initialForward, targetDirection);
            if (angle <= maxAngle)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    void ShootBullet()
{
    if (bulletPrefab == null || bulletSpawnPoint == null)
    {
        Debug.LogError("Bullet prefab or spawn point not assigned!");
        return;
    }

    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    
    Rigidbody rb = bullet.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
    }
    else
    {
        Debug.LogError("Bullet has no Rigidbody!");
    }

    Destroy(bullet, bulletLifetime);
}
}