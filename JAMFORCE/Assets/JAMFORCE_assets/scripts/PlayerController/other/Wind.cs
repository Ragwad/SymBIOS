using UnityEngine;

public class Wind : MonoBehaviour, IOnStateMachine
{
    public enum Layers { Base, }

    public enum BaseStates
    {
        Default = 753088835,
        Wind = -2126906432,
    }

    [HideInInspector] public Animator animator;

    //------------------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //------------------------------------------------------------------------------------------------------------------------------

    void IOnStateMachine.OnStateMachine(AnimatorStateInfo stateInfo, int layerIndex, bool onEnter)
    {
        switch ((Layers)layerIndex)
        {
            case Layers.Base:
                {
                    var state = (BaseStates)stateInfo.shortNameHash;

                    switch (state)
                    {
                        case BaseStates.Wind:
                            if (!onEnter)
                                Destroy(gameObject);
                            break;
                    }
                }
                break;
        }
    }
}