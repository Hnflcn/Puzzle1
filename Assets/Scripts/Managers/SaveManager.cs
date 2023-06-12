using UnityEngine;

namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        public static SaveManager Instance;

        private void Awake()
        {
            Instance ??= this;
        }

        public void SaveLevel(int value)
        {
            ES3.Save<int>("Level", value);
        }

        public int GetLevel()
        {
            return ES3.Load<int>("Level");
        }

        public void FirstScene()
        {
            ES3.Save<int>("Level", 1);
            ES3.Save<bool>("First", false);
        }

    }
}
