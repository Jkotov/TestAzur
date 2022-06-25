using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DistanceToFinish distanceToFinish;
    [SerializeField] private StateManager startRunManager;
    [SerializeField] private StateManager finishRunManager;

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Start()
    {
        distanceToFinish.Finished += PlayerFinished;
        StartCoroutine(Starter());
    }

    private IEnumerator Starter()
    {
        while(true)
        {
            if (Input.GetMouseButton(0))
            {
                GameStarted();
                yield break;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    
    private void GameStarted()
    {
        startRunManager.SetObjectsState();
    }
    
    private void PlayerFinished()
    {
        finishRunManager.SetObjectsState();
    }

    private void OnDestroy()
    {
        if (distanceToFinish != null)
        {
            distanceToFinish.Finished -= PlayerFinished;
        }
    }
}