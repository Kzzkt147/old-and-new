using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private Vector2 respawnPosition;
    [SerializeField] private float deathTime = 2f;

    private SpriteRenderer _spriteRenderer;
    private Coroutine _deathCoroutine;

    public void Die()
    {
        if(_deathCoroutine != null) StopCoroutine(_deathCoroutine);
        _deathCoroutine = StartCoroutine(StartDeathTimer());
    }

    private IEnumerator StartDeathTimer()
    {
        GameManager.Instance.PauseGame();
        _spriteRenderer.enabled = false;
        yield return new WaitForSeconds(deathTime);
        transform.position = respawnPosition;
        _spriteRenderer.enabled = true;
        GameManager.Instance.ResumeGame();
    }

    private void Start()
    {
        respawnPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
