using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CursorAnimation : MonoBehaviour
    {
        [SerializeField] private Transform rightAnchor;
        [SerializeField] private Transform leftAnchor;
        [SerializeField] private AnimationCurve curve;
        [SerializeField] private float speed;

        private void Update()
        {
            var scaledTime = Time.time * speed;
            var t = curve.Evaluate(scaledTime - (float)Math.Truncate(scaledTime));
            Vector3 newPos = transform.position;
            newPos.x = Mathf.Lerp(rightAnchor.transform.position.x, leftAnchor.transform.position.x, t);
            transform.position = newPos;
        }
    }
}