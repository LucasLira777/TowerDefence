using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreFogo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask; // Máscara de camada para identificar inimigos

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f; // Alcance dentro do qual a torre causa dano aos inimigos
    [SerializeField] private float dps = 10f; // Dano por segundo

    private void Update()
    {
        // Chama o método para causar dano aos inimigos na área a cada atualização do quadro
        DamageEnemiesInArea();
    }

    private void DamageEnemiesInArea()
    {
        // Faz um CircleCast ao redor da torre para detectar inimigos no alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // Verifica se algum inimigo foi atingido
        if (hits.Length > 0)
        {
            // Itera sobre todos os inimigos detectados
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                // Obtém o componente Health do inimigo
                Health enemyHealth = hit.transform.GetComponent<Health>();

                // Verifica se o inimigo possui um componente de saúde
                if (enemyHealth != null)
                {
                    // Causa dano ao inimigo, calculando o dano com base no dps e no tempo decorrido
                    enemyHealth.TakeDamage(Mathf.RoundToInt(dps * Time.deltaTime));
                }
            }
        }
    }
}
