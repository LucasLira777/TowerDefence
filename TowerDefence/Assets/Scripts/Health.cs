using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2; // Pontos de vida do inimigo
    [SerializeField] private int currencyWorth = 50; // Quantidade de moeda concedida ao jogador ao destruir o inimigo

    private bool isDestroyed = false; // Flag para garantir que o inimigo seja destru�do apenas uma vez

    // M�todo para o inimigo receber dano
    public void TakeDamage(int dmg)
    {
        // Reduz os pontos de vida do inimigo com base no dano recebido
        hitPoints -= dmg;

        // Verifica se os pontos de vida chegaram a zero e se o inimigo ainda n�o foi destru�do
        if (hitPoints <= 0 && !isDestroyed)
        {
            // Invoca o evento de destrui��o do inimigo para atualizar contadores, etc.
            EnemySpawner.onEnemyDestroy.Invoke();

            // Aumenta a moeda do jogador ao destruir o inimigo
            LevelManager.instance.IncreaseCurrency(currencyWorth);

            // Marca o inimigo como destru�do para evitar chamadas duplicadas
            isDestroyed = true;

            // Remove o inimigo da cena
            Destroy(gameObject);
        }
    }




}
