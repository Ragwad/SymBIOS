using UnityEngine;

public partial class PlayerController
{

    //------------------------------------------------------------------------------------------------------------------------------

    public void OnUpdate()
    {
        if (ground_hit.collider && Input.GetKeyDown(KeyCode.Space))
            playerManager.rigidbody.AddForce(playerManager.physic_grav_n * playerManager.jump_force * playerManager.rigidbody.mass, ForceMode2D.Impulse);

        Vector2 axis = Quaternion.Inverse(playerManager.grav_rot) * move_axis;

        if (axis.x > .5f)
        {
            if (state_base != BaseStates.move_right)
                animator.CrossFadeInFixedTime((int)BaseStates.move_right, .15f, (int)Layers.Base, .1f);
        }
        else if (axis.x < -.5f)
        {
            if (state_base != BaseStates.move_left)
                animator.CrossFadeInFixedTime((int)BaseStates.move_left, .15f, (int)Layers.Base, .1f);
        }
        else if (state_base != BaseStates.idle)
            animator.CrossFadeInFixedTime((int)BaseStates.idle, .25f, (int)Layers.Base);
    }
}