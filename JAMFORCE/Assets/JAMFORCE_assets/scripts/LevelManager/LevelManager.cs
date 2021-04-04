using UnityEngine;

public partial class LevelManager : MonoBehaviour
{
    public static LevelManager self;

    [SerializeField] Transform planet;

    [HideInInspector] public Vector2 planet_pos;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake() => 
        self = this;

    //------------------------------------------------------------------------------------------------------------------------------

    private void FixedUpdate()
    {
        planet_pos = planet.position;

        foreach (var prop in PhysicProp2D.selves)
            prop.OnFixedUpdate();
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDestroy()
    {
        if (this == self)
            self = null;
    }
}