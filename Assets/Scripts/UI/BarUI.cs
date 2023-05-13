using System;
using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    [SerializeField] private PlayerTimeSwitch playerTimeSwitch;
    [SerializeField] private Image barInside;

    private float _cooldownTime;
    private bool _isCooldown;
    private float _timer;

    public void StartCooldown(float cooldownTime)
    {
        _isCooldown = true;
        this._cooldownTime = cooldownTime;
        _timer = _cooldownTime;

        barInside.transform.localScale = new Vector3(1, 1, 1);
    }
    
    private void Update()
    {
        if (!_isCooldown) return;
            
        if (barInside.transform.localScale.y > 0)
        {
            _timer -= Time.deltaTime;

            barInside.transform.localScale = new Vector2(1, _timer / _cooldownTime);

            //overlayImage.localScale = Vector2.MoveTowards(overlayImage.localScale, new Vector2(1, 0), Time.deltaTime);
        }
        else
        {
            barInside.transform.localScale = new Vector2(1, 0);
            _isCooldown = false;
        }
    }

    private void OnEnable()
    {
        playerTimeSwitch.OnTimeSwitch += StartCooldown;
    }

    private void OnDisable()
    {
        playerTimeSwitch.OnTimeSwitch -= StartCooldown;
    }
}
