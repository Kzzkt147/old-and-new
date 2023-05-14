using UnityEngine;

public class TimeSwitchNoise : MonoBehaviour
{
    [SerializeField] private PlayerTimeSwitch playerTimeSwitch;
    [SerializeField] private AudioSource audioSource;

    private void PlayNoise(float cooldown)
    {
        audioSource.Play();
    }

    private void OnEnable()
    {
        playerTimeSwitch.OnTimeSwitch += PlayNoise;
    }

    private void OnDisable()
    {
        playerTimeSwitch.OnTimeSwitch -= PlayNoise;
    }
}
