using SofiaTestTask.UI;
using UnityEngine;

namespace SofiaTestTask
{   
    public class ViewsContainer : MonoBehaviour
    {
        [field: SerializeField] public Canvas RootCanvas { get; private set; }
        [field: SerializeField] public ControlsView ControlsView { get; private set; }
    }
}