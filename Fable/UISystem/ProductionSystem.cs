using UnityEngine;
using UnityEngine.SceneManagement;

public class ProductionSystem : MonoBehaviour
{
    public void LoadSceneBtn()
    {
        SceneManager.LoadScene("Result");
    }
}
