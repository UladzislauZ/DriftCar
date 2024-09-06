using UnityEngine;

namespace DriftCar
{
    public class LevelController : ILevelController
    {
        private readonly Timer _timerController;
        private readonly IDriftController _driftController;
        private readonly IUIController _uiController;

        private Level _currentLevel;

        public LevelController(Timer timerController, 
                               IDriftController driftController, 
                               IUIController uiController)
        {
            _timerController = timerController;
            _driftController = driftController;
            _uiController = uiController;
        }

        public void StartLevel()
        {
            _timerController.TimerEnd += OnLevelEnd;
            _timerController.StartTimer(_currentLevel.Time);
            _driftController.ChangedScore += OnChangedScore;
        }

        public void UpdateLevel(Level level)
        {
            _currentLevel = level;
        }

        private void OnLevelEnd()
        {
            _timerController.TimerEnd -= OnLevelEnd;
            _driftController.ChangedScore -= OnChangedScore;
            Debug.Log("Level end");
            _uiController.ShowEndPopup(_driftController.Score.ToString());  
        }

        private void OnChangedScore(float score)
        {
            _uiController.UpdateDriftScore(score);
        }
    }
}