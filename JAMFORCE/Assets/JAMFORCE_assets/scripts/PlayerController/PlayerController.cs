using UnityEngine;

public partial class PlayerController : ControllerBehaviour
{
    [HideInInspector] public PlayerManager playerManager;

    public enum Prefabs { ice, wind, _last_ }
    public enum Transforms { pivot_render, wind_thrower, _last_ }

    public readonly Transform[] transforms = new Transform[(int)Transforms._last_];

    [Header("~@ Player @~")]
    public Settings json;
    [SerializeField] GameObject[] prefabs;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        OnAwake();

        playerManager = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();

        transforms[(int)Transforms.pivot_render] = transform.Find("render pivot");
        transforms[(int)Transforms.wind_thrower] = transform.Find("render pivot/wind");

        lead_wind = new OnValue<bool>(true, delegate (bool value)
        {
            animator.SetFloat((int)Parameters.wind_f, value ? 1 : 0);
            playerManager.animator.CrossFadeInFixedTime(value ? (int)PlayerManager.BaseStates.Default : (int)PlayerManager.BaseStates.Aim, 0, (int)PlayerManager.Layers.Base);
            playerManager.animator.CrossFadeInFixedTime((int)PlayerManager.JellyStates.Switch, 0, (int)PlayerManager.Layers.Jelly);
        });

        JSon.Read("PlayerMovement.settings", ref json);
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            JSon.Read("PlayerMovement.settings", ref json);
    }

    //------------------------------------------------------------------------------------------------------------------------------

    public void OnUpdate() =>
        UpdateInputs();
}