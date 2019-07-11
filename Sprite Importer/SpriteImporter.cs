using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Converts a sprite to a CubePixel object
/// </summary>
public class SpriteImporter : MonoBehaviour  {

	#region Values
	/// <summary>
	/// The cube pixel object
	/// </summary>
	public GameObject cubePixel;
	/// <summary>
	/// The texture to convert
	/// </summary>
	public Texture2D texture;
	/// <summary>
	/// The size of each pixel
	/// </summary>
	public float cellSize;
	/// <summary>
	/// The gap between pixels
	/// </summary>
	public float gapWidth;
	/// <summary>
	/// the dimensions for generating a square
	/// </summary>
	[HideInInspector] public Vector2Int squareDims = Vector2Int.zero;
	/// <summary>
	/// The dimensions for generating a circle
	/// </summary>
	[HideInInspector] public int circleRadius = 5;

	/// <summary>
	/// Reference to the object holder for all the cubes
	/// </summary>
	private GameObject outputObj;
	#endregion

	/// <summary>
	/// Convert a texture to a cube sprite
	/// </summary>
	public void GenerateCubeSprite() {
		outputObj = new GameObject("CubeSprite");
		outputObj.transform.position = new Vector3(texture.width / 2.0f * cellSize, 0.0f, texture.height / 2.0f * cellSize);
		for (int y = 0; y < texture.height; y++) {
			for (int x = 0; x < texture.width; x++) {
				//Debug.Log(texture.GetPixel(x, y));
				if (texture.GetPixel(x,y).a != 0) GeneratePixelCube(new Vector2Int(x, y));
			}
		}

		Debug.Log("CubeSprite Generated");
	}

	/// <summary>
	/// Generates a cube sprite circle
	/// </summary>
	/// <param name="r"></param>
	public void GenerateCircle(int r) {
		outputObj = new GameObject("CubeSprite");
		outputObj.transform.position = new Vector3(r * cellSize, 0.0f, r * cellSize);
		Vector2Int[] points = CircleDrawingCode.GenerateCircle(r, cellSize);
		for (int i = 0; i < points.Length; i++) {
			GeneratePixelCube(points[i]);
		}
	}

	/// <summary>
	/// Generates a cube sprite square
	/// </summary>
	/// <param name="sizeX"></param>
	/// <param name="sizeY"></param>
	public void GenerateSquare(int sizeX, int sizeY) {
		outputObj = new GameObject("CubeSprite");
		outputObj.transform.position = new Vector3(sizeX / 2.0f * cellSize, 0.0f, sizeY / 2.0f * cellSize);
		for (int y = 0; y < sizeY; y++) {
			for (int x = 0; x < sizeX; x++) {
				GeneratePixelCube(new Vector2Int(x, y));
			}
		}
	}

	/// <summary>
	/// Calculates the scale from cellSize and gapWidth
	/// </summary>
	/// <returns></returns>
	private Vector3 GetScale() {
		float size = cellSize - (gapWidth / 2.0f);
		return new Vector3(size, size, size);
	}

	/// <summary>
	/// The the position world postion based on cell position and cellsize
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public Vector3 GetPositionOffset(Vector2Int input) {
		return new Vector3(input.x * cellSize, 0.0f, input.y * cellSize);
	}

	/// <summary>
	/// Creates a new 
	/// </summary>
	/// <param name="position"></param>
	public void GeneratePixelCube(Vector2Int position) {
		GameObject newPix = Instantiate(cubePixel);
		newPix.transform.localScale = GetScale();
		newPix.transform.parent = outputObj.transform;
		newPix.transform.position = GetPositionOffset(position);
	}
}
