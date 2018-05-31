using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum NodeColor { Red=0, Green=1, Blue=2 }


public class GameNode : MonoBehaviour {
    private static int GAMENODE_COLOR_COUNT = 3;
    private int _my_height;
    private int _my_width;

    private NodeColor _my_color;

    private bool is_destroyed = false;

    public string id;

    public override bool Equals(object other)
    {
        GameNode node = other as GameNode;
        if (Object.Equals(node, null))
        {
            return false;
        }
        return this.id == node.id;

    }

    public override int GetHashCode()
    {
        return this._my_height * this._my_height * 1009;
    }

    public override string ToString()
    {
        return string.Format("[{0,9},{1,4},{2,4}]",_my_color,_my_height,_my_width);
    }

    private void UpdatePosition(int height, int width)
    {
        _my_height = height;
        _my_width = width;
        id = _my_height + " , " + _my_width;
    }

    public void Init_Node(int height, int width)
    {
        _my_height = height;
        _my_width = width;
        id = _my_height + " , " + _my_width;
        _my_color = (NodeColor)Random.Range(0,GAMENODE_COLOR_COUNT);
    }
    public void Init_Node(int height, int width,NodeColor color)
    {
        _my_color = color;
        _my_height = height;
        _my_width = width;
    }
    public void UpdatePositionDrop(int height, int width)
    {
        UpdatePosition(height, width);
        //UI stuff here
    }

    public void UpdatePositionSwap(int height, int width)
    {
        UpdatePosition(height, width);
        //UI stuff Here
    }


    public void IncrementColor()
    {
        if(_my_color == NodeColor.Red)
        {
            _my_color = NodeColor.Green;
        }
        else if (_my_color == NodeColor.Green)
        {
            _my_color = NodeColor.Blue;
        }
        else if (_my_color == NodeColor.Blue)
        {
            _my_color = NodeColor.Red;
        }
    }


    public NodeColor GetColor()
    {
        return _my_color;
    }
    public int GetRowInd()
    {
        return _my_height;
    }

    public int GetColInd()
    {
        return _my_width;
    }


    public bool IsDestroyed()
    {
        return is_destroyed;
    }

    public void NodeDestroy()
    {
        is_destroyed = true;
    }

    







}

