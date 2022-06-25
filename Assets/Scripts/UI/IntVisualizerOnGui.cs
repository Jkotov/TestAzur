using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class IntVisualizerOnGui : MonoBehaviour, IIntVisualizer
    {
        [SerializeField] private float UpdateSpeed = 0.05f;
        private TextMeshProUGUI textMeshProUGUI;
        private int targetInt;
        private int currentInt;

        public void UpdateInt(int value)
        {
            targetInt = value;
            StartCoroutine(SmoothTextUpdate());
        }
        
        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        private IEnumerator SmoothTextUpdate()
        {
            while (currentInt != targetInt)
            {
                currentInt += Math.Sign(targetInt - currentInt);
                textMeshProUGUI.text = currentInt.ToString();
                yield return new WaitForSeconds(UpdateSpeed);
            }
        }
    }
}