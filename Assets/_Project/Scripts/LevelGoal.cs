using System.Collections;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float transitionDelay;
    
    private SceneLoader _sceneLoaderComponent;

    private void Start()
    {
        _sceneLoaderComponent = GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(GoToNextLevel());
    }

    private IEnumerator GoToNextLevel()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSecondsRealtime(transitionDelay);
        Time.timeScale = 1.0f;
        _sceneLoaderComponent.LoadScene(sceneToLoad);
    }
}
