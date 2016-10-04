using UnityEngine;

public class FallOnTriggerExit : MonoBehaviour
{
    public Rigidbody parentBody;
    public float fallDelay = 0.10f;
    public float destroyDelay = 4f;
    public Rigidbody childDiamond;

    private void Awake()
    {
        if (parentBody == null)
        {
            parentBody = GetComponent<Rigidbody>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("StartFalling", fallDelay);
            PlatformSpawnner.instance.RandomMoveAndSpawn();
        }
    }

    private void StartFalling()
    {
        parentBody.isKinematic = false;
        Destroy(parentBody.gameObject, destroyDelay);
        if (childDiamond != null)
        {
            childDiamond.isKinematic = false;
            Destroy(childDiamond.gameObject, destroyDelay);
        }
    }
}