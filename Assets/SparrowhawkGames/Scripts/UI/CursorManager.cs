using UnityEngine;

namespace SparrowhawkGames.Scripts.UI
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private CursorLockMode _initialCursorState;
        [SerializeField] private bool _visibleOnStart;

        public static CursorLockMode CursorLockState
        {
            get { return Cursor.lockState; }
            set { Cursor.lockState = value; }
        }

        private void Awake()
        {
            Cursor.lockState = _initialCursorState;
            Cursor.visible = _visibleOnStart;
        }

        public static void HideCursor()
        {
            Cursor.visible = false;
        }

        public static void ShowCursor()
        {
            Cursor.visible = true;
        }

        public static void ToggleCursor()
        {
            Cursor.visible = !Cursor.visible;
        }
    }
}