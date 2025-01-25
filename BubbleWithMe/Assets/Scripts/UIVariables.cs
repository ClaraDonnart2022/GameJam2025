using UnityEngine;
using UnityEngine.UI;

public class UIVariables : MonoBehaviour
{
    // Couleurs configurables depuis l'Inspector
    public Color redColor = Color.red;
    public Color orangeColor = new Color(1f, 0.5f, 0f); // Orange par défaut
    public Color blueColor = Color.blue;
    public Color greenColor = Color.green;

    // Référence à la RawImage cible
    [SerializeField] private RawImage targetImage;

    // Référence à l'Animator pour déclencher des animations
    [SerializeField] private Animator targetAnimator;

    // Booléens pour les couleurs
    public bool isRed = false;
    public bool isOrange = false;
    public bool isBlue = false;
    public bool isGreen = false;

    /// <summary>
    /// Énumération pour les couleurs.
    /// </summary>
    public enum ColorEnum
    {
        RED,
        ORANGE,
        BLUE,
        GREEN
    }

    /// <summary>
    /// Change la couleur active en fonction de l'énumération et applique les changements.
    /// </summary>
    /// <param name="color">Couleur sélectionnée (via l'énumération).</param>
    public void ChangeColor(ColorEnum color)
    {
        // Réinitialiser les booléens
        isRed = false;
        isOrange = false;
        isBlue = false;
        isGreen = false;

        TriggerAnimation();

        // Appliquer la couleur sélectionnée
        switch (color)
        {
            case ColorEnum.RED:
                isRed = true;
                SetRawImageColor(redColor);
                break;
            case ColorEnum.ORANGE:
                isOrange = true;
                SetRawImageColor(orangeColor);
                break;
            case ColorEnum.BLUE:
                isBlue = true;
                SetRawImageColor(blueColor);
                break;
            case ColorEnum.GREEN:
                isGreen = true;
                SetRawImageColor(greenColor);
                break;
        }

        // Déclencher une animation si un Animator est assigné
        
    }

    /// <summary>
    /// Définit la couleur de la RawImage cible.
    /// </summary>
    /// <param name="color">Couleur à appliquer.</param>
    private void SetRawImageColor(Color color)
    {
        if (targetImage != null)
        {
            targetImage.color = color;
        }
        else
        {
            Debug.LogWarning("RawImage non assignée dans l'Inspector !");
        }
    }

    /// <summary>
    /// Méthodes spécifiques pour chaque couleur.
    /// </summary>
    public void SetRed()
    {
        if (!isRed)
        {
            ChangeColor(ColorEnum.RED);
        }
    }

    public void SetOrange()
    {
        if (!isOrange)
        {
            ChangeColor(ColorEnum.ORANGE);
        }
    }

    public void SetBlue()
    {
        if (!isBlue)
        {
            ChangeColor(ColorEnum.BLUE);
        }
    }

    public void SetGreen()
    {
        if (!isGreen)
        {
            ChangeColor(ColorEnum.GREEN);
        }
    }

    /// <summary>
    /// Déclenche une animation via l'Animator.
    /// </summary>
    private void TriggerAnimation()
    {
        if (targetAnimator != null)
        {
            targetAnimator.SetBool("Click", true); // Modifier ou déclencher l'animation
        }
        else
        {
            Debug.LogWarning("Animator non assigné !");
        }
    }
}
