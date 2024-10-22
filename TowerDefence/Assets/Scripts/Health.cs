using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("attributes")]
    [SerializeField] private int hitPoints = 2;
    
    public void takeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0) 
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }







}
