using System.Collections;
using UnityEngine;

namespace SparrowhawkGames.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _maxSprintPool;
        [SerializeField] private float _sprintPoolDrainRate;
        [SerializeField] private float _sprintPoolChargeRate;
        [SerializeField] private float _accelerateFactor;
        [SerializeField] private float _decelerateFactor;

        private float _vertAxis;
        private float _horizAxis;
        private float _currentMoveSpeed;
        private float _currentSprintPool;
        private bool _sprinting;
        private Vector2 _moveDirection;

        public float CurrentSprintPool
        {
            get { return _currentSprintPool; }
            set { _currentSprintPool = Mathf.Clamp(value, 0f, _maxSprintPool); }
        }

        private void Awake()
        {
            _currentMoveSpeed = _movementSpeed;
            CurrentSprintPool = _maxSprintPool;
        }

        private void Update()
        {
            _vertAxis = Input.GetAxis("Vertical");
            _horizAxis = Input.GetAxis("Horizontal");
            if (!HasInput(_vertAxis) && !HasInput(_horizAxis)) return;

            float distance = _currentMoveSpeed * Time.deltaTime;

            if (SprintKeyDown())
            {
                _sprinting = true;
                if (CurrentSprintPool > 0)
                {
                    StartCoroutine(AccelerateToSprit());
                    CurrentSprintPool -= _sprintPoolDrainRate * Time.deltaTime;
                }
            }
            else
            {
                _sprinting = false;
                CurrentSprintPool += _sprintPoolChargeRate * Time.deltaTime;
                StartCoroutine(DecelerateToNormal());
            }

            _moveDirection = Vector2.zero;
            _moveDirection.x += _horizAxis * distance;
            _moveDirection.y += _vertAxis * distance;

            transform.Translate(_moveDirection);
        }

        private bool HasInput(float axis)
        {
            return (axis > 0 || axis < 0);
        }

        private bool SprintKeyDown()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }

        private IEnumerator AccelerateToSprit()
        {
            while (_currentMoveSpeed < _sprintSpeed && SprintKeyDown())
            {
                _currentMoveSpeed += _accelerateFactor;
                yield return null;
            }
            _currentMoveSpeed = _sprintSpeed;
        }

        private IEnumerator DecelerateToNormal()
        {
            while (_currentMoveSpeed > _movementSpeed)
            {
                _currentMoveSpeed -= _decelerateFactor;
                yield return null;
            }
            _currentMoveSpeed = _movementSpeed;
        }
    }
}