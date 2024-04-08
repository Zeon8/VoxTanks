using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

public class Flag : NetworkBehaviour
{
    public bool Captured { get; set; }

    public TankTeam FlagTeam => _flagTeam;

    [SerializeField] private TankTeam _flagTeam;
    [SerializeField] private Transform _pointLocation;

    public void ReturnFlagToPoint() => transform.position = _pointLocation.position;

    [ContextMenu("Make captured")]
    private void MakeCaptured() => Captured = true;
   


}
