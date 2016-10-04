using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(ScoreManager))]
public class BallController : MonoBehaviour
{
    public static int diamondScore = 10;
    public float speed = 8.0f;
    public float fallSpeed = 25f;
    public bool moving;
    public LayerMask groundLayer;
    public Transform diamondParticle;

    private Vector3 dir;
    private Rigidbody rb;
    private ScoreManager scoreManager;

    public void SwitchDirection()
    {
        dir = dir == Vector3.right ? Vector3.forward : Vector3.right;
    }

    public bool OnGround()
    {
        const float maxRayDistance = 5f;
        Debug.DrawRay(transform.position, Vector3.down*maxRayDistance, Color.red);
        bool onGround = Physics.Raycast(transform.position, Vector3.down, maxRayDistance, groundLayer);
        return onGround;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (groundLayer == 0)
        {
            groundLayer = LayerMask.GetMask("Ground");
        }
        scoreManager = GetComponent<ScoreManager>();
    }

    private void Start()
    {
        dir = Vector3.right;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (moving)
            {
                SwitchDirection();
            }
            else
            {
                moving = true;
                WorldManager.instance.StartGame();
            }
        }

        if (moving)
        {
            UpdateVelocity();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
            var part = Instantiate(diamondParticle, other.transform.position, diamondParticle.rotation) as Transform;
            System.Diagnostics.Debug.Assert(part != null, "[ASSERTION FAILED] part != null");
            Destroy(part.gameObject, 1f);
            scoreManager.IncrementScore(diamondScore);
        }
    }


    private void UpdateVelocity()
    {
        if (WorldManager.instance.GameOver)
        {
            rb.velocity = Vector3.down*fallSpeed;
        }
        else
        {
            rb.velocity = moving ? dir*speed : Vector3.zero;
        }
    }
}