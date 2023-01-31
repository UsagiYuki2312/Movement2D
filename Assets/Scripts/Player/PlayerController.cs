using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private BoxCollider2D colli;
    [SerializeField] private float runSpeed;
    private Vector3 dirMove;
    [SerializeField] private int maxJumpCount = 2;
    [SerializeField] private int jumpsRemaining = 2;
    private Vector3 vec = Vector3.zero;
    [SerializeField] private FixedJoystick joystick;

    [SerializeField] private Animator anim;
    [SerializeField] private UIMoveController uIMoveController;

    //[SerializeField] private 

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        //MoveJoystick();
    }

    private void Move()
    {

        MoveKeyBroad();
        //Force
        //Physic gia toc, van toc
        //rigid.AddForce(dirMove* runSpeed);

        //By Mouse
        //MoveByMouse();

        if (dirMove.x > 0)
        {
            gameObject.transform.localScale = new Vector3(5, 5, 5);
            AnimationMove();
            uIMoveController.PressWADS(2);
        }
        if (dirMove.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-5, 5, 5);
            AnimationMove();
            uIMoveController.PressWADS(1);
        }
        if (dirMove.x == 0)
        {
            AnimationIdle();
            uIMoveController.UnPressWADS(1);
            uIMoveController.UnPressWADS(2);
        }

    }

    void MoveKeyBroad()
    {
        //Inputmanager
        //dirMove.x = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.G))
        {
            dirMove.x = -1;
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            dirMove.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            dirMove.x = 1;
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            dirMove.x = 0;
        }
        transform.Translate(dirMove * Time.deltaTime * runSpeed);
    }

    void MoveByMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dirMove = vec - transform.position;

            //Get Direction
            if (vec.x - transform.position.x > 0.2f)
            {
                dirMove.x = 1;
                gameObject.transform.localScale = new Vector3(4, 4, 4);
            }

            if (vec.x - transform.position.x < -0.2f)
            {
                dirMove.x = -1;
                gameObject.transform.localScale = new Vector3(-4, 4, 4);
            }
            AnimationMove();
            Debug.Log("Move Direction.X: " + dirMove.x);

        }

        //Clear direction
        if ((vec.x - transform.position.x < 0.2f && vec.x - transform.position.x > 0) || (vec.x - transform.position.x > -0.2f && vec.x - transform.position.x < 0))
        {
            dirMove.x = 0;
            AnimationIdle();
        }

        if (vec.x - transform.position.x < 0.4f || vec.x - transform.position.x > -0.4f)
        {
            transform.Translate(new Vector2(dirMove.x, 0) * Time.deltaTime * runSpeed);
        }
    }
    private void MoveJoystick()
    {
        transform.Translate(joystick.Direction * runSpeed * Time.deltaTime * new Vector2(1, 0));
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            rigid.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
            jumpsRemaining -= 1; // remove 1 jump
            uIMoveController.PressSpace();
        }
        if (joystick.Direction.y > 0.5f && jumpsRemaining > 0)
        {
            rigid.AddForce(Vector2.up * 40, ForceMode2D.Impulse);
            jumpsRemaining -= 1; // remove 1 jump
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumpCount;
            uIMoveController.UnPressSpace();
        }
    }

    private void AnimationIdle()
    {
        anim.Play("Idle");
    }
    private void AnimationMove()
    {
        anim.Play("Run");
    }
}

