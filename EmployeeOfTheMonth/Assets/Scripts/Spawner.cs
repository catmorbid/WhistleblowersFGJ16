using UnityEngine;
using System.Collections;

// Very simple spawner;
public class Spawner : MonoBehaviour {

    public GameObject Prefab;
    public Transform SpawnPoint;
    public void Spawn()
    {
        if ( Prefab != null && SpawnPoint != null )
        {
            GameObject obj = GameObject.Instantiate(Prefab);
            obj.transform.position = SpawnPoint.position;
        }
    }
}
