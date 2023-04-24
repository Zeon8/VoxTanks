namespace VoxTanks.UI
{
    public interface IStatusBar
    {
        bool Visible { set; }
        float Health { set; }
        float Reload { set; }
    }
}