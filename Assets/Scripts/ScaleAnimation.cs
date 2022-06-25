using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 maxScale = Vector3.one;
    [SerializeField] private float duration = 1f;
    private float time;
    private Vector3 startScale;

    private void Awake()
    {
        startScale = transform.localScale;
    }

    private void Update()
    {
        time += Time.deltaTime;
        Vector3 currentScale;
        var t = time / duration;
        currentScale.x = Mathf.InverseLerp(startScale.x, maxScale.x, t);
        currentScale.y = Mathf.InverseLerp(startScale.y, maxScale.y, t);
        currentScale.z = Mathf.InverseLerp(startScale.z, maxScale.z, t);
        transform.localScale = currentScale;
    }
}
