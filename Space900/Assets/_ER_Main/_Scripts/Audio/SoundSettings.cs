using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class SoundSettings

{
    public string name;
    public AudioClip clip;

    public float volume;
    public bool isLooping;
    public AudioSource source;
}
