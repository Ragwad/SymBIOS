using UnityEngine;

public partial class GameManager
{
    [Header("~@ Inputs @~")]
    [Range(-1, 1)] public float left_axis;
    public Vector2 mouse_pos, mouse_pos_norm;

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateInputs()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos_norm = new Vector2(mouse_pos.x / Screen.width, mouse_pos.y / Screen.height);

        left_axis = Input.GetAxisRaw("Horizontal");
    }
}