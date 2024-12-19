using Leo.Sound;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Leo.UI
{
    public class CutSceneManager : MonoSingleton<CutSceneManager>
    {
        [SerializeField] private List<CutSceneCell> _cutSceneCells = new();
        [SerializeField] private SoundObject _soundObject;
         public int _index = 0;

        private void Update()
        {
            if (_index >= _cutSceneCells.Count)
            {
                _cutSceneCells[_index-=2].Play(LoadMapScene);
                return;
            }
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && _index < _cutSceneCells.Count)
            {
                _cutSceneCells[_index].Play();
                _index++;
                _soundObject.Play();
            } 
            
            
        }

        private void LoadMapScene()
        {
            SceneManager.LoadScene("MapScene");
        }
    }
}

