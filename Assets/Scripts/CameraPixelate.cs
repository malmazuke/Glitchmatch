using UnityEngine;
using System.Collections;

public class CameraPixelate : MonoBehaviour {

	[Tooltip("How chunky to make the screen")]
	public int pixelSize = 8;

	void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		// Create the scaled down texture
		// Make sure to use FilterMode.Point so that
		// no anti-aliasing occurs
		RenderTexture scaleBuffer = RenderTexture.GetTemporary(source.width / pixelSize, source.height / pixelSize);
		scaleBuffer.filterMode = FilterMode.Point;

		// Destination may be null if this is the last "OnRenderImage"
		// message ie: We are drawing directly to the screen now!
		if (destination != null) {
			destination.MarkRestoreExpected ();
		}
		// Blit the camera output onto a smaller texture
		Graphics.Blit(source, scaleBuffer);
		// Blit the smaller texture back onto the output
		// if destination is nil it will go straight to the screen
		Graphics.Blit(scaleBuffer, destination);

		// Free the temp scaled texture
		RenderTexture.ReleaseTemporary(scaleBuffer);
	}
}