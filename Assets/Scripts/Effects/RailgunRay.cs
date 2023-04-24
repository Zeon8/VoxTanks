using Unity.Netcode;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Effects {
    public class RailgunRay : NetworkBehaviour
    {
        [SerializeField] private LineRenderer _lineRender;
        [SerializeField] private float _timeToDespawn;
        private float _time;

        private Material _material;

        public override void OnNetworkSpawn()
        {
            _material = _lineRender.material;
            
            if(!IsServer)
                enabled = false;
        }

        public void Setup(Vector3 start, Vector3 end)
        {
            NetworkObject.Spawn();
            SetPositionsClientRpc(start, end);
        }

        [ClientRpc]
        private void SetPositionsClientRpc(Vector3 start, Vector3 end)
        {
            _lineRender.SetPosition(0, start);
            _lineRender.SetPosition(1, end);
        }

        private void Update()
        {

            if(_time < _timeToDespawn)
            {
                _time += Time.deltaTime;
                SetOpacityClientRpc((_timeToDespawn-_time)/_timeToDespawn);
            }
            else
                NetworkObject.Despawn(true);

        }

        [ClientRpc]
        private void SetOpacityClientRpc(float opacity)
        {
            //Debug.Log(opacity);
            var color = _material.color;
            color.a = opacity;
            _material.color = color;
        }


    }
}