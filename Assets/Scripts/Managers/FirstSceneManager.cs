using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Managers
{
    public class FirstSceneManager : MonoBehaviour
    {
        private int lvl;
        private int thisLevel;
        private void Start()
        {
            if (!ES3.KeyExists("First"))
                SaveManager.Instance.FirstScene();

            thisLevel = SaveManager.Instance.GetLevel();
            lvl = (thisLevel > 5) ? Random.Range(1, 5) : thisLevel;

            SceneManager.LoadScene(lvl);
        }
    
    
    }
}
