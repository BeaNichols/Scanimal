using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public int size;

    [SerializeField] public GameObject[] GroundTiles;
    [SerializeField] public GameObject Water;
    [SerializeField] public GameObject Beach;
    [SerializeField] public GameObject PlayerSpawnTile;
    [SerializeField] private GameObject m_TerrainHolder; 

    Cell[,] grid;

    void Start()
    {
        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (findSpawnPlayerTile(x,y))
                {
                    Cell cell = new Cell(Cell.type.Spawn);
                    grid[x, y] = cell;
                }
                else if (IsEdgeTile(x, y))
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
                switch (cell.CurrentType)
                {
                    case Cell.type.Water:
                        var cellObject = Instantiate(Water, new Vector3(x, 0, y), Quaternion.identity);
                        cellObject.transform.parent = m_TerrainHolder.transform;
                        break;
                    case Cell.type.Ground:
                        var cellObject1 = Instantiate(ChoseTile(GroundTiles), new Vector3(x, 0, y), Quaternion.identity);
                        cellObject1.transform.parent = m_TerrainHolder.transform;
                        break;
                    case Cell.type.Beach:
                        var cellObject2 = Instantiate(Beach, new Vector3(x, 0, y), Quaternion.identity);
                        cellObject2.transform.parent = m_TerrainHolder.transform;
                        break;
                    case Cell.type.Spawn:
                        var cellObject3 = Instantiate(PlayerSpawnTile, new Vector3(x, 0, y), Quaternion.identity);
                        cellObject3.transform.parent = m_TerrainHolder.transform;
                        break;
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

    private bool findSpawnPlayerTile(int x, int y)
    {
        var spawnTilePos = new int[size/2, size/ 2];

        if (x == size/2)
        {
            if (y == size / 2)
            {
                return true;
            }
        }
        return false;
    }
}