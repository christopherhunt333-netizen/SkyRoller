using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject[] platformPrefabs;
    

    public float platformSize = 26f;
    public int radius = 10;
    private Dictionary<Vector3Int, GameObject> activePlatforms = new Dictionary<Vector3Int, GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RefreshGrid();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshGrid();
    }

    void RefreshGrid()
    {
        Vector3Int playerCoord = WorldToGrid(player.position);

        HashSet<Vector3Int> desired = new HashSet<Vector3Int>();
        
        
        for (int z = playerCoord.z - radius; z <= playerCoord.z + radius; z++)
        {
            desired.Add(new Vector3Int(0, 0, z));
        }
        

        List<Vector3Int> toRemove = new List<Vector3Int>();

        foreach (var kvp in activePlatforms)
        {
            if(!desired.Contains(kvp.Key))
            {
                toRemove.Add(kvp.Key);
            }
        }

        foreach (Vector3Int coord in toRemove)
        {
            Destroy(activePlatforms[coord]);
            activePlatforms.Remove(coord);
        }

        foreach (Vector3Int coord in desired)
        {
           
            if (!activePlatforms.ContainsKey(coord))
            {
                PlaceChunk(coord);
            }
                
        }
    }
    /*
    Vector3 GetPreviousPosition(Vector3Int coord)
    {

        if (activePlatforms.TryGetValue(coord - Vector3Int.forward, out GameObject previousPlatform))
        {
            PlatformData previousData = previousPlatform.GetComponent<PlatformData>();
            return previousData.GetFinalXPosition();
        }

        return new Vector3(0f, 0f, 0f);
    }
    */
    

    void PlaceChunk(Vector3Int coord)
    {
        
        int randomIndex = Random.Range(0, platformPrefabs.Length);
        GameObject platformPrefab = platformPrefabs[randomIndex];

        Vector3 platformCoordinate = GridToWorld(coord);

        // Vector3 previousPosition = GetPreviousPosition(coord) + new Vector3(offSets[Random.Range(0, 3)], 0f, 0f);
        // platformCoordinate +=  previousPosition;

        GameObject chunk = Instantiate(platformPrefab, platformCoordinate, Quaternion.identity);
        activePlatforms.Add(coord, chunk);
    }

    public Vector3Int WorldToGrid(Vector3 worldPos)
    {
        return new Vector3Int(
            Mathf. FloorToInt(worldPos.x / platformSize),
            0,
            Mathf. FloorToInt(worldPos.z / platformSize)
        );
    }

    Vector3 GridToWorld(Vector3Int coord)
    {
        return new Vector3(coord.x * platformSize, 0f, coord.z * platformSize);
    }

    public Dictionary<Vector3Int, GameObject> GetActivePlatforms()
    {
        return activePlatforms;
    }

    public bool IsChunkActive(Vector3Int coord)
    {
        return activePlatforms.ContainsKey(coord);
    }

}