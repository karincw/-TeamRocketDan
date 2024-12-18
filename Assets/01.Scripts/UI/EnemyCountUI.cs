using TMPro;
using UnityEngine;

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
            if(!isEnd)
            {
                isEnd = true;
                Debug.Log("게임이 끝났습니다.");
            }
            return;
        }
        enemyCount += value;
        countText.text = enemyCount + "/" + maxCount + "마리";
    }
}
