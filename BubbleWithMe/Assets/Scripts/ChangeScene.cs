using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ChangeScene : MonoBehaviour
{

    public GameObject Phone;
    public GameObject BubbleRoom;


    // Fonction pour changer de scène avec sauvegarde et restauration
    public void ToggleScene()
    {
        Phone.SetActive(!Phone.activeSelf);
        BubbleRoom.SetActive(!BubbleRoom.activeSelf);

    }

}
