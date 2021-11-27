using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LineRenderer lineRenderer;

    Transform target;
    
    [Header("Attributes")]
    float fireCountDown;
    [SerializeField] float fireRate=1f;
    [SerializeField] float buildDelayTime = 1;
    [SerializeField] float weaponRang = 15f;
    [SerializeField] float weaponTurnSpeed = 10;
    [SerializeField] bool useLaser=false;
    [SerializeField] float firePower = 20f;
    public bool hasBeenBuild=false;
    void Start()
    {
        StartCoroutine(BuldBody());
        InvokeRepeating("FindClothestEnemy", 0, 0.5f);
        fireCountDown = 0;
    }
    void Update()
    {
        if (target == null)
        {
            if (useLaser&&lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
            return;
        }
        AimTarget();
        if (useLaser)
        {
            laser();
        }
        else
        {
            if (fireCountDown <= 0f)
            {
                Shoot();
                fireCountDown = fireRate;
            }
            fireCountDown -= Time.deltaTime;
        }
    }
    void laser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);
        target.GetComponent<Enemy>().TakeDamage(firePower * Time.deltaTime);
        target.GetComponent<EnemyMover>().Slow(0.5f);
    }
    void Shoot()
    {
        GameObject bulletInstance =(GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        if(bullet!= null)
        {
            bullet.GetTarget(target);
        }

    }
    void AimTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(weapon.rotation, lookRotation, Time.deltaTime * weaponTurnSpeed).eulerAngles;
        weapon.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void FindClothestEnemy()
    {
        Enemy[] enemys = FindObjectsOfType<Enemy>();
        float minDistance = Mathf.Infinity;
        Transform clothestEnemy = null;
        foreach (Enemy enemy in enemys)
        {
            float dis = Vector3.Distance(enemy.transform.position, transform.position);
            if (dis < minDistance)
            {
                clothestEnemy = enemy.transform;
                minDistance = dis;
            }
        }
        if (clothestEnemy != null && minDistance < weaponRang)
        {
            target = clothestEnemy;
        }
        else
        {
            target = null;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRang);
    }
    IEnumerator BuldBody()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach(Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelayTime);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }
}

