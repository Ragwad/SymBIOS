using System.Collections.Generic;
using UnityEngine;

public class LevelProp : MonoBehaviour
{
    public static readonly List<LevelProp> selves = new List<LevelProp>();

    [HideInInspector] public new Rigidbody2D rigidbody;

    [Header("~@ Physics @~")]
    [Min(0)] [SerializeField] int grav_force = 15;
    [HideInInspector] public Vector2 rigidbody_pos, rigidbody_vlc, grav_n;

    //------------------------------------------------------------------------------------------------------------------------------

    protected virtual void Awake()
    {
        selves.Add(this);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    //------------------------------------------------------------------------------------------------------------------------------

    public virtual void OnFixedUpdate()
    {
        rigidbody_pos = rigidbody.position;
        rigidbody_vlc = rigidbody.velocity;
        grav_n = LevelManager.self.GetGravAtPoint(rigidbody_pos);

        if (!rigidbody.isKinematic && grav_force != 0)
            rigidbody.AddForce(grav_force * -grav_n * rigidbody.mass, ForceMode2D.Force);
    }

    //------------------------------------------------------------------------------------------------------------------------------

    protected virtual void OnDestroy()
        => selves.Remove(this);
}