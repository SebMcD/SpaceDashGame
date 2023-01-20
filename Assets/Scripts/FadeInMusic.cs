using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInMusic : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(audioSource.volume < 0.1f)
        {
            audioSource.volume += Time.deltaTime / 20f;
        }
        if(audioSource.volume >= 0.09f && audioSource.volume <= 0.099f)
        {
            audioSource.volume = 0.1f;
        }
    }
}
