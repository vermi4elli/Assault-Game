using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyType;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>();
    [SerializeField]
    private int enemiesAmount;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemiesAmount; i++)
        {
            GameObject spawnPoint = ChooseSpawnPoint();
            GameObject enemyTemp = Instantiate(enemyType, spawnPoint.transform.position + Vector3.up, spawnPoint.transform.rotation);
            enemies.Add(enemyTemp);
        }
    }

    private GameObject ChooseSpawnPoint()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
