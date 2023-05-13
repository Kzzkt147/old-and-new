using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TipsController : MonoBehaviour
{
    [SerializeField] private GameObject tipsScreen;
    [SerializeField] private TextMeshProUGUI tipText;

    private Coroutine _typeCoroutine;

    public void NewTip(string tipDialogue, int textStayTime)
    {
        tipsScreen.SetActive(true);
        
        if(_typeCoroutine != null) StopCoroutine(_typeCoroutine);
        _typeCoroutine = StartCoroutine(TypeDialogue(tipDialogue, 20, textStayTime, () => tipsScreen.SetActive(false)));
    }

    public void NewTip(string tipDialogue)
    {
        NewTip(tipDialogue, 4);
    }

    public IEnumerator TypeDialogue(string tipDialogue, int lettersPerSecond, int textStayTime, Action onFinishedTyping = null)
    {
        tipText.text = "";
        
        foreach (var letter in tipDialogue)
        {
            tipText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        yield return new WaitForSeconds(textStayTime);
        onFinishedTyping?.Invoke();
    }
}
