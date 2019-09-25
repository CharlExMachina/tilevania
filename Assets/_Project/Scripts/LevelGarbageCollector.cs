using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGarbageCollector : MonoBehaviour
{
    private static LevelGarbageCollector _instance;
    private GarbageCollectable[] _allCollectables;
    private int _lastSceneIndex;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        SceneManager.sceneUnloaded += scene => _lastSceneIndex = scene.buildIndex;
        SceneManager.activeSceneChanged += (lastScene, nextScene) =>
        {
            if (_lastSceneIndex != nextScene.buildIndex)
            {
                DestroyGarbage();
            }
        };
        SceneManager.sceneLoaded += (loadedScene, mode) =>
        {
            RemoveLevelGarbage(loadedScene);
        };
    }

    private void DestroyGarbage()
    {
        foreach (Transform garbage in transform.GetChild(0))
        {
            Destroy(garbage.gameObject);
        }
    }

    private void RemoveLevelGarbage(Scene loadedScene)
    {
        _allCollectables = FindObjectsOfType<GarbageCollectable>();

        foreach (Transform garbage in transform.GetChild(0))
        {
            foreach (GarbageCollectable collectable in _allCollectables)
            {
                if (garbage.gameObject.name.Equals(collectable.gameObject.name) &&
                    garbage.position == collectable.transform.position)
                {
                    collectable.gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void AddToGarbageBin(Transform obj)
    {
        obj.parent = transform.GetChild(0);
        obj.gameObject.SetActive(false);
    }
}    
