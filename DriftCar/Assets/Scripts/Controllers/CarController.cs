using UnityEngine;

namespace DriftCar
{
    public class CarController : MonoBehaviour
    {
        public float gasInput;
        public float brakeInput;
        public float steeringInput;
        public float motorPower;
        public float brakePower;
        public float slipAngle;
        public AnimationCurve steeringCurve;

        private Player _player;
        private float speed;

        public void LoadPlayer(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            speed = _player.Rigidbody.velocity.magnitude;
            CheckInput();
            ApplyMotor();
            ApplySteering();
            ApplyBrake();
            ApplyWheelPositions();
        }

        private void CheckInput()
        {
            gasInput = Input.GetAxis("Vertical");
            steeringInput = Input.GetAxis("Horizontal");
            slipAngle = Vector3.Angle(transform.forward, _player.Rigidbody.velocity - transform.forward);
            float movingDirection = Vector3.Dot(transform.forward, _player.Rigidbody.velocity);
            if (movingDirection < -0.5f && gasInput > 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else if (movingDirection > 0.5f && gasInput < 0)
            {
                brakeInput = Mathf.Abs(gasInput);
            }
            else
            {
                brakeInput = 0;
            }
        }

        private void ApplyBrake()
        {
            _player.Colliders.FRWheel.brakeTorque = brakeInput * brakePower * 0.7f;
            _player.Colliders.FLWheel.brakeTorque = brakeInput * brakePower * 0.7f;
            _player.Colliders.BRWheel.brakeTorque = brakeInput * brakePower * 0.3f;
            _player.Colliders.BLWheel.brakeTorque = brakeInput * brakePower * 0.3f;
        }

        private void ApplyMotor()
        {
            _player.Colliders.BRWheel.motorTorque = motorPower * gasInput;
            _player.Colliders.BLWheel.motorTorque = motorPower * gasInput;
        }

        private void ApplySteering()
        {
            float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
            steeringAngle += Vector3.SignedAngle(transform.forward, _player.Rigidbody.velocity + transform.forward, Vector3.up);
            steeringAngle = Mathf.Clamp(steeringAngle, -60f, 60f);
            _player.Colliders.FRWheel.steerAngle = steeringAngle;
            _player.Colliders.FLWheel.steerAngle = steeringAngle;
        }

        private void ApplyWheelPositions()
        {
            UpdateWheel(_player.Colliders.FRWheel, _player.WheelMeshes.FRWheel);
            UpdateWheel(_player.Colliders.FLWheel, _player.WheelMeshes.FLWheel);
            UpdateWheel(_player.Colliders.BRWheel, _player.WheelMeshes.BRWheel);
            UpdateWheel(_player.Colliders.BLWheel, _player.WheelMeshes.BLWheel);
        }

        private void UpdateWheel(WheelCollider coll, MeshRenderer wheelMesh)
        {
            coll.GetWorldPose(out Vector3 position, out Quaternion quat);
            wheelMesh.transform.SetPositionAndRotation(position, quat);
        }
    }
}