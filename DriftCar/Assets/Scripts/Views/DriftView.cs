using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace DriftCar
{
    public class DriftView : MonoBehaviour
    {
        public Rigidbody playerRB;

        public float minSpeed = 10;
        public float minAngle = 10;
        public float driftingDelay = 0.2f;

        private float _speed = 0;
        private float _driftAngle = 0;
        private float _driftFactor = 1;
        private float _currentScore = 0;
        private bool isDrifting = false;
        private IEnumerator stopDriftingCoroutine = null;

        public float CurrentScore => _currentScore;
        public event Action<float> ScoreUpdated;

        private void Update()
        {
            ManageDrift();
        }

        private void ManageDrift()
        {
            _speed = playerRB.velocity.magnitude;
            _driftAngle = Vector3.Angle(playerRB.transform.forward, (playerRB.velocity + playerRB.transform.forward).normalized);
            if (_driftAngle > 120)
            {
                _driftAngle = 0;
            }

            if (_driftAngle >= minAngle && _speed > minSpeed)
            {
                if (!isDrifting || stopDriftingCoroutine != null)
                {
                    StartDrift();
                }
            }
            else
            {
                if (isDrifting && stopDriftingCoroutine == null)
                {
                    StopDrift();
                }
            }

            if (isDrifting)
            {
                _currentScore += Time.deltaTime * _driftAngle * _driftFactor;
                _driftFactor += Time.deltaTime;
                ScoreUpdated?.Invoke(_currentScore);
            }
        }

        private async void StartDrift()
        {
            if (!isDrifting)
            {
                await Task.Delay(Mathf.RoundToInt(1000 * driftingDelay));
                _driftFactor = 1;
            }

            if (stopDriftingCoroutine != null)
            {
                StopCoroutine(stopDriftingCoroutine);
                stopDriftingCoroutine = null;
            }

            isDrifting = true;
        }

        private void StopDrift()
        {
            stopDriftingCoroutine = StoppingDrift();
            StartCoroutine(stopDriftingCoroutine);
        }

        private IEnumerator StoppingDrift()
        {
            yield return new WaitForSeconds(driftingDelay * 4f);
            isDrifting = false;
        }
    }
}