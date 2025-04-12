using UnityEngine;

namespace JSY
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public OptionUI OptionUI { get; private set; }
        public SpeedUI SpeedUI { get; private set; }
        public ResultUI ResultUI { get; private set; }
        public NoticeUI NoticeUI { get; private set; }
        public EnemyCountUI EnemyCountUI { get; private set; }
        public MoneyUI MoneyUI { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            OptionUI = GetComponent<OptionUI>();
            SpeedUI = GetComponent<SpeedUI>();
            ResultUI = GetComponent<ResultUI>();
            NoticeUI = GetComponent<NoticeUI>();
            EnemyCountUI = GetComponent<EnemyCountUI>();
            MoneyUI = GetComponent<MoneyUI>();
        }
    }
}
