using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager {

    public enum Sound {
        Jump,
        Score,
        Death
    }

    public static void PlaySound(Sound sound) {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
    }    

    private static AudioClip GetAudioClip(Sound sound) {
        foreach (GameAssets.SoundAudioClip s in GameAssets.GetInstance().soundAudioClips) {
            if (s.sound == sound) {
                return s.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " couldn't be loaded'");
        return null;
    }

}
