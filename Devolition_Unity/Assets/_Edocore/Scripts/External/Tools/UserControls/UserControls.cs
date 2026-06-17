using edocle.external.tools.usercontrols;
using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace edocle.external.tools
{
    public class UserControls
    {
        #region Lifecycle

        public UserControls()
        {
            Init();
        }

        public void Kill()
        {
            Terminate();
        }

        #endregion Lifecycle

        void Init()
        {
            InitPointerControls();
        }

        void Terminate()
        {
            TerminatePointerControls();
        }

        #region Pointer controls

        // *** Calls ***

        /// <summary>
        /// Event fired when the user presses on the screen.
        /// </summary>
        public event Action<PointerInput, double> OnPress;

        /// <summary>
        /// Event fired as the user drags along the screen.
        /// </summary>
        public event Action<PointerInput, double> OnDrag;

        /// <summary>
        /// Event fired when the user hovers around.
        /// </summary>
        public event Action<PointerInput, double> OnHover;

        /// <summary>
        /// Event fired when the user releases a press.
        /// </summary>
        public event Action<PointerInput, double> OnRelease;

        // *** Methods ***

        PointerControl _pointerControls;

        void InitPointerControls()
        {
            _pointerControls = new PointerControl();

            _pointerControls.pointer.point.performed += OnPoint;
            _pointerControls.pointer.point.canceled += OnPoint;

            _pointerControls.Enable();
        }

        void TerminatePointerControls()
        {
            _pointerControls.pointer.point.performed -= OnPoint;
            _pointerControls.pointer.point.canceled -= OnPoint;
            _pointerControls.Dispose();
        }

        bool _pointerDragging = false;
        bool _pointerHovering = false;

        void OnPoint(InputAction.CallbackContext context)
        {
            InputControl control = context.control;
            PointerInput input = context.ReadValue<PointerInput>();

            InputDevice device = control.device;
            bool isMouseInput = device is Mouse;
            if (isMouseInput)
                input.InputId = PointerInputModule.kMouseLeftId;

            if (input.Contact)
            {
                if (_pointerDragging)
                {
                    OnDrag?.Invoke(input, context.time);

                }
                else
                {
                    OnPress?.Invoke(input, context.time);
                    _pointerDragging = true;
                    _pointerHovering = false;
                }
            }
            else
            {
                if (_pointerHovering)
                {
                    OnHover?.Invoke(input, context.time);

                }
                else
                {
                    OnRelease?.Invoke(input, context.time);
                    _pointerHovering = true;
                    _pointerDragging = false;
                }
            }
        }

        #endregion Pointer controls
    }
}