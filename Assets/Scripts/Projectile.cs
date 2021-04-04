using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private bool _isEnemyLaser = false;
    [SerializeField] private float _speed = 8f;
    private float _destroyOnYAxis = 7.5f;

    private void Update()
    {
        if (!_isEnemyLaser)
            MoveUp();
        else
            MoveDown();
    }

    private void MoveUp()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y >= _destroyOnYAxis)
        {
            if (transform.parent)
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -_destroyOnYAxis)
        {
            if (transform.parent)
                Destroy(transform.parent.gameObject);
            else
                Destroy(gameObject);
        }
    }

    public void IsEnemyLaser()
    {
        _isEnemyLaser = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _isEnemyLaser)
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
                player.Damage(1);

            Destroy(gameObject);
        }
    }
}
