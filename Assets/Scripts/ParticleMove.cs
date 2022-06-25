using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMove : MonoBehaviour
{
    public Transform destination;
    [SerializeField] private float speed;
    
    private void Update()
    {
        var dir = destination.position - transform.position;
        transform.position += speed * Time.deltaTime * dir;
    }
}
