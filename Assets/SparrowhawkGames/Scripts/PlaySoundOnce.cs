using UnityEngine;

namespace SparrowhawkGames.Scripts
{
    public class PlaySoundOnce : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _hitSounds;
        [SerializeField] private AudioSource _audioSource;

        private AudioClip _cliptoPlay;

        void Awake()
        {
            _cliptoPlay = _hitSounds[Random.Range(0, _hitSounds.Length)];
            _audioSource.PlayOneShot(_cliptoPlay);
            Invoke("Die", _cliptoPlay.length);
        }

        void Die()
        {
            Destroy(gameObject);
        }
    }
}