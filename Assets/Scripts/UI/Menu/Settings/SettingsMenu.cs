using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VoxTanks.UI.Menu
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _nameEdit;
        [SerializeField] private TMP_Dropdown _resolutionDropDown;
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private Settings _settings;

        private Resolution[] _resolutions;

        private void Start()
        {
            _resolutions = Screen.resolutions;
            AddResolutionsToDropDown();

            SettingsConfig config = _settings.Config;
            UpdateValues(config);
        }

        private void UpdateValues(SettingsConfig config)
        {
            _nameEdit.text = config.PlayerName;
            _fullScreenToggle.isOn = config.Fullscreen;
            _resolutionDropDown.value = GetResolutionIndex(config);
        }

        private void AddResolutionsToDropDown()
        {
            for (int i = 0; i < _resolutions.Length; i++)
            {
                Resolution resolution = _resolutions[i];
                var label = $"{resolution.width}x{resolution.height}";
                _resolutionDropDown.options.Add(new TMP_Dropdown.OptionData(label));
            }
        }

        private int GetResolutionIndex(SettingsConfig config)
        {
            for (int i = 0; i < _resolutions.Length; i++)
            {
                Resolution resolution = _resolutions[i];
                if (resolution.height == config.ScreenHeight
                    && resolution.width == config.ScreenWidth)
                {
                    return i;
                }
            }
            throw new InvalidOperationException();
        }

        public void ResetSettings()
        {
            SettingsConfig config = _settings.GetDefaultConfig();
            UpdateValues(config);
        }

        public void SaveSettings()
        {
            int index = _resolutionDropDown.value;
            Resolution resolution = _resolutions[index];
            _settings.Config.PlayerName = _nameEdit.text;
            _settings.Config.Fullscreen = _fullScreenToggle.isOn;
            _settings.Config.ScreenHeight = resolution.height;
            _settings.Config.ScreenWidth = resolution.width;
            _settings.Save();
            _settings.Apply();
        }
    }
}