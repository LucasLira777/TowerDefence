using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; // Instância estática da classe LevelManager para permitir acesso global
    public Transform startPoint; // Ponto inicial do nível, representado como um Transform
    public Transform[] path; // Array de Transforms que representam um caminho no nível

    public int currency; // Variável que representa a quantidade de moeda do jogador

    private void Awake()
    {
        // Define a instância para permitir acesso global ao LevelManager
        instance = this;
    }

    private void Start()
    {
        // Inicializa o valor da moeda com 100 no início do nível
        currency = 100;
    }

    public void IncreaseCurrency(int amount)
    {
        // Aumenta a quantidade de moeda pelo valor especificado
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        // Verifica se o jogador tem moeda suficiente para gastar
        if (amount < currency)
        {
            // Subtrai a quantidade de moeda e retorna true para indicar sucesso
            currency -= amount;
            return true;
        }
        else
        {
            // Exibe mensagem de erro se o jogador não tiver moeda suficiente
            Debug.Log("você não tem dinheiro para comprar esse item");
            return false;
        }
    }

}