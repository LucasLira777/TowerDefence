using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; // Inst�ncia est�tica da classe LevelManager para permitir acesso global
    public Transform startPoint; // Ponto inicial do n�vel, representado como um Transform
    public Transform[] path; // Array de Transforms que representam um caminho no n�vel

    public int currency; // Vari�vel que representa a quantidade de moeda do jogador

    private void Awake()
    {
        // Define a inst�ncia para permitir acesso global ao LevelManager
        instance = this;
    }

    private void Start()
    {
        // Inicializa o valor da moeda com 100 no in�cio do n�vel
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
            // Exibe mensagem de erro se o jogador n�o tiver moeda suficiente
            Debug.Log("voc� n�o tem dinheiro para comprar esse item");
            return false;
        }
    }

}