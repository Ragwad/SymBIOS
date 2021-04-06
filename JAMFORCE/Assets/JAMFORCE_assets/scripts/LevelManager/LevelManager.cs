using UnityEngine;

public partial class LevelManager : MonoBehaviour
{
    public static LevelManager self;

    [Header("~@ Gravity @~")]
    [SerializeField] bool useGravity = true;
    [SerializeField] Transform planet;

    Vector2 planet_pos;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake() =>
        self = this;

    //------------------------------------------------------------------------------------------------------------------------------

    private void FixedUpdate()
    {
        if (planet != null)
            planet_pos = planet.position;

        foreach (var prop in PhysicProp2D.selves)
            prop.OnFixedUpdate();
    }

    public Vector2 GetGravAtPoint(Vector2 point)
        => planet == null || !useGravity ? Vector2.up : (point - planet_pos).normalized;

    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDestroy()
    {
        if (this == self)
            self = null;
    }
}