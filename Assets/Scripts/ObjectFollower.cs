using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    public Transform target;

    [Tooltip("How fast to keep up to the target")]
    public float lerpRate = 5f;

    private Vector3 offset;

    // Use this for initialization
    private void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!WorldManager.instance.GameOver)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        Vector3 pos = target.position - offset;
        transform.position = Vector3.Lerp(transform.position, pos, lerpRate*Time.fixedDeltaTime);
    }
}