using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plot : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private SpriteRenderer sr; // Componente SpriteRenderer do objeto para alterar sua cor
    [SerializeField] private Color hoverColor; // Cor a ser exibida ao passar o mouse sobre o objeto

    private GameObject tower; // Refer�ncia para a torre constru�da neste objeto
    private Color startColor; // Cor inicial do objeto

    private void Start()
    {
        // Armazena a cor inicial do SpriteRenderer
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        // Altera a cor do objeto para hoverColor quando o mouse entra
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        // Restaura a cor inicial do objeto quando o mouse sai
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        // Verifica se j� existe uma torre constru�da neste objeto; se sim, retorna
        if (tower != null) return;

        // Obt�m a torre selecionada a partir do BuildManager
        Tower towerToBuild = BuildManager.Instance.GetSelectedTower();

        // Verifica se o jogador tem moeda suficiente para construir a torre
        if (towerToBuild.cost > LevelManager.instance.currency)
        {
            Debug.Log("Voc� n�o pode comprar esta torre");
            return;
        }

        // Deduz o custo da torre do total de moeda do jogador
        LevelManager.instance.SpendCurrency(towerToBuild.cost);

        // Instancia a torre selecionada na posi��o do objeto
        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
    }



}


