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
            MakeObjectAppearOrDisappear("RedUI", true);
            MakeObjectAppearOrDisappear("OrangeUI", false);
            MakeObjectAppearOrDisappear("BlueUI", false);
            MakeObjectAppearOrDisappear("GreenUI", false);
        }
    }

    public void SetOrange()
    {
        if (!isOrange)
        {
            ChangeColor(ColorEnum.ORANGE);
            MakeObjectAppearOrDisappear("RedUI", false);
            MakeObjectAppearOrDisappear("OrangeUI", true);
            MakeObjectAppearOrDisappear("BlueUI", false);
            MakeObjectAppearOrDisappear("GreenUI", false);
        }
    }

    public void SetBlue()
    {
        if (!isBlue)
        {
            ChangeColor(ColorEnum.BLUE);
            MakeObjectAppearOrDisappear("RedUI", false);
            MakeObjectAppearOrDisappear("OrangeUI", false);
            MakeObjectAppearOrDisappear("BlueUI", true);
            MakeObjectAppearOrDisappear("GreenUI", false);
        }
    }

    public void SetGreen()
    {
        if (!isGreen)
        {
            ChangeColor(ColorEnum.GREEN);
            MakeObjectAppearOrDisappear("RedUI", false);
            MakeObjectAppearOrDisappear("OrangeUI", false);
            MakeObjectAppearOrDisappear("BlueUI", false);
            MakeObjectAppearOrDisappear("GreenUI", true);
        }
    }

    public Transform FindInHierarchy(string name)
    {
        // Recherche récursive dans la hiérarchie pour trouver un objet par son nom
        foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
        {
            if (child.name == name)
            {
                return child;
            }
        }
        return null; // Retourne null si aucun objet n'a été trouvé
    }

    public void MakeObjectAppearOrDisappear(string name, bool appear)
    {
        // Trouve le GameObject enfant avec le nom spécifié
        Transform child = FindInHierarchy(name);

        if (child != null)
        {
            // Vérifie si le GameObject contient un composant Button
            GameObject button = child.gameObject;

            // Active le GameObject pour le rendre visible
            button.SetActive(appear);
        }
        else
        {
            // Affiche un avertissement si aucun enfant n'a été trouvé
            Debug.LogWarning($"Aucun object trouvé avec le nom '{name}'.");
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



