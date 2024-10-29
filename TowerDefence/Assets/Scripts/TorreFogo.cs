using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFogo : MonoBehaviour
{
   
    [Header("Fire Damage Settings")]
    [SerializeField] private float damagePerSecond = 5f;
    [SerializeField] private float damageDuration = 3f; // Tempo em segundos para o dano contínuo
    [SerializeField] private LayerMask enemyMask;

    private Transform target;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
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
            }
        }
    }

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
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 5f, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return target != null && Vector2.Distance(target.position, transform.position) <= 5f;
    }
}
