using TMPro;
using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankIdentifier : NetworkBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private TMP_Text _text;

        [SerializeField] private Color _blueTeamColor;
        [SerializeField] private Color _redTeamColor;
        [SerializeField] private Color _noneTeamColor;

        private void Start()
        {
            _canvas.SetActive(!IsLocalPlayer);
        }

        public void ApplySettings(TankSettings settings)
        {
            if (!IsLocalPlayer)
                Setup(settings);
        }

        private void Update()
        {
            if(!IsLocalPlayer && Camera.main != null)
                _canvas.transform.LookAt(Camera.main.transform);
        }

        private void Setup(TankSettings settings)
        {
            _text.text = settings.PlayerName;
            var color = settings.Team switch
            {
                TankTeam.Red => _redTeamColor,
                TankTeam.Blue => _blueTeamColor,
                _ => _noneTeamColor,
            };
            _text.color = color;
        }

        public void SetVisible(bool value)
        {
            if(!IsLocalPlayer)
                _canvas.SetActive(value);
        }
    }
}
