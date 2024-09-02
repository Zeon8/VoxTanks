using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VoxTanks.UI
{
    public class KillTabItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        [Multiline]
        [SerializeField] private string _textTemplate;

        [Tooltip("Time to stay on screen before disapear")]
        [SerializeField] private float _stayOnScreenTime;

        public void Setup(string attacker, string vicitim)
        {
            _text.text = string.Format(_textTemplate, attacker, vicitim);
            Destroy(gameObject, _stayOnScreenTime);
        }

    }
}
