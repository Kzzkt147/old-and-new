using System;
using System.Collections;
using UnityEngine;

public class PlayerTimeSwitch : MonoBehaviour
{
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private AudioSource potionAudio;
    private bool _canSwitchTime = true;
    private Coroutine _cooldownCoroutine;

    public event Action<float> OnTimeSwitch;
    
    public void SwitchTime()
    {
        if (!_canSwitchTime) return;
        
        if(_cooldownCoroutine != null) StopCoroutine(SwitchCooldown());
        _cooldownCoroutine = StartCoroutine(SwitchCooldown());
        
        TimeSwitchController.ToggleTimePeriod();
        potionAudio.Play();
        OnTimeSwitch?.Invoke(cooldown);
    }

    private IEnumerator SwitchCooldown()
    {
        _canSwitchTime = false;
        yield return new WaitForSeconds(cooldown);
        _canSwitchTime = true;

    }
}
