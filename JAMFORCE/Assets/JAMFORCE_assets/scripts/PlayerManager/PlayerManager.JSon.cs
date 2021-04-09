using UnityEngine;

public partial class PlayerManager
{
    [System.Serializable]
    public class Settings : JSon
    {
        public float camera_height;

        public KeyCode 
            jump_keyboard = KeyCode.Space, jump_gamepad = (KeyCode)Util.GamepadKeycodes.A,
            switch_keyboard = KeyCode.A, switch_gamepad = (KeyCode)Util.GamepadKeycodes.LB, switch_mouse = KeyCode.Mouse1;
    }

    [Header("~@ JSon @~")]
    public Settings json;
}