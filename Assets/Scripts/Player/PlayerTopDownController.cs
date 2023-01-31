using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTopDownController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigid;
    private Vector3 dirMove;
    [SerializeField] private float runSpeed = 5;
    private Vector3 vec = Vector3.zero;
    [SerializeField] private FixedJoystick joystick;

    [SerializeField] private Animator anim;
    [SerializeField] private UIMoveController uIMoveController;

    // Update is called once per frame
    void Update()
    {
        //Move();
        //MoveJoystick();
        MoveMouse();

    }
    private void Move()
    {
        dirMove.x = Input.GetAxisRaw("Horizontal");
        dirMove.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.G))
        {
            dirMove.x = -1;
        }
        if(Input.GetKeyDown(KeyCode.H)){
            dirMove.x = 1;
        }


        rigid.velocity = dirMove * runSpeed;

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
            uIMoveController.UnPressWADS(1);
            uIMoveController.UnPressWADS(2);
        }

        if (dirMove.y > 0)
        {
            uIMoveController.PressWADS(0);
            AnimationMove();
        }
        if (dirMove.y < 0)
        {
            uIMoveController.PressWADS(3);
            AnimationMove();
        }
        if (dirMove.y == 0)
        {
            uIMoveController.UnPressWADS(0);
            uIMoveController.UnPressWADS(3);
        }
        if (dirMove == Vector3.zero)
        {
            AnimationIdle();
        }

    }
    private void MoveJoystick()
    {
        rigid.velocity = (joystick.Direction * runSpeed);
        if (joystick.Direction.x > 0)
        {
            AnimationMove();
            gameObject.transform.localScale = new Vector3(5, 5, 5);
        }
        if (joystick.Direction.x < 0)
        {
            AnimationMove();
            gameObject.transform.localScale = new Vector3(-5, 5, 5);
        }
        if (joystick.Direction == Vector2.zero)
        {
            AnimationIdle();
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

    void MoveMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dirMove = vec - transform.position;
            Debug.Log(vec);
            if (vec.x > 0)
            {
                AnimationMove();
                gameObject.transform.localScale = new Vector3(5, 5, 5);
            }
            else
            {
                AnimationMove();
                gameObject.transform.localScale = new Vector3(-5, 5, 5);
            }
        }
        if (Vector2.Distance(vec, transform.position) <= 0.2f)
        {
            rigid.velocity = Vector3.zero;
            AnimationIdle();
        }
        else
        {
            rigid.velocity = dirMove;
        }
    }
}
