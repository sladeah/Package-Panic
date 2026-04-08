using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PackagePool : NetworkBehaviour
{
    public static PackagePool Instance { get; private set; }

    [Header("Pool Settings")]
    public GameObject packagePrefab;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private float testSpawnTimer = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(packagePrefab);
            pool.Enqueue(obj);
        }
    }

    private void Update()
    {
        if (!IsServer) return;

        testSpawnTimer += Time.deltaTime;
        if (testSpawnTimer >= 3f)
        {
            SpawnPackageFromPool(new Vector3(0, 5, 0)); 
            testSpawnTimer = 0f;
        }
    }

    public GameObject SpawnPackageFromPool(Vector3 spawnPosition)
    {
        if (!IsServer || pool.Count == 0) return null;

        GameObject obj = pool.Dequeue();
        
        obj.transform.position = spawnPosition;
        
        obj.GetComponent<NetworkObject>().Spawn();

        return obj;
    }

    public void ReturnPackageToPool(GameObject obj)
    {
        if (!IsServer) return;

        obj.GetComponent<NetworkObject>().Despawn(false);
        
        pool.Enqueue(obj);
    }
}