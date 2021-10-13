using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform weapon;
    [SerializeField] float weaponRang=15;
    [SerializeField] ParticleSystem ParticleSystem;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FindClothestEnemy();
        AimEnemy();
    }
    void FindClothestEnemy()
    {
        Enemy[] enemys = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;
        Transform clothestTarget = null;
        foreach (Enemy enemy in enemys)
        {
            float dis = Vector3.Distance(enemy.transform.position, transform.position);
            if (dis < maxDistance)
            {
                clothestTarget = enemy.transform;
                maxDistance = dis;
            }
        }
        target = clothestTarget;
    }
    void AimEnemy()
    {
        float dis = Vector3.Distance(target.transform.position, transform.position);

        weapon.LookAt(target);
        if (dis< weaponRang)
        {
            Fire(true);
        }
        else
        {
            Fire(false);
        }
    }
    void Fire(bool isActive)
    {
        var bullet = ParticleSystem.emission;
        bullet.enabled = isActive;
    }
}
