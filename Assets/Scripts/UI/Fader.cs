using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Fader : MonoBehaviour
{
    private Image _image;

    public IEnumerator FadeToBlack(float duration = 2f, Action callback = null)
    {
        var elapsedTime = 0.0f;
        var color = _image.color;
        
        while (elapsedTime < duration)
        {
            yield return new YieldInstruction();
            elapsedTime += Time.deltaTime ;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            _image.color = color;
        }
        
        callback?.Invoke();
    }

    public IEnumerator FadeToClear(float duration = 2f, Action callback = null)
    {
        var elapsedTime = 0.0f;
        var color = _image.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return new YieldInstruction();
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / duration);
            _image.color = color;
        }
        
        callback?.Invoke();
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
}
