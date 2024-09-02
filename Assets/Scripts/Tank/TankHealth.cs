using Unity.Netcode;
using UnityEngine;
using VoxTanks.UI;

namespace VoxTanks.Tank
{
    public class TankHealth : NetworkBehaviour
    {
        public float Armor { get; set; } = 1f;
        public bool HasFullHealth => _health.Value == _maxHealth;

        [SerializeField] private NetworkVariable<float> _health = new NetworkVariable<float>(100f);
        [SerializeField] private float _maxHealth = 100f;

        private TankDestroyer _tankDestroyer;
        private TankSetup _tankSetup;
        private StatusBar _statusUI;

        private void Start()
        {
            if (IsLocalPlayer)
            {
                _statusUI = FindObjectOfType<StatusBar>();
                _health.OnValueChanged += HealthChanged;
            }
            if (IsServer)
            {
                _tankSetup = GetComponent<TankSetup>();
                _tankDestroyer = GetComponent<TankDestroyer>();
                _health.Value = _maxHealth;
            }
        }

        public void HealthChanged(float oldValue, float newValue)
        {
            _statusUI.SetHealthProgress(newValue / _maxHealth);
        }

        public void Heal(float health)
        {
            _health.Value += health;
            if (_health.Value > _maxHealth)
                _health.Value = _maxHealth;
        }

        public void SetTankHealth(float health)
        {
            _maxHealth = health;
            _health.Value = health;
        }

        public void TakeDamage(float damage, string attackerName, TankTeam attackerTeam)
        {
            if(attackerTeam != TankTeam.None && attackerTeam == _tankSetup.Team)
                return;

            _health.Value -= damage / Armor;
            if (_health.Value <= 0)
            {
                _health.Value = _maxHealth;
                _tankDestroyer.Destroy(attackerName, attackerTeam);
            }
        }
    }
}