using UnityEngine;

public class OpenDialogAnimController : MonoBehaviour
{
    [SerializeField] private Animator targetAnimator; // Référence à l'Animator de l'autre objet

    public void TriggerAnimation()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetBool("Click", true); // Modifie le paramètre "Click" sur l'Animator
        }
        else
        {
            Debug.LogWarning("Animator non assigné !");
        }
    }
}