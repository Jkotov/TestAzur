using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footprint : MonoBehaviour
{
    [SerializeField] private float lifetime = 1f;
    private void OnEnable()
    {
        StartCoroutine(Lifetime());
    }

    private IEnumerator Lifetime()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
