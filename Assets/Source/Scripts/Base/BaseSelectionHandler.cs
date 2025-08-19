using System;
using Source.Scripts.Bots;
using Source.Scripts.Other;
using UnityEngine;

namespace Source.Scripts.Base
{
    public class BaseSelectionHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _flagPrefab;
        [SerializeField] private RaycastHandler _raycastHandler;

        private GameObject _currentFlag;
        private bool _isBaseSelected;

        public Tasks CurrentTask { get; private set; }
        public void SelectBase() => _isBaseSelected = true;
        public void CancelSelection() => _isBaseSelected = false;

        private void Awake()
        {
            CurrentTask = Tasks.CollectResources;
        }

        private void OnEnable()
        {
            _raycastHandler.PreferToCancelSelection += CancelSelection;
            _raycastHandler.PreferToPlaceFlag += TryPlaceFlag;
        }

        private void OnDisable()
        {
            _raycastHandler.PreferToCancelSelection -= CancelSelection;
            _raycastHandler.PreferToPlaceFlag -= TryPlaceFlag;
        }

        private void TryPlaceFlag(Vector3 flagPosition)
        {
            if (_isBaseSelected)
            {
                PlaceFlag(flagPosition);
            }
        }
        
        private void PlaceFlag(Vector3 flagPosition)
        {
            if (_currentFlag != null)
            {
                _currentFlag.transform.position = flagPosition;
            }
            else
            {
                _currentFlag = Instantiate(_flagPrefab, flagPosition, Quaternion.identity);
            }

            CurrentTask = Tasks.BuildBase;
        }
    }
}