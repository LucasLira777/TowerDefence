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
    [SerializeField] private float targetingRange = 5f; // Alcance dentro do qual a torre pode congelar inimigos
    [SerializeField] private float aps = 4f; // Ataques por segundo (quantidade de vezes que a torre tentar� congelar inimigos em um segundo)
    [SerializeField] private float freezeTime = 1f; // Dura��o do congelamento dos inimigos em segundos

    private float timeuntilfire; // Tempo acumulado at� o pr�ximo ataque

    private void Update()
    {
        // Acumula o tempo at� o pr�ximo ataque
        timeuntilfire += Time.deltaTime;

        // Verifica se � hora de atacar (congelar inimigos)
        if (timeuntilfire >= 1f / aps)
        {
            FreezeEnemies(); // Chama o m�todo para congelar inimigos
            timeuntilfire = 0f; // Reseta o tempo acumulado ap�s o ataque
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

                // Obt�m o componente EnemyMovement do inimigo
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                // Reduz a velocidade do inimigo para congel�-lo
                em.UpdateSpeed(0.5f);

                // Inicia uma coroutine para restaurar a velocidade ap�s o tempo de congelamento
                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    // Coroutine para restaurar a velocidade do inimigo ap�s o tempo de congelamento
    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        // Aguarda o tempo de congelamento
        yield return new WaitForSeconds(freezeTime);
        // Restaura a velocidade original do inimigo
        em.ResetSpeed();
    }



}
