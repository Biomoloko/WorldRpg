using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenLog : MonoBehaviour, IResources
{
    [SerializeField] private Rigidbody2D logRb;
    public Vector2 treePos;
    public Vector2 playerPos;
    public bool onGround = false;
    [SerializeField] private float resSpeed;
    void Start()
    {
    
    }
    void Update()
    {
        if (transform.position.y <= (treePos.y - 1) && onGround == false)
        {
            logRb.gravityScale = 0;
            logRb.velocity = new Vector2(0, 0);
            onGround = true;
        }
    }

    public IEnumerator LogGoingToChar(Transform playerPos)
    {
        yield return new WaitForSeconds(2f);

        while (transform.position.x != playerPos.position.x || transform.position.y != playerPos.position.y)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, resSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void ToThrowResChunk()
    {
        logRb.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), 1) * Random.Range(3, 5), ForceMode2D.Impulse);
    }

}

