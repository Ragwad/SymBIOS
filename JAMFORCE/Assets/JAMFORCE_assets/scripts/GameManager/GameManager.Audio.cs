using UnityEngine;

public partial class GameManager
{
    public AudioClip[] grass_steps, clips_jump, clips_opencape;

    //------------------------------------------------------------------------------------------------------------------------------

    public void PlayAudio(AudioSource source, params AudioClip[] clips)
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Stop();
        source.Play();
    }
}