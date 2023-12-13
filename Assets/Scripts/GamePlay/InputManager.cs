using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class InputManager
{
   public static Vector2Int GetCellFromMouse()
    {
        Vector2 position = Input.mousePosition;
        Vector2Int res = new Vector2Int();
        position = Camera.main.ScreenToWorldPoint(position);
        res.x = Mathf.CeilToInt(position.y) - 1;
        res.y = Mathf.CeilToInt(position.x) - 1;

        return res;
    }
}
