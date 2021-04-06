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

    public static GameManager self;

    [HideInInspector] public float _fixedDeltaTime, _deltaTime;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (self != null && self != this)
        {
            Destroy(gameObject);
            return;
        }

        self = this;
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void FixedUpdate() 
        => _fixedDeltaTime = 1 / Time.fixedDeltaTime;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Update() 
        => _deltaTime = 1 / Time.deltaTime;
}