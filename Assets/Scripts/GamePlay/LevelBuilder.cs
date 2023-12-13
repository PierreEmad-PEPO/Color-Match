using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build.Content;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject cellPrefab;



    Camera mainCamera;

    int rows;
    int columns;

    List<string> levelLines;

    List<List<GameObject>> grid;

    private void Start()
    {
        BuildLevel(1);
    }

    void BuildLevel(int level)
    {
        mainCamera = Camera.main;
        levelLines = new List<string>();
        grid = new List<List<GameObject>>();
        ReadLevel(level);
        Vector3 cameraPosition  = mainCamera.transform.position;
        cameraPosition.x = rows / 2;
        cameraPosition.y = columns / 2;
        mainCamera.transform.position = cameraPosition;
        mainCamera.GetComponent<Camera>().orthographicSize = rows / 2 + 1;

        for(int i = rows - 1; i >= 0; i--)
        {
            grid.Add(new List<GameObject>());
            for(int j = 0; j < columns; j++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector2(j +.5f, (rows - i - 1) + .5f), Quaternion.identity);
                SpriteRenderer childSpriteRenderer = cell.transform.GetChild(0).GetComponent<SpriteRenderer>();
                if (levelLines[i][j] != '.' )
                {

                   childSpriteRenderer.color = GetColor(levelLines[i][j]);
                }
                else
                {
                    childSpriteRenderer.sprite = null;
                }

                grid[grid.Count - 1].Add(cell);

            }
        }
    }

    void ReadLevel (int level)
    {
        string levelName = "level"+level.ToString() + ".txt";
        try
        {
            StreamReader input = File.OpenText(Path.Combine(Application.streamingAssetsPath, levelName));
            string line = input.ReadLine();

            while (line != null)
            {
                levelLines.Add(line);
                line = input.ReadLine();
            }

            rows = levelLines.Count;
            columns = levelLines[0].Length;
        }
        catch (Exception e)
        {
            throw;
        }
        
    }

    Color GetColor(char colorChar)
    {
        switch(colorChar)
        {
            case 'R':
                return Color.red;
            case 'B':
                return Color.blue;
            case 'G':
                return Color.green;
            case 'K':
                return Color.black;
            case 'W':
                return Color.white;
            case 'C':
                return Color.cyan;
            case 'Y':
                return Color.yellow;
            case 'M':
                return Color.magenta;
            case 'A':
                return Color.gray;
        }

        return Color.white;
    }

}
