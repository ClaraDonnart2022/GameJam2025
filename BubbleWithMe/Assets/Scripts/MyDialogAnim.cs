using UnityEngine;
using System.Collections.Generic;

public class MyDialogAnim : StateMachineBehaviour
{
    [System.Serializable] // Important pour permettre la sérialisation dans l'inspecteur
    public class PrefabList
    {
        public List<GameObject> prefabs = new List<GameObject>(); // Liste des prefabs
    }

    [SerializeField] public List<PrefabList> prefabsLists = new List<PrefabList>(); // Liste des prefabs assignables dans l'Inspecteur
    [SerializeField] private string parentName = "RedUI"; // Nom du parent dans la hiérarchie
    [SerializeField] private Vector3 spawnOffset = Vector3.zero; // Décalage de la position d'apparition

    // Méthode appelée lorsqu'on entre dans l'état de l'animation
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetInt("i", 1);
        SpawnFirstPrefab();
    }

    // Méthode pour instancier le premier prefab de la première sous-liste
    public void SpawnFirstPrefab()
    {
        if (prefabsLists.Count == 0)
        {
            Debug.LogError("La liste des prefabs est vide !");
            return;
        }

        // Trouver le parent nommé "RedUI" dans la hiérarchie
        GameObject parentObject = GameObject.Find(parentName);

        if (parentObject == null)
        {
            Debug.LogError($"Aucun GameObject nommé '{parentName}' trouvé dans la hiérarchie !");
            return;
        }
        
        int j= 0;
        // Parcourir tous les prefabs dans la première sous-liste
        foreach (GameObject prefab in prefabsLists[0].prefabs)  // Accéder à la liste de prefabs dans la première sous-liste
        {
            // Instancier le prefab
            GameObject spawnedPrefab = Instantiate(
                prefab,
                parentObject.transform.position + spawnOffset,  // Position basée sur le parent avec un décalage
                parentObject.transform.rotation,                 // Rotation basée sur le parent
                parentObject.transform                           // Définit "RedUi" comme parent
            );

            

            Debug.Log($"Prefab {prefab.name} instancié sous {parentObject.name} !");

            // Récupérer la position actuelle du prefab instancié
            Vector3 currentPosition = spawnedPrefab.transform.position;
            Debug.Log("currentPrefab " + currentPosition);

            // Modifier la position du prefab en X: 20, Y: -7, en tenant compte de la position actuelle de Z
            // Utilisez localPosition pour définir la position relative au parent
            spawnedPrefab.transform.localPosition = new Vector3(20f +j*20f, -20f, 0f);
            j++;

            // Afficher la nouvelle position dans la console
            Debug.Log("spawnedPrefab " + spawnedPrefab.transform.localPosition);
        }
    }
}