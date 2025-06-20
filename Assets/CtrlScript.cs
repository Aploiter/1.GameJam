using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float speedX = Input.GetAxis("Horizontal") * movSpeed;
        float speedY = Input.GetAxis("Vertical") * movSpeed;
        rb.linearVelocity = new Vector2(speedX, speedY);
    }
}
