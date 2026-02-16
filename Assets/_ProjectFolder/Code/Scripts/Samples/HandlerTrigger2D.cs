using UnityEngine;
using UnityEngine.Events;

public class HandlerTrigger2D : MonoBehaviour
{
    [SerializeField] private string _tag;
    [SerializeField] private UnityEvent _onTriggerEnter, _onTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(_tag)) return;
        _onTriggerEnter.Invoke();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(_tag)) return;
        _onTriggerExit.Invoke();
    }
}