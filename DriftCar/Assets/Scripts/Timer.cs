using System;
using System.Collections;
using UnityEngine;

namespace DriftCar
{
    public class Timer : MonoBehaviour
    {
        public event Action TimerEnd;

        public void StartTimer(int timeSeconds)
        {
            StartCoroutine(StartTimerCoroutine(timeSeconds));
        }

        public IEnumerator StartTimerCoroutine(int timeSeconds)
        {
            yield return new WaitForSeconds(timeSeconds);
            TimerEnd?.Invoke();
        }
    }
}