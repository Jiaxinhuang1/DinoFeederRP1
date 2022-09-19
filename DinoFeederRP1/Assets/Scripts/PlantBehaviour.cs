using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    public GameManager.FoodType foodType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginDestroySelf(){
        LeanTween.delayedCall(gameObject, Random.Range(5, 10), () => 
        {
            LeanTween.scale(gameObject, new Vector3(0, 0, 0), Random.Range(5, 10)).setOnComplete(DestroySelf);
            UIManager.instance.aliveCount--;
        });
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
