using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform leftP;
    [SerializeField] private Transform rightP;
    [SerializeField] private GameObject chopBarCanvas;
    [SerializeField] private Image chopBarImage;
    private Animator anim;
    private float presentHealth;
    [SerializeField] private float TreeHealth;
    [SerializeField] private WoodenLog log;
    [SerializeField] private int logAmount;
    [SerializeField] private ParticleSystem poof;

    public Transform LeftP => leftP;
    public Transform RightP => rightP;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        presentHealth = TreeHealth;
    }

    void Update()
    {
    }

    public void InterractAnimation(float getedDamage = 0)
    {
        anim.SetTrigger("Shake");
        presentHealth -= getedDamage;
        chopBarImage.fillAmount = presentHealth / TreeHealth;
    }

    public void BarVisualisation(bool state)
    {
        chopBarCanvas.SetActive(state);
    }

    public bool CheckIfMined(Transform charTransform)
    {
        if (presentHealth <= 0)
        {
            anim.SetTrigger("NoHealth");
            ResourceCreator(charTransform);
            StartCoroutine(PaticleAndDestroy());
            return true; 
        }
        return false;
    }

    public void ResourceCreator(Transform charTransform)
    {
        for(int i = 0; i < logAmount; i++)
        {
            var currentLog = Instantiate(log, gameObject.transform.position, Quaternion.identity);
            currentLog.treePos = transform.position;
            currentLog.ToThrowResChunk();
            currentLog.StartCoroutine(currentLog.LogGoingToChar(charTransform));
        }
    }

    public IEnumerator PaticleAndDestroy()
    {
        yield return new WaitForSeconds(2f);
        var playPoof = Instantiate(poof, transform.position, Quaternion.identity);
        playPoof.Play();
        gameObject.SetActive(false);
        Destroy(playPoof, 0.5f);
        Destroy(gameObject, 1f);
    }
}
