using UnityEngine;

public class StudentController : MonoBehaviour
{
    
    public float speed;
    Rigidbody2D rb2d;

    private SpriteRenderer playerSpriteRenderer;

    public Sprite spriteUp;

    public Sprite spriteDown;

    public Sprite spriteLeft;

    public Sprite spriteRight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = 0;
        float inputY = 0;

        if(Input.GetKey(KeyCode.LeftArrow)) 
        {
            inputX = -1;
        }
        if(Input.GetKey(KeyCode.RightArrow)) 
        {
            inputX = 1;
        }
        if(Input.GetKey(KeyCode.UpArrow)) 
        {
            inputY = 1;
        }
        if(Input.GetKey(KeyCode.DownArrow)) 
        {
            inputY = -1;
        }

        Vector2 direction = new Vector2(inputX, inputY);
        if(direction.magnitude > 1) 
        {
            direction.Normalize();
        }
        rb2d.linearVelocity = direction * speed;

        if(Input.GetKeyDown(KeyCode.RightShift)) 
        {
            //
        }

        AnimatePlayerSprite();
    }

    private void AnimatePlayerSprite() {

        Vector2 player_velocity = rb2d.linearVelocity;

        // moving up
        if (player_velocity.y > 0) 
        {
            playerSpriteRenderer.sprite = spriteUp;
        }

        // moving down
        if (player_velocity.y < 0) 
        {
            playerSpriteRenderer.sprite = spriteDown;
        }

        // moving left
        if (player_velocity.x < 0) 
        {
            playerSpriteRenderer.sprite = spriteLeft;
            playerSpriteRenderer.flipX = true;
        }

        // moving right
        if (player_velocity.x > 0) 
        {
            playerSpriteRenderer.sprite = spriteRight;
        }

    }


}
