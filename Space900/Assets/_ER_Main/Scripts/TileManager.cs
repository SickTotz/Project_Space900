using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{   
    [SerializeField] GameObject[] tilePrefabs;
    public float z_Spawn = -20;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    private List<GameObject> activeTiles = new List<GameObject>();
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < numberOfTiles; i++){
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - 35 > z_Spawn - (numberOfTiles * tileLength)){
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {

        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * z_Spawn, transform.rotation);
        activeTiles.Add(go);
        z_Spawn += tileLength; 
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
