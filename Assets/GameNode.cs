﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameNode{
    //OPPOSITES NEED TO BE NEXT TO EACH OTHER OTHERWISE THE 
    //ANTI FUNCTION WILL BREAK;
    public enum NeighborDirection { North=0, South=1, East=2, West=3 };
    
    private GameNode[] neighbors = new GameNode[System.Enum.GetNames(typeof(NeighborDirection)).Length];

    public GameNode east_node
    {
        get
        {
            return GetNeighbor(NeighborDirection.East);
        }
        set
        {
            SetNeighbor(value,NeighborDirection.East);
        }
    }

    public GameNode west_node
    {
        get
        {
            return GetNeighbor(NeighborDirection.West);
        }
        set
        {
            SetNeighbor(value, NeighborDirection.West);
        }
    }
    public GameNode north_node
    {
        get
        {
            return GetNeighbor(NeighborDirection.North);
        }
        set
        {
            SetNeighbor(value, NeighborDirection.North);
        }
    }

    public GameNode south_node
    {
        get
        {
            return GetNeighbor(NeighborDirection.North);
        }
        set
        {
            SetNeighbor(value, NeighborDirection.North);
        }
    }



    private Color GameNode_color;
    private int gen_id;
    

    public GameNode(int id)
    {

    }

    public int  GetID()
    {
        return gen_id;
    }

    public void SetNeighbor(GameNode new_neighbor, NeighborDirection direction)
    {

        neighbors[(int)direction] = new_neighbor;
        new_neighbor.SetNeighbor(this, AntiDirection(direction));

    }

    public GameNode GetNeighbor(NeighborDirection direction)
    {

        return neighbors[(int)direction];
    }

    public void SetNeighbors(GameNode[] new_neighbors)
    {
        neighbors = new_neighbors;
    }

    public GameNode[] GetNeighbors()
    {
        return neighbors;
    }

    private NeighborDirection AntiDirection(NeighborDirection direction)
    {
        int LorR = (int)direction & 1;
        int high_bits = (int)direction & -2;

        return (NeighborDirection)(~LorR ^ high_bits);

    }
    

    public void SwapWithNeighbor(GameNode swap_neighbor,NeighborDirection direction)
    {
        GameNode[] neighbor_neighbors = swap_neighbor.GetNeighbors();

        swap_neighbor.SetNeighbors(neighbors);
        this.neighbors = neighbor_neighbors;
        SetNeighbor(swap_neighbor, AntiDirection(direction));

    }



}








