using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; 

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Здоровье игрока: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Игрок умер!");
        
    }
}