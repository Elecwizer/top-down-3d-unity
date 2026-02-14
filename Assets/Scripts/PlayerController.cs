using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    // Movement variables
    [SerializeField] float _speed;
    [SerializeField] float _turnSpeed;

    // Shooting variables
    [SerializeField] GameObject _bullet;
    Vector3 _bulletOffset = new Vector3(-0.002f, 3.306f, 4.834f);
    int _poolSize = 20;
    [SerializeField] float _bulletSpeed;
    readonly Queue<GameObject> _bulletPool = new Queue<GameObject>();

    void Start()
    {
        _speed = 20.0f;
        _turnSpeed = 25.0f;
        _bulletSpeed = 25.0f;

        // Filling pool
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject b = Instantiate(_bullet);
            b.transform.position = new Vector3(0f, -5f, 0f);
            b.SetActive(false);

            // Ensure bullet can return itself to THIS pool
            var pooled = b.GetComponent<PooledBullet>();
            if (pooled == null) pooled = b.AddComponent<PooledBullet>();
            pooled.Init(this);

            _bulletPool.Enqueue(b);
        }
    }

    // Update is called once per frame
    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        // Move or rotate the tank depending on input
        transform.Translate(Vector3.forward * Time.deltaTime * _speed * verticalInput);
        transform.Rotate(Vector3.up, Time.deltaTime * _turnSpeed * horizontalInput);

        // Fire when spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

        void FireBullet()
    {
        GameObject b = GetBulletFromPool();
        if (b == null) return;

        // Spawn at player position going forward
        b.transform.position = transform.TransformPoint(_bulletOffset); // small offset so it doesnt collide with player
        b.transform.rotation = transform.rotation;

        // Give it velocity in forward direction
        Rigidbody rb = b.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.linearVelocity = b.transform.forward * _bulletSpeed;
        }

        b.SetActive(true);
    }

    GameObject GetBulletFromPool()
    {
        if (_bulletPool.Count == 0) return null;
        var b = _bulletPool.Dequeue();
        return b;
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        // Reset physics just in case
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        bullet.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }

}
