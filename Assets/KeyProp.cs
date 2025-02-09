using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class KeyProp : MonoBehaviour
{

    public void Interact()
    {
        GameplayManager.instance.getKey = true;
        var originalScale = transform.localScale;
        transform.DOScale(Vector3.zero, 0.5f).OnComplete((() =>
        {
            GetComponent<SpriteRenderer>().sprite= null;
            transform.localScale = originalScale;
        }));

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
