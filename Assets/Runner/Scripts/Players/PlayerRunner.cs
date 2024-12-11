using System;
using Runner.Scripts.LevelGeneration;
using TriInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Runner.Scripts.Players
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerRunner : MonoBehaviour
    {
        [SerializeField] private float _speedX = 1;
        [ShowInInspector, ReadOnly, HideInEditMode] private eLine Line;
        [ShowInInspector, ReadOnly, HideInEditMode] private float _lastInputX;

        private LevelGenerator LevelGenerator => FindAnyObjectByType<LevelGenerator>();
        private Rigidbody Rigidbody => GetComponent<Rigidbody>();
        private PlayerInput PlayerInput => GetComponent<PlayerInput>();

        private void Awake()
        {
            Rigidbody.isKinematic = true;
            PlayerInput.actions.Enable();
            var action = PlayerInput.actions.FindAction("MoveX");
            if (action == null)
            {
                Debug.LogError("action not found");
            }
            else
            {
                action.performed -= OnMoveXInputHandle;
                action.performed += OnMoveXInputHandle;
            }
        }

        private void FixedUpdate()
        {
            if (_lastInputX != 0f)
            {
                Debug.Log($"TryMoveX {_lastInputX}");
                TryMoveX((int)Mathf.Sign(_lastInputX));
                _lastInputX = 0f;
            }

            MoveXUpdate();
        }

        private void MoveXUpdate()
        {
            if (Mathf.Approximately(transform.position.x, LevelGenerator.Lines[Line].transform.position.x)) return;

            Vector3 newPosition = Vector3.MoveTowards(Rigidbody.position, LevelGenerator.Lines[Line].transform.position,
                _speedX);
            Rigidbody.MovePosition(newPosition);
        }

        private void TryMoveX(int delta)
        {
            Debug.Log($"TryMoveX {delta}");
            if (delta == 0) return;

            int result = (int)Line + delta;

            if (Enum.IsDefined(typeof(eLine), result))
            {
                MoveX((eLine)result);
            }
        }

        private void MoveX(eLine newLine)
        {
            Line = newLine;
            Debug.Log(Line);
        }

        private void OnMoveXInputHandle(CallbackContext callbackContext)
        {
            _lastInputX = callbackContext.ReadValue<float>();
        }
    }
}