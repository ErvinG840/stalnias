﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
//[RequireComponent(typeof(PolygonCollider2D))]
public class IngameObject : MonoBehaviour {
    internal DObject objectData;

    TileMap tilemap {
        get {
            return transform.parent.transform.parent.GetComponent<TileMap>();
        }
    }

    // Use this for initialization
    void Start () {
		if(objectData.gid>0)
        {
            SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();
            sprite_renderer.sortingOrder = objectData.sortingOrder;

            Color[] textureColors = tilemap.tiles[objectData.gid];

            int size = tilemap.textureResolution;

            Texture2D texture = new Texture2D(size, size);
            texture.SetPixels(textureColors);
            texture.Apply();

            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            sprite_renderer.sprite = sprite;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
