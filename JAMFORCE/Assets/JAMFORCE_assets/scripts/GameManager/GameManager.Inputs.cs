using UnityEngine;

public partial class GameManager
{
    [Header("~@ Inputs @~")]
    [Range(-1, 1)] public float left_axis;
    public bool mouse_hold, mouse_down;
    public Vector2 mouse_pos, mouse_pos_norm;

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateInputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            mouse_hold = mouse_down = true;
        else
        {
            mouse_down = false;

            if (!Input.GetKey(KeyCode.Mouse0))
                mouse_hold = false;
        }

        mouse_pos = Input.mousePosition;
        mouse_pos_norm = new Vector2(mouse_pos.x / Screen.width, mouse_pos.y / Screen.height);

        left_axis = Input.GetAxisRaw("Horizontal");

        switch (state_base)
        {
            case BaseStates.Home:
                break;

            case BaseStates.Options:
                break;

            case BaseStates.Gameplay:
                if (Input.GetKeyDown(KeyCode.Escape))
                    animator.CrossFadeInFixedTime((int)BaseStates.toPause, 0, (int)Layers.Base);
                break;

            case BaseStates.Credits:
                break;
        }
    }
}