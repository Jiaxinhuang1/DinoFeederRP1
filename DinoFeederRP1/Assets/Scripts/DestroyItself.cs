using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItself : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.delayedCall(gameObject, Random.Range(5, 10), () => 
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), Random.Range(5, 10)).setOnComplete(DestroySelf);
            gameObject.GetComponentInParent<TileBehaviour>().currentState = GameManager.State.dead;
            UIManager.instance.aliveCount--;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
