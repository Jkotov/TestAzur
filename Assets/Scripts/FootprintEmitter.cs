using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootprintEmitter : MonoBehaviour
{
    [SerializeField] private GameObject footprintPrefab;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float checkRadius = 0.01f;
    [SerializeField] private float createFootprintCooldown = 1f;
    private readonly List<GameObject> footprintsPool = new List<GameObject>();
    private float timeAfterFootprintCreated;

    private void Update()
    {
        timeAfterFootprintCreated += Time.deltaTime;
        if (timeAfterFootprintCreated < createFootprintCooldown)
            return;
        if (Physics.CheckSphere(transform.position, checkRadius, groundMask))
        {
            CreateFootprint(transform.position + Vector3.up * checkRadius);
            timeAfterFootprintCreated = 0;
        }
    }

    private void CreateFootprint(Vector3 position)
    {
        foreach (var footprint in footprintsPool)
        {
            if (!footprint.activeInHierarchy)
            {
                footprint.SetActive(true);
                footprint.transform.position = position;
                footprint.transform.rotation = Quaternion.identity;
                return;
            }
        }
        footprintsPool.Add(Instantiate(footprintPrefab, position, Quaternion.identity));
    }
}
