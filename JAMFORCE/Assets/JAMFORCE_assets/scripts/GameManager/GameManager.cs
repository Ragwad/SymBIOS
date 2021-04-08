using UnityEngine;

public partial class GameManager : MonoBehaviour
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
    public Settings json;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (self != null && self != this)
        {
            Destroy(gameObject);
            return;
        }

        self = this;

        Physics2D.queriesHitTriggers = false;
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
    }
}