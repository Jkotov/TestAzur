using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CoinOnRoad : MonoBehaviour, ICollectable
{
    [SerializeField] private ParticlePool particlePool;
    [SerializeField] private int coinInStack = 5;
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private Transform particleDestination;
    private float currentRotation;
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, currentRotation, 0);
        currentRotation += rotationSpeed;
    }

    public int Collect()
    {
        CreateParticles();
        StartCoroutine(Disable());
        return coinInStack;
    }

    [ContextMenu("CreateParticles")]
    private void CreateParticles()
    {
        var particles = particlePool.CreateParticles(transform.position);
        particles.GetComponent<ParticleMove>().destination = particleDestination;
    }
    private IEnumerator Disable()
    {
        yield return new WaitForFixedUpdate();
        gameObject.SetActive(false);
    }
}