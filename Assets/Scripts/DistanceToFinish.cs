using System;
using UI;
using UnityEngine;

public class DistanceToFinish : MonoBehaviour
{
    public delegate void FinishHandle();
    public event FinishHandle Finished;
    [SerializeField] private Transform player;
    [SerializeField] private Transform finish;
    [SerializeField] private GameObject distanceVisualizer;
    private IPercentVisualizer percentVisualizer;
    private float startZ;
    private float finishZ;
    private bool finished;
    private float NormalizedDistanceToFinish =>
        Mathf.InverseLerp(startZ, finishZ, player.transform.position.z);
    
    private void Awake()
    {
        startZ = player.transform.position.z;
        finishZ = finish.transform.position.z;
        if (distanceVisualizer != null)
            percentVisualizer = distanceVisualizer.GetComponent<IPercentVisualizer>();
    }

    private void Update()
    {
        var normalizedDistance = NormalizedDistanceToFinish;
        percentVisualizer?.UpdatePercent(normalizedDistance);
        if (normalizedDistance >= 1 && !finished)
        {
            Finished?.Invoke();
            finished = true;
        }
    }
}