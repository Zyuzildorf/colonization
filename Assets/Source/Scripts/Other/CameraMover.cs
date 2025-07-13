using UnityEngine;

namespace Source.Scripts.Other
{
    [RequireComponent(typeof(InputReader))]
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private InputReader _inputReader;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
        }

        private void OnEnable()
        {
            _inputReader.MoveKeyPressed += Move;
        }

        private void OnDisable()
        {
            _inputReader.MoveKeyPressed -= Move;
        }

        private void Move(Vector2 input)
        {
            transform.Translate(input.x * _speed * Time.deltaTime, 0, input.y * _speed * Time.deltaTime);
        }
    }
}