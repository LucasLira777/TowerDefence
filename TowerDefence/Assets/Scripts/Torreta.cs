using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask; // Máscara de camada para identificar inimigos
    [SerializeField] private GameObject bulletprefab; // Prefab da bala que será disparada
    [SerializeField] private Transform firingpoint; // Ponto de onde as balas serão disparadas

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f; // Alcance de ataque da torre
    [SerializeField] private float bps = 1f; // Balas por segundo (taxa de disparo)

    private Transform target; // Inimigo atualmente mirado
    private float timeuntilfire; // Tempo até o próximo disparo

    private void Update()
    {
        // Se não houver um alvo, tenta encontrar um novo
        if (target == null)
        {
            FindTarget();
            return;
        }

        // Verifica se o alvo está dentro do alcance
        if (!CheckTargetIsInRange())
        {
            // Remove o alvo se estiver fora do alcance
            target = null;
        }
        else
        {
            // Incrementa o tempo até o próximo disparo
            timeuntilfire += Time.deltaTime;

            // Se o tempo acumulado for suficiente para disparar, dispara e reseta o tempo
            if (timeuntilfire >= 1f / bps)
            {
                shoot();
                timeuntilfire = 0f;
            }
        }
    }

    private void shoot()
    {
        // Instancia uma bala e define seu alvo
        GameObject bulletObj = Instantiate(bulletprefab, firingpoint.position, Quaternion.identity);
        bullet bulletScript = bulletObj.GetComponent<bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        // Executa um CircleCast para detectar inimigos dentro do alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // Se houver inimigos dentro do alcance, seleciona o primeiro como alvo
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        // Verifica se o alvo atual está dentro do alcance de ataque
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    // Desenha uma esfera no editor para visualizar o alcance da torre
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}