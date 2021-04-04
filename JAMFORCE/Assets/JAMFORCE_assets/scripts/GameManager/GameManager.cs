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
}