using UnityEngine;

namespace JSY
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public OptionUI optionUI { get; private set; }
        public SpeedUI speedUI { get; private set; }
        public ResultUI ResultUI { get; private set; }
        public NoticeUI NoticeUI { get; private set; }
        public EnemyCountUI EnemyCountUI { get; private set; }
        public MoneyUI MoneyUI { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            optionUI = GetComponent<OptionUI>();
            speedUI = GetComponent<SpeedUI>();
            ResultUI = GetComponent<ResultUI>();
            NoticeUI = GetComponent<NoticeUI>();
            EnemyCountUI = GetComponent<EnemyCountUI>();
            MoneyUI = GetComponent<MoneyUI>();
        }
    }
}
