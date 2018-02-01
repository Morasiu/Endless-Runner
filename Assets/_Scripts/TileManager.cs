using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject playerGO;
    public GameObject[] tilePrefab;
    /* 
     * 0 = Empty
     * 1 = Cubes
     * 2 = CentralPath
     * 3 = Ramp
     * 4 = Hole
    */

    float spawnZ= -10f;
    float tileLength = 10f;
    float safeZone = 15f;
    int amnTilesOfScreen = 15;
    int lastPrefabIndex = 0;

    List<GameObject> activeTiles;
    Player player;


    void Start () {
       player= playerGO.GetComponent<Player>();
       activeTiles = new List<GameObject> ();
       for(int i = 0; i  <amnTilesOfScreen; i++) {
            if (i < 3)
                SpawnTile(0);
            else
                SpawnTile();
        }
    }

	void Update () {
		if(player.GetPlayerPosition() - safeZone > (spawnZ - amnTilesOfScreen * tileLength)) {
            SpawnTile();
            DeleteTile();
        }
	}

    private void SpawnTile(int prefabIndex = -1) {
        GameObject go;
        if(prefabIndex == -1)
            go = Instantiate(tilePrefab[GetRandomTile()]) as GameObject;
        else
            go = Instantiate(tilePrefab[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
    private void DeleteTile() {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    int GetRandomTile() {
        if (tilePrefab.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex) {
            randomIndex = Random.Range(0, tilePrefab.Length);
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}