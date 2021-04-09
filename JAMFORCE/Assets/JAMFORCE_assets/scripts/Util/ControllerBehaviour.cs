using UnityEngine;

public abstract class ControllerBehaviour : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    //------------------------------------------------------------------------------------------------------------------------------

    public virtual void OnAwake()
    {
        animator = GetComponent<Animator>();
    }
}