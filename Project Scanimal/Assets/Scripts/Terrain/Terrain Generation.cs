using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class TerrainGeneration : MonoBehaviour
{
    public int size;

    public GameObject[] GroundTiles;
    public GameObject Water;
    public GameObject Beach;
    public GameObject PlayerSpawnTile;
    [SerializeField] private GameObject m_TerrainHolder;
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject RockPrefab;
    [SerializeField] private GameObject FlowerPrefab;

    public float NoiseScale = .05f;
    public float treeDensity = .5f;
    public float RockDensity = .5f;
    public float FlowerDensity = .5f;

    public Cell[,] grid;
    public Cell[,] SavedGrid;

    public List<Foliage> foliage;
    public List<Foliage> savedFoliage;

    void Start()
    {
        SetUpGrid();
    }

    private void SetUpGrid()
    {
        grid = new Cell[size, size];
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                if (findSpawnPlayerTile(x, y))
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
                    Cell cell = ChoseGround(x, y);
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
        GenerateFoliage(grid);
    }

    private void DrawTerrainObject(Cell[,] grid)
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
                        var Ground = Instantiate(GroundTiles[0], new Vector3(x, 0, y), Quaternion.identity);
                        Ground.transform.parent = m_TerrainHolder.transform;
                        break;
                    case Cell.type.Ground1:
                        var Ground1 = Instantiate(GroundTiles[1], new Vector3(x, 0, y), Quaternion.identity);
                        Ground1.transform.parent = m_TerrainHolder.transform;
                        break;
                    case Cell.type.Ground2:
                        var Ground2 = Instantiate(GroundTiles[2], new Vector3(x, 0, y), Quaternion.identity);
                        Ground2.transform.parent = m_TerrainHolder.transform;
                        break;
                    case Cell.type.Ground3:
                        var Ground3 = Instantiate(GroundTiles[3], new Vector3(x, 0, y), Quaternion.identity);
                        Ground3.transform.parent = m_TerrainHolder.transform;
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

    private Cell ChoseGround(int x, int y)
    {
        float RandomTileNum = Random.Range(1, 100);
        if (RandomTileNum <= 40)
        {
            return new Cell(Cell.type.Ground);
        }
        else if (RandomTileNum <= 60)
        {
            return new Cell(Cell.type.Ground1);
        }
        else if (RandomTileNum <= 80)
        {
            return new Cell(Cell.type.Ground2);
        }
        else if (RandomTileNum <= 100)
        {
            return new Cell(Cell.type.Ground3);
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

    private void GenerateFoliage(Cell[,] grid)
    {
        float[,] noiseMap = new float[size, size];
        (float xOffset, float yOffset) = (Random.Range(-10000f, 10000f), Random.Range(-10000f, 10000f));
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float noiseValue = Mathf.PerlinNoise(x * NoiseScale + xOffset, y * NoiseScale + yOffset);
                noiseMap[x, y] = noiseValue;
            }
        }
        foliage = new List<Foliage>();
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                Cell cell = grid[x, y];
                if (cell.CurrentType != Cell.type.Water && cell.CurrentType != Cell.type.Beach)
                {
                    float T = Random.Range(0f, treeDensity);
                    if (noiseMap[x, y] < T)
                    {
                        GameObject prefab = treePrefab;
                        GameObject tree = Instantiate(prefab, transform);
                        tree.transform.position = new Vector3(x + Random.Range(-0.5f, 0.5f), 0.25f, y + Random.Range(-0.5f, .5f));
                        tree.transform.parent = m_TerrainHolder.transform;
                        WorldOverlayController treeCon = tree.GetComponent<WorldOverlayController>();
                        if (treeCon.colliding)
                        {
                            Destroy(tree);
                        }
                        Foliage treeData = new Foliage(Foliage.typeOfFoliage.Tree, tree.transform.position.x, tree.transform.position.y, tree.transform.position.z);
                        foliage.Add(treeData);
                    }

                    float R = Random.Range(0f, RockDensity);
                    if (noiseMap[x, y] < R)
                    {
                        GameObject prefab = RockPrefab;
                        GameObject rock = Instantiate(prefab, transform);
                        rock.transform.position = new Vector3(x + Random.Range(-0.5f, 0.5f), 0, y + Random.Range(-0.5f, .5f));
                        rock.transform.parent = m_TerrainHolder.transform;
                        WorldOverlayController rockCon = rock.GetComponent<WorldOverlayController>();
                        if (rockCon.colliding)
                        {
                            Destroy(rock);
                        }
                        Foliage rockData = new Foliage(Foliage.typeOfFoliage.Rock, rock.transform.position.x, rock.transform.position.y, rock.transform.position.z);
                        foliage.Add(rockData);
                    }

                    float F = Random.Range(0f, FlowerDensity);
                    if (noiseMap[x, y] < F)
                    {
                        GameObject prefab = FlowerPrefab;
                        GameObject flower = Instantiate(prefab, transform);
                        flower.transform.position = new Vector3(x + Random.Range(-0.5f, 0.5f), 0, y + Random.Range(-0.5f, .5f));
                        flower.transform.parent = m_TerrainHolder.transform;
                        WorldOverlayController flowerCon = flower.GetComponent<WorldOverlayController>();
                        if (flowerCon.colliding)
                        {
                            Destroy(flower);
                        }
                        Foliage flowerData = new Foliage(Foliage.typeOfFoliage.Flower, flower.transform.position.x, flower.transform.position.y, flower.transform.position.z);
                        foliage.Add(flowerData);
                    }

                }
            }
        }
    }

    private void LoadFoliage(List<Foliage> foliages)
    {
        foreach (Foliage foliageData in foliages)
        {
            if (foliageData.CurrentType == Foliage.typeOfFoliage.Tree)
            {
                GameObject prefab = treePrefab;
                GameObject tree = Instantiate(prefab, new Vector3(foliageData.pos[0], foliageData.pos[1], foliageData.pos[2]), Quaternion.identity);
                tree.transform.parent = m_TerrainHolder.transform;
            }

            if (foliageData.CurrentType == Foliage.typeOfFoliage.Rock)
            {
                GameObject rockprefab = RockPrefab;
                GameObject rock = Instantiate(rockprefab, new Vector3(foliageData.pos[0], foliageData.pos[1], foliageData.pos[2]), Quaternion.identity);
                rock.transform.parent = m_TerrainHolder.transform;
            }

            if (foliageData.CurrentType == Foliage.typeOfFoliage.Flower)
            {
                GameObject floweprefab = FlowerPrefab;
                GameObject flower = Instantiate(floweprefab, new Vector3(foliageData.pos[0], foliageData.pos[1], foliageData.pos[2]), Quaternion.identity);
                flower.transform.parent = m_TerrainHolder.transform;
            }
        }
    }

    public void OnClickRegenTerrain()
    {
        foreach (Transform child in m_TerrainHolder.transform)
        {
            Destroy(child.gameObject);
        }
        SetUpGrid();
    }

    public void LoadSavedMap()
    {
        foreach (Transform child in m_TerrainHolder.transform)
        {
            Destroy(child.gameObject);
        }

        DrawTerrainObject(SavedGrid);
        LoadFoliage(savedFoliage);
    }
}