using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HouseProp : MonoBehaviour
{

    public void Interact()
    {
        if (GameplayManager.instance.getKey)
        {
            GameplayManager.instance.GoToMapById(6);
        }
        else
        {
            Shake();
        }
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
