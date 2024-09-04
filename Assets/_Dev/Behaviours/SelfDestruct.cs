using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float LifeTime;

    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        var timeAlive = Time.time - _startTime;

        if(timeAlive > LifeTime)
        {
            Destroy(gameObject);
        }
    }
}