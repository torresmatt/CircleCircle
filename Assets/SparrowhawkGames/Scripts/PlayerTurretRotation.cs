using UnityEngine;

namespace SparrowhawkGames.Scripts
{
    public class PlayerTurretRotation : MonoBehaviour
    {
        [SerializeField] private Transform _turretRoot;

        private void Update()
        {
            Vector3 turretPosition = Camera.main.WorldToScreenPoint(_turretRoot.position);
            Vector3 dirToMouse = Input.mousePosition - turretPosition;
            float goalAngle = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg;
            _turretRoot.rotation = Quaternion.AngleAxis(goalAngle, Vector3.forward);
        }
    }
}