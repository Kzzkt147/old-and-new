using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Fader fader;

    private Coroutine _fadeCoroutine;
    
    public void ChangeScene(string sceneName)
    {
        if(_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(fader.FadeToBlack(callback: () => SceneManager.LoadScene(sceneName)));
        
    }

    private void Start()
    {
        if(_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(fader.FadeToClear());
    }
}
