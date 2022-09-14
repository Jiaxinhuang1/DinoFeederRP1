using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using UnityEngine.UI;
public class PlayerDetection : MonoBehaviour
{
    [SerializeField]
    private Tilemap tileMap;
    public AudioSource waterSound;
    public AudioSource eatSound;
    public Slider progressSlider;
    public TileData tileDataSO;
    // Start is called before the first frame update
    void Start()
    {
        tileDataSO.progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int index = FindIndex(tileMap.GetTile(Vector3Int.RoundToInt(this.transform.position)));
            if (index < tileDataSO.tiles.Length - 1)
            {
                if (index == 0)
                {
                    tileDataSO.progress++;
                    progressSlider.value = tileDataSO.progress;
                }
                tileMap.SetTile(Vector3Int.RoundToInt(this.transform.position), tileDataSO.tiles[index + 1]);
                waterSound.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            int index = FindIndex(tileMap.GetTile(Vector3Int.RoundToInt(this.transform.position)));
            if (index > 0)
            {
                if (index == 1)
                {
                    tileDataSO.progress--;
                    progressSlider.value = tileDataSO.progress;
                }
                tileMap.SetTile(Vector3Int.RoundToInt(this.transform.position), tileDataSO.tiles[index - 1]);
                eatSound.Play();
            }
        }
    }

    private int FindIndex(TileBase tb)
    {
        try
        {
            if (tb.name != null)
            {
                Debug.Log(tb.name);
                int index = System.Array.IndexOf(tileDataSO.tiles, tb);
                return index;
            }
        }
        catch (NullReferenceException)
        {
            Debug.Log("Base Tile");
        }
        return 0;
    }
}
