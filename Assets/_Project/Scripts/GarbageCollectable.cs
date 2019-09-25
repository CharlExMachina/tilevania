using UnityEngine;

public class GarbageCollectable : MonoBehaviour
{
    private LevelGarbageCollector _garbageCollector;
    
    private void Start()
    {
        _garbageCollector = FindObjectOfType<LevelGarbageCollector>();
    }

    public void ThrowToGarbage()
    {
        _garbageCollector.AddToGarbageBin(transform);
    }
}
