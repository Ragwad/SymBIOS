using UnityEngine;

public partial class PlayerManager
{
    public enum AudioSources { steps, air, cape, _last_ }

    public readonly AudioSource[] sources = new AudioSource[(int)AudioSources._last_];

    //------------------------------------------------------------------------------------------------------------------------------

    void InitAudio()
    {
        sources[(int)AudioSources.steps] = transform.Find("PhysicBody/Audios/steps").GetComponent<AudioSource>();
        sources[(int)AudioSources.air] = transform.Find("PhysicBody/Audios/air").GetComponent<AudioSource>();
        sources[(int)AudioSources.cape] = transform.Find("PhysicBody/Audios/cape").GetComponent<AudioSource>();
    }
}