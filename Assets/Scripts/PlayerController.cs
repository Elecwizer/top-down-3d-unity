using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float Speed;
    void Start()
    {
        Speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }
}
