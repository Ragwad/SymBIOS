using UnityEngine;

public partial class PlayerController
{
    public const int ground_mask = 1 << (int)GameManager.UserLayers.Default;

    [Header("~@ Physics @~")]
    [SerializeField] float jump_time;
    [SerializeField] bool isGround;
    [SerializeField] float groundHeight;
    [SerializeField] float ground_range = .1f;

    public RaycastHit2D ground_hit;

    [Header("~ Slide ~")]
    [Range(-1, 1)] [SerializeField] float ground_dot;
    [Range(0, 1)] [SerializeField] float move_weight, slope_weight;

    //------------------------------------------------------------------------------------------------------------------------------

    public void OnFixedUpdate()
    {
        if (jump_time > Time.fixedTime)
            isGround = false;
        else
        {
            ground_hit = Physics2D.CircleCast(
                playerManager.rigidbody_pos + playerManager.physic_grav_n * (playerManager.collider.offset.y + .5f * (-playerManager.collider.size.y + playerManager.collider.size.x)), // origin
                .9f * .5f * playerManager.collider.size.x, // radius
                -playerManager.physic_grav_n, // direction
                playerManager.collider.size.x, // distance
                ground_mask // layers
                );

            if (ground_hit.collider != null)
            {
                groundHeight = (Quaternion.Inverse(playerManager.grav_rot) * (ground_hit.point - playerManager.rigidbody_pos)).y;
                isGround = groundHeight > -ground_range;
            }
            else
                isGround = false;
        }

        Vector2 move_axis;

        if (isGround)
        {
            move_axis = Quaternion.LookRotation(Vector3.forward, ground_hit.normal) * Vector2.right * GameManager.self.left_axis;

            ground_dot = Vector2.Dot(playerManager.physic_grav_n, ground_hit.normal);
            slope_weight = Mathf.InverseLerp(json.slope_range_max, json.slope_range_min, ground_dot);
            move_weight = json.slide_default * (1 - slope_weight);

            if (ground_hit.collider.sharedMaterial != null)
                switch (ground_hit.collider.sharedMaterial.name)
                {
                    case "ice":
                        move_weight *= json.slide_ice;
                        break;
                }
        }
        else
        {
            move_axis = playerManager.grav_rot * Vector2.right * GameManager.self.left_axis;

            ground_dot = 1;
            slope_weight = 0;
            move_weight = json.slide_air;
        }

        switch (state_base)
        {
            case BaseStates.Move:
            case BaseStates.Idle:
                if (!isGround)
                    animator.CrossFadeInFixedTime(playerManager.rigidbody_lcl_vlc.y > 0 ? (int)BaseStates.JumpUp : (int)BaseStates.JumpDown, 0, (int)Layers.Base);
                break;

            case BaseStates.JumpDown:
                if (lead_wind.value && playerManager.jump_hold && playerManager.rigidbody_lcl_vlc.y < 0)
                {
                    animator.CrossFadeInFixedTime((int)BaseStates.Fly, 0, (int)Layers.Base);
                    playerManager.animator.CrossFadeInFixedTime((int)PlayerManager.JellyStates.Jump, 0, (int)PlayerManager.Layers.Jelly);
                }
                else
                    goto case BaseStates.JumpUp;
                break;

            case BaseStates.JumpUp:
                if (isGround)
                    animator.CrossFadeInFixedTime((int)BaseStates.Idle, 0, (int)Layers.Base);
                else if (playerManager.rigidbody_lcl_vlc.y < 0 && state_base != BaseStates.JumpDown)
                    animator.CrossFadeInFixedTime((int)BaseStates.JumpDown, 0, (int)Layers.Base);
                break;

            case BaseStates.Fly:
                if (isGround)
                    animator.CrossFadeInFixedTime((int)BaseStates.Idle, 0, (int)Layers.Base);
                else if (playerManager.rigidbody_lcl_vlc.y > 0)
                    animator.CrossFadeInFixedTime((int)BaseStates.JumpUp, 0, (int)Layers.Base);
                else if (!playerManager.jump_hold)
                    animator.CrossFadeInFixedTime((int)BaseStates.JumpDown, 0, (int)Layers.Base);
                break;
        }

        Vector2 force = default;
        Vector2 vlc = isGround && ground_hit.rigidbody != null && !ground_hit.rigidbody.isKinematic ? ground_hit.rigidbody.GetPointVelocity(playerManager.rigidbody_pos) : default;

        if (move_weight > 0)
            force += GameManager.self._fixedDeltaTime * move_weight * (move_axis * json.move_speed - (Vector2)Vector3.ProjectOnPlane(playerManager.rigidbody_vlc - vlc, playerManager.physic_grav_n));

        if (isGround)
            force += (1 - slope_weight) * GameManager.self._fixedDeltaTime * playerManager.physic_grav_n * (GameManager.self._fixedDeltaTime * groundHeight - (Quaternion.Inverse(playerManager.grav_rot) * (playerManager.rigidbody_vlc - vlc)).y);

        switch (state_base)
        {
            case BaseStates.Fly:
                if (playerManager.rigidbody_lcl_vlc.y < -json.fly_speed)
                    force += GameManager.self._fixedDeltaTime * playerManager.physic_grav_n * json.fly_force * (-json.fly_speed * playerManager.rigidbody_lcl_vlc.y);
                break;

            default:
                force -= playerManager.physic_grav_n * json.grav_force * (isGround ? slope_weight : 1);
                break;
        }

        playerManager.rigidbody.AddForce(playerManager.rigidbody.mass * force, ForceMode2D.Force);
    }
}