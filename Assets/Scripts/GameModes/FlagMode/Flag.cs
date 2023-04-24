using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using VoxTanks.Tank;

public class Flag : NetworkBehaviour
{
    [SerializeField] private TankTeam _flagTeam;
    public bool Captured { get; set; }

    public TankTeam FlagTeam => _flagTeam;


    //[EditorButton(nameof(MakeCaptured),"Make captured",ButtonActivityType.OnPlayMode)]
    [SerializeField] private Transform _pointLocation;

    [ContextMenu("Make captured")]
    private void MakeCaptured() => Captured = true;
    
    
    public void ReturnFlagToPoint() => transform.position = _pointLocation.position;


}
