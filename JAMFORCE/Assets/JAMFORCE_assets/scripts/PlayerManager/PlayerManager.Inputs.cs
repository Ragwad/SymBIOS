using UnityEngine;

public partial class PlayerManager
{
    [Header("~@ Inputs @~")]
    [Range(-1, 1)] public float left_axis;
    public bool jump_down, jump_hold;
    public Vector2 mouse_pos, mouse_to;

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateInputs()
    {
        mouse_pos = Input.mousePosition;
        mouse_to = camera.WorldToViewportPoint(targetpos_sv2.target);

        if (mouse_to.sqrMagnitude > 0)
        {
            mouse_to.x *= Screen.width;
            mouse_to.y *= Screen.height;

            if (mouse_to.sqrMagnitude > 1)
                mouse_to = Vector2.ClampMagnitude(mouse_pos - mouse_to, 1);
            else
                mouse_to.Normalize();
        }

        mouse_pos.x /= Screen.width;
        mouse_pos.y /= Screen.height;

        left_axis = Input.GetAxisRaw("Horizontal");

        jump_down = Input.GetKeyDown(json.jump_keyboard) || Input.GetKeyDown(json.jump_gamepad);
        jump_hold = Input.GetKey(json.jump_keyboard) || Input.GetKey(json.jump_gamepad);
    }
}