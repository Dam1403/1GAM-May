using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap : MonoBehaviour {



    private bool is_initialized = false;
    public GameObject GameNodePrefab;


    public void InitializeMap(int square_length)
    {


    }
	// Use this for initialization
	void Start () {
        GameNode SW = create_diamond(5);
        print_diamond(SW);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void build_square_map(int square_length)
    {
        
    }

    private GameNode create_diamond(int side_length)
    {
        GameNode top = new GameNode(1);

        top.east_node = new GameNode(2);
        top.east_node.west_node = top;

        top.north_node = new GameNode(2);
        top.north_node.south_node = top;

        diamond_rec(top.east_node, top.north_node,side_length);

        return top;

    }

    private void diamond_rec(GameNode east, GameNode north, int side_length)
    {
        int end_len = (side_length * 2) - 2;



        if (end_len == east.GetID() && end_len == north.GetID())
        {
            GameNode end_node = new GameNode(east.GetID() + 1);
            east.north_node = end_node;
            end_node.south_node = east;

            north.east_node = end_node;
            end_node.west_node = north; // this can be done in accessor

        }

        else if (side_length <= east.GetID() && side_length <= north.GetID())
        {

            east.north_node = new GameNode(east.GetID() + 1);
            east.north_node.south_node = east;

            north.east_node = new GameNode(north.GetID() + 1);
            north.east_node.west_node = north;

            diamond_rec(east.north_node, north.east_node, side_length);
        }

        else
        {
            east.east_node = new GameNode(east.GetID() + 1);
            east.east_node.west_node = east;
            north.north_node = new GameNode(north.GetID() + 1);
            north.north_node.south_node = north;
            diamond_rec(east.east_node, north.north_node, side_length);
        }

    }


    private void print_diamond(GameNode SW_corner)
    {
        string diamond_output = "";
        GameNode curr_node = SW_corner;
        while(curr_node.north_node != null)
        {
            diamond_output += "N" + curr_node.GetID();
            curr_node = curr_node.north_node;
        }
        while (curr_node.east_node != null)
        {
            diamond_output += "E" + curr_node.GetID();
            curr_node = curr_node.east_node;
        }
        while (curr_node.south_node != null)
        {
            diamond_output += "S" + curr_node.GetID();
            curr_node = curr_node.south_node;
        }
        while (curr_node.west_node != null)
        {
            diamond_output += "W" + curr_node.GetID();
            curr_node = curr_node.west_node;
        }

        Debug.Log(diamond_output);
    }
}
