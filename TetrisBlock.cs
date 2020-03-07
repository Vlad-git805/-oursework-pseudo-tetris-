﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private float prevTime;
    public float fallTime = 0.9f;

	void Start () {
		
	}
	
	void Update () {
        //Move the figure to the right and to the left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
        }

        //vertical movement
        if (Time.time - prevTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
            transform.position += new Vector3(0, -1, 0);
            prevTime = Time.time;
        }
    }
}
