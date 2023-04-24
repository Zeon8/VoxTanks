using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;
using VoxTanks.UI;

namespace VoxTanks.GameModes
{
    public class InfoTab : NetworkBehaviour
    {
        private NetworkVariable<int> _blueTeamScore = new NetworkVariable<int>();
        private NetworkVariable<int> _redTeamScore = new NetworkVariable<int>();

        [SerializeField] private TMP_Text _blueTeamScoreText;
        [SerializeField] private TMP_Text _redTeamScoreText;

        private void Start()
        {
            if(!NetworkManager.Singleton.GetComponent<TeamsGameMode>())
                gameObject.SetActive(false);
            UpdateScore();
            _blueTeamScore.OnValueChanged += (oldValue,newValue) => _blueTeamScoreText.text = newValue.ToString();
            _redTeamScore.OnValueChanged += (oldValue, newValue) => _redTeamScoreText.text = newValue.ToString();
        }

        public void UpdateScore()
        {
            _blueTeamScoreText.text = _blueTeamScore.Value.ToString();
            _redTeamScoreText.text = _redTeamScore.Value.ToString();
        }

        public void AddTeamScore(TankTeam team)
        {
            if(team == TankTeam.Blue)
                _blueTeamScore.Value++;
            else
                _redTeamScore.Value++;
        }
    }
}
