using Enums;
using Lean.Touch;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private bool isFull;
        private bool canMove;


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

        private void OnFingerDown(LeanFinger finger)
        {
            canMove = true;
        }

        private void OnFingerUp(LeanFinger finger)
        {
            isFull = false;
            canMove = false;
       
        
        }


        private void OnFingerUpdate(LeanFinger finger)
        {
            if (!canMove) return;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit) ||
                !hit.collider.transform.parent.TryGetComponent(out Block block) || isFull ||
                block.lastSituation != LastCondition.NotTrue) return;
            isFull = true;
            block.blockSituation = BlockSituation.Dragable;
        }

    }
}
