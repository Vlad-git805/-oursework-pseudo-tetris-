﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float prevTime;
    public float fallTime = 0.9f;
    public static int heigh = 20;
    public static int width = 12;
    private static Transform[,] grid = new Transform[width, heigh];


    void Start()
    {

    }

    void Update()
    {
        //Move the figure to the right and to the left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //rotate
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }

        //vertical movement
        if (Time.time - prevTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckLines();
                this.enabled = false;
                FindObjectOfType<SpawnTetro>().NewTetromino();
            }
            prevTime = Time.time;
        }
    }

    void CheckLines()
    {
        for (int i = heigh - 1; i >= 0; i--)
        {
            if(HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine (int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject); ///////////////////////////
            grid[j, i] = null;
        }
    }

    void RowDown (int i)
    {
        for (int y = i; y < heigh; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null) 
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if(roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= heigh)
            {
                return false;
            }
            if (grid[roundedX, roundedY] != null)
            {
                return false;
            }
        }
        return true;
    }
}

