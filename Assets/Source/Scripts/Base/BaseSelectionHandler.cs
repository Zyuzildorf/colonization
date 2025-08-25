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
        private Base _selectedBase;
        
        private void CancelSelection() => _isBaseSelected = false;

        private void OnEnable()
        {
            _raycastHandler.BaseSelected += SelectBase;
            _raycastHandler.PreferToCancelSelection += CancelSelection;
            _raycastHandler.PreferToPlaceFlag += TryPlaceFlag;
        }

        private void OnDisable()
        {
            _raycastHandler.BaseSelected -= SelectBase;
            _raycastHandler.PreferToCancelSelection -= CancelSelection;
            _raycastHandler.PreferToPlaceFlag -= TryPlaceFlag;
        }

        private void SelectBase(Base selectedBase)
        {
            _selectedBase = selectedBase;
            _isBaseSelected = true;
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
            
            _selectedBase.GetFlagObject(_currentFlag.transform);
        }
    }
}