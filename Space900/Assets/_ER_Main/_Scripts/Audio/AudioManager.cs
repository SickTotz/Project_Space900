using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public SoundSettings[] sounds;
    public float fadeInDuration = 1.0f; // Durata della transizione di volume in secondi
    public float fadeOutDuration = 1.0f; // Durata della transizione di volume in secondi

    private Coroutine fadeCoroutine; // Riferimento al coroutine per la transizione di volume

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
                // Interrompi la transizione di volume corrente, se presente
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                    fadeCoroutine = null;
                }

                // Avvia una nuova transizione di volume
                s.source.volume = 0f; // Imposta il volume iniziale a 0
                s.source.Play(); // Avvia la riproduzione audio
                fadeCoroutine = StartCoroutine(FadeSound(s.source, fadeDuration, 0f, s.volume)); // Esegui il fade-in
            }
        }
    }

    public void StopSoundWithFade(float fadeDuration)
    {
        foreach (SoundSettings s in sounds)
        {
            if (s.source.isPlaying)
            {
                // Interrompi la transizione di volume corrente, se presente
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                    fadeCoroutine = null;
                }

                // Avvia una nuova transizione di volume per interrompere gradualmente l'audio
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
