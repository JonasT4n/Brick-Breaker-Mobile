using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIMain : MonoBehaviour
{
    private void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
        }
    }
    private void OnMouseDown() 
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }
}
