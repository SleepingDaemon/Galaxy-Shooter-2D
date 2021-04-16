using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private EnemyMovement _movement = null;

    private void Awake()
    {
        _movement = GetComponentInParent<EnemyMovement>();
        if (_movement == null)
            Debug.Log("EnemyMovement is null");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7)
        {
            Debug.Log(other.gameObject.name);
            _movement.SetMovementType(EnemyMovemenType.Aggressive);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _movement.SetMovementType(EnemyMovemenType.Default);
    }
}