using UnityEngine;

public partial class PlayerManager
{
    [Header("~@ Inputs @~")]
    public Vector2 left_axis;
    public bool camera_grav = true, camera_input;

    const KeyCode camera_grav_key = KeyCode.G, camera_input_key = KeyCode.H;

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

        left_axis = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1);

        if (Input.GetKeyDown(camera_grav_key))
            camera_grav = !camera_grav;

        if (Input.GetKeyDown(camera_input_key))
            camera_input = !camera_input;
    }
}