using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // enemy prefab
    public GameObject enemyType;
    [SerializeField]
    private List<GameObject> enemies;
    // amount of needed enemies on the level
    [SerializeField]
    private int enemiesAmount;
    // amount of active or alive enemies
    [SerializeField]
    private int activeEnemiesAmount;

    // Singleton
    public static EnemyManager instance;

    void Awake()
    {
        instance = this;

        enemies = ObjectPool.instance.CreatePool(enemyType, enemiesAmount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
