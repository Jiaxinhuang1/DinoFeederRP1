using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    CompositeCollider2D boundingBox;

    public enum State{ live, dead }
    public enum ItemType{ wateringCan, seed, bone, cloner }
    public enum FoodType{ none, meat, veggie, special }
    public static GameManager instance;
    public List<GameObject> tiles;
    public int mapSizeX, mapSizeY;
    public float tileSize;
    public GameObject tile;
    public GameObject fenceBack;
    public GameObject fenceFront;
    public GameObject fenceLeft;
    public GameObject fenceRight;
    public GameObject topLeft;
    public GameObject topRight;
    public GameObject bottomLeft;
    public GameObject bottomRight;
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
                GameObject newFence;
                if (i == 0 && j == 0)
                {
                    newFence = Instantiate(bottomLeft, transform);
                }
                else if (i == 0 && j == mapSizeX - 1)
                {
                    newFence = Instantiate(topLeft, transform);
                }
                else if (i == mapSizeX - 1 && j == 0)
                {
                    newFence = Instantiate(bottomRight, transform);
                }
                else if (i == mapSizeX - 1 && j == mapSizeX - 1)
                {
                    newFence = Instantiate(topRight, transform);
                }
                else if (i == 0)
                {
                    newFence = Instantiate(fenceLeft, transform);
                }
                else if (i == mapSizeX - 1)
                {
                    newFence = Instantiate(fenceRight, transform);
                }
                else if (j == 0)
                {
                    newFence = Instantiate(fenceFront, transform);
                }
                else if (j == mapSizeX - 1)
                {
                    newFence = Instantiate(fenceBack, transform);
                }
                else
                {
                    newFence = Instantiate(tile, transform);
                }
                newFence.name = i + ", " + j;
                newFence.transform.position = new Vector2(i * tileSize + tileSize / 2, j * tileSize + tileSize / 2);
                tiles.Add(newFence);
            }
        }

        GameObject newHouse = Instantiate(house, tiles[30].transform.position, Quaternion.identity, transform);
        GameObject newWell = Instantiate(well, tiles[18].transform.position, Quaternion.identity, transform);
        tiles[18].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[19].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[20].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[40].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[41].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[42].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[29].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[30].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[31].GetComponent<TileBehaviour>().contains = newHouse;
        tiles[17].GetComponent<TileBehaviour>().contains = newHouse;
    }
}
