using UnityEngine;

public partial class PlayerManager : MonoBehaviour
{
    [System.Serializable]
    public class Settings : JSon
    {
        public KeyCode jump_gamepad = KeyCode.JoystickButton0, jump_keyboard = KeyCode.Space;
    }

    [HideInInspector] public Animator animator;
    [HideInInspector] public new Rigidbody2D rigidbody;
    [HideInInspector] public new CapsuleCollider2D collider;
    [HideInInspector] public PlayerController player;

    [Header("~@ Player @~")]
    public Settings json;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        animator = GetComponent<Animator>();
        listener = transform.Find("AudioListener").GetComponent<AudioListener>();
        camera = transform.Find("PlayerCamera").GetComponent<Camera>();
        camera_pivot = camera.transform.Find("Pivot");
        rigidbody = transform.Find("PhysicBody").GetComponent<Rigidbody2D>();
        collider = rigidbody.GetComponentInChildren<CapsuleCollider2D>(true);
        player = transform.Find("PhysicBody/VisualBody/PlayerController").GetComponent<PlayerController>();
        player_pivot = transform.Find("PhysicBody/CameraPivot");

        InitUI();
        InitAudio();
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            JSon.Read("Player.settings", ref json);
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
        UpdateUI();
    }
}