using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection.Emit;
using System.Security.Cryptography;


/**
 * This class demonstrates the CaveGenerator on a Tilemap.
 * 
 * By: Erel Segal-Halevi
 * Since: 2020-12
 */

public class TilemapCaveGenerator: MonoBehaviour {
    public GameObject player;
    [SerializeField] Tilemap tilemap = null;

    [Tooltip("The tile that represents a wall (an impassable block)")]
    [SerializeField] TileBase wallTile = null;

    [Tooltip("The tile that represents a floor (a passable block)")]
    [SerializeField] TileBase floorTile = null;

    [Tooltip("The percent of walls in the initial random map")]
    [Range(0, 1)]
    [SerializeField] float randomFillPercent = 0.5f;

    [Tooltip("Length and height of the grid")]
    [SerializeField] int gridSize = 100;

    [Tooltip("How many steps do we want to simulate?")]
    [SerializeField] int simulationSteps = 20;

    [Tooltip("For how long will we pause between each simulation step so we can look at the result?")]
    [SerializeField] float pauseTime = 1f;

    private CaveGenerator caveGenerator;
    private KeyboardMoverByTile keyboardMoverByTile;


    void Start()  {
        //To get the same random numbers each time we run the script
        //Random.InitState(100);
        UnityEngine.Random.InitState(100);

        caveGenerator = new CaveGenerator(randomFillPercent, gridSize);
        caveGenerator.RandomizeMap();

        //For testing that init is working
        GenerateAndDisplayTexture(caveGenerator.GetMap());

        //Start the simulation
        SimulateCavePattern();
    }


    //Do the simulation in a coroutine so we can pause and see what's going on
    async void SimulateCavePattern()
    {
        int counter = 0;
        float x = 0;
        float y = 0;
        while (counter < 100 || caveGenerator.GetMap()[(int)x, (int)y] == 1)
        {

            caveGenerator.RandomizeMap();
            System.Random random = new System.Random();
            x = RandomNumberGenerator.GetInt32(0, gridSize-1); // 1 to 100
            Console.WriteLine(x);
            y = RandomNumberGenerator.GetInt32(0, gridSize - 1); // 1 to 100
            Console.WriteLine(y);
            player.transform.position = new Vector3((float)(x+0.5), (float)(y+0.5), 0);

            for (int i = 0; i < simulationSteps; i++)
            {
                await Awaitable.WaitForSecondsAsync(pauseTime);

                //Calculate the new values
                caveGenerator.SmoothMap();

                //Generate texture and display it on the plane
                GenerateAndDisplayTexture(caveGenerator.GetMap());
            }
            
            counter = CountReachableZeros((int)x, (int)y);
           
        }
        Debug.Log("Simulation completed!");
    }

    

    public int CountReachableZeros(int startX, int startY)
    {
        int[,] caveMap = caveGenerator.GetMap();

        int reachableZeroCount = 0;
        Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
        bool[,] visited = new bool[gridSize, gridSize];

        queue.Enqueue((startX, startY));
        visited[startX, startY] = true;

        while (queue.Count > 0)
        {
            var (x, y) = queue.Dequeue();
            reachableZeroCount++;

            foreach (var (nx, ny) in GetNeighbors(x, y))
            {
                if (nx >= 0 && ny >= 0 && nx < gridSize && ny < gridSize && !visited[nx, ny] && caveMap[nx, ny] == 0)
                {
                    queue.Enqueue((nx, ny));
                    visited[nx, ny] = true;
                }
            }
        }

        return reachableZeroCount;
    }
    

    private List<(int x, int y)> GetNeighbors(int x, int y)
    {
        return new List<(int x, int y)> {
            (x + 1, y),
            (x - 1, y),
            (x, y + 1),
            (x, y - 1)
        };
    }




//Generate a black or white texture depending on if the pixel is cave or wall
//Display the texture on a plane
private void GenerateAndDisplayTexture(int[,] data) {
        for (int y = 0; y < gridSize; y++) {
            for (int x = 0; x < gridSize; x++) {
                var position = new Vector3Int(x, y, 0);
                var tile = data[x, y] == 1 ? wallTile: floorTile;
                tilemap.SetTile(position, tile);
            }
        }
    }

}
