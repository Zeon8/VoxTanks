using VoxTanks.Tank.Spawners;

namespace VoxTanks.GameModes
{
    public abstract class TeamGameMode : GameMode
    {
        public override ITankRespawner Respawner => FindObjectOfType<TeamTankRespawner>();

        protected InfoTab InfoTab { get; private set; }

        public sealed override void Initialize()
        {
            InfoTab = FindAnyObjectByType<InfoTab>();
            InitializeTeamMode();
        }

        protected virtual void InitializeTeamMode() { }
    }
}
