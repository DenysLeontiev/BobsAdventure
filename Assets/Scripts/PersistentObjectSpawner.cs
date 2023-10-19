using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject persistentObjectPrefab;

    public static bool hasSpawned = false;

    private void Awake()
    {
        if(hasSpawned == false)
        {
            hasSpawned = true;
            SpawnPersistentObject();
        }
    }

    private void SpawnPersistentObject()
    {
        GameObject persistentObj= Instantiate(persistentObjectPrefab);
        DontDestroyOnLoad(persistentObj);
    }
}
