using System.Collections;
using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    [Header("Door Sprites")]
    [SerializeField] private Sprite[] doorSprites = new Sprite[3];
    
    [Header("Sound Effects")]
    [SerializeField] private AudioClip sound;
    private float soundVolume = 0.7f;
    
    [Header("Settings")]
    [SerializeField] private float transitionDelay = 0.15f;
    private string playerTag = "Player";
    
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool isOpen = false;
    private bool isAnimating = false;
    private Coroutine animationCoroutine;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && !isOpen)
        {
            OpenDoor();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag) && isOpen)
        {
            CloseDoor();
        }
    }
    
    private void OpenDoor()
    {
        if (isAnimating)
        {
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
        }
        AudioSource.PlayClipAtPoint(sound, transform.position, soundVolume);
        animationCoroutine = StartCoroutine(AnimateDoor(true));
    }
    
    private void CloseDoor()
    {
        if (isAnimating)
        {
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);
        }
        AudioSource.PlayClipAtPoint(sound, transform.position, soundVolume);
        animationCoroutine = StartCoroutine(AnimateDoor(false));
    }
    
    private IEnumerator AnimateDoor(bool opening)
    {
        isAnimating = true;
        
        if (opening)
        {
            // Play opening animation sequence
            spriteRenderer.sprite = doorSprites[0];
            yield return new WaitForSeconds(transitionDelay);
            
            spriteRenderer.sprite = doorSprites[1];
            yield return new WaitForSeconds(transitionDelay);
            
            spriteRenderer.sprite = doorSprites[2];
            isOpen = true;
        }
        else
        {
            // Play closing animation sequence
            spriteRenderer.sprite = doorSprites[2];
            yield return new WaitForSeconds(transitionDelay);
            
            spriteRenderer.sprite = doorSprites[1];
            yield return new WaitForSeconds(transitionDelay);
            
            spriteRenderer.sprite = doorSprites[0];
            isOpen = false;
        }
        
        isAnimating = false;
    }
}