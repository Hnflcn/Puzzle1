using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private Button finishButton;
        [SerializeField] private GameObject finishPanel;

        private void Start()
        {
            finishButton.onClick.AddListener(FinishButtonClick);
            levelText.text = "Level: " + SaveManager.Instance.GetLevel();
        
        }

        private void FinishButtonClick()
        {
            EventManager.NextClick?.Invoke();
        }

        public void OpenFinishPanel()
        {
            finishPanel.SetActive(true);
        }
    
 
    
    }
}
