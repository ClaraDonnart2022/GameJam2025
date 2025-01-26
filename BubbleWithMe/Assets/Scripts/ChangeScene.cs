using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ChangeScene : MonoBehaviour
{

    public GameObject Phone;
    public GameObject BubbleRoom;

   [SerializeField]
    public List<GameObject> ColorContainer;

    private SceneManager sceneManager;

    
    // Fonction pour mettre à jour l'état des enfants
    public void UpdateAnimals()
    {
        sceneManager = GetComponent<SceneManager>();
        if (ColorContainer.Count != sceneManager.isFinished.Count)
        {
            Debug.LogError("ColorContainer et isFinished doivent avoir la même longueur !");
            return;
        }

        for (int i = 0; i < ColorContainer.Count; i++)
        {
            GameObject parent = ColorContainer[i];
            
            bool shouldActivateAnimal = sceneManager.isFinished[i];
            parent.GetComponent<Drag>().enabled = shouldActivateAnimal;


            Transform egg = FindChildWithTag(parent.transform, "Egg");
            Transform animal = FindChildWithTag(parent.transform, "Animal");

            if(egg){
                Debug.Log("egg"+egg.name);
            }
            if(animal){
                Debug.Log("animal"+animal.name);
            }
            

            if (egg != null && animal != null)
            {
                // Activer/désactiver en fonction de isFinished
                egg.gameObject.SetActive(!shouldActivateAnimal);
                animal.gameObject.SetActive(shouldActivateAnimal);

                Debug.Log($"GameObject: {parent.name}, Egg Active: {!shouldActivateAnimal}, Animal Active: {shouldActivateAnimal}");
            }
            else
            {
                Debug.LogWarning($"Les enfants Egg ou Animal sont manquants pour {parent.name}");
            }
        }
    }

    // Fonction utilitaire pour trouver un enfant avec un tag donné
    private Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform child in parent.GetComponentsInChildren<Transform>(true))
        {
            if (child.CompareTag(tag))
            {
                return child;
            }
        }
        return null;
    }


    // Fonction pour changer de scène avec sauvegarde et restauration
    public void ToggleScene()
    {
        Phone.SetActive(!Phone.activeSelf);
        BubbleRoom.SetActive(!BubbleRoom.activeSelf);
        UpdateAnimals();

    }

}
