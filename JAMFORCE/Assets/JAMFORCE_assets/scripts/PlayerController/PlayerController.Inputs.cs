using UnityEngine;

public partial class PlayerController
{
    void UpdateInputs()
    {
        if (ground_hit.collider && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)))
            playerManager.rigidbody.AddForce(playerManager.physic_grav_n * playerManager.jump_force * playerManager.rigidbody.mass, ForceMode2D.Impulse);

        Vector2 axis = Quaternion.Inverse(playerManager.grav_rot) * move_axis;

        if (axis.x > .3f)
        {
            if (state_base != BaseStates.move_right)
                animator.CrossFadeInFixedTime((int)BaseStates.move_right, state_base == BaseStates.idle_right ? 0 : .1f, (int)Layers.Base, .1f);
        }
        else if (axis.x < -.3f)
        {
            if (state_base != BaseStates.move_left)
                animator.CrossFadeInFixedTime((int)BaseStates.move_left, state_base == BaseStates.idle_left ? 0 : .1f, (int)Layers.Base, .1f);
        }
        else
        {
            if (state_base == BaseStates.move_left)
                animator.CrossFadeInFixedTime((int)BaseStates.idle_left, 0, (int)Layers.Base);
            else if (state_base == BaseStates.move_right)
                animator.CrossFadeInFixedTime((int)BaseStates.idle_right, 0, (int)Layers.Base);
        }

        if (playerManager.mouse_to.sqrMagnitude > 0 && Input.GetKeyDown(KeyCode.Mouse0))
            Shoot();
    }
}