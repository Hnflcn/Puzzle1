using UnityEngine.Events;

namespace Managers
{
    public static class EventManager
    {
        public static UnityEvent<Block> ControlFinish = new();
        public static UnityEvent NextClick = new();
    }
}