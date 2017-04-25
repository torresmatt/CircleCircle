using UnityEngine;

namespace SparrowhawkGames.Scripts.Weapons.Guns
{
    public class BulletGun : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private Transform _turretTip;
        [SerializeField] private float _bulletsPerSecond;
        [SerializeField] private AudioClip[] _shootSounds;
        [SerializeField] [Range(0f, 1f)] private float _shotVolume;
        [SerializeField] private AudioSource _audioSource;

        private float _firingInterval;
        private float _lastTimeFired;

        private void Awake()
        {
            _firingInterval = 1f / _bulletsPerSecond;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0)) Fire();
        }

        private void Fire()
        {
            if (!CanFire()) return;
            _lastTimeFired = Time.realtimeSinceStartup;
            _audioSource.PlayOneShot(_shootSounds[Random.Range(0, _shootSounds.Length)], _shotVolume);
            Instantiate(_bulletPrefab, _turretTip.position, _turretTip.rotation);
        }

        private bool CanFire()
        {
            return Time.realtimeSinceStartup - _lastTimeFired > _firingInterval;
        }
    }
}