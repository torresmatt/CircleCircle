using System;
using UnityEngine;

namespace SparrowhawkGames.Scripts
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _startingHealth;

        private float _currentHealth;

        public event Action<float> HealthChanged;

        private void Awake()
        {
            CurrentHealth = _startingHealth;
        }

        private void OnDestroy()
        {
            HealthChanged = null;
        }

        public float CurrentHealth
        {
            get { return _currentHealth; }
            set
            {
                _currentHealth = Mathf.Clamp(value, 0f, _maxHealth);
                if (HealthChanged != null) HealthChanged.Invoke(HealthPct);
                if (CurrentHealth <= 0.0f) Die();
            }
        }

        public float MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        public float HealthPct
        {
            get { return CurrentHealth / MaxHealth; }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}