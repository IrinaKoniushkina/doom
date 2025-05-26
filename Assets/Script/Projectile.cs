using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f; 
    public float speed = 10f;  
    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Enemy")) 
        {
            Destroy(gameObject);
        }
    }
}