using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject _player;

    Vector3 _offset = new Vector3(0, 7, -7);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
