using System.Collections;
using System.Collections.Generic;
using Base;
using Base.Enums;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using World;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject loosePopup;
    [SerializeField] private GameObject[] gameObjectsToPool;
    [SerializeField]public ObjectsPool objectsPool = new ObjectsPool();

    public float TargetToWin { get; set; }
    public int Difficulty { get; set; }

    public const int WorldSize = 25;

    private readonly Dictionary<ObjectTypes, List<GameObject>> _objectsDictionary = new Dictionary<ObjectTypes, List<GameObject>>();

    public void OnSceneReady()
    {
        _objectsDictionary.Clear();
        
        objectsPool.ClearPool();
        
        foreach (var toPool in gameObjectsToPool)
        {
            var objectType = toPool.GetComponent<BaseGameObject>().ObjectType;
            if (_objectsDictionary.ContainsKey(objectType))
            {
                _objectsDictionary[objectType].Add(toPool);
            }
            else
            {
                var newList = new List<GameObject> { toPool };
                _objectsDictionary.Add(objectType, newList);
            }
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
        return GetPolledObjectOfType(ObjectTypes.Car);
    }

    private GameObject GetPolledObjectOfType(ObjectTypes key)
    {
        if (!_objectsDictionary.ContainsKey(key)) return null;
        var list = _objectsDictionary[key];
        var element = list.RandomElement();
        var outObject = objectsPool.GetPolledObject(element);
        if (outObject)
        {
            return outObject;
        }
        return objectsPool.AddToPoll(element);
    }

    public GameObject GetTreeGameObject()
    {
        return GetPolledObjectOfType(ObjectTypes.Tree);
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
