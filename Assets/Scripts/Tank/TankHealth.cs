using Unity.Netcode;
using UnityEngine;
using VoxTanks.Effects;
using VoxTanks.GameModes;
using VoxTanks.Tank.Spawners;
using VoxTanks.UI;

namespace VoxTanks.Tank
{
    public class TankHealth : NetworkBehaviour
    {
        public float Armor { get; set; } = 1f;

        public bool HasFullHealth => _health.Value == _maxHealth;

        [SerializeField] private NetworkVariable<float> _health = new NetworkVariable<float>(100f);

        [SerializeField] private float _maxHealth = 100f;

        [SerializeField] private NetworkObject _destroyTankEffect;

        private float effectDelay;

        [EditorButton(nameof(DestroyTank), "Destroy")]
        [EditorButton(nameof(DestroyTank), "Update Health Info")]
        
        [SerializeField] private TankUI _tankUI;

        private IStatusBar _statusUI;

        private KillTab _killTab;
        private TeamDeathmatchGameMode  _teamsDeathmatchMode;
        private TankSetup _tankSetup;

        private TankFlagCatcher _tankFlagCatcher;

        private TankSound _tankSound;
        private TankLife _tankLife;


        private void Start()
        {
            var autoDestroy = _destroyTankEffect.GetComponent<AutoDestroy>();
            effectDelay = autoDestroy.Duration;

            _health.Value = _maxHealth;
            _health.OnValueChanged += HealthChanged;
            _statusUI = GameObject.FindObjectOfType<StatusBar>();
            _tankSetup = GetComponent<TankSetup>();
            _tankLife = GetComponent<TankLife>();
            _killTab = FindObjectOfType<KillTab>();
            _teamsDeathmatchMode = NetworkManager.GetComponent<TeamDeathmatchGameMode>();
            _tankFlagCatcher = GetComponent<TankFlagCatcher>();
            _tankSound = GetComponent<TankSound>();
        }

        public void HealthChanged(float oldValue, float newValue)
        {
            if (IsLocalPlayer)
                _statusUI.Health = _health.Value / _maxHealth;
        }

        public void Heal(float health)
        {
            _health.Value += health;
            if (_health.Value > _maxHealth)
                _health.Value = _maxHealth;
        }


        public void TakeDamage(float damage,string attacker,TankTeam attackerTeam)
        {
            if(attackerTeam != TankTeam.None && attackerTeam == _tankSetup.Team)
                return;

            Debug.Log("Tank Health was: " + _health.Value);
            Debug.Log("Damaged with armor: " + (damage / Armor));
            _health.Value -= damage / Armor;
            Debug.Log("Tank health become: " + _health.Value);
            if (_health.Value <= 0)
            {
                _health.Value = _maxHealth;
                UpdateGameInfo(attacker, attackerTeam);
                DestroyTank();
            }
        }

        private void UpdateGameInfo(string attacker, TankTeam tankTeam)
        {
            _killTab.AddKillClientRpc(attacker, _tankSetup.Playername);
            if (tankTeam != TankTeam.None)
                _teamsDeathmatchMode?.AddTeamScore(tankTeam);
        }

#if UNITY_EDITOR
        [ContextMenu("Destroy tank")]
        private void DestroyTank()
        {
            SetTankEnabled(false);
            _tankSound.PlayTankExplodeServerRpc();
            SpawnEffect();
            _tankFlagCatcher?.LostFlag();
        }
#endif

        private void SetTankEnabled(bool state)
        {
            _tankLife.SetControllableClientRpc(state);
            _tankLife.SetVisibleClientRpc(state);
            _tankUI.SetUIVisibleClientRpc(state);
        }

        private void SpawnEffect()
        {
            NetworkObject effect = Instantiate(_destroyTankEffect, transform.position, Quaternion.identity);
            effect.Spawn();
            effect.GetComponent<AutoDestroy>().OnDestroyed += RespawnTank;
        }

        private void RespawnTank()
        {
            SetTankEnabled(true);
            _tankLife.RespawnTank();
        }
    }
}