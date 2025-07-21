using System;
using UnityEngine;

namespace Source.Scripts.Other
{
    public class InputReader : MonoBehaviour
    {
        private const string HorizontalAxis = "Horizontal";
        private const string VerticalAxis = "Vertical";
        private const KeyCode SprintButton = KeyCode.LeftShift;
        private const int LeftMouseButton = 0;
        private const int RightMouseButton = 1;

        private Vector2 _horizontalVerticalInput;

        public event Action<Vector2> MoveKeyPressed;
        public event Action SprintButtonPressedDown;
        public event Action SprintButtonPressedUp;
        public event Action LeftMouseButtonClicked;
        public event Action RightMouseButtonClicked;

        private void Update()
        {
            CheckMoveKeysInput();
            CheckLeftMouseButtonInput();
            CheckRightMouseButtonInput();
            CheckSprintButtonInput();
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

        private void CheckSprintButtonInput()
        {
            if (Input.GetKeyDown(SprintButton))
            {
                SprintButtonPressedDown?.Invoke();
            }
            else if (Input.GetKeyUp(SprintButton))
            {
                SprintButtonPressedUp?.Invoke();
            }
        }
        
        private void CheckLeftMouseButtonInput()
        {
            if (Input.GetMouseButtonDown(LeftMouseButton))
            {
                LeftMouseButtonClicked?.Invoke();
            }
        }
        
        private void CheckRightMouseButtonInput()
        {
            if (Input.GetMouseButtonDown(RightMouseButton))
            {
                RightMouseButtonClicked?.Invoke();
            }
        }
    }
}