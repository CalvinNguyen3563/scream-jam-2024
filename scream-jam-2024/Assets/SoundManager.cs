using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    /*
    public enum Sound
    {

    }

    private static Dictionary<Sound, float> soundTimerDictionary;


    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    }

    public static void PlaySound(Sound sound, Vector 3 position)
    {
        if (CanPlaySound((sound)) == null)
        {
            Gameobject soundGameObject = new Gameobject("Sound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play();
        }
    }

    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound((sound)) == null)
        {
            Gameobject soundGameObject = new Gameobject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
        }
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach ()
            if (soundAudioClip.sound == soiund)
            {
                return soundAudioClip.audioClip;
            }
        Debug.LogError("Sound" + sound + "not found!");
        return null;
    }

    */
}
