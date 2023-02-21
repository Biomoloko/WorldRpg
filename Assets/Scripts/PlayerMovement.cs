using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private Camera cam;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject destination;
    [SerializeField] private float degrees;
    [SerializeField] private AIDestinationSetter aIDestinationSetter;
    [SerializeField] private AIPath aIPath;
    [SerializeField] private Seeker seeker;
    [SerializeField] private float error;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;
    [SerializeField] private GameObject highlighter;
    [SerializeField] private MeleeWeapon meleeWeapon;
    private bool charLefDir;
    private IInteractable currentInteractable;
    public IInteractable CurrentInteractable
    {
        set
        {
            if (currentInteractable == value)
            {
                return;
            }
            currentInteractable?.BarVisualisation(false);
            currentInteractable = value;
            currentInteractable?.BarVisualisation(true);
            if (currentInteractable == null)
            {
                meleeWeapon.MeleeSwitch(MeleeType.Weapon);
            }
            else
            {
                meleeWeapon.MeleeSwitch(MeleeType.Tool);
            }
        }
        get { return currentInteractable; }
    }




    void Start()
    {
        aIPath.canMove = false;
        aIDestinationSetter.target.position = transform.position;
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        seeker.pathCallback += OnTargetRiched;
    }

    private void OnTargetRiched(Path p)
    {
        if (CompareVector3(transform.position, p.vectorPath[^1]) && CurrentInteractable == null)
        {
            destination.transform.parent = gameObject.transform;
            aIPath.canMove = false;
            destination.SetActive(false);
            anim.SetFloat("Direction", 0);
            anim.SetTrigger("Idle");
            spriteRenderer.sprite = PlayerDirection();
        }
        else if (CompareVector3(transform.position, p.vectorPath[^1]) && CurrentInteractable != null)
        {
            destination.transform.parent = gameObject.transform;
            aIPath.canMove = false;
            destination.SetActive(false);
            anim.SetFloat("Direction", 0);

            anim.SetTrigger(charLefDir == true ? "ChopRight" : "ChopLeft");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);
            mousePos.z = 0;
            if (hit.collider == null)
            {
                //CurrentInteractable?.BarVisualisation(false);
                CurrentInteractable = null;
                SetDestination(mousePos);
            }
        }
    }

    public void SetDestination(Vector3 destinationPoint, bool isLeft = false)
    {
        destination.transform.parent = null;
        aIPath.canMove = true;
        destination.SetActive(true);
        aIDestinationSetter.target.position = destinationPoint;
        degrees = Vector2.SignedAngle(destination.transform.position - transform.position, Vector2.up);

        anim.SetFloat("Direction", degrees);
        charLefDir = isLeft;
    }

    private bool CompareVector3(Vector3 playerVector, Vector3 wayVector)
    {
        if (Mathf.Abs(playerVector.x - wayVector.x) > error)
        {
            return false;
        }
        else if (Mathf.Abs(playerVector.y - wayVector.y) > error)
        {
            return false;
        }
        return true;
    }

    private Sprite PlayerDirection()
    {
        return MathF.Sign(degrees) == 1 ? rightSprite : leftSprite;
    }
    public void SwitchSelection(bool selected)
    {
        //spriteRenderer.color = selected ? Color.green : Color.white;
        highlighter.SetActive(selected);
    }

    public void ChopAnim()
    {
        CurrentInteractable?.InterractAnimation(meleeWeapon.toolProperties.damage);
        bool ifMined = CurrentInteractable?.CheckIfMined(transform) ?? false;
        if (ifMined)
        {
            anim.SetTrigger("IsMined");
            CurrentInteractable = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<IResources>(out var resources))
        {
            
            Destroy(col.gameObject);
        }
    }
}
