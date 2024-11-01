using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shore
{
    public GameObject shore;
    public bool isRight = true;
    public int priestCount, devilCount;
    public Shore(Vector3 position,bool isR)
    {
        isRight = isR;
        if (isRight)
        {
            shore = GameObject.Instantiate(Resources.Load("Prefabs/Rshore", typeof(GameObject))) as GameObject;
        }
        else
        {
            shore = GameObject.Instantiate(Resources.Load("Prefabs/Lshore", typeof(GameObject))) as GameObject;
        }
        shore.transform.position = position;
        priestCount = devilCount = 0;
    }
}
