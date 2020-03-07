using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTetro : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    // Use this for initialization
    void Start()
    {
        NewTetromino();
    }

    public void NewTetromino()
    {
        Instantiate(Tetrominoes[Random.Range(0, Tetrominoes.Length)], transform.position, Quaternion.identity);
    }

}
