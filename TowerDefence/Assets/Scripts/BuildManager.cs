using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance; // Inst�ncia est�tica para permitir acesso global ao BuildManager

    [Header("References")]
    [SerializeField] private Tower[] towers; // Array de torres dispon�veis para constru��o

    private int SelectedTower = 0; // �ndice da torre selecionada atualmente

    private void Awake()
    {
        // Define a inst�ncia para permitir acesso global ao BuildManager
        Instance = this;
    }

    // Retorna a torre atualmente selecionada no array de torres
    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }

    // Define a torre selecionada com base no �ndice fornecido
    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }

}
