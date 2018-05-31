using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {

        GameMap map = new GameMap();
        map.init_map(4, 4);
        Debug.Log(map);
        Debug.Log("Board Changed " + map.UpdateBoard());


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
