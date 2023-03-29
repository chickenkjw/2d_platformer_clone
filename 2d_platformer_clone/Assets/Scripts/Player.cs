using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public GameObject spawnPoint;

    float h;

    bool isJump = false;
    bool isRun = false;

    LayerMask ground = 3;

    public float force = 5f;
    public float runForce = 7f;
    public float jumpForce = 8f;

    void Start()
    {
        transform.position = spawnPoint.transform.position;

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

        if(collision.gameObject.CompareTag("Enemy")) {
            if(collision.gameObject.name == "Body") {
                Debug.Log("몸 충돌!");
				rigid.AddForce(new Vector2(-h, 1) * jumpForce * 0.3f, ForceMode2D.Impulse);
                isJump = true;

                spriteRenderer.color = new Color(1, 0, 0, 0.3f);
                Invoke(nameof(BackColor), 0.15f);
			}
            else if(collision.gameObject.name == "Head") {
				rigid.AddForce(Vector2.up * jumpForce * 0.4f, ForceMode2D.Impulse);
				isJump = true;
			}
            else if(collision.gameObject.name == "Monster") {
                Debug.Log("몬스터에 충돌");
            }
        }
    }

    void BackColor() {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

    }
}
