using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    Rigidbody2D rb2d;
    [Header("Interact")]
    public GameObject interactIcon;
    private Vector2 boxSize = new Vector2(1.2f, 1.2f); // interaction area size
    public LayerMask interactableLayer;
    private Interactable currentInteractable = null;
    [Header("Animation")]
    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteRight;
    public SpriteRenderer _playerSpriteRenderer;
    public Sprite[] spriteUpFrames;
    public Sprite[] spriteDownFrames;
    public Sprite[] spriteRightFrames;
    int currentFrameIndex = 0;
    float frameTimer;
    private float framesPerSecond = 6;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        interactIcon.SetActive(false);
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        frameTimer = 1f / framesPerSecond;
        currentFrameIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Moving
        float inputX = 0;
        float inputY = 0;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            inputX = -1;
        } if (Input.GetKey(KeyCode.RightArrow)) {
            inputX = 1;
        } if (Input.GetKey(KeyCode.UpArrow)) {
            inputY = 1;
        } if (Input.GetKey(KeyCode.DownArrow)) {
            inputY = -1;
        }
        Vector2 direction = new Vector2(inputX, inputY);
        if (direction.magnitude > 1) {
            direction.Normalize();
        }
        rb2d.linearVelocity = direction * speed;

        // Interaction
        FindNearbyInteractable();
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.Space) && currentInteractable != null) {
            currentInteractable.Interact();
            Debug.Log("player clicked space");
        }

        // Animation
        AnimateSprite();

    }

    public void CheckInteraction() {
        if (currentInteractable != null) {
            interactIcon.SetActive(true);
            interactIcon.transform.position = currentInteractable.transform.position + Vector3.up * 1.2f;
        } else {
            interactIcon.SetActive(false);
        }
    }
    void FindNearbyInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f, interactableLayer);
        currentInteractable = null;
        foreach (var hit in hits)
        {
            var interact = hit.GetComponent<Interactable>();
            if (interact != null)
            {
                currentInteractable = interact;
                break;
            }
        }
    }

    private void AnimateSprite() {
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0) {
            currentFrameIndex++;
            if (currentFrameIndex >= spriteRightFrames.Length) {
                currentFrameIndex = 0;
            }
            frameTimer = 1f / framesPerSecond;
            if (Input.GetKey(KeyCode.UpArrow)) {
                _playerSpriteRenderer.sprite = spriteUpFrames[currentFrameIndex];
            } else if (Input.GetKey(KeyCode.DownArrow)) {
            _playerSpriteRenderer.sprite = spriteDownFrames[currentFrameIndex];
            } else if (Input.GetKey(KeyCode.LeftArrow)) {
                _playerSpriteRenderer.sprite = spriteRightFrames[currentFrameIndex];
                _playerSpriteRenderer.flipX = true;
            } else if (Input.GetKey(KeyCode.RightArrow)) {
                _playerSpriteRenderer.sprite = spriteRightFrames[currentFrameIndex];
                _playerSpriteRenderer.flipX = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            _playerSpriteRenderer.sprite = spriteUp;
        } else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            _playerSpriteRenderer.sprite = spriteDown;
        } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            _playerSpriteRenderer.sprite = spriteRight;
            _playerSpriteRenderer.flipX = true;
        } else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            _playerSpriteRenderer.sprite = spriteRight;
            _playerSpriteRenderer.flipX = false;
        }
    }
}
