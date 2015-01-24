using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform
{
	Vector2 start, end, lastEnd;
	GameObject tile, tileLeft, tileRight; //tile as prefab
	float tileWidth;

	int maxBlocks;

	GameObject platform;
	
	List<GameObject> generatedTiles = new List<GameObject> ();

	public Platform (GameObject platformTile, GameObject platformTileLeft, GameObject platformTileRight, int maxBlocks)
	{
		this.tile = platformTile;
		this.tileLeft = platformTileLeft;
		this.tileRight = platformTileRight;
		this.tileWidth = this.tile.GetComponent<SpriteRenderer>().renderer.bounds.size.x;
		this.platform = new GameObject ();
		this.platform.name = "Platform";
		this.maxBlocks = maxBlocks;
	}
	
	public int DynamicConstruction(Vector2 platformStart, Vector2 platformEnd)
	{
		lastEnd = end;
		this.start = platformStart;
		this.end = platformEnd;
		//if starting and ending position are too close, then increase width to accomodate left and right tiles
//		if ((this.end - this.start).magnitude < tileWidth) {
//			this.end = this.start + Vector2.right * tileWidth/2;
//		}
		
		Vector2 tilingVector = this.end-this.start;
		float distance = tilingVector.magnitude;
		int nTiles = (int)(distance / this.tileWidth)+1;
		if (distance < tileWidth) 
		{
			nTiles = 2;
			tilingVector = Vector2.right;
		}
		Debug.Log ("ntiles " + nTiles.ToString());
		Debug.Log ("tilecount " + generatedTiles.Count.ToString());
		
		Quaternion orientation = Quaternion.FromToRotation(Vector2.right, tilingVector);
		platform.transform.rotation = orientation;
		platform.transform.position = this.start;

		if (nTiles > maxBlocks) {
						return 0;
				}

		if (nTiles != generatedTiles.Count || lastEnd != end ) {
				Debug.Log ("generating tiles");
				//			InstanceTiles(nTiles);
				//			PositionTiles(this.start, this.end, orientation, tileWidth);
				foreach (GameObject go in generatedTiles) {
						GameObject.Destroy (go);
				}
				generatedTiles.Clear ();
				Vector2 position = start;

				AddBorderTile (tileLeft, position, orientation);

				for (int i = 1; i < nTiles-1; i++) {
						position = start + tilingVector.normalized * i * tileWidth;
						AddCenterTile (tile, position, orientation);
				}

				position = start + tilingVector.normalized * (nTiles - 1) * tileWidth;
				AddBorderTile (tileRight, position, orientation);

				foreach (GameObject child in generatedTiles) {
						child.transform.parent = platform.transform;
						child.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
				}
		}
		return nTiles;
	}

	public void ConfirmPlatform()
	{
		foreach (GameObject child in generatedTiles) {
			child.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
	}

	private void AddBorderTile(GameObject tilePrefab, Vector3 position, Quaternion orientation)
	{
		GameObject borderTile = (GameObject)GameObject.Instantiate (tilePrefab, new Vector3 (position.x, position.y), orientation);
		BoxCollider2D collider = borderTile.AddComponent<BoxCollider2D> ();
		collider.center = new Vector2 (collider.center.x, collider.center.y + this.tileWidth);
		generatedTiles.Add (borderTile);
	}
	private void AddCenterTile(GameObject tilePrefab, Vector3 position, Quaternion orientation)
	{
		GameObject centerTile = (GameObject)GameObject.Instantiate (tilePrefab, new Vector3 (position.x, position.y), orientation);
		BoxCollider2D collider = centerTile.AddComponent<BoxCollider2D> ();
		collider.center = new Vector2 (collider.center.x, collider.center.y);
		generatedTiles.Add (centerTile);
	}
	public void Remove()
	{
		GameObject.Destroy (platform, 5);
	}
	public List<GameObject> getTiles()
	{
		return this.generatedTiles;
	}
}









