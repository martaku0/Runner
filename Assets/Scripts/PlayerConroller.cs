using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConroller : MonoBehaviour
{
    public float jumpForce;
    public float liftingForce;

    public bool jumped;
    public bool doubleJumped;

    public LayerMask whatIsGround;

    public Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;
    private float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.inGame) return;

        if(IsGrounded() && Time.time >= timestamp)
        {
            if(jumped || doubleJumped)
            {
                jumped = false;
                doubleJumped = false;
            }

            timestamp = Time.time + 1f;
        }

        if(Input.GetMouseButtonDown(0))
        {
            SoundManager.instance.PlayOnceClick();

            if(!jumped)
            {
                SoundManager.instance.PlayOnceJump();
                rb2d.velocity = (new Vector2(0f, jumpForce));
                jumped = true;
            }
            else if(!doubleJumped)
            {
                SoundManager.instance.PlayOnceJump();
                rb2d. velocity = (new Vector2(0f, jumpForce));
                doubleJumped = true;
            }
        }

        if(Input.GetMouseButton(0))
        {
            rb2d.AddForce(new Vector2(0f, liftingForce * Time.deltaTime));
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, 0.1f, whatIsGround);
        return hit.collider != null;
    }

    void PlayerDeath()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

        GameManager.instance.GameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            PlayerDeath();
        }
        else if (other.CompareTag("Coin"))
        {
            GameManager.instance.CoinCollected();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Immortality") && !GameManager.instance.isImmortal)
        {
            GameManager.instance.ImmortalityCollected();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Magnet"))
        {
            GameManager.instance.MagnetCollected();
            Destroy(other.gameObject);
        }
    }
}
