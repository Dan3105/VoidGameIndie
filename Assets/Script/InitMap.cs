using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMap : MonoBehaviour
{
    public Transform[] gameObjects;
    public GameObject[] mapTiles;
    public int typeRoom;
    private void Start()
    {
        if (mapTiles.Length < 0) return;
        foreach(var obj in gameObjects)
        {
            int indexRandom = Random.Range(0, mapTiles.Length);
            GameObject tileWall = Instantiate(mapTiles[indexRandom], obj.position, Quaternion.identity);
            tileWall.transform.parent = transform;
        }
    }

    public void DestroyRoom()
    {
        Destroy(this.gameObject);
    }
}
