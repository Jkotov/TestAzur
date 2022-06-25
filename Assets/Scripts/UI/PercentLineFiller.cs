using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class PercentLineFiller : MonoBehaviour, IPercentVisualizer
    {
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void UpdatePercent(float percent)
        {
            image.fillAmount = percent;
        }
    }
}