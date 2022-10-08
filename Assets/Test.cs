using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Test : MonoBehaviour
{
    private MapMaskHandler mapMaskHandler;

    private int x, y;
    // Start is called before the first frame update
    void Start()
    {
        x = 30;
        y = 30;
        mapMaskHandler = new MapMaskHandler(x, y);
    }

    private void Update()
    {
        int[,,] mask = null;
        if (Input.GetKeyDown(KeyCode.Q))
        {
            x--;
            mask = mapMaskHandler.UpdateMap(x, y);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            x++;
            mask = mapMaskHandler.UpdateMap(x, y);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            y++;
            mask = mapMaskHandler.UpdateMap(x, y);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            y--;
            mask = mapMaskHandler.UpdateMap(x, y);
        }
        
        if(mask is not null) PrintMask(mask);
        
    }

    private void PrintMask(int[,,] mask)
    {
        var hideString = "Hide = [";
        for (int j = 0; j < mask.GetLength(1); j++)
        {
            hideString += $"[{mask[0, j, 0]},{mask[0, j, 1]}],";
        }
        hideString += "]";
        var showStrin = "Show = [";
        for (int j = 0; j < mask.GetLength(1); j++)
        {
            showStrin += $"[{mask[1, j, 0]},{mask[1, j, 1]}],";
        }
        showStrin += "]";
        
        Debug.Log(hideString);
        Debug.Log(showStrin);
    }
}
