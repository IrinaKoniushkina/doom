using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public AudioSource shootSound;

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Левая кнопка мыши
        {
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        shootSound.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}