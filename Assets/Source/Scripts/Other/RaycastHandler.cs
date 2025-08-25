using System;
using UnityEngine;
using Source.Scripts.Base;

namespace Source.Scripts.Other
{
    public class RaycastHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private LayerMask _baseLayer;
        [SerializeField] private InputReader _inputReader;

        public event Action PreferToCancelSelection;
        public event Action<Vector3> PreferToPlaceFlag;
        public event Action<Base.BotsBase> BaseSelected;
        
        private void OnEnable()
        {
            _inputReader.LeftMouseButtonClicked += ProcessLeftButton;
            _inputReader.RightMouseButtonClicked += OnRightButtonClicked;
        }

        private void OnDisable()
        {
            _inputReader.LeftMouseButtonClicked -= ProcessLeftButton;
            _inputReader.RightMouseButtonClicked -= OnRightButtonClicked;
        }

        private void OnRightButtonClicked()
        {
            PreferToCancelSelection?.Invoke();
        }

        private void ProcessLeftButton()
        {
            if (TryCastRaycast(_baseLayer, out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent(out Base.BotsBase selectedBase))
                {
                    PreferToCancelSelection?.Invoke();
                    BaseSelected?.Invoke(selectedBase);
                    Debug.Log("BotsBase selected");
                }
            }
            else if (TryCastRaycast(_groundLayer, out hit))
            {
                PreferToPlaceFlag?.Invoke(hit.point);
            }
        }
        
        private bool TryCastRaycast(LayerMask mask, out RaycastHit hit)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
        }
    }
}