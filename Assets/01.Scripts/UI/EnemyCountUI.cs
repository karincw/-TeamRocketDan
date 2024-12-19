using TMPro;

namespace JSY
{
    public class EnemyCountUI : MonoSingleton<EnemyCountUI>
    {
        private TextMeshProUGUI countText;
        private int enemyCount = 0;
        private int maxCount = 35;

        public bool isEnd { get; private set; }

        protected override void Awake()
        {
            countText = GetComponentInChildren<TextMeshProUGUI>();
            UpdateCount(0);
        }

        public bool IsAllDead() => enemyCount == 0 ? true : false;

        public void GameOver()
        {
            isEnd = true;
            ResultUI.Instance.GameOver();
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
            enemyCount += value;
            countText.text = enemyCount + "/" + maxCount + "마리";
        }

        
    }
}
