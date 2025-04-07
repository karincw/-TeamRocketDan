using TMPro;
using UnityEngine;

namespace JSY
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField] private Transform waveUITrm;
        private TextMeshProUGUI waveText;
        private int waveCount = -1;

        private void Awake()
        {
            waveText = waveUITrm.GetComponentInChildren<TextMeshProUGUI>();
            HandleStartTurnEvent();
            WaveManager.Instance.OnStartTurnEvent += HandleStartTurnEvent;
        }

        private void HandleStartTurnEvent()
        {
            waveCount++;
            waveText.text = "웨이브 " + waveCount;
        }
    }
}
