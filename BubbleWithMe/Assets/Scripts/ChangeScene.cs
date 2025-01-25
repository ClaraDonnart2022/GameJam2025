using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene: MonoBehaviour
{
    public string scene1 = "BubbleRoom";
    public string scene2 = "Phone";

    public void ToggleScene()
    {
        // Obtenir le nom de la scène active
        string currentScene = SceneManager.GetActiveScene().name;
        // Vérifier la scène active et basculer
        if (currentScene == scene1)
        {
            SceneManager.LoadScene(scene2);
        }
        else
        {
            SceneManager.LoadScene(scene1);
        }
    }
}
