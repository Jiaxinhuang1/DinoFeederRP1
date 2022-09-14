using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    GameManager gM;
    public float xPosScaler;

    void Start()
    {
        gM = GameManager.instance;

        transform.position = new Vector3(0 + gM.mapSizeX * xPosScaler, transform.position.y, transform.position.z);   
    }
}
