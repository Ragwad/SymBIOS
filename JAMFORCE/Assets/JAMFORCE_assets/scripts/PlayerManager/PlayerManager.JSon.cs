using UnityEngine;

public partial class PlayerManager
{
    [System.Serializable]
    public class Settings : JSon
    {
        public KeyCode 
            jump_keyboard = KeyCode.Space, jump_gamepad = (KeyCode)Util.GamepadKeycodes.A,
            switch_keyboard = KeyCode.A, switch_gamepad = (KeyCode)Util.GamepadKeycodes.LB;
    }

    [Header("~@ JSon @~")]
    public Settings json;
}