using UnityEngine;

public partial class PlayerManager : IOnStateMachine
{
    public enum Layers { Base, Jelly, }

    public enum BaseStates
    {
        Default = 753088835,
        Aim = 153482222,
    }

    public enum JellyStates
    {
        Default = 753088835,
        Jump = 125937960,
        Switch = 1749372462,
    }

    [Header("~@ States @~")]
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
    }
}