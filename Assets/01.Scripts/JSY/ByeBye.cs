using UnityEngine;
using UnityEngine.SceneManagement;

public class ByeBye : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
