using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject Coin;
    public int coinAmount;
    private List<Vector3> SpawnedPositions = new List<Vector3>();
        public float SpawnRadius = 0.5f; // Radius for checking overlap


    // Start is called before the first frame update
    void Start()
    {
        BoxCollider BoxCollider = GetComponent<BoxCollider>();
        Vector3 BoxSize = BoxCollider.size;
        Vector3 BoxCenter = BoxCollider.center;


        for (int i = 0; i < coinAmount; i++)
        {
            CreateCoin(BoxSize, BoxCenter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void CreateCoin(Vector3 BoxSize, Vector3 BoxCenter)
    {
        

        bool PositionFound = false;

        // Try to find a valid position for the coin
        for (int attempts = 0; attempts < 100; attempts++)
        {
            Vector3 RandomPosition = new Vector3(
                Random.Range(-BoxSize.x / 2, BoxSize.x / 2) + BoxCenter.x,
                BoxCenter.y,
                Random.Range(-BoxSize.z / 2, BoxSize.z / 2) + BoxCenter.z
            );

            // Check if this position is too close to any already spawned coins
            if (IsPositionValid(RandomPosition))
            {
                SpawnedPositions.Add(RandomPosition); // Store the position
                Instantiate(Coin, RandomPosition, Quaternion.identity);
                PositionFound = true;
                break; // Exit the attempts loop
            }
        }

        
    }

    bool IsPositionValid(Vector3 Position)
    {
        foreach (Vector3 SpawnedPosition in SpawnedPositions)
        {
            if (Vector3.Distance(Position, SpawnedPosition) < SpawnRadius)
            {
                return false; // Position is too close to an existing coin
            }
        }
        return true; // Position is valid
    }
}
