using System.Linq;
using UnityEngine;

namespace SparrowhawkGames.Scripts.Weapons.Bullets
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private GameObject _splatPrefab;
        [SerializeField] private GameObject _hitsoundPrefab;
        [SerializeField] private string[] _tagsToSplat;
        [SerializeField] private float _damage;
        [SerializeField] private Transform _bulletTip;
        [SerializeField] private float _rayCastDistance;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _lifetime;

        private float _birthTime;

        private void Awake()
        {
            _birthTime = Time.realtimeSinceStartup;
        }

        private void FixedUpdate()
        {
            RaycastHit2D hit = Physics2D.Raycast(_bulletTip.position, Vector2.right * _rayCastDistance);
            if (hit.collider != null)
            {
                if (CanBeSplatted(hit.collider.gameObject)) Splat();
                Health otherHealth = hit.collider.gameObject.GetComponent<Health>();
                if (otherHealth != null) otherHealth.CurrentHealth -= _damage;
                Instantiate(_hitsoundPrefab, transform.position, Quaternion.identity);
                Die();
            }
        }

        private void Update()
        {
            transform.Translate(transform.right * _bulletSpeed * Time.deltaTime, Space.World);

            if (Time.realtimeSinceStartup - _birthTime > _lifetime) Destroy(gameObject);
        }

        private void OnBecameInvisible()
        {
            Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }

        private bool CanBeSplatted(GameObject other)
        {
            return _tagsToSplat.Contains(other.tag);
        }

        private void Splat()
        {
            Instantiate(_splatPrefab, _bulletTip.position, _bulletTip.rotation);
        }
    }
}
