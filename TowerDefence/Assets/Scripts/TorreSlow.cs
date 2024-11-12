using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TorreSlow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask; // Máscara de camada para identificar quais objetos são inimigos
    [SerializeField] private GameObject bulletPrefab; // Prefab da bala (não usado neste código, mas pode ser parte de um sistema de ataque)
    [SerializeField] private Transform firingPoint; // Ponto de onde as balas seriam disparadas (também não utilizado neste trecho)

    // Atributos da torre
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f; // Alcance dentro do qual a torre pode congelar inimigos
    [SerializeField] private float aps = 4f; // Ataques por segundo (quantidade de vezes que a torre tentará congelar inimigos em um segundo)
    [SerializeField] private float freezeTime = 1f; // Duração do congelamento dos inimigos em segundos

    private float timeuntilfire; // Tempo acumulado até o próximo ataque

    private void Update()
    {
        // Acumula o tempo até o próximo ataque
        timeuntilfire += Time.deltaTime;

        // Verifica se é hora de atacar (congelar inimigos)
        if (timeuntilfire >= 1f / aps)
        {
            FreezeEnemies(); // Chama o método para congelar inimigos
            timeuntilfire = 0f; // Reseta o tempo acumulado após o ataque
        }
    }

    private void FreezeEnemies()
    {
        // Realiza um CircleCast para detectar inimigos dentro do alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // Verifica se algum inimigo foi atingido
        if (hits.Length > 0)
        {
            // Itera sobre todos os inimigos detectados
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                // Obtém o componente EnemyMovement do inimigo
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                // Reduz a velocidade do inimigo para congelá-lo
                em.UpdateSpeed(0.5f);

                // Inicia uma coroutine para restaurar a velocidade após o tempo de congelamento
                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    // Coroutine para restaurar a velocidade do inimigo após o tempo de congelamento
    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        // Aguarda o tempo de congelamento
        yield return new WaitForSeconds(freezeTime);
        // Restaura a velocidade original do inimigo
        em.ResetSpeed();
    }



}
