using UnityEngine;

public partial class PlayerController
{
    public const int ground_mask = 1 << (int)GameManager.UserLayers.Default;

    [Header("~@ Physics @~")]
    public RaycastHit2D ground_hit;

    Vector2 move_axis;

    //------------------------------------------------------------------------------------------------------------------------------

    public void OnFixedUpdate()
    {
        playerManager.rigidbody.AddForce(-playerManager.physic_grav_n * playerManager.grav_force * playerManager.rigidbody.mass, ForceMode2D.Force);

        ground_hit = Physics2D.CircleCast(playerManager.camera_pivot.position, playerManager.collider.size.x, -playerManager.physic_grav_n, 1.2f, ground_mask);

        if (ground_hit.collider != null)
        {
            move_axis = (playerManager.camera_input ? playerManager.input_rot : playerManager.grav_rot) * playerManager.left_axis;

            playerManager.rigidbody.AddForce(Vector3.ProjectOnPlane(move_axis * playerManager.move_speed - playerManager.rigidbody.velocity, playerManager.physic_grav_n), ForceMode2D.Impulse);
        }
    }
}