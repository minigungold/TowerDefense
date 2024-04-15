using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedItem : MonoBehaviour
{
    public static SelectedItem Instance;
    public ScriptableTurret selectItem;
    private SpriteRenderer selectedItemRenderer;
    GameTile tile;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        selectedItemRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (selectItem != null)
        {
            selectedItemRenderer.sprite = selectItem.turretSprite;
        }

    }

}
