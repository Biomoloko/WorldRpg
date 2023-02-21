using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeleeType
{
    Weapon,
    Tool
}
public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private MeleeWeaponObject meleeProperties;
    [SerializeField] private SpriteRenderer weaponVisual;
    public MeleeWeaponObject toolProperties;
    private MeleeWeaponObject curentMeleeInHand;

    //public MeleeWeaponObject MeleeProperties
    //{
    //    get { return meleeProperties; }
    //    set
    //    {
    //        meleeProperties = value;
    //        weaponVisual.sprite = meleeProperties.weaponSprite;
    //    }
    //}

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnValidate()
    //{
    //    MeleeProperties = MeleeProperties;
    //}

    /// <summary>
    /// определяем то что попадет в руку
    /// </summary>
    /// <param name="meeleType"></param>
    public void MeleeSwitch(MeleeType meeleType)
    {
        curentMeleeInHand = meeleType == MeleeType.Weapon ? meleeProperties : toolProperties;
        weaponVisual.sprite = curentMeleeInHand.weaponSprite;
    }
}
