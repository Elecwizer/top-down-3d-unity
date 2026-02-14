using UnityEngine;

public class RockController : MonoBehaviour
{
    // Move speed by tag bigger = slower
    [SerializeField] float _smallRockSpeed = 18f;
    [SerializeField] float _rockSpeed = 12f;
    [SerializeField] float _bigRockSpeed = 8f;

    // Rocks leaving arena get destroyed
    [SerializeField] private float _minZ = -20f;
    [SerializeField] private float _maxAbsX = 40f;
    [SerializeField] private float _maxY = 40f;

    int _hp;
    float _speed;

    void Start()
    {
        // Set speed + HP based on tag
        if (CompareTag("Small_Rock"))
        {
            _hp = 1;
            _speed = _smallRockSpeed;
        }
        else if (CompareTag("Big_Rock"))
        {
            _hp = 3;
            _speed = _bigRockSpeed;
        }
        else // "Rock" tag
        {
            _hp = 2;
            _speed = _rockSpeed;
        }
    }

    void Update()
    {
        // Move toward player down the road
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.World);

        // Destroy if leaving arena
        Vector3 p = transform.position;
        if (p.z < _minZ || Mathf.Abs(p.x) > _maxAbsX || p.y > _maxY)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If rock hits player => game over
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("GAME OVER");
            return;
        }

        // Bullet hits rock => reduce HP
        if (collision.gameObject.CompareTag("Bullet"))
        {
            _hp--;

            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
