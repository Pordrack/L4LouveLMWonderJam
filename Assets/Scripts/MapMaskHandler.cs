using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaskHandler
{
    //TODO : For optimisation purpose, keep a track of the current visible map and just update this (better)
    private readonly int[] _playerPos;
    public static int DrawnMapSize = 11;
    private int _halfMapSize = DrawnMapSize / 2;
    
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
        var maskUpdate = new int[2,DrawnMapSize,2];
        var deltaX = newX - _playerPos[0];
        var deltaY = newY - _playerPos[1];

    //        Debug.Log($"old pos ({_playerPos[0]},{_playerPos[1]}) and new pos ({newX},{newY})" +
      //      $"deltaX: {deltaX}, deltaY: {deltaY}");
        if (deltaX != 0)
        {
            for(var i=0; i<11; i++)
            {
                //blocks to hide
                maskUpdate[0, i, 0] = _playerPos[0] - deltaX*_halfMapSize;
                maskUpdate[0, i, 1] = _playerPos[1] - _halfMapSize + i;
                
                //blocks to draw
                maskUpdate[1, i, 0] = _playerPos[0] + deltaX*(_halfMapSize+1);
                maskUpdate[1, i, 1] = _playerPos[1] - _halfMapSize + i;
            }
        }
        else if (deltaY != 0)
        {
            for(var i=0; i<11; i++)
            {
                //blocks to hide
                maskUpdate[0, i, 0] = _playerPos[0] - _halfMapSize + i;
                maskUpdate[0, i, 1] = _playerPos[1] - deltaY*_halfMapSize;
                
                //blocks to draw
                maskUpdate[1, i, 0] = _playerPos[0] - _halfMapSize + i;
                maskUpdate[1, i, 1] = _playerPos[1] + deltaY*(_halfMapSize+1);
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
        return GetSurroundingMap(_playerPos[0], _playerPos[1]);
    }
    

    public int[,,] GetSurroundingMap(int x, int y)
    {
        var mask = new int[DrawnMapSize, DrawnMapSize, 2];
        for (var i = 0; i < DrawnMapSize; i++)
        {
            for (var j = 0; j < DrawnMapSize; j++)
            {
                mask[i, j, 0] = x - _halfMapSize + i;
                mask[i, j, 1] = y - _halfMapSize+ j;
            }
        }
        return mask;
    }
    
    public void UpdatePlayerPosition(int x, int y)
    {
        _playerPos[0] = x;
        _playerPos[1] = y;
    }
}
