using Unity.Netcode;
using UnityEngine;
using VoxTanks.GameModes.FlagMode;

namespace VoxTanks.Tank
{
    public class TankFlagCatcher : NetworkBehaviour
    {
        private TankSetup _tankSetup;
        private TeamFlagsMode _flagMode;
        [SerializeField] private Transform _flagPosition;
        [SerializeField] private float _lostDistance;

        private Flag _flag;

        private void Start()
        {
            _tankSetup = GetComponent<TankSetup>();
            _flagMode = NetworkManager.Singleton.GetComponent<TeamFlagsMode>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                LostFlag();
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (IsServer && other.TryGetComponent(out Flag flag))
            {
                if (flag.FlagTeam == _tankSetup.Team)
                {
                    if (flag.Captured)
                    {
                        _flagMode.OnFlagReturned(_tankSetup.Playername, flag.FlagTeam);
                        ReturnFlag(flag);
                    }
                    else if (_flag != null)
                    {
                        _flag.transform.SetParent(null);
                        _flagMode.OnFlagDelivered(_tankSetup.Playername,_tankSetup.Team, _flag.FlagTeam);
                        ReturnFlag(_flag);
                        _flag = null;
                    }
                    
                }
                else
                {
                    _flag = flag;
                    flag.Captured = true;
                    flag.GetComponent<Collider>().enabled = false;

                    _flagMode?.OnFlagCaptured(_tankSetup.Playername, flag.FlagTeam);
                    flag.transform.SetParent(transform, true);
                    PickupFlagClientRpc(flag.NetworkObject);
                }

            }
        }

        private void ReturnFlag(Flag flag)
        {
            flag.Captured = false;
            ReturnFlagClientRpc(flag.NetworkObject);
        }

        [ClientRpc]
        private void ReturnFlagClientRpc(NetworkObjectReference flagReference)
        {
            if (flagReference.TryGet(out NetworkObject flag))
            {
                flag.GetComponent<Collider>().enabled = true;
                flag.GetComponent<Flag>().ReturnFlagToPoint();
            }
        }

        [ClientRpc]
        private void PickupFlagClientRpc(NetworkObjectReference flagReference)
        {
            if (flagReference.TryGet(out NetworkObject flag))
            {
                flag.GetComponent<Collider>().enabled = false;
                var flagTransform = flag.transform;
                flagTransform.localPosition = _flagPosition.localPosition;
                flagTransform.localRotation = _flagPosition.localRotation;
            }
        }

        public void LostFlag()
        {
            if(_flag is null)
                return;
            
            _flagMode?.OnFlagLost(_tankSetup.Playername, _tankSetup.Team);
            
            _flag.transform.SetParent(null);
            LostFlagClientRpc(_flag.NetworkObject);
            _flag = null;
        }


        [ClientRpc]
        private void LostFlagClientRpc(NetworkObjectReference flagReference)
        {
            if (flagReference.TryGet(out NetworkObject flag))
            {
                flag.GetComponent<Collider>().enabled = true;
                var flagTransform = flag.transform;
                var position = flagTransform.position - flag.transform.forward * _lostDistance ;
                if(Physics.Raycast(position,Vector3.down,out RaycastHit hit))
                    flagTransform.position = hit.point;
            }
        }
    }
}
