using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public float maxPlayerHealth;
    public float currentHealth;

    public Image HealthBar;
   

    void Start()
    {
        currentHealth = maxPlayerHealth;
       
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            ReloadCurrentScene();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            currentHealth -= 10;
            HealthBar.fillAmount = currentHealth /maxPlayerHealth;
           
        }
    }

   
    private void ReloadCurrentScene()
    {
        int currentSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentSceneIndex);
    }
public void ReplenishHealth(float amount)
{
    currentHealth = Mathf.Min(maxPlayerHealth, currentHealth + amount);
    HealthBar.fillAmount = currentHealth / maxPlayerHealth;
}

}
