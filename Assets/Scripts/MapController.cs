using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;

    public float checkerRadius;
    public LayerMask terrainMask;
    Vector3 noTerrainPosition;
    public GameObject currentChunk;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    public GameObject latestChunk;
    public float maxOpDist;
    float opDist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }
    void ChunkChecker() 
    {
        if (!currentChunk)
        {
            return;
        }
        if (Input.GetKey(KeyCode.D))//right
        {
            ChunkCheckerHelper("UpRight", "Right", "DownRight");
        }
        else if (Input.GetKey(KeyCode.A))//left
        {
            ChunkCheckerHelper("UpLeft", "Left", "DownLeft");
        }
        if(Input.GetKey(KeyCode.W))//up
        {
            ChunkCheckerHelper("UpLeft", "Up", "UpRight");
        }
        else if(Input.GetKey(KeyCode.S))//down
        {
            ChunkCheckerHelper("DownLeft", "Down", "DownRight");
        }
    }
    void ChunkCheckerHelper(string c1, string c2, string c3) 
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(c1).position, checkerRadius, terrainMask))
        {
            noTerrainPosition = currentChunk.transform.Find(c1).position;
            SpawnChunk();
        }
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(c2).position, checkerRadius, terrainMask))
        {
            noTerrainPosition = currentChunk.transform.Find(c2).position;
            SpawnChunk();
        }
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(c3).position, checkerRadius, terrainMask))
        {
            noTerrainPosition = currentChunk.transform.Find(c3).position;
            SpawnChunk();
        }
    }
    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        latestChunk.transform.parent = gameObject.transform;
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }

        }
    }
}
