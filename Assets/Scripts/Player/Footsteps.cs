using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> footsteps;
    public void PlayFootstepAudio()
    {
        var clip = Random.Range(0, footsteps.Count);
        audioSource.PlayOneShot(footsteps[clip]);
    }
}
