using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



[RequireComponent(typeof(CanvasGroup))]

public class Drag: MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Canvas canvas;
    CanvasGroup canvasGroup;
    public Collider2D containerCollider;  // Le collider du container
    public Collider2D bubbleCollider;    // Le collider de la bulle


    public AudioSource audioSource;
    
    public float duration_fade;


    public bool AreColliding()
    {
        // Vérifier si les boîtes englobantes (bounds) des deux colliders se croisent
        return bubbleCollider.bounds.Intersects(containerCollider.bounds);
    }

    public void FadeIn()
    {
        StartCoroutine(FadeAudio(0f, 1f));
    }

    // Fade-out de l'audio
    public void FadeOut()
    {
        audioSource.volume = 0;
    }

    // Coroutine générique pour gérer le fade
    private IEnumerator FadeAudio(float startVolume, float targetVolume)
    {
        float elapsedTime = 0f;
        audioSource.volume = startVolume;

        while (elapsedTime < duration_fade)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration_fade);
            yield return null;
        }

        // S'assurer que le volume final est exactement celui attendu
        audioSource.volume = targetVolume;

    }


    void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.7f;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        var are_colliding= AreColliding();
        Debug.Log("AreRectsOverlapping"+are_colliding);
        canvasGroup.alpha = 1f;
        if( are_colliding)
        {
            Debug.Log("Fade In");
            FadeIn();
        }

        else 
        {
            Debug.Log("Fade Out");
            FadeOut();
        }
        
        
        Debug.Log("OnEndDrag");
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointer");
    }
}
