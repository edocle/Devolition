using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace edocle.external.tools.usercontrols
{
    public struct PointerInput
    {
        /// <summary>
        /// Mouse: Left click
        /// Touch: touch
        /// </summary>
        public bool Contact;

        /// <summary>
        /// Mouse: Right click
        /// </summary>
        public bool AltContact;

        /// <summary>
        /// Mouse: Middle click
        /// </summary>
        public bool AltContact2;

        /// <summary>
        /// Position of input.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Tap count.
        /// </summary>
        public int TapCount;

        /// <summary>
        /// Touch: ID of input type.
        /// </summary>
        public int InputId;

        /// <summary>
        /// Touch: Pressure of input.
        /// </summary>
        public float? Pressure;

        /// <summary>
        /// Touch: Radius of input.
        /// </summary>
        public Vector2? Radius;

        /// <summary>
        /// Mouse: Delta of scroll.
        /// </summary>
        public Vector2? Scroll;
    }

#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class PointerInputComposite : InputBindingComposite<PointerInput>
    {
        [InputControl(layout = "Button")]
        public int contact;

        [InputControl(layout = "Button")]
        public int altContact;

        [InputControl(layout = "Button")]
        public int altContact2;

        [InputControl(layout = "Vector2")]
        public int position;

        [InputControl(layout = "Integer")]
        public int tapCount;

        [InputControl(layout = "Integer")]
        public int inputId;

        [InputControl(layout = "Axis")]
        public int pressure;

        [InputControl(layout = "Vector2")]
        public int radius;

        [InputControl(layout = "Vector2")]
        public int scroll;

        public override PointerInput ReadValue(ref InputBindingCompositeContext context)
        {
            var contact = context.ReadValueAsButton(this.contact);
            var altContact = context.ReadValueAsButton(this.altContact);
            var altContact2 = context.ReadValueAsButton(this.altContact2);
            var position = context.ReadValue<Vector2, Vector2MagnitudeComparer>(this.position);
            var tapCount = context.ReadValue<int>(this.tapCount);
            var inputId = context.ReadValue<int>(this.inputId);
            var pressure = context.ReadValue<float>(this.pressure);
            var radius = context.ReadValue<Vector2, Vector2MagnitudeComparer>(this.radius);
            var scroll = context.ReadValue<Vector2, Vector2MagnitudeComparer>(this.scroll);

            return new PointerInput
            {
                Contact = contact,
                AltContact = altContact,
                AltContact2 = altContact2,
                Position = position,
                TapCount = tapCount,
                InputId = inputId,
                Pressure = pressure != 0 ? pressure : (float?)null,
                Radius = radius != Vector2.zero ? radius : (Vector2?)null,
                Scroll = scroll != Vector2.zero ? scroll : (Vector2?)null
            };
        }

#if UNITY_EDITOR
        static PointerInputComposite()
        { Register(); }
#endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Register()
        { InputSystem.RegisterBindingComposite<PointerInputComposite>(); }
    }
}