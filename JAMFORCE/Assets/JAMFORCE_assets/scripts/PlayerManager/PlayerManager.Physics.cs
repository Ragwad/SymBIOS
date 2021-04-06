using UnityEngine;

public partial class PlayerManager
{
    [Header("~@ Physics @~")]
    [Range(-180, 180)] public float physic_grav_a;
    [Min(0)] public float move_speed = 10, jump_force = 20, grav_force = 55;

    [HideInInspector] public Vector2 rigidbody_pos, rigidbody_vlc, rigidbody_lcl_vlc, physic_grav_n;
    [HideInInspector] public Quaternion grav_rot;

    //------------------------------------------------------------------------------------------------------------------------------

    void FixedUpdatePhysics()
    {
        rigidbody_pos = rigidbody.position;
        rigidbody_vlc = rigidbody.velocity;

        physic_grav_n = LevelManager.self.GetGravAtPoint(rigidbody_pos);
        rigidbody_lcl_vlc = Quaternion.Inverse(grav_rot) * rigidbody_vlc;

        physic_grav_a = Vector2.SignedAngle(Vector2.up, physic_grav_n);
        rigidbody.rotation = physic_grav_a;
        grav_rot = Quaternion.LookRotation(Vector3.forward, physic_grav_n);
    }
}