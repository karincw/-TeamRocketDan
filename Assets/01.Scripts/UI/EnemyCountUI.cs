using System;
using TMPro;
using UnityEngine;

namespace JSY
{
    public class EnemyCountUI : MonoSingleton<EnemyCountUI>
    {
        private TextMeshProUGUI countText;
        private int enemyCount = 0;
        [SerializeField]private int maxCount = 30;

        public bool isEnd { get; private set; }

        protected override void Awake()
        {
            base.Awake(); 
            countText = GetComponentInChildren<TextMeshProUGUI>();
            UpdateCount(0);
        }

        public bool IsAllDead() => enemyCount == 0 ? true : false;

        public void GameOver()
        {
            isEnd = true;
            ResultUI.Instance.GameOver();
        }

        private void Update()
        {
            UpdateCount(0); 
        }

        public void UpdateCount(int value)
        {
            if (enemyCount == maxCount)
            {
                if (!isEnd)
                {
                    GameOver();
                }
                return;
            }
            enemyCount = EnemyCreateManager.Instance.enemyParent.childCount;

            if (enemyCount >= 20)
                countText.text = "<color=red>" + enemyCount + "/" + maxCount + "마리";
            else
                countText.text = enemyCount + "/" + maxCount + "마리";
        }

        
    }
}
