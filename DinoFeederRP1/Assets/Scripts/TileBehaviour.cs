using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour
{
    UIManager uM;
    GameManager gM;
    MeshRenderer meshRenderer;
    public GameObject contains;
    public GameManager.State currentState;
    public List<Material> dead;
    public List<Material> live;
    public float objectSpawnRate;
    public GameObject spawnedObject;
    public int health;

    IEnumerator TileUpdate()
    {
        while (true){
            yield return new WaitForSeconds(1f);
            if(Random.Range(0, 100) < objectSpawnRate && gM.currentFossil == null){
                GameObject newObjectSpawnedWithin = Instantiate(spawnedObject, transform.position, Quaternion.identity);
                contains = newObjectSpawnedWithin;
                gM.currentFossil = newObjectSpawnedWithin;
            }
            if(health <= 0){
                currentState = GameManager.State.dead;
            }
            switch(currentState){
                case GameManager.State.dead:
                if(contains != null && contains.TryGetComponent(out PlantBehaviour plantBehaviour)){
                    plantBehaviour.BeginDestroySelf();
                }
                break;
                case GameManager.State.live:
                    health--;
                break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gM = GameManager.instance;
        uM = UIManager.instance;
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        StartCoroutine(TileUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    void Refresh(){
        switch(currentState){
            case GameManager.State.dead:
                meshRenderer.material = dead[0/*Random.Range(0, dead.Count)*/];
                break;
            case GameManager.State.live:
                meshRenderer.material = live[0/*Random.Range(0, dead.Count)*/];
                break;
        }
    }
}
