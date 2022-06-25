using System;
using UI;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Collector : MonoBehaviour
{
    [SerializeField] private GameObject textObject;
    private IIntVisualizer visualizer;
    private int collected;
    
    private void Awake()
    {
        if (textObject != null)
            visualizer = textObject.GetComponent<IIntVisualizer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectable item))
        {
            collected += item.Collect();
            visualizer?.UpdateInt(collected);
        }
    }
}