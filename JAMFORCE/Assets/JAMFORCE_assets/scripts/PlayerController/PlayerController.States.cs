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
        wind_f = 1041428444,
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

                    AudioClip GetClip(AudioClip[] clips)
                        => source.clip = clips[Random.Range(0, clips.Length)];

                    string mat = "";

                    if (isGround && ground_hit.collider != null && ground_hit.collider.sharedMaterial != null)
                        mat += ground_hit.collider.sharedMaterial.name;

                    switch (mat)
                    {
                        case "rock":
                            GetClip(GameManager.self.rock_steps);
                            break;

                        case "grass":
                            GetClip(GameManager.self.grass_steps);
                            break;

                        case "ice":
                            GetClip(GameManager.self.ice_steps);
                            break;

                        case "water":
                            GetClip(GameManager.self.water_steps);
                            break;

                        case "nenuphar":
                            break;

                        case "nuage":
                            break;

                        case "crystal":
                            break;
                    }

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