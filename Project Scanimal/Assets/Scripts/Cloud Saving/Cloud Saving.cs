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
        var data = new Dictionary<string, object> { {"MapData", genarator.grid } };
        await CloudSaveService.Instance.Data.ForceSaveAsync(data);
    }

    public async void LoadMapData()
    {
        GameObject terrainGen = GameObject.Find("Terrain Generator");
        TerrainGeneration genarator = terrainGen.GetComponent<TerrainGeneration>();

        //Dictionary<string, string> serverMapData = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> {"MapData"});
        var query = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> { "MapData" });
        if (query.ContainsKey("MapData"))
        {
            //Debug.Log("data obtained");
            //string test = serverMapData["MapData"];
            //Debug.Log(test);
            var stringData = query["MapData"];
            var deserialized = JsonConvert.DeserializeObject<Cell[,]>(stringData);
            genarator.SavedGrid = deserialized;
            genarator.LoadSavedMap();
        }
        else 
        {
            Debug.Log("key not found");
        }
    }
   
}
