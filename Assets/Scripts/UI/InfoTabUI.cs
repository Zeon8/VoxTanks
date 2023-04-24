using TMPro;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes;

namespace VoxTanks.UI
{
    public class InfoTabUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _redTeamText;
        [SerializeField] private TMP_Text _blueTeamText;

        public int RedTeamScore
        {
            set => _redTeamText.text = value.ToString();
        }

        public int BlueTeamScore
        {
            set => _blueTeamText.text = value.ToString();
        }

        private void Start()
        {
            Debug.Log(NetworkManager.Singleton.GetComponent<TeamsGameMode>());
        }

    }
}
