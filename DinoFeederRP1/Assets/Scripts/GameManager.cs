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
    public GameObject player;
    public GameObject house;
    public GameObject well;

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
        GameObject newHouse = Instantiate(house, tiles[19].transform.position, Quaternion.identity, transform);
        GameObject newWell = Instantiate(well, tiles[8].transform.position, Quaternion.identity, transform);
        tiles[19].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[18].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[17].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[9].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[8].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[7].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[29].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[28].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[27].GetComponent<TileBehaviour>().contains = newHouse;
    }
}
