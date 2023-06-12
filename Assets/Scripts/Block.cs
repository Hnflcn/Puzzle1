using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base;
using Enums;
using Lean.Touch;
using Managers;
using UnityEngine;

public class Block : Move
{
    #region Variables
    
    public BlockSituation blockSituation = BlockSituation.NotDragable;
    public LastCondition lastSituation = LastCondition.NotTrue;

    public Piece[] myPieces;

    private bool canMove;
    private bool isCondition;

    private Vector3 firstPosition;
    
    #endregion
    
    #region UnityFunctions

    private void Awake()
    {
        firstPosition = transform.position;
    }

    private void Start()
    {
        transform.position += Vector3.up * 10;
        foreach (var piece in myPieces)
        {
            piece.meshRenderer.enabled = true;
        }
    }

    private void OnEnable()
    {
        LeanTouch.OnFingerDown += OnFingerDown;
        LeanTouch.OnFingerUp += OnFingerUp;
        LeanTouch.OnFingerUpdate += OnFingerUpdate;
    }

    private void OnDisable()
    {
        LeanTouch.OnFingerDown -= OnFingerDown;
        LeanTouch.OnFingerUp -= OnFingerUp;
        LeanTouch.OnFingerUpdate -= OnFingerUpdate;
    }

    #endregion
    
    #region LeanTouchFunctions

    protected virtual void OnFingerDown(LeanFinger finger)
    {
        canMove = true;
    }

    protected virtual void OnFingerUp(LeanFinger finger)
    {
        canMove = false;
        if (blockSituation != BlockSituation.Dragable) return;

        blockSituation = BlockSituation.NotDragable;
        var pos = transform.position;
        var distance = Vector2.Distance(firstPosition, pos);

        transform.position = distance < 0.5f ? firstPosition : new Vector3(pos.x, pos.y, 0);

        if (distance < 0.5f)
        {
            lastSituation = LastCondition.InTrueCondition;
            EventManager.ControlFinish?.Invoke(this);
        }
    }

    protected virtual void OnFingerUpdate(LeanFinger finger)
    {
        switch (canMove)
        {
            case true when blockSituation == BlockSituation.Dragable:
                Moving();
                break;
        }
    }

    #endregion

}
