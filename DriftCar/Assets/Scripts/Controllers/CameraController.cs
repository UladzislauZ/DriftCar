using UnityEngine;

namespace DriftCar
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 Offset;
        public float speed;

        private Player _player;

        public void SubscribePlayer(Player player)
        {
            _player = player;
        }

        void FixedUpdate()
        {
            Vector3 playerForward = (_player.Rigidbody.velocity + _player.TransformCar.forward).normalized;
            transform.position = Vector3.Lerp(transform.position,
                _player.TransformCar.position + _player.TransformCar.TransformVector(Offset) + playerForward * (-5f),
                speed * Time.deltaTime);
            transform.LookAt(_player.TransformCar);
        }
    }
}
