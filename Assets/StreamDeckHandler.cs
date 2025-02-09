using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using inspiration.Utilities;
using OpenMacroBoard.SDK;
using Sirenix.OdinInspector;
using StreamDeckSharp;
using UnityEngine;

public class StreamDeckHandler : MonoBehaviour
{
    private IMacroBoard streamdeck;
    public RenderTexture[] RenderTextures = new RenderTexture[15];
    public bool noSdInEditor = true;
    private void StartStreamDeck()
    {
        if (noSdInEditor && Application.isEditor)
        {
            Debug.Log("streamdeck ignored");
            return;
        }
        try
        {
            
            streamdeck = StreamDeck.OpenDevice();
            streamdeck.KeyStateChanged += StreamDeckOnKeyStateChanged;
            // StartCoroutine(nameof(Tick));

        }
        catch (Exception e)
        {
            Debug.LogError(e);
            return;
        }
        Debug.Log("streamdeck opened,SI:" + streamdeck.GetSerialNumber());
    }

    private void StreamDeckOnKeyStateChanged(object sender, KeyEventArgs e)
    {
        var state = e.IsDown ? "PRESSED" : "RELEASED";
        Debug.Log($"key {e.Key} is {state}");
        if(!e.IsDown)
        {
            GameplayManager.instance.TriggerTileEvent(e.Key);
        }
    }

    private void Update()
    {
        RenderImageToSD();

    }

    void Start()
    {
        path = DirectoryPathHelper.GetFolder(DirectoryPathHelper.OutputPath.RelativeToPeristentData,
            $"tiles");
        EnsureDirectory(path);
        StartStreamDeck();
        Application.quitting += OnApplicationExit;
        initialized = true;

    }
    public static bool EnsureDirectory(string path)
    {
        try
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Debug.Log($"Directory created successfully at: {path}");
            }
            else
            {
                Debug.Log($"Directory already exists at: {path}");
            }
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error creating directory at {path}: {e.Message}");
            return false;
        }
    }

    private string path;
    private bool initialized = false;
    [Button]
    public async void RenderImageToSD()
    {
        
        
        for (int i = 0; i < RenderTextures.Length; i++)
        {
            string file = Path.Combine(path, $"tile{i}.png"); 
            TextureToFileHelper.SaveTextureToFile(RenderTextures[i],file,RenderTextures[0].width,RenderTextures[0].height,asynchronous:false);
            // RenderTextures[0]
            if (streamdeck != null)
            {
                var bitmap = await KeyBitmap.Create.FromFileAsync(file);
                streamdeck.SetKeyBitmap(i,bitmap);
            }
        }
       
    }

    private void OnApplicationExit()
    {
        StopStreamDeck();

    }
    private void StopStreamDeck()
    {
        if (streamdeck != null)
        {
            streamdeck.Dispose();
            streamdeck = null;
            Debug.Log("disposed");
        }
        
        
    }
    private void OnDestroy()
    {
        StopStreamDeck();
    }
}
