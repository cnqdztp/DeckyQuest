using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TileHandler : MonoBehaviour
{
    public SpriteRenderer tileRenderer;
    public UnityEvent willDoEvent;

    public bool willGoToMap = false;
    [ShowIf("willGoToMap")] public int goTo;
    [Button]
    private void GetRendererInstance()
    {
        tileRenderer = GetComponent<SpriteRenderer>();
    }

    public void TriggerEvent()
    {
        if(willGoToMap)
            GameplayManager.instance.GoToMapById(goTo);
        willDoEvent?.Invoke();
    }

    public void Shake()
    {
        transform.DOShakeRotation(0.5f);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
