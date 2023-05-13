using UnityEngine;
using UnityEngine.Audio;

public class AudioSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioMixerGroup pastSound;
    [SerializeField] private AudioMixerGroup futureSound;

    private void SwitchAudio()
    {
        if (TimeSwitchController.TimePeriod == TimePeriod.Future)
        {
            audioSource.outputAudioMixerGroup = futureSound;
        }
        else
        {
            audioSource.outputAudioMixerGroup = pastSound;
        }
    }
    
    private void OnEnable()
    {
        TimeSwitchController.OnPeriodSwitch += SwitchAudio;
    }

    private void OnDisable()
    {
        TimeSwitchController.OnPeriodSwitch -= SwitchAudio;
    }
}
