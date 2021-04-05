using UnityEngine;

public partial class PlayerController : IOnStateMachine
{
    public enum Layers { Base, }

    public enum BaseStates
    {
        idle_left = -1222159098,
        idle_right = 504199673,
        move_left = 1947196380,
        move_right = 2026400445,
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