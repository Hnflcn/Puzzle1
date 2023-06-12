using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private float upBorder;
    private float downBorder;
    private float rightBorder;
    private float leftBorder;

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
        
        downBorder = firstPosition.y - .5f;
        upBorder = firstPosition.y + .5f;
        rightBorder = firstPosition.x + .5f;
        leftBorder = firstPosition.x - .5f;
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
        if (blockSituation == BlockSituation.Dragable)
        {
            blockSituation = BlockSituation.NotDragable;
            var pos = transform.position;

            if (pos.y > downBorder && pos.y < upBorder && pos.x < rightBorder && pos.x > leftBorder)
            {
                transform.position = firstPosition;
                lastSituation = LastCondition.InTrueCondition;
                EventManager.ControlFinish?.Invoke(this);
            }
            else
            {
                transform.position = new Vector3(pos.x, pos.y, 0);
            }
        }
        
    }

    protected virtual void OnFingerUpdate(LeanFinger finger)
    {
        if (canMove  && blockSituation == BlockSituation.Dragable)
        {
            Moving();
        }
    }

    #endregion

}
