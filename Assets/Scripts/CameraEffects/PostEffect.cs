using UnityEngine;
using System.Collections;

public class PostEffect : MonoBehaviour
{
    Camera AttachedCamera;
    public Shader Post_Outline;
    public Shader DrawSimple;
    Camera temporaryCamera;
    Material postEffectOutlineMaterial;
    // public RenderTexture TempRT;


    void Start()
    {
        AttachedCamera = GetComponent<Camera>();
        temporaryCamera = new GameObject().AddComponent<Camera>();
        temporaryCamera.enabled = false;
        postEffectOutlineMaterial = new Material(Post_Outline);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        //set up a temporary camera
        temporaryCamera.CopyFrom(AttachedCamera);
        temporaryCamera.clearFlags = CameraClearFlags.Color;
        temporaryCamera.backgroundColor = Color.black;

        //cull any layer that isn't the outline
        temporaryCamera.cullingMask = 1 << LayerMask.NameToLayer("Outline");

        //make the temporary rendertexture
        RenderTexture renderTexture = new RenderTexture(source.width, source.height, 0, RenderTextureFormat.R8);

        //put it to video memory
        renderTexture.Create();

        //set the camera's target texture when rendering
        temporaryCamera.targetTexture = renderTexture;

        //render all objects this camera can render, but with our custom shader.
        temporaryCamera.RenderWithShader(DrawSimple, "");

        postEffectOutlineMaterial.SetTexture("_SceneTex", source);

        //copy the temporary RT to the final image
        Graphics.Blit(renderTexture, destination, postEffectOutlineMaterial);

        //release the temporary RT
        renderTexture.Release();
    }

}