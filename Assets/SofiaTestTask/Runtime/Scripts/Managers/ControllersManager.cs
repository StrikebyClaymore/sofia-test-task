using System;
using System.Collections.Generic;
using SofiaTestTask.UI;

namespace SofiaTestTask
{
    public class ControllersManager
    {
        private readonly Dictionary<Type, IController> _controllers = new();
        
        public ControllersManager(ControlsController controls)
        {
            _controllers.Add(controls.GetType(), controls);
        }
        
        public T GetController<T>() where T : class
        {
            foreach (var controller in _controllers.Values)
            {
                if (controller is T tController)
                {
                    return tController;
                }
            }
            return null;
        }
        
        public bool ShowController(Type type)
        {
            if (_controllers.TryGetValue(type, out var controller))
            {
                controller.Show();
                return true;
            }
            return false;
        }
        
        public bool HideController(Type type)
        {
            if (_controllers.TryGetValue(type, out var controller))
            {
                controller.Hide();
                return true;
            }
            return false;
        }
    }
}