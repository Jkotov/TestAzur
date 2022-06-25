using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private GameObject particleSystemPrefab;

    private readonly List<GameObject> particleSystemObjects = new List<GameObject>();
    private readonly List<ParticleSystem> particleSystems = new List<ParticleSystem>();
    
    public GameObject CreateParticles(Vector3 position)
    {
        for (int i = 0; i < particleSystems.Count; i++)
        {
            if (particleSystems[i].isStopped)
            {
                particleSystemObjects[i].transform.position = position;
                particleSystems[i].Play();
                return particleSystemObjects[i];
            }
        }
        var particleSystemObject = Instantiate(particleSystemPrefab, position, particleSystemPrefab.transform.rotation);
        var particles = particleSystemObject.GetComponent<ParticleSystem>();
        particleSystemObjects.Add(particleSystemObject);
        particleSystems.Add(particles);
        return particleSystemObject;
    }
}
