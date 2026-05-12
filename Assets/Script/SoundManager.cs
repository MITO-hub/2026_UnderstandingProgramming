using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip clipBGM;
    public AudioClip clipFx;

    AudioSource audioSourceFx;
    AudioSource audioSoureceBGM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;

        audioSourceFx = gameObject.AddComponent<AudioSource>();
        audioSoureceBGM = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySoundFx()
    {
        audioSourceFx.PlayOneShot(clipFx);
    }

    public void PlayBGM()
    {
        audioSoureceBGM.clip = clipBGM;
        audioSoureceBGM.loop = true;
        audioSoureceBGM.Play();
    }
}
