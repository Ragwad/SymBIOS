using UnityEngine;
using UnityEngine.Audio;

public partial class GameManager
{
    enum AudioParameters { volume_general, volume_musique, volume_bruitages }

    [Header("~@ Audio @~")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] OnValue<Vector3> audio_volumes;
    public AudioClip[] grass_steps, ice_steps, rock_steps, water_steps, clips_jump, clips_opencape;

    //------------------------------------------------------------------------------------------------------------------------------

    void InitAudio()
    {
        audio_volumes = new OnValue<Vector3>(.8f * Vector3.one, delegate (Vector3 value)
        {
            for (int i = 0; i < 3; i++)
                audioMixer.SetFloat("" + (AudioParameters)i, Mathf.Lerp(-80, 20, value[i]));
        });
    }

    //------------------------------------------------------------------------------------------------------------------------------

    public void PlayAudio(AudioSource source, params AudioClip[] clips)
    {
        source.clip = clips[Random.Range(0, clips.Length)];
        source.Stop();
        source.Play();
    }
}