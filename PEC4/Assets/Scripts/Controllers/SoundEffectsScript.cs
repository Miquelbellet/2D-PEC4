using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundEffects;
    private AudioSource effectsAudioSource;
    void Start()
    {
        effectsAudioSource = GetComponent<AudioSource>();
    }

    public void CheckpointActivateSound()
    {
        effectsAudioSource.PlayOneShot(soundEffects[0]);
    }
    public void CoinSound()
    {
        effectsAudioSource.PlayOneShot(soundEffects[1]);
    }
    public void EnemieDieSound()
    {
        effectsAudioSource.PlayOneShot(soundEffects[2]);
    }
    public void FoodSound()
    {
        effectsAudioSource.PlayOneShot(soundEffects[3]);
    }
    public void AttackSound()
    {
        effectsAudioSource.PlayOneShot(soundEffects[4]);
    }
    public void JumpSound()
    {
        effectsAudioSource.PlayOneShot(soundEffects[5]);
    }
    public void HitSound()
    {
        effectsAudioSource.PlayOneShot(soundEffects[6]);
    }
}
