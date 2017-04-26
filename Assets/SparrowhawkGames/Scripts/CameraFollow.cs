using UnityEngine;

namespace SparrowhawkGames.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] [Range(0, .5f)] private float _lerpDistance;

        void LateUpdate()
        {
            if (_player == null) return;

            Vector3 cameraPosition = transform.position;
            Vector3 playerPosition = _player.position;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            playerPosition.z = cameraPosition.z;
            mousePosition.z = cameraPosition.z;

            Vector3 newCameraPosition = Vector3.Lerp(playerPosition, mousePosition, _lerpDistance);

            transform.position = newCameraPosition;
        }
    }
}