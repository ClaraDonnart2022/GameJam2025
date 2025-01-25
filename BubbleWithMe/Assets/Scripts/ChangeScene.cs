using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class SceneState
{
    // Liste de données pour chaque objet à suivre
    public List<GameObjectState> gameObjectStates;
}

[System.Serializable]
public class GameObjectState
{
    public string objectName;
    public bool isActive;
    public Vector3 position;
    public Quaternion rotation;
    public List<GameObjectState> childrenStates;
}

public class ChangeScene : MonoBehaviour
{
    public string scene1 = "BubbleRoom";
    public string scene2 = "Phone";

    // Chemin pour sauvegarder l'état des objets dans un fichier JSON
    private string sceneDataPath = "Assets/Scenes/SceneData.json";


    // Sauvegarder l'état d'activation des objets de la scène
    public void SaveSceneState()
    {
        SceneState sceneState = new SceneState();
        sceneState.gameObjectStates = new List<GameObjectState>();

        // Trouver tous les objets dans la scène
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Sauvegarder l'état de chaque objet
        foreach (var obj in allObjects)
        {
            GameObjectState objState = new GameObjectState
            {
                objectName = obj.name,
                isActive = obj.activeSelf,
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                childrenStates = new List<GameObjectState>()
            };

            // Sauvegarder l'état des enfants de cet objet
            foreach (Transform child in obj.transform)
            {
                GameObjectState childState = new GameObjectState
                {
                    objectName = child.gameObject.name,
                    isActive = child.gameObject.activeSelf,
                    position = child.transform.position,
                    rotation = child.transform.rotation
                };
                objState.childrenStates.Add(childState);
            }

            sceneState.gameObjectStates.Add(objState);
        }

        // Sauvegarder les états dans un fichier JSON
        string json = JsonUtility.ToJson(sceneState);
        File.WriteAllText(sceneDataPath, json);
        Debug.Log("Scene state saved." + json);
    }

    // Restaurer l'état des objets de la scène
    public void RestoreSceneState()
    {
        if (File.Exists(sceneDataPath))
        {
            string json = File.ReadAllText(sceneDataPath);
            Debug.Log("json2: " + json);
            SceneState sceneState = JsonUtility.FromJson<SceneState>(json);

            // Récupérer l'état de chaque objet et le restaurer
            foreach (var objState in sceneState.gameObjectStates)
            {
                GameObject obj = GameObject.Find(objState.objectName);
                if (obj != null)
                {
                    obj.SetActive(objState.isActive);
                    obj.transform.position = objState.position;
                    obj.transform.rotation = objState.rotation;

                    // Restaurer l'état des enfants
                    foreach (var childState in objState.childrenStates)
                    {
                        GameObject child = obj.transform.Find(childState.objectName)?.gameObject;
                        if (child != null)
                        {
                            child.SetActive(childState.isActive);
                            child.transform.position = childState.position;
                            child.transform.rotation = childState.rotation;
                        }
                        else
                        {
                            Debug.LogWarning($"Child object {childState.objectName} not found for restoration in {obj.name}");
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"Object {objState.objectName} not found in the scene for restoration!");
                }
            }

            Debug.Log("Scene state restored.");
        }
        else
        {
            Debug.LogWarning("No saved data found to restore.");
        }
    }

    // Fonction pour changer de scène avec sauvegarde et restauration
    public void ToggleScene()
    {

         // Obtenir le nom de la scène active
        string currentScene = SceneManager.GetActiveScene().name;

        // Sauvegarder l'état avant de changer de scène
        if (currentScene == scene2){
            SaveSceneState();
        }
        

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

    // Lors de l'activation d'une scène, restaurer l'état
    void OnEnable()
    {
        // Restaurer l'état si la scène actuelle est celle qu'on veut restaurer
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == scene2)
        {
            RestoreSceneState();
        }
    }
}
