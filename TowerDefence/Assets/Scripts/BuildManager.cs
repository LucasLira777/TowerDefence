using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance;
    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int SelectedTower = 0;

    private void Awake()
    {
        Instance = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }

}
