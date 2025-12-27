using UnityEngine;
using UnityEngine.InputSystem;

namespace ADayInTheOffice.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 3.5f;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            _rb.freezeRotation = true;
        }

        private void FixedUpdate()
        {
            Vector2 dir = Vector2.zero;

            if (Keyboard.current != null)
            {
                if (Keyboard.current.wKey.isPressed) dir.y += 1f;
                if (Keyboard.current.sKey.isPressed) dir.y -= 1f;
                if (Keyboard.current.aKey.isPressed) dir.x -= 1f;
                if (Keyboard.current.dKey.isPressed) dir.x += 1f;
            }

            if (dir.sqrMagnitude > 1f) dir = dir.normalized;
            _rb.linearVelocity = dir * _moveSpeed;
        }
    }
}
