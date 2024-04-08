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

        public void SetRedTeamScore(int score)
        {
            _redTeamText.text = score.ToString();
        }

        public void SetBlueTeamScore(int score)
        {
            _blueTeamText.text = score.ToString();
        }

    }
}
