using SofiaTestTask.UI;
using Zenject;

namespace SofiaTestTask
{
    public class GameManager : IInitializable
    {
        private readonly ControllersManager _controllers;
        
        public GameManager(ControllersManager controllers)
        {
            _controllers = controllers;
        }
        
        public void Initialize()
        {
            _controllers.ShowController(typeof(ControlsController));
        }
    }
}