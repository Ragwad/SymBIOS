using UnityEngine;

public class PlateForme : LevelProp
{
    enum States { idle_A, move_B, idle_B, move_A }

    [Header("~@ Settings @~")]
    [SerializeField] States state;
    [Min(0)] [SerializeField] float timer_idle = 1, timer_move = 2;
    [Range(0, 1)] [SerializeField] float force_move = .5f;
    [SerializeField] Transform A, B;

    float timer;

    //------------------------------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
    [ContextMenu(nameof(Setup))]
    private void Setup()
    {
        var p = new GameObject(name).transform;
        p.SetParent(transform.parent, false);
        p.SetPositionAndRotation(transform.position, transform.rotation);
        transform.SetParent(p, false);

        A = Setup("A");
        B = Setup("B");

        Transform Setup(string name)
        {
            var T = new GameObject(name).transform;
            T.SetParent(p, false);
            T.SetPositionAndRotation(transform.position, transform.rotation);
            return T;
        }
    }
#endif

    //------------------------------------------------------------------------------------------------------------------------------

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        timer += Time.fixedDeltaTime;

        Vector2 target = default;

        switch (state)
        {
            case States.idle_A:
                if (timer > timer_idle)
                {
                    timer -= timer_idle;
                    state++;
                }
                target = A.position;
                break;

            case States.move_B:
                if (timer > timer_move)
                {
                    timer -= timer_idle;
                    state++;
                }
                target = Vector2.Lerp(A.position, B.position, Mathf.SmoothStep(0, 1, Mathf.InverseLerp(0, timer_move, timer)));
                break;

            case States.idle_B:
                if (timer > timer_idle)
                {
                    timer -= timer_idle;
                    state++;
                }
                target = B.position;
                break;

            case States.move_A:
                if (timer > timer_move)
                {
                    timer -= timer_idle;
                    state = 0;
                }
                target = Vector2.Lerp(B.position, A.position, Mathf.SmoothStep(0, 1, Mathf.InverseLerp(0, timer_move, timer)));
                break;
        }

        target = target - rigidbody_pos - rigidbody_vlc;
        rigidbody.AddForce(force_move * target, ForceMode2D.Impulse);
    }
}