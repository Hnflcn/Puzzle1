using UnityEngine;

namespace Base
{
    public abstract class Move : MonoBehaviour
    {
        private Vector3 objPos;
        
        protected void Moving()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = 9f;
            objPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = objPos;
        }
    }
}
