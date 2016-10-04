using System;
using UnityEngine;
using Debug = System.Diagnostics.Debug;
using Random = UnityEngine.Random;

public class PlatformSpawnner : MonoBehaviour
{
    public static PlatformSpawnner instance;

    public Transform currentPlatform; // last platform to duplicate from

    [Tooltip("Lower threshold favor right direction, otherwise favor forward")]
    [Range(0, 1)]
    public float randomThreshold = 0.5f;

    public float currentScale = 2.3f;

    [Tooltip("Maximum Subtract/Add to the currentScale for different platform sizes")]
    public float scaleVariation = 0.5f;

    public int startSpawns = 15;
    public Transform diamond;

    [Range(0, 1)]
    public float diamondSpawnChance = 0.25f;

    private int spawnedCount;

    public void SpawnPlatform(Vector3 scale)
    {
        currentPlatform = Instantiate(currentPlatform, transform.position, Quaternion.identity) as Transform;
        Debug.Assert(currentPlatform != null, "[FAILED ASSERTION] currentPlatform != null");
        currentPlatform.localScale = scale;
        currentPlatform.name = "Platform " + spawnedCount;
        spawnedCount++;
    }

    public void SnappingMove(Vector3 direction, Vector3 newScale)
    {
        Vector3 lastScale = currentPlatform.lossyScale;
        Vector3 pos = transform.position;
        if (direction == Vector3.right)
        {
            pos.x += lastScale.x/2 + newScale.x/2;
        }
        else if (direction == Vector3.forward)
        {
            pos.z += lastScale.z/2 + newScale.z/2;
        }
        else
        {
            throw new ArgumentException("Direction should be right or forward only.");
        }
        transform.position = pos;
    }

    public void MoveAndSpawn(Vector3 direction, float scaleX, float scaleZ)
    {
        Vector3 newScale = Vector3.zero;
        newScale.x = scaleX;
        newScale.y = currentPlatform.lossyScale.y; // do not scale y axis
        newScale.z = scaleZ;
        SnappingMove(direction, newScale);
        SpawnPlatform(newScale);
    }

    // threshold is in range [0, 1]
    // low threshold = favor right direction
    // otherwise, favor forward direction
    public void RandomMoveAndSpawn()
    {
        // making random threshold to be in range [.25, .75]
        randomThreshold = (Mathf.Sin(Time.timeSinceLevelLoad) + 1)/4 + 0.25f;

        // spawn platform
        float chance = Random.Range(0f, 1f);
        Vector3 dir = chance >= randomThreshold ? Vector3.right : Vector3.forward;
        float variation = Random.Range(-scaleVariation, +scaleVariation);
        float variation2 = Random.Range(-scaleVariation, +scaleVariation);
        MoveAndSpawn(dir, currentScale + variation, currentScale + variation2);

        // spawn diamond
        float diamondChance = Random.Range(0f, 1f);
        if (diamondChance <= diamondSpawnChance)
        {
            SpawnDiamond();
        }
    }

    public void SpawnDiamond()
    {
        var diamondTransform = Instantiate(diamond, transform.position + Vector3.up, diamond.rotation) as Transform;
        Debug.Assert(diamondTransform != null, "[ASSERTION FAILED] diamondTransform != null");
        currentPlatform.GetComponentInChildren<FallOnTriggerExit>().childDiamond =
            diamondTransform.GetComponent<Rigidbody>();
//        Destroy(diamondTransform.gameObject, 10f);
    }

    public void InitSpawn()
    {
        for (var i = 0; i < startSpawns; i++)
        {
            RandomMoveAndSpawn();
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        transform.position = currentPlatform.position;
    }
}