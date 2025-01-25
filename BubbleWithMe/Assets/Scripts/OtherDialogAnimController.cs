using UnityEngine;
using System.Collections.Generic;

public class OtherDialogAnimController : StateMachineBehaviour
{

    
    [SerializeField] private List<GameObject> prefabs; // Liste des prefabs assignables dans l'Inspecteur
    [SerializeField] private string parentName = "RedUI"; // Nom du parent dans la hiérarchie
    [SerializeField] private Vector3 spawnOffset = Vector3.zero; // Décalage de la position d'apparition


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SpawnFirstPrefab();
    }

    public void SpawnFirstPrefab()
    {
        if (prefabs.Count == 0)
        {
            Debug.LogError("La liste des prefabs est vide !");
            return;
        }

        // Trouver le parent nommé "RedUi" dans la hiérarchie
        GameObject parentObject = GameObject.Find(parentName);

        if (parentObject == null)
        {
            Debug.LogError($"Aucun GameObject nommé '{parentName}' trouvé dans la hiérarchie !");
            return;
        }

        // Instancier le prefab comme enfant de "RedUi"
        GameObject spawnedPrefab = Instantiate(
            prefabs[0],
            parentObject.transform.position + spawnOffset, // Position basée sur le parent avec un décalage
            parentObject.transform.rotation, // Rotation basée sur le parent
            parentObject.transform // Définit "RedUi" comme parent
        );

        // Afficher un message dans la console
        Debug.Log($"Prefab {prefabs[0].name} instancié sous {parentObject.name} !");

        // Récupérer la position actuelle du prefab instancié
        Vector3 currentPosition = spawnedPrefab.transform.position;
        Debug.Log("currentPrefab " + currentPosition);

        // Modifier la position du prefab en X: 20, Y: -7, en tenant compte de la position actuelle de Z
        // Utilisez localPosition pour définir la position relative au parent
        spawnedPrefab.transform.localPosition = new Vector3(-20f, -5f, currentPosition.z);

        // Afficher la nouvelle position dans la console
        Debug.Log("spawnedPrefab " + spawnedPrefab.transform.localPosition);

    }
}
