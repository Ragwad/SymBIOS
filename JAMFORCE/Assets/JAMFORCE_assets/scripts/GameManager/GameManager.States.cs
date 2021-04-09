using UnityEngine;

public partial class GameManager : IOnStateMachine
{
    public enum Layers { Base, Navigation, }

    public enum BaseStates
    {
        Init = 1715882826,
        Home = -773544978,
        Options = 529056539,
        toLoad = -1637639465,
        onLoad = -904773313,
        Gameplay = -238111021,
        Credits = -1901401886,
        toPause = -1399792579,
        Pause = 375111145,
    }

    public enum NavigationStates
    {
        Hover = 991533264,
    }

    public enum Parameters
    {
        nav_f = -1439416509,
    }

    [Header("~@ States @~")]
    public BaseStates state_base;

    //------------------------------------------------------------------------------------------------------------------------------

    void OnAnimatorEvent(AnimationEvent e)
    {
        switch (e.stringParameter)
        {
            case "load":
                StartCoroutine(ELoadScene());
                break;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------

    void IOnStateMachine.OnStateMachine(AnimatorStateInfo stateInfo, int layerIndex, bool onEnter)
    {
        switch ((Layers)layerIndex)
        {
            case Layers.Base:
                if (onEnter)
                    state_base = (BaseStates)stateInfo.shortNameHash;
                break;
        }
    }
}