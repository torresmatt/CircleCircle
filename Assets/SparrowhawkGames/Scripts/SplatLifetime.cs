using UnityEngine;

namespace SparrowhawkGames.Scripts
{
    public class SplatLifetime : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void LateUpdate()
        {
            if (_particleSystem.isStopped) Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}