using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stone : MonoBehaviour
{
    public GameObject manCha;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    BoxCollider2D boxCollider;
    private Collision2D collision;
    private bool isMoving = false;
    private Vector3 MoveTarget, vel = Vector3.zero;
    public float smoothSpeed;
    private Vector3 manCharacterPosition;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (isMoving)
        {
            rigid.transform.position = Vector3.Lerp(rigid.position, MoveTarget, Time.deltaTime * smoothSpeed);
        }
    }
    private IEnumerator myCoroutineMove(float movingTime)
    {
        yield return new WaitForSeconds(movingTime);
        isMoving = false;
        rigid.transform.position = MoveTarget;
    }

    public void myMove(Vector3 MoveDirection)
    {
        MoveTarget = new Vector3(rigid.position.x + MoveDirection.x, rigid.position.y + MoveDirection.y, 0);
    }

    public void CheckRayCast()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            manCharacterPosition = GameManager.Instance.GetManCharacterPosition();
            if (rigid.position.x < manCharacterPosition.x)
            {
                boxCollider.enabled = false;
                RaycastHit2D rayHit1 = Physics2D.Raycast(rigid.position, new Vector3(-1f, 0), 1f);
                boxCollider.enabled = true;
                if (rayHit1.collider == null)
                {
                    isMoving = true;
                    myMove(new Vector3(-1f, 0));
                    StartCoroutine(myCoroutineMove(Time.deltaTime * smoothSpeed));
                }
            }
            if (rigid.position.x > manCharacterPosition.x)
            {
                boxCollider.enabled = false;
                RaycastHit2D rayHit3 = Physics2D.Raycast(rigid.position, new Vector3(1f, 0), 1f);
                boxCollider.enabled = true;
                if (rayHit3.collider == null)
                {
                    isMoving = true;
                    myMove(new Vector3(1f, 0));
                    StartCoroutine(myCoroutineMove(Time.deltaTime * smoothSpeed));
                }
            }
        }
        if (Input.GetButtonDown("Vertical"))
        {
            manCharacterPosition = GameManager.Instance.GetManCharacterPosition();
            if (rigid.position.y > manCharacterPosition.y)
            {
                boxCollider.enabled = false;
                RaycastHit2D rayHit2 = Physics2D.Raycast(rigid.position, new Vector3(0, 1f), 1f);
                boxCollider.enabled = true;
                if (rayHit2.collider == null)
                {
                    isMoving = true;
                    myMove(new Vector3(0, 1f));
                    StartCoroutine(myCoroutineMove(Time.deltaTime * smoothSpeed));
                }
            }

            if (rigid.position.y < manCharacterPosition.y)
            {
                boxCollider.enabled = false;
                RaycastHit2D rayHit4 = Physics2D.Raycast(rigid.position, new Vector3(0, -1f), 1f);
                boxCollider.enabled = true;
                if (rayHit4.collider == null)
                {
                    isMoving = true;
                    myMove(new Vector3(0, -1f));
                    StartCoroutine(myCoroutineMove(Time.deltaTime * smoothSpeed));
                }
            }
        }
    }
    private Vector3 moveVector(Vector2 moveArrow)
    {
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, moveArrow, 1f);
        if (rayHit.collider != null) return moveArrow;
        else return new Vector3(0, 0);
    }
}
