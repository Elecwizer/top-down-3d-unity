using UnityEngine;

public class PooledBullet : MonoBehaviour
{
    private PlayerController _poolOwner;

    public void Init(PlayerController owner)
    {
        _poolOwner = owner;
    }

    void Update()
    {
        // Return to pool if out of bounds
        if (Mathf.Abs(transform.position.x) > 20 ||
            Mathf.Abs(transform.position.y) > 10 ||
            Mathf.Abs(transform.position.z) > 400)
        {
            ReturnToPool();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Return to pool when hitting something
        ReturnToPool();
    }

    void ReturnToPool()
    {
        if (_poolOwner != null)
            _poolOwner.ReturnBulletToPool(gameObject);
        else
            gameObject.SetActive(false); // fallback
    }
}
