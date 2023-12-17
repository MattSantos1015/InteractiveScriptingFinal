using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;
    public int enemyMaxHealth;

    void Start()
    {
        enemyHealth = enemyMaxHealth;
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
            ScoreManager.AddPoints(100); // Increase the score using the ScoreManager script
          
        }
    }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            enemyHealth -= 10;
        }
    }
}
