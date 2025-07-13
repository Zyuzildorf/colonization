using System;
using UnityEngine;

namespace Source.Scripts.Other
{
    public class InputReader : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";

        private Vector2 _horizontalVerticalInput;
        
        public event Action<Vector2> MoveKeyPressed;

        private void Update()
        {
            CheckMoveKeysInput();
        }

        private void CheckMoveKeysInput()
        {
            if (Input.GetAxis(HorizontalAxis) != 0 || Input.GetAxis(VerticalAxis) != 0)
            {
                _horizontalVerticalInput.x = Input.GetAxis(HorizontalAxis);
                _horizontalVerticalInput.y = Input.GetAxis(VerticalAxis);
                
                _horizontalVerticalInput.Normalize();
                
                MoveKeyPressed?.Invoke(_horizontalVerticalInput);
            }   
        }
    }
}