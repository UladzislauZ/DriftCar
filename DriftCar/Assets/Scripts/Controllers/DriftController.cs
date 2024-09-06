using System;

namespace DriftCar
{
    public class DriftController : IDriftController
    {
        private DriftView _driftView;

        public event Action<float> ChangedScore;

        public float Score => _driftView.CurrentScore;

        public DriftController(DriftView driftView)
        {
            _driftView = driftView;
            _driftView.ScoreUpdated += OnChangedScore;
        }

        private void OnChangedScore(float score)
        {
            ChangedScore?.Invoke(score);
        }
    }
}