using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder)
        {
            if (Mathf.Abs(vertical) > 0f)
            {
                isClimbing = true;
            }
            else
            {
                isClimbing = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;

            if (!isLadder)
            {
                // Stop moving upwards when not on the ladder
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Ladder"))
    {
        Debug.Log("Entered Ladder");
    }
}

void OnTriggerExit2D(Collider2D other)
{
    if (other.CompareTag("Ladder"))
    {
        Debug.Log("Exited ladder");
    }
}

}
