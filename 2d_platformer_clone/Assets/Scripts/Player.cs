using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    float h;

    bool isJump = false;
    bool isRun = false;

    LayerMask ground = 3;

    public float force = 5f;
    public float runForce = 7f;
    public float jumpForce = 8f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Inputs();
        Animation();
    }

    void Inputs() {
        h = Input.GetAxisRaw("Horizontal");
        isRun = Input.GetKey(KeyCode.LeftShift);

        // Move right and left
        if(h != 0) {
            Vector3 moveVec =  new Vector3(h, 0, 0) * Time.deltaTime;
            if(isRun) {
                moveVec *= runForce;
            }
            else {
                moveVec *= force;
            }

            if(h < 0) {
                spriteRenderer.flipX = true;
            }
            else {
                spriteRenderer.flipX = false;
            }

            transform.position += moveVec;
        }
        // Jump
        if(Input.GetKeyDown(KeyCode.Space) && !isJump) {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJump = true;
        }
    }

    void Animation() {
        anim.SetBool("isJump", isJump);
        anim.SetBool("isRun", h != 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == ground) {
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Finish")) {

        }
    }
}
