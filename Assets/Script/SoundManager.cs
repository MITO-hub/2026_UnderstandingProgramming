using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip clipBGM;
    public AudioClip clipFx;

    AudioSource audioSourceFx;
    AudioSource audioSourceBGM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;

        audioSourceFx = gameObject.AddComponent<AudioSource>();
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
    }

    public void PlaySoundFx()
    {
        audioSourceFx.PlayOneShot(clipFx);
    }

    public void PlayBGM()
    {
        audioSourceBGM.clip = clipBGM;
        audioSourceBGM.loop = true;
        audioSourceBGM.Play();
    }

    public void OnOffBGM(bool isOn)
    {
        if(isOn)
        {
            audioSourceBGM.volume = 1;
        }
        else
        {
            audioSourceBGM.volume = 0;
        }
    }

    public void OnOffFx(bool isOn)
    {
        if (isOn)
        {
            audioSourceFx.volume = 1;
        }
        else
        {
            audioSourceFx.volume = 0;
        }
    }

    public void ChangeBGMVolume(float volume)
    {
        audioSourceBGM.volume = volume;
    }

    public void ChangeFxVolume(float volume)
    {
        audioSourceFx.volume = volume;
    }
}
