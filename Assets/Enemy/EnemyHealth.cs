using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth=5;
    [SerializeField] int currentHealth;
    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other)
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

        }
    }
}
