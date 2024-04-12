using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] Sprite cursorSprite;
    private Cursor cursor;

    private void Awake()
    {
        cursor = GetComponent<Cursor>();
        
    }
}
