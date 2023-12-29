using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manChaMove : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator manAnimator;
    private SpriteRenderer spriteRenderer;
    private Vector3 MoveDirection, MoveTarget;
    private Collision2D collision;
    private bool isMoving = false, CanAttackBool, CanMoveBool;
    public bool MapChange = false;
    private RaycastHit2D rayHit;
    public float smoothSpeed;
    internal string currentMapName;

    void Awake()
    {
        manAnimator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if ((Input.GetButtonDown("Horizontal") && Mathf.Abs(Input.GetAxisRaw("Vertical")) < 0.1f) ||
        (Input.GetButtonDown("Vertical") && Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.1f) ||
        isMoving)
        {
            GameManager.Instance.SetManCharacterPosition(rigid.position);
            if (!isMoving)
            {
                MoveDirection.x = Input.GetAxisRaw("Horizontal");
                MoveDirection.y = Input.GetAxisRaw("Vertical");
                
                MoveTarget = new Vector3(rigid.transform.position.x + MoveDirection.x, rigid.transform.position.y + MoveDirection.y, 0);
            }
            CanAttack();
            if (CanAttackBool && CanMoveBool)
            {
                myMove(MoveTarget);
            }
        }
        CoRoutineCanKeyEnterTrue(Time.deltaTime * smoothSpeed);
    }

    public Vector3 GetPosition()
    {
        return rigid.position;
    }
    private void myMove(Vector3 moveTarget)
    {
        if (!isMoving)
        {
            StartCoroutine(MoveWithCooldown(moveTarget));
        }
    }

    private IEnumerator MoveWithCooldown(Vector3 moveTarget)
    {
        isMoving = true;
            manAnimator.SetTrigger("manDash");

        if (rigid.velocity.x > 0.2f)
        {
            spriteRenderer.flipX = false;
        }
        else if (rigid.velocity.x < -0.2f)
        {
            spriteRenderer.flipX = true;
        }

        float elapsedTime = 0f;
        float duration = 0.2f;

        while (elapsedTime < duration)
        {
            rigid.transform.position = Vector3.Lerp(rigid.transform.position, moveTarget, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            if (MapChange)
            {
                StartCoroutine(MapChangeBoolChange());
                break;
            }
            yield return null;
        }

        isMoving = false;
        rigid.transform.position = moveTarget;
    }
    private IEnumerator MapChangeBoolChange()
    {
        yield return new WaitForSeconds(0.2f);
        MapChange = false;
    }
    private IEnumerator CoRoutineCanKeyEnterTrue(float movingTime)
    {
        yield return new WaitForSeconds(movingTime);
    }
    private void CanAttack()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.right * Input.GetAxisRaw("Horizontal"), 1f, LayerMask.GetMask("Stone"));
            RaycastHit2D rayHitWall = Physics2D.Raycast(rigid.position, Vector3.right * Input.GetAxisRaw("Horizontal"), 1f, LayerMask.GetMask("Wall"));
            if(rayHitWall.collider != null)
            {
                CanMoveBool = false;
            }
            else
            {
                CanMoveBool = true;
            }
            myAttack(rayHit);
        }
        if (Input.GetButtonDown("Vertical"))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.up * Input.GetAxisRaw("Vertical"), 1f, LayerMask.GetMask("Stone"));
            RaycastHit2D rayHitWall = Physics2D.Raycast(rigid.position, Vector3.up * Input.GetAxisRaw("Vertical"), 1f, LayerMask.GetMask("Wall"));
            if (rayHitWall.collider != null)
            {
                CanMoveBool = false;
            }
            else
            {
                CanMoveBool = true;
            }
            myAttack(rayHit);
        }
    }
    private void myAttack(RaycastHit2D rayHit)
    {
        if (rayHit.collider != null)
        {
            manAnimator.SetTrigger("manAttack");
            CanAttackBool = false;
            Stone stone = rayHit.collider.GetComponent<Stone>();
            stone.CheckRayCast();
        }
        else CanAttackBool = true;
    }
}
