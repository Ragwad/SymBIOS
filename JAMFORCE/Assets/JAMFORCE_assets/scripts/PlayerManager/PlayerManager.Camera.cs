using UnityEngine;

public partial class PlayerManager
{
    [HideInInspector] public new Camera camera;
    [HideInInspector] public AudioListener listener;
    [HideInInspector] public Transform camera_pivot, player_pivot;

    [Header("~@ Camera @~")]
    [Tooltip("damp, spring")] [SerializeField] Vector2 camera_smooth = new Vector2(.1f, 2);
    [HideInInspector] public SmoothVector2 targetpos_sv2 = new SmoothVector2();

    [Range(-180, 180)] [SerializeField] float camera_grav_a;
    [SerializeField] float camera_clamp = 30;

    [HideInInspector] public Quaternion camera_rot;
    Vector3 camera_pos;

    //------------------------------------------------------------------------------------------------------------------------------

    void UpdateCamera()
    {
        targetpos_sv2.target = player_pivot.position;
        targetpos_sv2.SmoothDamp(camera_smooth.x, camera_smooth.y, Time.deltaTime);

        camera_pos += (Vector3)targetpos_sv2.value - camera_pivot.position;
        camera_pos.y = camera_height;

        camera_grav_a = Vector2.SignedAngle(Vector2.up, LevelManager.self.GetGravAtPoint(camera_pos));
        camera_grav_a = Mathf.MoveTowardsAngle(camera_grav_a, 0, Mathf.Min(Mathf.Abs(Mathf.Abs(camera_grav_a) - 180), camera_clamp));

        camera_rot = Quaternion.Euler(0, 0, camera_grav_a);
        camera.transform.SetPositionAndRotation(camera_pos, camera_rot);

        float a = Vector3.Angle(Vector3.forward, camera_rot * Vector3.forward);
        float tan = Mathf.Tan(a * Mathf.Deg2Rad);
        float dst = tan * -camera_pos.z;
        listener.transform.position = camera_pos + camera_rot * Vector3.forward * Mathf.Pow(dst * dst + camera_pos.z * camera_pos.z, .5f);
    }
}