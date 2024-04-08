using System.Collections;
using TMPro;
using UnityEngine;

namespace Vox.UI.Menu
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _messageText;

        public void Setup(string title, string message)
        {
            _titleText.text = title;
            _messageText.text = message;
        }

        public void Close() => Destroy(gameObject);

    }
}