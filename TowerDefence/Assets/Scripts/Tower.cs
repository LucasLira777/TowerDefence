using System;
using UnityEngine;

[Serializable]
public class Tower
{
    public string name; // Nome da torre
    public int cost; // Custo da torre em moeda
    public GameObject prefab; // Prefab da torre a ser instanciado ao construir

    // Construtor da classe Tower para inicializar uma nova instância com nome, custo e prefab
    public Tower(string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}
