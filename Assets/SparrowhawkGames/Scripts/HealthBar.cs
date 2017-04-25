using UnityEngine;

namespace SparrowhawkGames.Scripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Transform _healthBarTransform;
        [SerializeField] private Health _health;

        void Awake()
        {
            _health.HealthChanged += UpdateHealthBar;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= UpdateHealthBar;
        }

        void UpdateHealthBar(float newPct)
        {
            Vector3 newScale = _healthBarTransform.localScale;
            newScale.x = newPct;
            _healthBarTransform.localScale = newScale;
        }
    }
}