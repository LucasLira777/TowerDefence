using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TorreSlow : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask; // M�scara de camada para identificar quais objetos s�o inimigos
    [SerializeField] private GameObject bulletPrefab; // Prefab da bala (n�o usado neste c�digo, mas pode ser parte de um sistema de ataque)
    [SerializeField] private Transform firingPoint; // Ponto de onde as balas seriam disparadas (tamb�m n�o utilizado neste trecho)

    // Atributos da torre
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f; // Alcance dentro do qual a torre aplica lentid�o nos inimigos
    [SerializeField] private float aps = 4f; // Ataques por segundo (quantidade de vezes que a torre tentar� aplicar lentid�o nos inimigos em um segundo)
    [SerializeField] private float slowAmount = 0.5f; // Percentual de redu��o da velocidade do inimigo (0.5f = 50% de redu��o)
    [SerializeField] private float slowDuration = 1f; // Dura��o do efeito de lentid�o nos inimigos

    private float timeUntilFire; // Tempo acumulado at� o pr�ximo efeito de lentid�o

    private void Update()
    {
        // Acumula o tempo at� o pr�ximo efeito de lentid�o
        timeUntilFire += Time.deltaTime;

        // Verifica se � hora de aplicar o efeito de lentid�o
        if (timeUntilFire >= 1f / aps)
        {
            ApplySlowEffect(); // Chama o m�todo para aplicar o efeito de lentid�o nos inimigos
            timeUntilFire = 0f; // Reseta o tempo acumulado ap�s o efeito de lentid�o
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
                // Obt�m o componente EnemyMovement do inimigo
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();

                if (em != null && !em.IsSlowed) // Aplica lentid�o apenas se o inimigo ainda n�o estiver lento
                {
                    em.UpdateSpeed(slowAmount); // Reduz a velocidade do inimigo

                    // Inicia uma coroutine para restaurar a velocidade ap�s o tempo de lentid�o
                    StartCoroutine(RemoveSlowEffect(em));
                }
            }
        }
    }

    // Coroutine para restaurar a velocidade do inimigo ap�s o tempo de lentid�o
    private IEnumerator RemoveSlowEffect(EnemyMovement em)
    {
        // Aguarda o tempo de lentid�o
        yield return new WaitForSeconds(slowDuration);

        // Restaura a velocidade original do inimigo
        em.ResetSpeed();
    }


}
