using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    GameManager gameManager;
   
    [SerializeField] float speed = 100f;
    [SerializeField] float firePower=20f;
    [SerializeField] float explosionRadius = 0;
    //[SerializeField] GameObject impactEffect;
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void GetTarget(Transform t)
    {
        target = t;
    }
    void Update()
    {
        if (target == null)
        {
            
            return;
        }
        FollowTarget();
    }
    void FollowTarget()
    {
        Vector3 dir = target.position  - transform.position;
        float distanceThisFram = speed * Time.deltaTime;
        transform.Translate(dir.normalized * distanceThisFram, Space.World);
        transform.LookAt(target);
        if (dir.magnitude <= distanceThisFram)
        {
            HitTarget();
            return;
        }
    }
    void HitTarget()
    {
        if (explosionRadius == 0)
        {
            target.GetComponent<Enemy>().TakeDamage(firePower);
            //GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
           // Destroy(effect, 2f);
        }
        else
        {
            ExplosionDamage(transform.position, explosionRadius);
        }
        Destroy(gameObject);
    }
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Enemy")
            {
                hitCollider.SendMessage("TakeDamage", firePower);
            }
        }
    }
    
}
