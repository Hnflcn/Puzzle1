using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [HideInInspector] public MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
}
