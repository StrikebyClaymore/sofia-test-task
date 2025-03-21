using UnityEngine;
using UnityEngine.UI;

namespace SofiaTestTask.UI
{
    public class ControlsView : BaseView
    {
        [field: SerializeField] public RectTransform OuterImage { get; private set; }
        [field: SerializeField] public RectTransform InnerImage { get; private set; }
        [field: SerializeField] public Joystick Joystick { get; private set; }
        [field: SerializeField] public Button DropButton { get; private set; }

        private void Awake()
        {
            Hide();
            DropButton.gameObject.SetActive(false);
        }
    }
}