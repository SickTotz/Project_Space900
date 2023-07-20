using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundSettings[] sounds;
    public float fadeInDuration = 1.0f;
    public float fadeOutDuration = 1.0f; 

    private Coroutine fadeCoroutine;

    void Start()
    {
        foreach (SoundSettings s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.isLooping;
        }

        PlaySoundWithFade("MainMenu Theme", fadeInDuration);
    }

    public void PlaySoundWithFade(string name, float fadeDuration)
    {
        foreach (SoundSettings s in sounds)
        {
            if (s.name == name)
            {
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                    fadeCoroutine = null;
                }

                s.source.volume = 0f; 
                s.source.Play();
                fadeCoroutine = StartCoroutine(FadeSound(s.source, fadeDuration, 0f, s.volume));
            }
        }
    }

    public void StopSoundWithFade(float fadeDuration)
    {
        foreach (SoundSettings s in sounds)
        {
            if (s.source.isPlaying)
            {
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                    fadeCoroutine = null;
                }

                fadeCoroutine = StartCoroutine(FadeSound(s.source, fadeDuration, s.source.volume, 0f));
            }
        }
    }

    private IEnumerator FadeSound(AudioSource audioSource, float duration, float startVolume, float targetVolume)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, timer / duration);
            yield return null;
        }

        // Se il volume di destinazione è zero, interrompi l'audio
        if (targetVolume == 0f)
        {
            audioSource.Stop();
        }

        audioSource.volume = targetVolume;
    }
}
