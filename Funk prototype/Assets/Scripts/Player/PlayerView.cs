using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private List<SpriteRenderer> characterSprites = new List<SpriteRenderer>();
    public int commonSortingOrder { get; set; }
    private void Awake()
    {
        foreach(var sprite in GetComponentsInChildren<SpriteRenderer>())
        {
            characterSprites.Add(sprite);
            if(sprite.gameObject.TryGetComponent(out IWeapon weapon))
            {
                weapon.gameObject.SetActive(false);
            }
        }
        commonSortingOrder = characterSprites[1].sortingOrder;

    }
}
