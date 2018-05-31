using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMap:MonoBehaviour{

    private GameNode[,] _map;
    private int _map_height;
    private int _map_width;
    //0 High
    private List<GameNode> _to_destroy = new List<GameNode>();
    private HashSet<GameNode> _visited = new HashSet<GameNode>();

    //private enum NeighborDirection { South=0,East=1,North=2,West=3};
    private int SOUTH = 0;
    private int EAST = 1;
    private int NORTH = 2;
    private int WEST = 3;


    public override string ToString()
    {
        int node_strlen =(new GameNode()).ToString().Length;
        string result;
        string header = string.Format("{0," + node_strlen + "}   ", ""); ;
        for(int j = 0; j < _map.GetLength(1); j++)
        {
            header += string.Format("{0," + node_strlen + "}", j);
        }
        result = header + "\n";

        for(int i = 0;i < _map.GetLength(0); i++)
        {
            string row_str = string.Format("{0," + node_strlen + "}",i);
            for(int j = 0; j < _map.GetLength(1); j++)
            {
              if(_map[i,j] == null)
                {
                    row_str += string.Format("{0," + node_strlen + "}", _map[i, j]);
                }
                else
                {
                    row_str += string.Format("{0," + node_strlen + "}", "Null");
                }
            }
            row_str += "\n";
            result += row_str;
        }
        return result;
        
    }

    public void init_map(int height, int width)
    {

        _map = new GameNode[height, width];
        for(int row_indy = 0; row_indy < height; row_indy++)
        {
            for(int col_indy = 0; col_indy < width; col_indy++)
            {

                _map[row_indy, col_indy] = new GameNode();
                _map[row_indy, col_indy].Init_Node(row_indy, col_indy);
            }
        }
        _map_height = height;
        _map_width = width;
    }


    public GameNode GetNode(int row,int col)
    {
        if (row >= _map_height || row < 0) return null;

        if (col >= _map_height || col < 0) return null;

        return _map[row, col];
    }


    public void SwapNodes(int row_1, int col_1, int row_2, int col_2)
    {
        GameNode temp = _map[row_1, col_1];
        _map[row_1,col_1] = _map[row_2, col_2];
        _map[row_2, col_2] = temp;

        _map[row_1, col_1].UpdatePositionSwap(row_1, col_1);
        _map[row_2, col_2].UpdatePositionSwap(row_2, col_2);
        UpdateBoard();

    }
    //Return if change happened
    //Wait for maximum animation then re update if change occured
    public bool UpdateBoard()
    {
        MarkDestroyed();
        if (DestroyMarked())
        {
            Debug.Log(this);
            DropNodes();
            Debug.Log(this);
            return true;
        }
        return false;

    }


    private void DropNodes()
    {
        GameNode curr_node = null;
        GameNode lower_node = null;
        int new_indy = 0;
        for (int row_indy = _map_height - 2; row_indy >= 0 ; row_indy--)
        {
            for (int col_indy = _map_width - 1; col_indy >= 0; col_indy--)
            {
                curr_node = _map[row_indy, col_indy];

                if (Equals(curr_node, null)) continue;

                new_indy = row_indy + 1;
                lower_node = _map[new_indy, col_indy];
                Debug.LogFormat("{0}",curr_node);
                while (Equals(lower_node,null) || lower_node.IsDestroyed() )
                {
                    _map[new_indy, col_indy] = curr_node;
                    _map[new_indy - 1, col_indy] = null;

                    new_indy = new_indy + 1;
                    if (new_indy == _map_height)
                    {
                        break;
                    }
                    lower_node = _map[new_indy, col_indy];
                    Debug.Log(this);

                }
         
            }
        }
    }


    private bool DestroyMarked()
    {
        if (_to_destroy.Count == 0) return false;
        //Debug.Log("Marked");
        Debug.Log(_to_destroy.Count);
        foreach (GameNode node in _to_destroy)
        {
            node.NodeDestroy();
            _map[node.GetRowInd(), node.GetColInd()] = null;
        }
        _to_destroy.Clear();

        return true;
    }

    private void MarkDestroyed()
    {
        GameNode curr_node;
        for (int row_indy = 0; row_indy < _map_height; row_indy++)
        {
            for (int col_indy = 0; col_indy < _map_width; col_indy++)
            {
                curr_node = _map[row_indy, col_indy];
                //Debug.Log(curr_node);
                if (curr_node.IsDestroyed()) continue;

                if (ShouldDestroy(curr_node)) {
                    _to_destroy.Add(curr_node);
                    MarkDestroyRec(curr_node);
                    _visited.Clear();
                }
            }
        }
    }

    private void MarkDestroyRec(GameNode groot)
    {
        _visited.Add(groot);
        foreach (GameNode neighbor in GetNeighbors(groot))
        {
            //Debug.Log(_visited.Count);
            if (Equals(neighbor,null) || _visited.Contains(neighbor)) continue;


            if (groot.GetColor() == neighbor.GetColor())
            {
                _to_destroy.Add(neighbor);
                if (!neighbor.IsDestroyed())
                {
                    MarkDestroyRec(neighbor);
                }

            }

        }


    }

    private GameNode[] GetNeighbors(GameNode node)
    {
        GameNode[] neighbors = new GameNode[4];

        int node_row = node.GetRowInd();
        int node_col = node.GetColInd();

        neighbors[NORTH] = GetNode(node_row - 1, node_col);//up
        neighbors[SOUTH] = GetNode(node_row + 1, node_col);//down
        neighbors[EAST] = GetNode(node_row, node_col + 1);//right
        neighbors[WEST] = GetNode(node_row, node_col - 1);//left

        return neighbors;
    }


    private bool ShouldDestroy(GameNode node)
    {
        GameNode[] neighbors = GetNeighbors(node);
        //Debug.LogFormat("{0},{1},{2},{3}", neighbors[0], neighbors[1], neighbors[2], neighbors[3]);
        if (ColorMatch(neighbors[NORTH], node, neighbors[SOUTH]))
        {
            return true;
        }
        if (ColorMatch(neighbors[EAST], node, neighbors[WEST]))
        {
            return true;
        }
        return false;
    }

    private bool ColorMatch(GameNode node1, GameNode root ,GameNode node2)
    {
        // THERES A BUG HERE
        

        if (Equals(node1,null)|| Equals(node2,null))
        {
            return false;// is null
        }
        
        if (node1.GetColor() != node2.GetColor()) return false;// do neighbors match
        if (node1.GetColor() != root.GetColor()) return false;// do the neighbors match the core?
        return true;

    }
}
