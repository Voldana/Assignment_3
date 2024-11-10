using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private Image crosshairImage;

        private void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void Update()
        {
            FollowMouse();
        }

        private void FollowMouse()
        {
            if (!crosshairImage) return;
            Vector2 mousePosition = Input.mousePosition;
            crosshairImage.rectTransform.position = mousePosition;
        }
    }
}
