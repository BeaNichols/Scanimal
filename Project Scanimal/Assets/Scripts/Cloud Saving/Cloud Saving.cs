using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.CloudSave;
using Unity.VisualScripting;
using Newtonsoft.Json;

public class CloudSaving : MonoBehaviour
{

    public async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    public async void SaveMapData()
    {
        GameObject terrainGen = GameObject.Find("Terrain Generator");
        TerrainGeneration genarator = terrainGen.GetComponent<TerrainGeneration>();

        var mapData = new Dictionary<string, object> { { "MapData", genarator.grid } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(mapData);

    }

    public async void SaveFoliageData()
    {
        GameObject terrainGen = GameObject.Find("Terrain Generator");
        TerrainGeneration genarator = terrainGen.GetComponent<TerrainGeneration>();

        var foliageData = new Dictionary<string, object> { { "FoliageData", genarator.foliage } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(foliageData);
    }

    public async void LoadMapData()
    {
        GameObject terrainGen = GameObject.Find("Terrain Generator");
        TerrainGeneration genarator = terrainGen.GetComponent<TerrainGeneration>();

        var mapQuery = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "MapData" });
        if (mapQuery.ContainsKey("MapData"))
        {
            var stringData = mapQuery["MapData"];
            var deserialized = JsonConvert.DeserializeObject<Cell[,]>(stringData);
            genarator.SavedGrid = deserialized;
        }
        else
        {
            Debug.Log("key not found");
        }

        var foliageQuery = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "FoliageData" });
        if (foliageQuery.ContainsKey("FoliageData"))
        {
            var stringData = foliageQuery["FoliageData"];
            var deserialized = JsonConvert.DeserializeObject<List<Foliage>>(stringData);
            genarator.savedFoliage = deserialized;
        }
        else
        {
            Debug.Log("key not found");
        }


        genarator.LoadSavedMap();
    }
}