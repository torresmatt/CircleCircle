using UnityEngine;

namespace SparrowhawkGames.Scripts
{
    public class EnemyAimer : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _turretRoot;
        [SerializeField] private Transform _turretTip;
        [SerializeField] private float _bulletsPerSecond;
        [SerializeField] private string _targetTag;
        [SerializeField] private AudioClip[] _shootSounds;
        [SerializeField] private AudioSource _audioSource;

        private Transform _target;

        private float _firingInterval;
        private float _lastTimeFired;

        private void Awake()
        {
            _target = GameObject.FindGameObjectWithTag(_targetTag).transform;
            _firingInterval = 1f / _bulletsPerSecond;
        }

        void Update()
        {
            if (_target == null) return;

            Vector3 turretPosition = Camera.main.WorldToScreenPoint(_turretRoot.position);
            Vector3 targetPosition = Camera.main.WorldToScreenPoint(_target.position);
            Vector3 dirToTarget = targetPosition - turretPosition;
            float goalAngle = Mathf.Atan2(dirToTarget.y, dirToTarget.x) * Mathf.Rad2Deg;
            _turretRoot.rotation = Quaternion.AngleAxis(goalAngle, Vector3.forward);
            Fire();
        }

        private void Fire()
        {
            if (!CanFire() || (!CanSeeTarget())) return;
            _lastTimeFired = Time.realtimeSinceStartup;
            _audioSource.PlayOneShot(_shootSounds[Random.Range(0, _shootSounds.Length)]);
            Instantiate(_bulletPrefab, _turretTip.position, _turretTip.rotation);
        }

        private bool CanFire()
        {
            return Time.realtimeSinceStartup - _lastTimeFired > _firingInterval;
        }

        private bool CanSeeTarget()
        {
            bool result = false;

            RaycastHit2D hit = Physics2D.Raycast(_turretTip.position, _turretTip.right, Mathf.Infinity);
            if (hit.collider != null)
            {
                result = (hit.collider.gameObject.CompareTag("Player"));
            }

            return result;
        }
    }
}