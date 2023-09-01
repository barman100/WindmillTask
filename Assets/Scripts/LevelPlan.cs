using System;
using System.Collections.Generic;
using UnityEngine;


public class LevelPlan : MonoBehaviour 
{
    [Header ("Add Stages Game Object in Order")]
    public GameObject[] Stages;
    [Header("Root Node Position")]
    public List<Transform> RootNodeTransform;
}

