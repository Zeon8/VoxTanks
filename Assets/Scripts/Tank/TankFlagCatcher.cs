using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes.FlagMode;
using VoxTanks.Game;
using System;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

namespace VoxTanks.Tank
{
    public class TankFlagCatcher : NetworkBehaviour
    {
        [SerializeField] private Transform _flagPosition;
        [SerializeField] private float _lostDistance;

        private TankSetup _tankSetup;
        private CatchFlagMode _flagMode;
        private Flag _flag;

        private void Start()
        {
            _flagMode = FindObjectOfType<GameSetup>().CurrentGameMode as CatchFlagMode;
            if (!IsServer || _flagMode == null)
            {
                return;
            }

            _tankSetup = GetComponent<TankSetup>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F) && IsLocalPlayer)
                DropFlagServerRpc();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsServer && other.TryGetComponent(out Flag flag))
            {
                if (flag.FlagTeam == _tankSetup.Team)
                {
                    if (flag.WasCaptured)
                    {
                        OnFlagReturnedClientRpc(_tankSetup.PlayerName, flag.FlagTeam);
                        ReturnFlagClientRpc(flag.NetworkObject);
                    }
                    else if (_flag != null)
                    {
                        OnFlagDeliveredClientRpc(_tankSetup.PlayerName, _tankSetup.Team, _flag.FlagTeam);
                        ReturnFlagClientRpc(_flag.NetworkObject);
                        _flag = null;
                    }
                }
                else
                {
                    flag.Capture(transform);
                    OnFlagCapturedClientRpc(_tankSetup.PlayerName, flag.FlagTeam);
                    CatchFlagClientRpc(flag.NetworkObject);
                    _flag = flag;
                }

            }
        }

        [ClientRpc]
        private void ReturnFlagClientRpc(NetworkObjectReference flagReference)
        {
            if (flagReference.TryGet(out NetworkObject flag))
            {
                flag.GetComponent<Flag>().Return();
            }
        }

        [ClientRpc]
        private void CatchFlagClientRpc(NetworkObjectReference flagReference)
        {
            if (flagReference.TryGet(out NetworkObject flag))
            {
                flag.transform.SetLocalPositionAndRotation(_flagPosition.localPosition, _flagPosition.localRotation);
            }
        }

        [ServerRpc]
        private void DropFlagServerRpc() => DropFlag();

        public void DropFlag()
        {
            if(_flag == null)
                return;

            GroundFlagClientRpc(_flag.NetworkObject);
            OnFlagLostClientRpc(_tankSetup.PlayerName, _flag.FlagTeam);
            _flag.Drop();

            _flag = null;
        }

        [ClientRpc]
        private void GroundFlagClientRpc(NetworkObjectReference flagReference)
        {
            if (flagReference.TryGet(out NetworkObject flag))
            {
                var flagTransform = flag.transform;
                var position = flagTransform.position - flag.transform.forward * _lostDistance ;
                if (Physics.Raycast(position, Vector3.down * float.MaxValue, out RaycastHit hit)) 
                    flagTransform.position = hit.point;
            }
        }

        [ClientRpc]
        private void OnFlagReturnedClientRpc(string name, TankTeam team) => _flagMode.OnFlagReturned(name, team);

        [ClientRpc]
        private void OnFlagDeliveredClientRpc(string playerName, TankTeam team, TankTeam flagTeam)
        {
            _flagMode.OnFlagDelivered(playerName, team, flagTeam);
        }

        [ClientRpc]
        private void OnFlagCapturedClientRpc(string playerName, TankTeam flagTeam)
        {
            _flagMode.OnFlagCaptured(playerName, flagTeam);
        }

        [ClientRpc]
        private void OnFlagLostClientRpc(string playerName, TankTeam team)
        {
            _flagMode.OnFlagLost(playerName, team);
        }
    }
}
