using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{   
    public SoundSettings[] sounds;
    // Start is called before the first frame update
    void Start()
    {
        foreach(SoundSettings s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLooping;
        }

        PlaySound("MainMenu Theme");
    }

    public void PlaySound(string name)
    {
        foreach(SoundSettings s in sounds){
            if(s.name == name){
                s.source.Play();
            }
        }
    }
}
