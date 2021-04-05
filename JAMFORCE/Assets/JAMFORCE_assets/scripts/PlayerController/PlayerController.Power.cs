using UnityEngine;

public partial class PlayerController
{
    [Header("~@ Shoot @~")]
    [SerializeField] float shoot_force = 15;
    [Tooltip("min, max")] [SerializeField] Vector2 shoot_lifetime = new Vector2(10, 15);

    //------------------------------------------------------------------------------------------------------------------------------

    void Shoot()
    {
        Vector2 impulse = playerManager.camera_rot * playerManager.mouse_to;

        var clone = Instantiate(projectile, playerManager.targetpos_sv2.target + 2 * impulse, Quaternion.identity).GetComponent<Rigidbody2D>();

        clone.velocity = shoot_force * impulse + .5f * playerManager.rigidbody_vlc;

        Destroy(clone.gameObject, Random.Range(shoot_lifetime.x, shoot_lifetime.y));

        Debug.DrawRay(clone.position, -2 * impulse, Color.white, 2);
    }
}