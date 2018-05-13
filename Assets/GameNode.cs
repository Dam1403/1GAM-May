using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameNode : MonoBehaviour {
    //OPPOSITES NEED TO BE NEXT TO EACH OTHER OTHERWISE THE 
    //ANTI FUNCTION WILL BREAK;
    public enum NeighborDirection { North=0, South=1, East=2, West=3 };
    
    private GameNode[] neighbors = new GameNode[System.Enum.GetNames(typeof(NeighborDirection)).Length];

    private Color GameNode_color;
    private int gen_id;
    

    public void Initialize(int id)
    {
        gen_id = id;
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








