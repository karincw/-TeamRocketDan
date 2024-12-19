using TMPro;

namespace JSY
{
    public class EnemyCountUI : MonoSingleton<EnemyCountUI>
    {
        private TextMeshProUGUI countText;
        private int enemyCount = 0;
        private int maxCount = 35;

        private bool isEnd;

        protected override void Awake()
        {
            countText = GetComponentInChildren<TextMeshProUGUI>();
            UpdateCount(0);
        }

        public void UpdateCount(int value)
        {
            if (enemyCount == maxCount)
            {
                if (!isEnd)
                {
                    isEnd = true;
                    ResultUI.Instance.GameOver();
                }
                return;
            }
            enemyCount += value;
            countText.text = enemyCount + "/" + maxCount + "¸¶¸®";
        }
    }
}
