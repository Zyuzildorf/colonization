using UnityEngine;

namespace Source.Scripts.Other
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private InputReader _inputReader;

        private float _currentSpeed;
        
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _currentSpeed = _speed;
        }

        private void OnEnable()
        {
            _inputReader.MoveKeyPressed += Move;
            _inputReader.SprintButtonPressedDown += SetSprintSpeed;
            _inputReader.SprintButtonPressedUp += SetStartSpeed;
        }

        private void OnDisable()
        {
            _inputReader.SprintButtonPressedDown -= SetSprintSpeed;
            _inputReader.SprintButtonPressedUp -= SetStartSpeed;
            _inputReader.MoveKeyPressed -= Move;
        }

        private void Move(Vector2 input)
        {
            transform.Translate(input.x * _currentSpeed * Time.deltaTime, 0, input.y * _currentSpeed * Time.deltaTime);
        }

        private void SetSprintSpeed()
        {
            _currentSpeed = _sprintSpeed;
        }

        private void SetStartSpeed()
        {
            _currentSpeed = _speed;
        }
    }
}