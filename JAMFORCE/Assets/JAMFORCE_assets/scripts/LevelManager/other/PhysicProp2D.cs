using System.Collections.Generic;
using UnityEngine;

public partial class PhysicProp2D : MonoBehaviour
{
    public static readonly List<PhysicProp2D> selves = new List<PhysicProp2D>();

    [HideInInspector] public new Rigidbody2D rigidbody;

    [Header("~@ Physics @~")]
    [Min(0)] [SerializeField] int grav_force = 15;

    [HideInInspector] public Vector2 rigidbody_pos, grav_n;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        selves.Add(this);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    //------------------------------------------------------------------------------------------------------------------------------

    public void OnFixedUpdate()
    {
        rigidbody_pos = rigidbody.position;
        grav_n = (rigidbody_pos - LevelManager.self.planet_pos).normalized;

        rigidbody.AddForce(grav_force * -grav_n * rigidbody.mass, ForceMode2D.Force);
    }

    //------------------------------------------------------------------------------------------------------------------------------

    private void OnDestroy() 
        => selves.Remove(this);
}