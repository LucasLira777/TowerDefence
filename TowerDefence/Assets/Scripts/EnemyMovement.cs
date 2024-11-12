using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; // Componente Rigidbody2D do inimigo para controlar seu movimento
    [SerializeField] private float moveSpeed = 2f; // Velocidade de movimento do inimigo
    private Transform target; // Pr�ximo ponto de destino no caminho do inimigo
    private int pathIndex = 0; // �ndice do ponto atual no caminho
    private float baseSpeed; // Velocidade base do inimigo para restaurar ap�s modifica��es

    private void Start()
    {
        // Armazena a velocidade base e define o primeiro destino no caminho
        baseSpeed = moveSpeed;
        target = LevelManager.instance.path[pathIndex];
    }

    void Update()
    {
        // Verifica se o inimigo est� pr�ximo o suficiente do destino atual
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++; // Avan�a para o pr�ximo ponto no caminho

            // Verifica se o inimigo chegou ao final do caminho
            if (pathIndex == LevelManager.instance.path.Length)
            {
                // Invoca o evento de destrui��o do inimigo e destr�i o inimigo
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                // Define o pr�ximo destino no caminho
                target = LevelManager.instance.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        // Calcula a dire��o em dire��o ao alvo e move o inimigo nessa dire��o
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
