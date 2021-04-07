//using UnityEngine;

//public partial class PlayerController
//{
//    public const int ground_mask = 1 << (int)GameManager.UserLayers.Default;

//    [Header("~@ Physics @~")]
//    [SerializeField] bool isGround;
//    [SerializeField] float groundHeight;
//    [SerializeField] float ground_range = .1f;

//    public RaycastHit2D ground_hit;

//    [Header("~ Slide ~")]
//    [Range(0, 1)] [SerializeField] float slide_ice = .03f;
//    [Range(0, 1)] [SerializeField] float slope = 1;
//    [Range(-1, 1)] [SerializeField] float slope_dot;
//    [SerializeField] Vector2 slope_range = new Vector2(.5f, .7f);

//    Vector2 move_axis;

//    [Header("~ Jump ~")]
//    [SerializeField] bool isJumping;
//    [SerializeField] float jump_time;

//    //------------------------------------------------------------------------------------------------------------------------------

//    void Jump()
//    {
//        jump_time = .3f;
//        playerManager.rigidbody.AddForce(playerManager.physic_grav_n * playerManager.jump_force * playerManager.rigidbody.mass, ForceMode2D.Impulse);
//    }

//    public void OnFixedUpdate()
//    {
//        jump_time -= Time.fixedDeltaTime;
//        isJumping = jump_time > 0;

//        if (jump_time > 0)
//            isGround = false;
//        else
//        {
//            Vector2 origin = playerManager.rigidbody_pos + playerManager.physic_grav_n * (playerManager.collider.offset.y + .5f * (-playerManager.collider.size.y + playerManager.collider.size.x));
//            float radius = .9f * .5f * playerManager.collider.size.x;
//            float distance = playerManager.collider.size.x;

//            ground_hit = Physics2D.CircleCast(origin, radius, -playerManager.physic_grav_n, distance, ground_mask);

//            if (ground_hit.collider != null)
//            {
//                groundHeight = (Quaternion.Inverse(playerManager.grav_rot) * (ground_hit.point - playerManager.rigidbody_pos)).y;
//                isGround = groundHeight > -ground_range;
//            }
//            else
//                isGround = false;
//        }

//        if (isGround)
//        {
//            Debug.DrawRay(playerManager.rigidbody_pos, playerManager.physic_grav_n, Color.red);

//            slope_dot = Vector2.Dot(playerManager.physic_grav_n, ground_hit.normal);
//            slope = Mathf.InverseLerp(slope_range.x, slope_range.y, slope_dot);

//            if (ground_hit.collider.sharedMaterial != null)
//                switch (ground_hit.collider.sharedMaterial.name)
//                {
//                    case "ice":
//                        slope *= slide_ice;
//                        break;
//                }

//            move_axis = playerManager.grav_rot * Quaternion.FromToRotation(playerManager.physic_grav_n, ground_hit.normal) * playerManager.left_axis;
//            Debug.DrawRay(playerManager.rigidbody_pos, move_axis, Color.white);

//            Vector2 force = default;

//            if (slope > 0)
//                force += slope * GameManager.self._fixedDeltaTime * (-playerManager.rigidbody_vlc + (GameManager.self._fixedDeltaTime * playerManager.physic_grav_n * groundHeight) + playerManager.move_speed * move_axis);
//            if (slope < 1)
//                force += -playerManager.physic_grav_n * playerManager.grav_force;

//            playerManager.rigidbody.AddForce(force, ForceMode2D.Force);
//        }
//        else
//        {
//            slope_dot = 0;
//            isGround = false;
//            groundHeight = 0;
//            playerManager.rigidbody.AddForce(-playerManager.physic_grav_n * playerManager.grav_force * playerManager.rigidbody.mass, ForceMode2D.Force);
//        }
//    }
//}