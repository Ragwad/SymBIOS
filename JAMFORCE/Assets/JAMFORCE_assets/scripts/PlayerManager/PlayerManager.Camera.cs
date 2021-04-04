using UnityEngine;

public partial class PlayerManager
{
    [HideInInspector] public new Camera camera;
    [HideInInspector] public Transform camera_pivot, player_pivot;

    [Header("~@ Camera @~")]
    [SerializeField] float camera_clamp = 1;
    [Tooltip("damp, spring")] [SerializeField] Vector2 camera_smooth = new Vector2(.1f, 2);
    [SerializeField] SmoothVector2 targetpos_sv2 = new SmoothVector2();

    Vector3 camera_pos;
    [HideInInspector] public Quaternion input_rot;

    Vector2 camera_grav_n;

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateCamera()
    {
        targetpos_sv2.target = player_pivot.position;
        targetpos_sv2.SmoothDamp(camera_smooth.x, camera_smooth.y, Time.deltaTime);

        camera_pos += (Vector3)targetpos_sv2.value - camera_pivot.position;

        camera_grav_n = (camera_pos - (Vector3)LevelManager.self.planet_pos).normalized;

        input_rot = camera_grav ? Quaternion.LookRotation(Vector3.forward, camera_grav_n) : Quaternion.identity;
        camera.transform.SetPositionAndRotation(camera_pos, input_rot);
    }
}