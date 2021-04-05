using UnityEngine;

public partial class PlayerManager : IOnStateMachine
{
    public enum Layers { Base, }

    public enum BaseStates
    {
        Default = 753088835,
        Power = 1783312036,
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