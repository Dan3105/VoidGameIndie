using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AudioVfx
{
    public AudioClip audio;
    public string nameAudio;
}

public class SoundManager : SingletonTemplate<SoundManager>
{
    public AudioClip menuTheme;
    public AudioSource source;
    public AudioVfx[] list;
    public Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();
    private void Start()
    {
        foreach(var audio in list)
        {
            sounds[audio.nameAudio] = audio.audio;
        }
    }

    
}
