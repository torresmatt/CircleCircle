using System.Linq;
using Assets.SparrowhawkGames.Scripts;
using UnityEngine;

namespace SparrowhawkGames.Scripts.Weapons.Bullets
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private GameObject _splatPrefab;
        [SerializeField] private GameObject _hitsoundPrefab;
        [SerializeField] private string[] _tagsToSplat;
        [SerializeField] private float _damage;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _lifetime;

        private float _birthTime;

        private void Awake()
        {
            _birthTime = Time.realtimeSinceStartup;
        }

        private void FixedUpdate()
        {
            float distance = _bulletSpeed * Time.deltaTime;

            Vector2 moveDirection = transform.right * distance;
            moveDirection = moveDirection.normalized;

            Vector2 newPosition = (Vector2) transform.position + moveDirection;

            RaycastHit2D[] hits = new RaycastHit2D[8];

            int numHits = _collider2D.Cast(moveDirection, hits, distance);

            int hitIndex = -1;

            if (numHits > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    if (!hits[i].collider.isTrigger)
                    {
                        hitIndex = i;
                        break;
                    }
                }
                if (hitIndex >= 0)
                {
                    newPosition = hits[hitIndex].centroid + hits[hitIndex].normal * .005f;
                    if (CanBeSplatted(hits[hitIndex].collider.gameObject)) Splat(hits[hitIndex].point);
                    Health otherHealth = hits[hitIndex].collider.gameObject.GetComponent<Health>();
                    if (otherHealth != null) otherHealth.CurrentHealth -= _damage;
                    Die();
                }
            }

            transform.position = newPosition;
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

        private void Splat(Vector2 position)
        {
            Instantiate(_splatPrefab, position, transform.rotation);
        }
    }
}