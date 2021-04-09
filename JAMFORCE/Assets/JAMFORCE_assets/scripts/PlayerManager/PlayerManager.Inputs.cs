using UnityEngine;

public partial class PlayerManager
{
    [Header("~@ Inputs @~")]
    public Vector2  mouse_to;
    public bool jump_down, jump_hold;

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateInputs()
    {
        mouse_to = GameManager.self.mouse_pos_norm - (Vector2)camera.WorldToViewportPoint(targetpos_sv2.target);

        if (mouse_to.sqrMagnitude > 0)
        {
            if (mouse_to.sqrMagnitude > 1)
                mouse_to = Vector2.ClampMagnitude(mouse_to, 1);
            else
                mouse_to.Normalize();
        }

        jump_down = Input.GetKeyDown(json.jump_keyboard) || Input.GetKeyDown(json.jump_gamepad);
        jump_hold = Input.GetKey(json.jump_keyboard) || Input.GetKey(json.jump_gamepad);
    }
}