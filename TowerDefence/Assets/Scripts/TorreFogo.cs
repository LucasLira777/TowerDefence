using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFogo : MonoBehaviour
{
<<<<<<< HEAD
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bps = 1f;
    [SerializeField] private float damagePerSecond = 10f; // Dano por segundo

    private Transform target;
    private float timeUntilFire;
=======
   
    [Header("Fire Damage Settings")]
    [SerializeField] private float damagePerSecond = 5f;
    [SerializeField] private float damageDuration = 3f; // Tempo em segundos para o dano contínuo
    [SerializeField] private LayerMask enemyMask;

    private Transform target;
>>>>>>> 26d5a0dbbc663aa9ed72b165c5e0e2047af828bb

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
<<<<<<< HEAD
            return;
        }

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
=======
        }
        else
        {
            if (!CheckTargetIsInRange())
            {
                target = null;
            }
            else
            {
                ApplyContinuousDamage();
>>>>>>> 26d5a0dbbc663aa9ed72b165c5e0e2047af828bb
            }
        }
    }

<<<<<<< HEAD
    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        DamageBullet bulletScript = bulletObj.GetComponent<DamageBullet>();
        bulletScript.SetTarget(target, damagePerSecond);
=======
    private void ApplyContinuousDamage()
    {
        // Aplica dano contínuo ao alvo
        if (target != null)
        {
            // Verifique se o alvo tem um componente de saúde
            Health targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                // Converte o dano para int antes de chamar o método TakeDamage
                targetHealth.TakeDamage((int)(damagePerSecond * Time.deltaTime));
            }
        }
>>>>>>> 26d5a0dbbc663aa9ed72b165c5e0e2047af828bb
    }

    private void FindTarget()
    {
<<<<<<< HEAD
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);
=======
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 5f, Vector2.zero, 0f, enemyMask);
>>>>>>> 26d5a0dbbc663aa9ed72b165c5e0e2047af828bb

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
<<<<<<< HEAD
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
}
=======
        return target != null && Vector2.Distance(target.position, transform.position) <= 5f;
    }
}
>>>>>>> 26d5a0dbbc663aa9ed72b165c5e0e2047af828bb
