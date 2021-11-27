using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHealth = 20f;
    [SerializeField] float currentHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] AudioClip dieSound;
   public bool isAlive;
    bool isplaying;
    ObjectPool pool;
    Animator animator;
    AudioSource audioSource;
    Rigidbody rd;
    private void OnEnable()
    {
        pool = FindObjectOfType<ObjectPool>();
        currentHealth = maxHealth+pool.numberOfEnemy*10;
        healthBar.maxValue = currentHealth;
        isplaying = false;
        isAlive = true;
       // rd = GetComponent<Rigidbody>();
    }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
        {

            animator.SetTrigger("die");
            if (isplaying==false)
            {
                audioSource.PlayOneShot(dieSound);
                isplaying = true;
            }
            isAlive = false;
            GameManager.Instance.money += 50;//the money player get when kill the enemy
            GameManager.Instance.enemyCount--;
            Invoke("DoSomething", 1);
        }
    }
    private void DoSomething()
    {
        gameObject.SetActive(false);
       
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
    }
}
