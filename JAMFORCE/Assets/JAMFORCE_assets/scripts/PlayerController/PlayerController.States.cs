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

    void OnAnimatorEvent(AnimationEvent e)
    {
        switch (e.stringParameter)
        {
            case "step":
                {
                    AudioSource source = playerManager.sources[(int)PlayerManager.AudioSources.steps];

                    source.clip = GameManager.self.grass_steps[Random.Range(0, GameManager.self.grass_steps.Length)];
                    source.Stop();
                    source.Play();
                }
                break;

            default:
                print("error: " + e.stringParameter);
                break;
        }
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
                        case BaseStates.Fly:
                            if (onEnter)
                            {
                                GameManager.self.PlayAudio(playerManager.sources[(int)PlayerManager.AudioSources.air], GameManager.self.clips_opencape);
                                playerManager.sources[(int)PlayerManager.AudioSources.cape].Play();
                            }
                            else
                                playerManager.sources[(int)PlayerManager.AudioSources.cape].Stop();
                            break;
                    }

                    if (onEnter)
                        state_base = state;
                }
                break;
        }

        if (onEnter)
            state_last = Time.time;
    }
}