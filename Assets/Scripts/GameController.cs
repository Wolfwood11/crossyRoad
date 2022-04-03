using System;
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
    public Action SceneIsReady;
    
    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject loosePopup;
    [SerializeField] private GameObject[] gameObjectsToPool;
    [SerializeField] public ObjectsPool objectsPool = new ObjectsPool();

    private int _forwardStepsToWin = 0;
    
    public int ForwardStepsToWin
    {
        get => _forwardStepsToWin;
        set
        {
            if (_forwardStepsToWin != value && value == 0)
            {
                Instance.Difficulty++;
                Instance.ShowWinPopup();
                BaseGameObject.IsPaused = true;
            }
            _forwardStepsToWin = value;
        } 
    }
    
    public int Difficulty { get; set; }

    public const int WorldSize = 25;

    private readonly Dictionary<ObjectTypes, List<GameObject>> _objectsDictionary = new Dictionary<ObjectTypes, List<GameObject>>();

    private MapGenerator _map;
    public void Loose()
    {
        BaseGameObject.IsPaused = true;
        Instance.ShowLoosePopup();
    }

    public void StepForward()
    {
        Instance.ForwardStepsToWin--;
        if (_map != null)
        {
            _map.StepForward();
        }
    }
    
    private IEnumerator PrepareScene()
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
            yield return new WaitForSeconds(0.05f);
        }
        
        yield return new WaitForSeconds(0.5f);
        
        
        GenerateNewWorld();
    }

    public void GenerateNewWorld(bool deactivate = false)
    {
        BaseGameObject.IsPaused = false;
        
        ForwardStepsToWin = -1;
        
        if (deactivate)
        {
            objectsPool.DeactivateAllPooled();
        }

        if (_map == null)
        {
            _map = FindObjectOfType<MapGenerator>();
        }
        
        _map.GenerateWorld();
        
        SceneIsReady?.Invoke();
    }

    public void OnSceneReady()
    {
        StartCoroutine(PrepareScene());
    }

    private void ShowLoosePopup()
    {
        Instantiate(loosePopup, Vector3.zero, Quaternion.identity);
    }

    private void ShowWinPopup()
    {
        Instantiate(winPopup, Vector3.zero, Quaternion.identity);
    }

    public GameObject InstantiateObjectOfType(ObjectTypes key, Vector3 pos, Quaternion rot)
    {
        var getPooled = GetPolledObjectOfType(key);
        if (getPooled != null)
        {
            getPooled.transform.position = pos;

            getPooled.transform.rotation = rot;
            getPooled.SetActive(true);
            return getPooled;
        }
        return null;
    }

    public GameObject GetPolledObjectOfType(ObjectTypes key)
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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
