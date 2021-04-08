using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerManager playerManager;
    [HideInInspector] public Animator animator;

    public enum Transforms { pivot_render, _last_ }

    public readonly Transform[] transforms = new Transform[(int)Transforms._last_];

    [Header("~@ Player @~")]
    public Settings json;
    [SerializeField] GameObject projectile;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        playerManager = GetComponentInParent<PlayerManager>();
        animator = GetComponent<Animator>();

        transforms[(int)Transforms.pivot_render] = transform.Find("render pivot");

        lead_wind = new OnValue<bool>(true, delegate (bool value)
        {
            animator.SetFloat((int)Parameters.wind_f, value ? 1 : 0);
            playerManager.animator.CrossFadeInFixedTime((int)PlayerManager.JellyStates.Switch, 0, (int)PlayerManager.Layers.Jelly);
        });
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