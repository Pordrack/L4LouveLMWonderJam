using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaskHandler
{
    private readonly int[] _playerPos;
    
    public MapMaskHandler(int x, int y)
    {
        _playerPos = new int[] { x, y };
    }

    /// <summary>
    /// This methods returns the blocks to hide and to draw.
    /// </summary>
    /// <param name="newX"></param>
    /// <param name="newY"></param>
    /// <returns>int[[list of blocks' pos to hide], [list of blocks' pos to draw]]</returns>
    public int[,,] UpdateMap(int newX, int newY)
    {
        var maskUpdate = new int[2,11,2];
        var deltaX = newX - _playerPos[0];
        var deltaY = newY - _playerPos[1];

        Debug.Log($"old pos ({_playerPos[0]},{_playerPos[1]}) and new pos ({newX},{newY})" +
            $"deltaX: {deltaX}, deltaY: {deltaY}");
        if (deltaX != 0)
        {
            for(var i=0; i<11; i++)
            {
                //blocks to hide
                maskUpdate[0, i, 0] = _playerPos[0] - deltaX*5;
                maskUpdate[0, i, 1] = _playerPos[1] - 5 + i;
                
                //blocks to draw
                maskUpdate[1, i, 0] = _playerPos[0] + deltaX*6;
                maskUpdate[1, i, 1] = _playerPos[1] - 5 + i;
            }
        }
        else if (deltaY != 0)
        {
            for(var i=0; i<11; i++)
            {
                //blocks to hide
                maskUpdate[0, i, 0] = _playerPos[0] - 5 + i;
                maskUpdate[0, i, 1] = _playerPos[1] - deltaY*5;
                
                //blocks to draw
                maskUpdate[1, i, 0] = _playerPos[0] - 5 + i;
                maskUpdate[1, i, 1] = _playerPos[1] + deltaY*6;
            }
        }
        else //No movement
        {
            return null;
        }

        //Update player position
        _playerPos[0] = newX;
        _playerPos[1] = newY;
        
        return maskUpdate;
    }
    
    

    public int[,,] InitMask()
    {
        var mask = new int[11, 11, 2];
        for (var i = 0; i < 11; i++)
        {
            for (var j = 0; j < 11; j++)
            {
                mask[i, j, 0] = _playerPos[0] - 5 + i;
                mask[i, j, 1] = _playerPos[1] - 5 + j;
            }
        }
        return mask;
    }
}
