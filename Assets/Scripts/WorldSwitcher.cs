using UnityEngine;
using UnityEngine.Rendering;

public class WorldSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject futureWorld;
    [SerializeField] private GameObject pastWorld;

    [SerializeField] private Volume globalVolume;
    [SerializeField] private VolumeProfile futureProfile;
    [SerializeField] private VolumeProfile pastProfile;

    private void UpdateCurrentWorld()
    {
        if (TimeSwitchController.TimePeriod == TimePeriod.Past)
        {
            pastWorld.SetActive(true);
            futureWorld.SetActive(false);
            globalVolume.profile = pastProfile;
        }
        else
        {
            pastWorld.SetActive(false);
            futureWorld.SetActive(true);
            globalVolume.profile = futureProfile;
        }
    }

    private void OnEnable()
    {
        TimeSwitchController.OnPeriodSwitch += UpdateCurrentWorld;
    }

    private void OnDisable()
    {
        TimeSwitchController.OnPeriodSwitch -= UpdateCurrentWorld;
    }
}
