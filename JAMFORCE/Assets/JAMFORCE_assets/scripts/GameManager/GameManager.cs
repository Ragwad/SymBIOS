using UnityEngine;

public partial class GameManager : ControllerBehaviour
{
    public enum UserLayers
    {
        Default = 0,
        TransparentFX = 1,
        IgnoreRaycast = 2,
        Water = 4,
        UI = 5,
        player = 6,
    }

    [System.Serializable]
    public class Settings : JSon
    {

    }

    public static GameManager self;

    [HideInInspector] public float _fixedDeltaTime, _deltaTime;

    [Header("~@ Game @~")]
    [Range(0, 1)] [SerializeField] float timeScale = 1;
    public Settings json;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        OnAwake();

        if (self != null && self != this)
        {
            Destroy(gameObject);
            return;
        }

        self = this;
        DontDestroyOnLoad(gameObject);

        Physics2D.queriesHitTriggers = false;

        InitUI();
        InitAudio();
        InitScenes();

        JSon.Read("Game.settings", ref json);
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            JSon.Read("Game.settings", ref json);
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void FixedUpdate() 
        => _fixedDeltaTime = 1 / Time.fixedDeltaTime;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        _deltaTime = 1 / Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.T))
            Time.timeScale = Time.timeScale > .5f ? .2f : 1;

        UpdateInputs();
        UpdateUI();
    }
}