using System;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class ResourcesCounterView : MonoBehaviour
    {
        [SerializeField] private ResourcesCounter _counter;
        [SerializeField] private TextMeshProUGUI _scoreCounterText;

        private void Start()
        {
            _scoreCounterText.text = Convert.ToString(_counter.ResourcesCount);
        }
    
        private void OnEnable()
        {
            _counter.ResourcesValueChanged += UpdateCounter;
        }

        private void OnDisable()
        {
            _counter.ResourcesValueChanged -= UpdateCounter;
        }

        private void UpdateCounter(int resourcesValue)
        {
            _scoreCounterText.text = Convert.ToString(_counter.ResourcesCount);
        }
    }
}
