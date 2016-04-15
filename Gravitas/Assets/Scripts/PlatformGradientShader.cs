using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Camera/PlatformGradient")]
public class PlatformGradientShader : MonoBehaviour
{
    public RenderTexture source1;
    public RenderTexture source2;

    private Texture2D texture1;
    private Texture2D texture2;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        texture1 = new Texture2D(Screen.width, Screen.height);
        texture2 = new Texture2D(Screen.width, Screen.height);

        RenderTexture.active = source1;
        texture1.ReadPixels(new Rect(0.0f, 0.0f, source1.width, source1.height), 0, 0);

        RenderTexture.active = source2;
        texture2.ReadPixels(new Rect(0.0f, 0.0f, source2.width, source2.height), 0, 0);

        Color[] source1Pixels = texture1.GetPixels();
        Color[] source2Pixels = texture2.GetPixels();

        Color[] resultPixels = new Color[source1Pixels.Length];

        for (int i = 0; i < source1Pixels.Length; i++)
        {
            resultPixels[i] = new Color(
                (source1Pixels[i].r * source2Pixels[i].r) / 1.0f,
                (source1Pixels[i].g * source2Pixels[i].g) / 1.0f,
                (source1Pixels[i].b * source2Pixels[i].b) / 1.0f,
                (source1Pixels[i].a * source2Pixels[i].a) / 1.0f
            );
        }

        Texture2D resultTexture = new Texture2D(Screen.width, Screen.height);

        resultTexture.SetPixels(resultPixels);
        resultTexture.Apply();


        //Graphics.Blit(resultTexture, destination);

        GUI.DrawTexture(new Rect(0.0f, 0.0f, 300.0f, 300.0f), resultTexture);

        //GL.Clear(true, true, Color.white, 1.0f);
        source1.Release();
        source2.Release();
        Texture2D.DestroyImmediate(texture1);
        Texture2D.DestroyImmediate(texture2);


        //color = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);
        //RenderTexture.ReleaseTemporary(color);
    }
}
