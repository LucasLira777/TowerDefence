using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Componente Rigidbody2D do inimigo para controlar seu movimento
    [SerializeField] private float moveSpeed = 2f; // Velocidade de movimento do inimigo
    private Transform target; // Próximo ponto de destino no caminho do inimigo
    private int pathIndex = 0; // Índice do ponto atual no caminho
    private float baseSpeed; // Velocidade base do inimigo para restaurar após modificações

    private void Start()
    {
        // Armazena a velocidade base e define o primeiro destino no caminho
        baseSpeed = moveSpeed;
        target = LevelManager.instance.path[pathIndex];
    }

    void Update()
    {
        // Verifica se o inimigo está próximo o suficiente do destino atual
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++; // Avança para o próximo ponto no caminho

            // Verifica se o inimigo chegou ao final do caminho
            if (pathIndex == LevelManager.instance.path.Length)
            {
                // Invoca o evento de destruição do inimigo e destrói o inimigo
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                // Define o próximo destino no caminho
                target = LevelManager.instance.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        // Calcula a direção em direção ao alvo e move o inimigo nessa direção
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    // Atualiza a velocidade de movimento do inimigo para um novo valor
    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    // Restaura a velocidade de movimento do inimigo para o valor base
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
