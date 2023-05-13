using System;
using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    [Header("Connected Components")]
    [SerializeField] private Animator animator;
    
    void SwitchCharacter()
    {
        if (TimeSwitchController.TimePeriod == TimePeriod.Future)
        {
            animator.SetLayerWeight(animator.GetLayerIndex("Ancestor"), 0);
        }
        else
        {animator.SetLayerWeight(animator.GetLayerIndex("Ancestor"), 1);
        }
    }
    
    private void OnEnable()
    {
        TimeSwitchController.OnPeriodSwitch += SwitchCharacter;
    }

    private void OnDisable()
    {
        TimeSwitchController.OnPeriodSwitch -= SwitchCharacter;
    }
}
