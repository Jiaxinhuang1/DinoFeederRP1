using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CompositeCollider2D boundingBox;

    public enum State{ live, dead }
    public enum ItemType{ wateringCan, seed, bone }
    public static GameManager instance;
    public List<GameObject> tiles;
    public int mapSizeX, mapSizeY;
    public float tileSize;
    public GameObject tile;
    public GameObject currentFossil;

    void Awake(){
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boundingBox = GetComponent<CompositeCollider2D>();
        GenerateTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTiles(){
        for (int i = 0; i < mapSizeX; i++)
        {
            for (int j = 0; j < mapSizeY; j++)
            {
                GameObject newTile = Instantiate(tile, transform);
                newTile.name = i + ", " + j;
                newTile.transform.position = new Vector2(i * tileSize + tileSize/2, j * tileSize + tileSize/2);
                tiles.Add(newTile);
            }
        }
    }
}
