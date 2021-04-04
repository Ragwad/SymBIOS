using UnityEngine;

public partial class PlayerManager : MonoBehaviour
{
    [HideInInspector] public PlayerController player;
    [HideInInspector] public new Rigidbody2D rigidbody;
    [HideInInspector] public new CapsuleCollider2D collider;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        camera = transform.Find("PlayerCamera").GetComponent<Camera>();
        camera_pivot = camera.transform.Find("Pivot");
        rigidbody = transform.Find("PhysicBody").GetComponent<Rigidbody2D>();
        collider = rigidbody.GetComponentInChildren<CapsuleCollider2D>(true);
        player = transform.Find("PhysicBody/VisualBody/PlayerController").GetComponent<PlayerController>();
        player_pivot = transform.Find("PhysicBody/CameraPivot");
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void FixedUpdate()
    {
        FixedUpdatePhysics();

        if (player != null)
            player.OnFixedUpdate();
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        UpdateInputs();

        if (player != null)
            player.OnUpdate();

        UpdateCamera();
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void OnGUI()
    {
        int x = 15, y = 0;

        GUI.Label(new Rect(x, y += 15, 250, 30), nameof(camera_grav) + " (" + camera_grav_key + " key) : " + (camera_grav ? "on" : "off"));
        GUI.Label(new Rect(x, y += 15, 250, 30), nameof(camera_input) + " (" + camera_input_key + " key) : " + (camera_input ? "on" : "off"));
    }
}