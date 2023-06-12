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
            {
                SaveManager.Instance.FirstScene();
            }

            thisLevel = SaveManager.Instance.GetLevel();
        
            if (thisLevel > 5)
            {
                var randomLevel = Random.Range(1, 5);
                lvl = randomLevel;
            }
            else
            {
                lvl = thisLevel;
            }
        
            SceneManager.LoadScene(lvl);
        }
    
    
    }
}
