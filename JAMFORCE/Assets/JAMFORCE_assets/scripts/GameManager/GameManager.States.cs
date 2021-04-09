using UnityEngine;

public partial class GameManager : IOnStateMachine
{
    public enum Layers { Base, Navigation, }

    public enum BaseStates
    {
        Init = 1715882826,
        Home = -773544978,
    }

    public enum NavigationStates
    {
        Hover = 991533264,
        toMenu = -966769575,
    }

    public enum Parameters
    {
        nav_f = -1439416509,
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

                    switch (state)
                    {

                    }

                    if (onEnter)
                        state_base = state;
                }
                break;
        }
    }
}