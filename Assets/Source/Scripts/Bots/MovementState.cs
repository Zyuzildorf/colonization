using UnityEngine;

namespace Source.Scripts.Bots
{
    public abstract class MovementState : CollectorState
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;

        protected virtual void Move(Vector3 target)
        {
            
            transform.position = Vector3.MoveTowards(transform.position,
               target, _moveSpeed * Time.deltaTime);
        }

        protected virtual void Rotate(Vector3 target)
        {
            Vector3 direction = transform.position - target;
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }
}