using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class MapHandler : SerializedMonoBehaviour
{
    // [InlineEditor]
    // public
    // MapSO MapSo;[TableMatrix(HorizontalTitle = "Square Celled Matrix", SquareCells = true)]
    [SerializeField][ReadOnly] private string mapUid = "";
    [SerializeField] [ReadOnly] private int mapId = -1;
    // public Sprite emptySprite;
    public int MapId => mapId;
    [OnCollectionChanged("BuildMap")]
    [TableMatrix(SquareCells = true,HorizontalTitle = "map")]
    [ShowInInspector]
    [SerializeField]
    public Sprite[,] tiles = new Sprite[5,3];

    public TileHandler[] mapTile = new TileHandler[15];
    // Start is called before the first frame update
    void Start()
    {
        BuildMap();
    }


    public void AssignUid(int id)
    {
        mapUid = Guid.NewGuid().ToString();
        // Debug.Log(id);
        mapId = id;
    }
    [Button]

    void BuildMap()
    {
        int index = 0;
        for (int i = 0; i < 3; i++) // Loop through rows
        {
            for (int j = 0; j < 5; j++) // Loop through columns
            {
                // Ensure that the index does not exceed the length of mapTile
                if (index < mapTile.Length)
                {
                    // Debug.Log($"{i},{j}");
                    mapTile[index].tileRenderer.sprite = tiles[j, i]; // Assign the sprite from maps to mapTile
                    index++;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
