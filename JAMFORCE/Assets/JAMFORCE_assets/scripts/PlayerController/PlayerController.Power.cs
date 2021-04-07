using UnityEngine;

public partial class PlayerController
{
    void Shoot()
    {
        Vector2 impulse = playerManager.camera_rot * playerManager.mouse_to;

        var clone = Instantiate(projectile, playerManager.targetpos_sv2.target + 2 * impulse, Quaternion.identity).GetComponent<Rigidbody2D>();

        clone.velocity = json.shoot_force * impulse + .5f * playerManager.rigidbody_vlc;

        Destroy(clone.gameObject, Random.Range(json.shoot_lifetime_min, json.shoot_lifetime_max));

        Debug.DrawRay(clone.position, -2 * impulse, Color.white, 2);
    }
}