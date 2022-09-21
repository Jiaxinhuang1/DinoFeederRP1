using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBehaviour : MonoBehaviour
{
    public ItemBehaviour contains;
    public GameObject[] dinoPrefabs;
    public float consumptionTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update(){
        //StartCoroutine(Consume());
    }

    // Update is called once per frame
    public IEnumerator Consume()
    {
        while(contains != null){
            contains.gameObject.SetActive(false);
            yield return new WaitForSeconds(consumptionTime);
            if(contains.TryGetComponent(out ItemBehaviour itemBehaviour)){
                switch(itemBehaviour.type){
                    case GameManager.ItemType.bone:
                        ConsumeContainedObject(contains.gameObject);
                    break;
                    case GameManager.ItemType.wateringCan:
                        contains.gameObject.SetActive(true);
                    break;
                }
            }
            contains = null;
        }
    }

    void ConsumeContainedObject(GameObject candidate){
        GameObject newDino = Instantiate(dinoPrefabs[Random.Range(0, dinoPrefabs.Length)]);
        AudioManager.instance.dinoSpawnSound.Play();
        newDino.transform.position = transform.position;
        contains = null;
        Destroy(contains);
    }
}
