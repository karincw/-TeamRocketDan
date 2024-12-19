using System.Collections.Generic;
using UnityEngine;

namespace JSY
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private Transform level;
        [SerializeField] private List<GameObject> mapPrefabs = new List<GameObject>();

        private int mapIndex = 0, nextMapIndex;
        private bool isChange = false;

        private void Start()
        {
            WaveManager.Instance.OnStartBossTurnEvent += HandleBossTurnEvent;
            WaveManager.Instance.OnChangeTurnEvent += HandleChangeTurnEvent;
        }

        private void HandleChangeTurnEvent()
        {
            if (!isChange) return;
            if (EnemyCountUI.Instance.isEnd) return;
            isChange = false;
            level.GetChild(mapIndex).gameObject.SetActive(false);
            mapIndex = nextMapIndex;
            level.GetChild(mapIndex).gameObject.SetActive(true);
        }

        private void HandleBossTurnEvent()
        {
            isChange = true;
            nextMapIndex++;
            if(mapIndex >= mapPrefabs.Count) mapIndex = 0;
        }
    }
}
