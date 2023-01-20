using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip laserAudioClip;
    [SerializeField] [Range(0f, 1f)] public float laserClipVolume = 1f;
    [SerializeField] public AudioClip powerupAudioClip;
    [SerializeField] [Range(0f, 1f)] public float powerupClipVolume = 1f;
    [SerializeField] public AudioClip enemyHitAudioClip;
    [SerializeField] [Range(0f, 1f)] public float enemyHitClipVolume = 1f;
    [SerializeField] public AudioClip shieldActivatedAudioClip;
    [SerializeField] [Range(0f, 1f)] public float shieldActivatedClipVolume = 1f;
    [SerializeField] public AudioClip shieldHitAudioClip;
    [SerializeField] [Range(0f, 1f)] public float shieldHitClipVolume = 1f;
    [SerializeField] public AudioClip superLaserAudioClip;
    [SerializeField] [Range(0f, 1f)] public float superLaserClipVolume = 1f;
    [SerializeField] public AudioClip dashAudioClip;
    [SerializeField] [Range(0f, 1f)] public float dashClipVolume = 1f;
}
