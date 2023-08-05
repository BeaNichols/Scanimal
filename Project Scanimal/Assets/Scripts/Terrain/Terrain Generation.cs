using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public Material terrainMaterial;
    public int size = 100;
    public GameObject[] GroundTiles;
    public GameObject Water;
    public GameObject Beach;
    [SerializeField]
    private GameObject m_TerrainHolder; 
    Cell[,] grid;

    void Start()
    {
        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (IsEdgeTile(x, y))
                {
                    Cell cell = new Cell(Cell.type.Water);
                    grid[x, y] = cell;
                }
                else if (!IsEdgeTile(x, y))
                {
                    Cell cell = new Cell(Cell.type.Ground);
                    grid[x, y] = cell;
                }
                if (GetBeachTiles(x, y) == true)
                {
                    Cell cell = new Cell(Cell.type.Beach);
                    grid[x, y] = cell;
                }
            }
        }
        DrawTerrainObject(grid);
    }

    void DrawTerrainObject(Cell[,] grid)
    {
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (cell.CurrentType == Cell.type.Water)
                {
                    var cellObject = Instantiate(Water, new Vector3(x, 0 ,y), Quaternion.identity);
                    cellObject.transform.parent = m_TerrainHolder.transform;
                }
                else if(cell.CurrentType == Cell.type.Ground)
                {
                    var cellObject = Instantiate(ChoseTile(GroundTiles), new Vector3(x, 0, y), Quaternion.identity);
                    cellObject.transform.parent = m_TerrainHolder.transform;
                }
                else if (cell.CurrentType == Cell.type.Beach)
                {
                    var cellObject = Instantiate(Beach, new Vector3(x, 0, y), Quaternion.identity);
                    cellObject.transform.parent = m_TerrainHolder.transform;
                }
            }
        }
    }

    private bool IsEdgeTile(int x, int y)
    {
        if (x == 0 || y == 0)
        {
            return true;
        }

        if (x == size -1 || y == size -1 )
        {
            return true;
        }

        return false;
    }

    private bool GetBeachTiles(int x, int y)
    {
        if (grid[x, y].CurrentType == Cell.type.Water)
        {
            return false;
        }
        if (IsEdgeTile(x + 1, y + 1))
        {
            return true;
        }
        else if (IsEdgeTile(x - 1, y - 1))
        {
            return true;
        }

        return false;
    }

    private GameObject ChoseTile(GameObject[] ObjectList)
    {
        float RandomTileNum = Random.Range(1, 100);
        if (RandomTileNum <= 40)
        {
            return ObjectList[0];
        }
        else if (RandomTileNum <= 60)
        {
            return ObjectList[1];
        }
        else if (RandomTileNum <= 80)
        {
            return ObjectList[2];
        }
        else if (RandomTileNum <= 100)
        {
            return ObjectList[3];
        }

        return null;
    }
}
