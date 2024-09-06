using System;
using UnityEngine;

namespace DriftCar
{
    public class UIController : IUIController
    {
        private readonly UIView _uiView;
        private readonly FinishView _finishView;

        public event Action LevelEnd;
        public event Action ShowAd;

        public UIController(UIView uiView, FinishView finishView)
        {
            _uiView = uiView;
            _finishView = finishView;
        }

        public void UpdateDriftScore(float driftScore)
        {
            _uiView.DriftScore.text = driftScore.ToString();
        }

        public void ShowEndPopup(string rewardValue)
        {
            _finishView.RewardValue.text = rewardValue;
            _finishView.CompleteBtn.onClick.AddListener(Complete);
            _finishView.ISBtn.onClick.AddListener(ClickIS);
            _finishView.gameObject.SetActive(true);
        }

        private void Complete()
        {
            _finishView.CompleteBtn.onClick.RemoveListener(Complete);
            LevelEnd?.Invoke();
        }

        private void ClickIS()
        {
            _finishView.ISBtn.onClick.RemoveListener(ClickIS);
            ShowAd?.Invoke();
        }
    }
}