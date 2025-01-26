using UnityEngine;

public class Red : MonoBehaviour
{
    public int response;
    public GameObject sceneManager;


    public void DestroyElement(string id)
    {
        // Parcourt tous les enfants du transform
        foreach (Transform child in transform)
        {
            // Vérifie si le nom de l'enfant correspond à l'id
            if (child.name == id)
            {
                // Détruit l'enfant correspondant
                Destroy(child.gameObject);
                return; // Quitte la méthode après avoir détruit l'enfant
            }
        }

        // Si aucun enfant n'a été trouvé avec le nom donné
        Debug.LogWarning($"Aucun enfant trouvé avec le nom '{id}'.");
    
    }

    // Cette méthode permet de définir la valeur de "response"
    public void PopulateSceneManager(int i)
    {
        SceneManager managerScript = sceneManager.GetComponent<SceneManager>();
        if (managerScript != null)
        {
            // Modifier un élément dans la liste
            managerScript.isFinished[i] = true;
            Debug.Log($"isFinished[{i}] est maintenant : {managerScript.isFinished[i]}");
        }
        else
        {
            Debug.LogError("Le script SceneManagerScript n'est pas attaché à sceneManager !");
        }
    }

    public void MakeButtonAppear(string name)
    {
        // Trouve le GameObject enfant avec le nom spécifié
        Transform child = transform.Find(name);

        if (child != null)
        {
            // Vérifie si le GameObject contient un composant Button
            GameObject button = child.gameObject;

            // Active le GameObject pour le rendre visible
            button.SetActive(true);
        }
        else
        {
            // Affiche un avertissement si aucun enfant n'a été trouvé
            Debug.LogWarning($"Aucun bouton trouvé avec le nom '{name}'.");
        }
    
    }
}