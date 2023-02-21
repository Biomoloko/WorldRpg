using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private LayerMask unitMask;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private Camera cam;
    private Tree tree;
    void Start()
    {
        cam = Camera.main;
        tree = FindObjectOfType<Tree>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            ToRayFiendlyUnit(mouseRay);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
            ToRayInteractable(mouseRay);
        }
    }

    public void ToRayFiendlyUnit(Ray mouseRay)
    {
        RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, 100, unitMask);
        if (hit.collider != null)
        {
            var unit = hit.collider.GetComponent<PlayerMovement>();
            SelectChar(unit);
        }
    }

    public void SelectChar(PlayerMovement character)
    {
        if (playerMovement != null)
        {
            playerMovement.SwitchSelection(false);
            playerMovement.enabled = false;
        }
        character.enabled = true;
        character.SwitchSelection(true);
        playerMovement = character;
    }

    private void ToRayInteractable(Ray mouseRay)
    {
        if (playerMovement == null) 
            return;
        RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction, 100, interactableMask);
        if (hit.collider != null)
        {
            var myTree = hit.transform.GetComponent<Tree>();

            var destination = DistanceForPoint(myTree.LeftP, myTree.RightP, out bool isLeft);
            playerMovement.SetDestination(destination, isLeft);

            playerMovement.CurrentInteractable = myTree;
        }
    }

    private Vector3 DistanceForPoint(Transform leftP, Transform rightP, out bool isLeft)
    {
        if (Vector3.Distance(leftP.position, playerMovement.transform.position) > Vector3.Distance(rightP.position, playerMovement.transform.position))
        {
            isLeft = false;
            return rightP.position;
        }
        else
        {
            isLeft = true;
            return leftP.position;
        }
    }
}
