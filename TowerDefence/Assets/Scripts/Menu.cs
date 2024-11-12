using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI; // Refer�ncia ao componente de UI TextMeshPro para exibir o valor da moeda

    private void OnGUI()
    {
        // Atualiza o texto da UI com o valor atual da moeda do jogador
        currencyUI.text = LevelManager.instance.currency.ToString();
    }

    public void SetSelected()
    {
        // M�todo reservado para definir a torre selecionada. Implementa��o futura pode ir aqui.
    }

}
