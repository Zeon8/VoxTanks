namespace VoxTanks.UI
{
    public interface IStatusBar
    {
        public void SetVisible(bool value);
        public void SetHealthProgress(float value);
        public void SetReloadProgress(float value);
    }
}