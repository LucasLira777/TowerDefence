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
    [SerializeField] private float targetingRange = 5f; // Alcance dentro do qual a torre aplica lentidão nos inimigos
    [SerializeField] private float aps = 4f; // Ataques por segundo (quantidade de vezes que a torre tentará aplicar lentidão nos inimigos em um segundo)
    [SerializeField] private float slowAmount = 0.5f; // Percentual de redução da velocidade do inimigo (0.5f = 50% de redução)
    [SerializeField] private float slowDuration = 1f; // Duração do efeito de lentidão nos inimigos

    private float timeUntilFire; // Tempo acumulado até o próximo efeito de lentidão

    private void Update()
    {
        // Acumula o tempo até o próximo efeito de lentidão
        timeUntilFire += Time.deltaTime;

        // Verifica se é hora de aplicar o efeito de lentidão
        if (timeUntilFire >= 1f / aps)
        {
            ApplySlowEffect(); // Chama o método para aplicar o efeito de lentidão nos inimigos
            timeUntilFire = 0f; // Reseta o tempo acumulado após o efeito de lentidão
        }
    }

    private void ApplySlowEffect()
    {
        // Realiza um CircleCast para detectar inimigos dentro do alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // Verifica se algum inimigo foi atingido
        if (hits.Length > 0)
        {
            // Itera sobre todos os inimigos detectados
            foreach (RaycastHit2D hit in hits)
            {
                // Obtém o componente EnemyMovement do inimigo
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();

                if (em != null && !em.IsSlowed) // Aplica lentidão apenas se o inimigo ainda não estiver lento
                {
                    em.UpdateSpeed(slowAmount); // Reduz a velocidade do inimigo

                    // Inicia uma coroutine para restaurar a velocidade após o tempo de lentidão
                    StartCoroutine(RemoveSlowEffect(em));
                }
            }
        }
    }

    // Coroutine para restaurar a velocidade do inimigo após o tempo de lentidão
    private IEnumerator RemoveSlowEffect(EnemyMovement em)
    {
        // Aguarda o tempo de lentidão
        yield return new WaitForSeconds(slowDuration);

        // Restaura a velocidade original do inimigo
        em.ResetSpeed();
    }


}
