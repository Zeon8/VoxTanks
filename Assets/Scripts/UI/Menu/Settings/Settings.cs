using System.IO;
using UnityEngine;

namespace VoxTanks.UI.Menu
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private string _configFileName;

        public SettingsConfig Config { get; private set; }

        private string FullConfigPath => Path.Combine(Application.dataPath, _configFileName);

        private Resolution _defaultResolution;

        private void Start()
        {
            _defaultResolution = Screen.currentResolution;

            Config = LoadOrDefault();
            Apply();
        }

        private SettingsConfig LoadOrDefault()
        {
            var path = FullConfigPath;
            if (File.Exists(path))
                return Load(path);

            var random = new System.Random();
            var config = new SettingsConfig
            {
                PlayerName = "Player" + random.Next(9999),
                ScreenWidth = Screen.currentResolution.width,
                ScreenHeight = Screen.currentResolution.height,
                Fullscreen = Screen.fullScreen
            };
            File.WriteAllText(path, JsonUtility.ToJson(config));
            return config;
        }

        private SettingsConfig Load(string path)
        {
            string text = File.ReadAllText(path);
            return JsonUtility.FromJson<SettingsConfig>(text);
        }

        public void Save()
        {
            File.WriteAllText(FullConfigPath, JsonUtility.ToJson(Config));
        }

        public SettingsConfig GetDefaultConfig()
        {
            var random = new System.Random();
            return new SettingsConfig
            {
                PlayerName = "Player" + random.Next(9999),
                ScreenWidth = _defaultResolution.width,
                ScreenHeight = _defaultResolution.height,
                Fullscreen = Screen.fullScreen
            };
        }

        public void Apply()
        {
            PlayerSettings.PlayerName = Config.PlayerName;
            Screen.SetResolution(Config.ScreenWidth, Config.ScreenHeight, Config.Fullscreen);
        }    

    }
}
