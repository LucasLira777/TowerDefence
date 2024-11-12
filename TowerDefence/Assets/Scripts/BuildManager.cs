using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance; // Instância estática para permitir acesso global ao BuildManager

    [Header("References")]
    [SerializeField] private Tower[] towers; // Array de torres disponíveis para construção

    private int SelectedTower = 0; // Índice da torre selecionada atualmente

    private void Awake()
    {
        // Define a instância para permitir acesso global ao BuildManager
        Instance = this;
    }

    // Retorna a torre atualmente selecionada no array de torres
    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }

    // Define a torre selecionada com base no índice fornecido
    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }

}
