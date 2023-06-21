using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap tilemap;  // Reference to the Tilemap component
    public TileBase sandTile;  // Reference to the sand tile
    public int width = 10;  // Width of the grid
    public int height = 10;  // Height of the grid
    public int overflowThreshold = 4;  // The threshold at which a tile overflows
    private SandTile[,] grid;  // The 2D array of sand tiles

    public Gradient sandColorGradient;

    void Start()
    {
        grid = new SandTile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new SandTile();
                tilemap.SetTile(new Vector3Int(x, y, 0), sandTile);
            }
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
            AddSand(cellPosition.x, cellPosition.y);
        }
    }

    public void AddSand(int x, int y)
    {
        grid[x, y].AddSand(1);

        if (grid[x, y].sandCount >= overflowThreshold)
        {
            Overflow(x, y);
        }

        float t = (float)grid[x, y].sandCount / overflowThreshold;
        Color sandColor = sandColorGradient.Evaluate(t);

        // Note that we're using 'SetTileFlags' to remove the 'LockColor' flag, 
        // which allows us to change the color of individual tiles
        tilemap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
        tilemap.SetColor(new Vector3Int(x, y, 0), sandColor);
    }

    private void Overflow(int x, int y)
    {
        grid[x, y].sandCount -= overflowThreshold;

        if (x > 0) AddSand(x - 1, y);  // Left neighbour
        if (x < width - 1) AddSand(x + 1, y);  // Right neighbour
        if (y > 0) AddSand(x, y - 1);  // Bottom neighbour
        if (y < height - 1) AddSand(x, y + 1);  // Top neighbour
    }
}