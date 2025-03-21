using SofiaTestTask.UI;
using SofiaTestTask.Utility;

namespace SofiaTestTask.Entities
{
    public class Player : IUpdatable
    {
        private readonly PlayerMovement _movement;
        private readonly PlayerInteractions _interactions;

        public Player(PlayerConfig config, PlayerView view, CameraFollow cameraFollow, InputManager inputManager, ControlsController controls)
        {
            _movement = new PlayerMovement(config, view, cameraFollow, inputManager, controls);
            _interactions = new PlayerInteractions(config, view, inputManager, cameraFollow.Camera, controls);
        }

        public void CustomUpdate()
        {
            _movement.CustomUpdate();
            _interactions.CustomUpdate();
        }

        public void CustomFixedUpdate()
        {
            _movement.CustomFixedUpdate();
        }

        public void CustomLateUpdate()
        {
            _movement.CustomLateUpdate();
        }
    }
}