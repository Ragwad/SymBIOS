using UnityEngine;

public partial class PlayerManager
{
    [Header("~@ Physics @~")]
    [Range(-360, 360)] public float physic_grav_a;
    [HideInInspector] public Vector2 rigidbody_pos, rigidbody_vlc, physic_grav_n;
    [Min(0)] public float move_speed = 10, jump_force = 20, grav_force = 70;

    [HideInInspector] public Quaternion grav_rot;

    //------------------------------------------------------------------------------------------------------------------------------

    void FixedUpdatePhysics()
    {
        rigidbody_pos = rigidbody.position;
        rigidbody_vlc = rigidbody.velocity;

        physic_grav_n = (rigidbody_pos - LevelManager.self.planet_pos).normalized;

        physic_grav_a = Vector2.SignedAngle(Vector2.up, physic_grav_n);
        rigidbody.rotation = physic_grav_a;

        grav_rot = Quaternion.Euler(0, 0, physic_grav_a);
    }
}