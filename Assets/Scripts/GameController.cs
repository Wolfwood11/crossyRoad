using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using World;

public class GameController : MonoBehaviour
{
    public static GameController Instance = null;
    
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject loosePopup;
    [SerializeField] private GameObject[] gameObjectsToPool;
    [SerializeField]public ObjectsPool objectsPool = new ObjectsPool();

    public float TargetToWin { get; set; } = 0;
    public int Difficulty { get; set; } = 0;

    public const int WorldSize = 25;

    public void OnSceneReady()
    {
        objectsPool.ClearPool();
        
        foreach (var toPool in gameObjectsToPool)
        {
            objectsPool.PollObject(toPool);
        }

        var roads = FindObjectsOfType<RoadController>();
        foreach (var road in roads)
        {
            road.StartEmittingCars();
        }
        
        var lines = FindObjectsOfType<SafeZoneController>();
        foreach (var line in lines)
        {
            line.StartEmittingTrees();
        }
    }
    public void ShowLoosePopup()
    {
        Instantiate(loosePopup, Vector3.zero, Quaternion.identity);
    }
    
    public void ShowWinPopup()
    {
        Instantiate(winPopup, Vector3.zero, Quaternion.identity);
    }

    public GameObject GetCarGameObject()
    {
        return objectsPool.GetPolledObject(gameObjectsToPool[0]);
    }
    
    public GameObject GetTreeGameObject()
    {
        return objectsPool.GetPolledObject(gameObjectsToPool[1]);
    }
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
