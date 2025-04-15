using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    Rigidbody2D rb2d;
    [Header("Moving")]
    public KeyCode inputUp = KeyCode.UpArrow;
    public KeyCode inputDown = KeyCode.DownArrow;
    public KeyCode inputLeft = KeyCode.LeftArrow;
    public KeyCode inputRight = KeyCode.RightArrow;
    public KeyCode shift = KeyCode.RightShift;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moving
        float inputX = 0;
        float inputY = 0;

        if (Input.GetKey(inputLeft)) {
            inputX = -1;
        } if (Input.GetKey(inputRight)) {
            inputX = 1;
        } if (Input.GetKey(inputUp)) {
            inputY = 1;
        } if (Input.GetKey(inputDown)) {
            inputY = -1;
        }
        Vector2 direction = new Vector2(inputX, inputY);
        if (direction.magnitude > 1) {
            direction.Normalize();
        }
        rb2d.linearVelocity = direction * speed;

    }

    public void OpenInteractableIcon() {}
    

}
