using UnityEngine;

public partial class PlayerManager
{
    [Header("~@ Inputs @~")]
    public Vector2 left_axis;
    public bool camera_grav, camera_input;

    const KeyCode camera_grav_key = KeyCode.G, camera_input_key = KeyCode.H;

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateInputs()
    {
        left_axis = Vector2.ClampMagnitude(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")), 1);

        if (Input.GetKeyDown(camera_grav_key))
            camera_grav = !camera_grav;

        if (Input.GetKeyDown(camera_input_key))
            camera_input = !camera_input;
    }
}