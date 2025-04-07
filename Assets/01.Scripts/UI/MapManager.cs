using Karin;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JSY
{
    [Serializable]
    public struct MapObject
    {
        public GameObject levelObj;
        public Transform startTrm;
        public List<MovePoint> pointGroup;
    }

    public class MapManager : MonoBehaviour
    {
        [SerializeField] private List<MapObject> mapPrefabs = new List<MapObject>();
        private Transform spawnPos;

        private int mapIndex = 0, nextMapIndex;
        private bool isChange = false;

        private void Start()
        {
            WaveManager.Instance.OnStartBossTurnEvent += HandleBossTurnEvent;
            WaveManager.Instance.OnChangeTurnEvent += HandleChangeTurnEvent;

            EnemyCreateManager.Instance.UpdatePoints(mapPrefabs[mapIndex].startTrm, mapPrefabs[mapIndex].pointGroup);

            spawnPos = mapPrefabs[mapIndex].levelObj.transform.Find("Platform");
            MochiMove.Instance.UpdatePosition(spawnPos);
            MochiManager.Instance.UpdateSpawnPos(spawnPos);
        }

        private void OnDestroy()
        {
            WaveManager.Instance.OnStartBossTurnEvent -= HandleBossTurnEvent;
            WaveManager.Instance.OnChangeTurnEvent -= HandleChangeTurnEvent;
        }

        private void HandleChangeTurnEvent()
        {
            if (!isChange) return;
            if (UIManager.Instance.EnemyCountUI.isEnd) return;
            isChange = false;
            mapPrefabs[mapIndex].levelObj.gameObject.SetActive(false);
            mapIndex = nextMapIndex;
            mapPrefabs[mapIndex].levelObj.gameObject.SetActive(true);
            spawnPos = mapPrefabs[mapIndex].levelObj.transform.Find("Platform");
            MochiMove.Instance.UpdatePosition(spawnPos);
            MochiManager.Instance.UpdateSpawnPos(spawnPos);
            EnemyCreateManager.Instance.UpdatePoints(mapPrefabs[mapIndex].startTrm, mapPrefabs[mapIndex].pointGroup);
        }

        private void HandleBossTurnEvent()
        {
            isChange = true;
            nextMapIndex++;
            if(nextMapIndex >= mapPrefabs.Count) nextMapIndex = 0;
        }
    }
}
