using Unity.Netcode;
using UnityEngine;

namespace Effects
{
    public class RailgunRay : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRender;
        [SerializeField] private float _timeToDespawn;
        private float _time;

        private Material _material;

        private void Start()
        {
            _time = _timeToDespawn;
            _material = _lineRender.material;
        }

        public void Setup(Vector3 start, Vector3 end)
        {
            _lineRender.SetPosition(0, start);
            _lineRender.SetPosition(1, end);
        }

        private void Update()
        {
            if (_time > 0)
            {
                _time -= Time.deltaTime;
                SetOpacity(_time / _timeToDespawn);
            }
            else
                Destroy(gameObject);
        }

        private void SetOpacity(float opacity)
        {
            Color color = _material.color;
            color.a = opacity;
            _material.color = color;
        }


    }
}