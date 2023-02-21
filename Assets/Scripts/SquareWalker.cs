using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class SquareWalker : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private GameObject wayPoint;
    private List<Vector3> positions = new List<Vector3>();
    [SerializeField] private float cubeDisplace;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        FollowCursor();
    }
    
    private void FollowCursor()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
            mousePos.x = Mathf.Round(mousePos.x) + cubeDisplace;
            mousePos.y = Mathf.Round(mousePos.y) + cubeDisplace;
            mousePos.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject() && !positions.Any(pos => pos == mousePos))
            {
                Instantiate(wayPoint, mousePos, Quaternion.identity);
                positions.Add(mousePos);
            }
            
        }
        transform.position = mousePos;
    }
}
