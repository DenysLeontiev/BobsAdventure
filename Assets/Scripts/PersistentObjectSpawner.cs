using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject persistentObjectPrefab;

    public static bool hasSpawned = false;

    private void Awake()
    {
        var other = FindObjectOfType<PersistentObjectSpawner>();

        if (other != this)
        {
            Destroy(gameObject);
        }

        if(hasSpawned == false)
        {
            hasSpawned = true;
            SpawnPersistentObject();
        }

        DontDestroyOnLoad(gameObject);
    }

    private void SpawnPersistentObject()
    {
        GameObject persistentObj= Instantiate(persistentObjectPrefab);
        DontDestroyOnLoad(persistentObj);
    }
}
