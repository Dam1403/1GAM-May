using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour {



    private bool is_initialized = false;
    public GameObject GameNodePrefab;


    public void InitializeMap(int height, int width)
    {



        GameObject bot_left_corner = Instantiate(GameNodePrefab);
        bot_left_corner.GetComponent<GameNode>().Initialize(0);


    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void build_map_rec(GameObject node_obj, int id)
    {
        if(node_obj == null)
        {
            return;
        }

        GameNode node = node_obj.GetComponent<GameNode>();
        if(node.GetID - 1  )
    }
}
