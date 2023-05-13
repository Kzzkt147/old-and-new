using UnityEngine;

public class DeathObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        col.gameObject.TryGetComponent<PlayerDeath>(out var playerDeath);
        if(playerDeath) playerDeath.Die();
    }
}
