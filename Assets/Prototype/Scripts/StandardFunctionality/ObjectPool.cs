using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Singleton

    public static ObjectPool instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public List<GameObject> CreatePool(GameObject prefab, int amount)
    {
        List<GameObject> objectPool = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            GameObject objectTemp = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            objectTemp.name = prefab.name + i;
            objectTemp.SetActive(false);
            objectPool.Add(objectTemp);
        }

        return objectPool;
    }
}
