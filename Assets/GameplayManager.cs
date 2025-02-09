using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    public MapHandler currentMapHandler;
    private void Awake()
    {
        instance = this;
        currentMapHandler = mapHandlers[0];
    }

    // Start is called before the first frame update
    // [SerializeField] [ReadOnly] private int mapIdCurrent = 0;
    public Transform renderCameraOrigin;
    [OnCollectionChanged("AssignMapID")]
    public List<MapHandler> mapHandlers = new List<MapHandler>();

    void AssignMapID()
    {
        int mapIdCurrent = 0;
        foreach (var map in mapHandlers)
        {
            map.AssignUid(mapIdCurrent++);
        }
    }

    public void GoToMapById(int id)
    {
        var map = GetMapById(id);
        currentMapHandler = map;
        renderCameraOrigin.DOLocalMove(map.transform.position - new Vector3(1.5f,0,10f),0.5f) ;
    }

    [Button]
    public void TriggerTileEvent(int tileID)
    {
        currentMapHandler.mapTile[tileID].TriggerEvent();
    }
    public MapHandler GetMapById(int id)
    {
        return mapHandlers.FirstOrDefault(map => map.MapId == id);
    }



    public void ForceExit()
    {
        foreach (var process in Process.GetProcessesByName("DeckyQuest.exe"))
        {
            process.Kill();
        }
    }
    public bool getKey = false;
}
