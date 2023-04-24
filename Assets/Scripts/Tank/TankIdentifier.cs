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
        private TankSetup _tankSetup;

        private void Start()
        {
            _tankSetup = GetComponent<TankSetup>();
            if(IsLocalPlayer)
                Destroy(_canvas);
            else
                Setup();
        }

        [ClientRpc]
        public void SetupClientRpc()
        {
            if(!IsLocalPlayer)
                Setup();
        }

        private void Setup()
        {
            if (_text != null)
                return;

            _text.text = _tankSetup.Playername;

            Color color = _noneTeamColor;

            switch (_tankSetup.Team)
            {
                case TankTeam.Red: color = _redTeamColor; break;
                case TankTeam.Blue: color = _blueTeamColor; break;
            }

            _text.color = color;
        }
    }
}
