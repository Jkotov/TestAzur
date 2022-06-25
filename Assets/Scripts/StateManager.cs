using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> elementsToDisable;
    [SerializeField] private List<GameObject> elementsToEnable;
    [SerializeField] private PlayerMove move;
    [SerializeField] private bool canPlayerMove;
    
    public void SetObjectsState()
    {
        move.CanRun = canPlayerMove;
        foreach (var element in elementsToDisable)
        {
            element.SetActive(false);
        }

        foreach (var element in elementsToEnable)
        {
            element.SetActive(true);
        }
    }
}