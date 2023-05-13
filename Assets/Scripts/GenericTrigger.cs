using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class GenericTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrigger;
    [SerializeField] private bool destroyOnTrigger = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            onTrigger?.Invoke();
            
            if (destroyOnTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}
