using Unity.Netcode;
using UnityEngine;

namespace VoxTanks.Tank
{
    public class TankCameraControl : NetworkBehaviour
    {

        public Transform Camera;

        [SerializeField] private float _sensivityX;
        [SerializeField] private float _sensivityY;


        public void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * _sensivityX;
            float mouseY = -Input.GetAxis("Mouse Y") * _sensivityY;

            Camera.Rotate(mouseY, mouseX, 0);
            var rotation = Camera.localEulerAngles;
            rotation.z = 0;
            rotation.x = Clamp(rotation.x);
            Camera.localEulerAngles = rotation;
        }

        private float Clamp(float angle)
        {
            if (angle > 180)
            {
                angle -= 360;
            }
            return Mathf.Clamp(angle, -30, 60);
        }


    }
}