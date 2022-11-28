using UnityEngine;

namespace MokomoGames.Loading
{
    public class LoadingIcon : MonoBehaviour
    {
        [SerializeField] private RectTransform pivot;
        [SerializeField] private float rotateInterval;
        private float _currentRotateTimer;

        private void Awake()
        {
            _currentRotateTimer = 0.0f;
        }

        public void Tick(float dt)
        {
            _currentRotateTimer += dt;
            if (_currentRotateTimer > rotateInterval)
            {
                _currentRotateTimer = 0f;
                pivot.Rotate(Vector3.forward, -45f);
            }
        }
    }
}