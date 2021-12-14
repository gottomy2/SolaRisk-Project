using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager {

    public static void PlaySound(AudioClip audioClip) {
        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
    }    

}