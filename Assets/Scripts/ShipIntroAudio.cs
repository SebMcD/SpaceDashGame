using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipIntroAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip shipIntroAudioClip;
    [SerializeField] [Range(0f, 1f)] float shipIntroVolume;
    [SerializeField] float seconds;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAudioClipAfterSeconds(seconds));
    }

    IEnumerator PlayAudioClipAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        audioSource.PlayOneShot(shipIntroAudioClip, shipIntroVolume);
    }
}
