using UnityEngine;

namespace DriftCar 
{
    public class StartsController : MonoBehaviour
    {
        [SerializeField]
        private UIView _uiView;

        [SerializeField]
        private FinishView _finishView;

        [SerializeField]
        private DriftView _driftView;

        [SerializeField]
        private CarController _carController;

        [SerializeField]
        private CameraController _cameraController;

        [SerializeField]
        private Player _player;

        [SerializeField]
        private Level _level;

        [SerializeField]
        private Timer _timer;

        private IUIController _uiController;
        private IDriftController _driftController;
        private ILevelController _levelController;

        private void Awake()
        {
            CreateControllers();
            _cameraController.SubscribePlayer(_player);
            _carController.LoadPlayer(_player);
            _levelController.UpdateLevel(_level);
            _levelController.StartLevel();
        }

        public void CreateControllers()
        {
            _uiController = new UIController(_uiView, _finishView);
            _driftController = new DriftController(_driftView);
            _levelController = new LevelController(_timer, _driftController, _uiController);
        }
    } 
}