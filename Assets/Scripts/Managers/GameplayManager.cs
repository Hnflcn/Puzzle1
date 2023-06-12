using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameplayManager : MonoBehaviour
    {

        #region Variables
    
        public Block[] blocks;
        public List<Transform> positions = new List<Transform>();
        public List<Block> blocksInTruePos = new List<Block>();

        [SerializeField] private ParticleSystem particleEffect;
        [SerializeField] private UIManager uiManager;

        private int level;
        private int thisLevelCount;
        private int newLevel;
        #endregion
    
        #region Unity Functions
        private void OnEnable()
        {
            EventManager.ControlFinish.AddListener(ControlFinish);
            EventManager.NextClick.AddListener(GoNextLevel);
      
        }
        private void OnDisable()
        {
            EventManager.ControlFinish.RemoveListener(ControlFinish);
            EventManager.NextClick.RemoveListener(GoNextLevel);
        }

        private void Start()
        {
            StartCoroutine(MoveDownPlace());

            level = SaveManager.Instance.GetLevel();
            thisLevelCount = SceneManager.GetActiveScene().buildIndex;
        }
    

        #endregion
    
        #region At Beginning

        private IEnumerator MoveDownPlace()
        {
            foreach (var block in blocks)
            {
                block.transform.DOMove(positions[0].position, .1f).OnComplete(() =>
                {
                    positions.Remove(positions.First());
                });
                yield return new WaitForSeconds(.1f);
            }
        }
    
        #endregion

        #region Finish

        private void ControlFinish(Block block)
        {
            blocksInTruePos.Add(block);

            if (blocksInTruePos.Count == blocks.Length)
            {
                Finish();
            }
        }
    
        private void Finish()
        {
            particleEffect.Play();
            uiManager.OpenFinishPanel();
        }

        private void GoNextLevel()
        {
            SaveManager.Instance.SaveLevel(level + 1);
        
            if (thisLevelCount > 4)
            {
                var randomLevel = Random.Range(1, 5);
                newLevel = randomLevel;
            }
            else
            {
                newLevel = thisLevelCount + 1;
            }
        
            SceneManager.LoadScene(newLevel);
        }


        private void GenerateLevel()
        {
            //     SceneManager.LoadScene()
        }

    

        #endregion

    }
}
