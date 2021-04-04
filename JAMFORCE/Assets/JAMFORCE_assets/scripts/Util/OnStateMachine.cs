using UnityEngine;

interface IOnStateMachine { void OnStateMachine(AnimatorStateInfo stateInfo, int layerIndex, bool onEnter); }

public class OnStateMachine : StateMachineBehaviour
{
    [SerializeField] bool defaut;

    //------------------------------------------------------------------------------------------------------------------------------

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try { animator.GetComponent<IOnStateMachine>().OnStateMachine(stateInfo, layerIndex, true); }
        catch (System.Exception e) { Debug.Log(e); }

        if (defaut)
            animator.SetLayerWeight(layerIndex, 0);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        try { animator.GetComponent<IOnStateMachine>().OnStateMachine(stateInfo, layerIndex, false); }
        catch (System.Exception e) { Debug.Log(e); }

        if (defaut)
            animator.SetLayerWeight(layerIndex, 1);
    }
}