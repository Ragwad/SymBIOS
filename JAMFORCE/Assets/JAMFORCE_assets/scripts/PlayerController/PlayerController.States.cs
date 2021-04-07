using UnityEngine;

public partial class PlayerController : IOnStateMachine
{
    public enum Layers { Base, }

    public enum BaseStates
    {
        Move = 1326225478,
        Idle = 2081823275,
        JumpUp = 608663733,
        Fly = 1808254291,
        JumpDown = -1289821550,
        Power = 1783312036,
        PowerDown = -218250187,
    }

    public enum Parameters
    {
        aim_a = 1674008911,
    }

    [Header("~@ States @~")]
    public float state_last;
    public BaseStates state_base;

    //------------------------------------------------------------------------------------------------------------------------------

    void IOnStateMachine.OnStateMachine(AnimatorStateInfo stateInfo, int layerIndex, bool onEnter)
    {
        switch ((Layers)layerIndex)
        {
            case Layers.Base:
                {
                    var state = (BaseStates)stateInfo.shortNameHash;

                    if (onEnter)
                        state_base = state;
                }
                break;
        }

        if (onEnter)
            state_last = Time.time;
    }
}