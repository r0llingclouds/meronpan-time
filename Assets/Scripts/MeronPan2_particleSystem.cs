using UnityEngine;
using System.Collections.Generic;

public class MeronPan2_particleSystem : MonoBehaviour
{
    [Header("Prefab Settings")]
    public GameObject prefabToSpawn;
    public int maxPrefabInstances = 20;

    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    
    [Header("Spawn Settings")]
    [Range(0.1f, 5f)]
    public float spawnRate = 0.5f;
    public float prefabLifetime = 10f;
    
    [Header("Spawn Area")]
    public float spawnAreaWidth = 10f;
    public float spawnAreaLength = 10f;
    public float height = 0f;
    
    [Header("Initial Rotation")]
    public bool randomInitialRotation = true;
    public float minRotationX = 0f;
    public float maxRotationX = 360f;
    public float minRotationY = 0f;
    public float maxRotationY = 360f;
    public float minRotationZ = 0f;
    public float maxRotationZ = 360f;
    
    [Header("Continuous Rotation")]
    public bool enableContinuousRotation = true;
    [Range(5f, 100f)]
    public float minRotationSpeed = 15f;
    [Range(5f, 100f)]
    public float maxRotationSpeed = 45f;
    
    [Header("Movement")]
    public bool enableMovement = true;
    public float movementSpeed = 0.5f;
    public Vector3 movementDirection = Vector3.forward;
    
    // Internal tracking
    private List<GameObject> activePrefabs = new List<GameObject>();
    private List<float> spawnTimes = new List<float>();
    private List<Vector3> rotationAxes = new List<Vector3>();
    private List<float> rotationSpeeds = new List<float>();
    private float nextSpawnTime = 0.5f;
    
    void Update()
    {
        // Remove expired prefabs
        RemoveExpiredPrefabs();
        
        // Spawn new prefabs at a gentle rate
        if (Time.time >= nextSpawnTime && activePrefabs.Count < maxPrefabInstances)
        {
            SpawnPrefab();
            nextSpawnTime = Time.time + (3f / spawnRate);
        }
        
        // Update prefabs
        UpdatePrefabs();
    }
    
    void SpawnPrefab()
    {
        // Calculate spawn position within the area
        float halfWidth = spawnAreaWidth / 2f;
        float halfLength = spawnAreaLength / 2f;
        
        float randomX = Random.Range(-halfWidth, halfWidth);
        float randomZ = Random.Range(-halfLength, halfLength);
        
        Vector3 localSpawnPosition = new Vector3(randomX, height, randomZ);
        Vector3 worldSpawnPosition = transform.TransformPoint(localSpawnPosition);
        
        // Generate initial random rotation
        Quaternion spawnRotation;
        if (randomInitialRotation)
        {
            float rotX = Random.Range(minRotationX, maxRotationX);
            float rotY = Random.Range(minRotationY, maxRotationY);
            float rotZ = Random.Range(minRotationZ, maxRotationZ);
            spawnRotation = Quaternion.Euler(rotX, rotY, rotZ);
        }
        else
        {
            spawnRotation = transform.rotation;
        }
        
        // Instantiate the prefab
        GameObject newPrefab = Instantiate(prefabToSpawn, worldSpawnPosition, spawnRotation);
        float scale = Random.Range(minScale, maxScale);
        newPrefab.transform.localScale = new Vector3(scale, scale, scale);
        // Generate random rotation axis for continuous rotation
        Vector3 randomAxis = Random.onUnitSphere; // Random direction on a unit sphere
        float rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        
        // Add to tracking lists
        activePrefabs.Add(newPrefab);
        spawnTimes.Add(Time.time);
        rotationAxes.Add(randomAxis);
        rotationSpeeds.Add(rotationSpeed);
    }
    
    void UpdatePrefabs()
    {
        for (int i = 0; i < activePrefabs.Count; i++)
        {
            GameObject prefab = activePrefabs[i];
            if (prefab != null)
            {
                // Apply continuous rotation if enabled
                if (enableContinuousRotation)
                {
                    Vector3 rotationAxis = rotationAxes[i];
                    float rotationSpeed = rotationSpeeds[i];
                    
                    prefab.transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
                }
                
                // Apply movement if enabled
                if (enableMovement)
                {
                    prefab.transform.Translate(
                        movementDirection.normalized * movementSpeed * Time.deltaTime, 
                        Space.World
                    );
                }
            }
        }
    }
    
    void RemoveExpiredPrefabs()
    {
        for (int i = activePrefabs.Count - 1; i >= 0; i--)
        {
            if (Time.time - spawnTimes[i] >= prefabLifetime || activePrefabs[i] == null)
            {
                if (activePrefabs[i] != null)
                    Destroy(activePrefabs[i]);
                    
                activePrefabs.RemoveAt(i);
                spawnTimes.RemoveAt(i);
                rotationAxes.RemoveAt(i);
                rotationSpeeds.RemoveAt(i);
            }
        }
    }
    
    // Cleanup when the script is disabled or destroyed
    void OnDisable()
    {
        foreach (GameObject prefab in activePrefabs)
        {
            if (prefab != null)
            {
                Destroy(prefab);
            }
        }
        
        activePrefabs.Clear();
        spawnTimes.Clear();
        rotationAxes.Clear();
        rotationSpeeds.Clear();
    }
    
    // Visualize the spawn area in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(new Vector3(0, height, 0), new Vector3(spawnAreaWidth, 0.1f, spawnAreaLength));
    }
}