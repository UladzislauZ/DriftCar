using System;

namespace DriftCar
{
    public interface IUIController
    {
        event Action LevelEnd;
        event Action ShowAd;
        void UpdateDriftScore(float driftScore);
        void ShowEndPopup(string rewardValue);
    }
}