using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitArea : MonoBehaviour
{
    [Serializable]
    public enum EntracePoint { A, B, C, D }


    [SerializeField] private int sceneIndexToLoad = -1;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private EntracePoint entracePoint;

    public Transform GetSpawnPoint()
    {
        return spawnPoint;
    }

    public EntracePoint GetEntracePoint()
    {
        return entracePoint;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(Transiotion());  
        }
    }

    private IEnumerator Transiotion()
    {
        if(sceneIndexToLoad < 0)
        {
            Debug.Log("SceneIndex must be bigger or equal to 0");
            yield break;
        }

        DontDestroyOnLoad(gameObject);

        yield return SceneManager.LoadSceneAsync(sceneIndexToLoad); // waiting for scene to load

        var otherExitArea = GetOtherExitArea();
        UpdatePlayer(otherExitArea);

        Destroy(gameObject);
    }

    private void UpdatePlayer(ExitArea otherExitArea)
    {
        var player = PlayerController.Instance.GetComponent<Transform>();
        player.position = otherExitArea.GetSpawnPoint().position;
        player.rotation = otherExitArea.GetSpawnPoint().rotation;
    }

    private ExitArea GetOtherExitArea() 
    {
        foreach (ExitArea exitArea in GameObject.FindObjectsOfType<ExitArea>())
        {
            if (exitArea == this)
                continue;
            if (exitArea.GetEntracePoint() != GetEntracePoint())
                continue; 
            return exitArea;
        }

        return null;    
    }
}
