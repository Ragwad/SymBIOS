using UnityEngine;

public partial class PlayerController
{
    [Header("~@ Inputs @~")]
    [SerializeField] bool lead_wind;
    [SerializeField] int jumps;
    [SerializeField] TowardFloat side_f = new TowardFloat(1);

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateInputs()
    {
        if (state_base == BaseStates.Power)
        {
            float aim_a = Vector2.SignedAngle(playerManager.physic_grav_n, playerManager.mouse_to);
            side_f.target = side_f.value = aim_a > 0 ? -1 : 1;
            animator.SetFloat((int)Parameters.aim_a, Mathf.Abs(aim_a));
        }
        else
        {
            if (Mathf.Abs(playerManager.left_axis) > .2f)
            {
                if (isGround && state_base != BaseStates.Move)
                    animator.Play((int)BaseStates.Move, (int)Layers.Base);

                if (playerManager.left_axis < -.2f)
                    side_f.target = -1;
                else if (playerManager.left_axis > .2f)
                    side_f.target = 1;
            }
            else if (state_base == BaseStates.Move)
                animator.Play((int)BaseStates.Idle, (int)Layers.Base);


            if (playerManager.jump_down && Time.time > jump_time)
            {
                switch (state_base)
                {
                    case BaseStates.Move:
                    case BaseStates.Idle:
                    case BaseStates.Power:
                        if (isGround)
                        {
                            AudioJump();

                            jump_time = Time.time + .1f;
                            jumps = 1;

                            playerManager.rigidbody.AddForce(playerManager.physic_grav_n * json.jump1_force * playerManager.rigidbody.mass, ForceMode2D.Impulse);

                            animator.CrossFadeInFixedTime((int)BaseStates.JumpUp, 0, (int)Layers.Base);
                        }
                        break;

                    case BaseStates.JumpUp:
                    case BaseStates.JumpDown:
                        if (lead_wind && jumps > 0)
                        {
                            AudioJump();

                            jump_time = Time.time + .1f;
                            jumps = 0;

                            playerManager.rigidbody.AddForce(playerManager.physic_grav_n * json.jump2_force * playerManager.rigidbody.mass, ForceMode2D.Impulse);
                        }
                        break;
                }

                void AudioJump()
                {
                    AudioSource source = playerManager.sources[(int)PlayerManager.AudioSources.air];

                    source.clip = GameManager.self.clips_jump[Random.Range(0, GameManager.self.clips_jump.Length)];
                    source.Stop();
                    source.Play();
                }
            }
        }

        if (side_f.Towards(json.side_speed, Time.deltaTime, false) || true)
            transforms[(int)Transforms.pivot_render].localScale = new Vector3(side_f.value, 1, 1);

        if (isGround && playerManager.mouse_to.sqrMagnitude > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (state_base != BaseStates.Power)
                animator.CrossFadeInFixedTime((int)BaseStates.Power, 0, (int)Layers.Base);

            Vector2 impulse = playerManager.camera_rot * playerManager.mouse_to;

            var clone = Instantiate(projectile, playerManager.targetpos_sv2.target + 2 * impulse, Quaternion.identity).GetComponent<Rigidbody2D>();

            clone.velocity = json.shoot_force * impulse + .5f * playerManager.rigidbody_vlc;

            Destroy(clone.gameObject, Random.Range(json.shoot_lifetime_min, json.shoot_lifetime_max));
        }
    }
}