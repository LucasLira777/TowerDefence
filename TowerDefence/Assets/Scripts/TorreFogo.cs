using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFogo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float dps = 10f; // Dano por segundo

    private void Update()
    {
        DamageEnemiesInArea();
    }

    private void DamageEnemiesInArea()
    {
        // Faz um círculo ao redor da torre para detectar inimigos no alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                Health enemyHealth = hit.transform.GetComponent<Health>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(Mathf.RoundToInt(dps * Time.deltaTime));
                }
            }
        }
    }
}
