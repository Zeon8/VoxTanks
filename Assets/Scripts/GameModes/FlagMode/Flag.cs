using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class Flag : NetworkBehaviour
{
    public bool WasCaptured { get; private set; }

    public TankTeam FlagTeam => _flagTeam;

    [SerializeField] private TankTeam _flagTeam;
    [SerializeField] private Transform _pointLocation;

    private Collider _collider;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }
   
    public void Capture(Transform tankTransform)
    {
        WasCaptured = true;
        _collider.enabled = false;
        transform.SetParent(tankTransform, true);
    }

    public void Return()
    {
        WasCaptured = false;
        transform.position = _pointLocation.position;
        Drop();
    }

    public void Drop()
    {
        _collider.enabled = true;
        if(IsServer)
            transform.SetParent(null, true);
    }
}
