using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using PlanetsCore;

public class Script7 : MonoBehaviour
{
    public ComputeShader shader;
    public RawImage RawImage;

    Texture2D texture;

    //index of main function of shader
    private int kernel;

    private ComputeBuffer bodiesBuffer;
    private ComputeBuffer colorBuffer;
    private Info[] bodies;
    private Color[] colors;
    private const int size = 1024;

    struct Info
    {
        public Vector2 pos; // 4 * 2 = 8 bytes
        public float mass;  //4 bytes
    };

    struct ColorInfo
    {
        public Color color;// 4 * 4 = 16 bytes
    };

    void Start()
    {
    }

    private void InitShader()
    {
        //get index of enter point
        kernel = shader.FindKernel("CSMain");

        //get orbital bodies
        bodies = StateBus.Model.Bodies.OfType<OrbitalBody>()
                        .Where(b => b.Mass > Constants.EARTH_MASS / 10)
                        .Where(b => b.Parent == null || (b.Parent is OrbitalBody && (b.Parent as OrbitalBody).OrbitRadius < float.Epsilon))
                        .Select(b=> new Info
                        {
                            pos = new Vector2(b.Position.x, b.Position.z),
                            mass = (float)b.Mass
                        }).ToArray();


        //create arr
        colors = new Color[size * size];

        //create bodiesBuffer
        bodiesBuffer = new ComputeBuffer(bodies.Length, 12); //12 bytes per element of Info
        colorBuffer = new ComputeBuffer(size * size, 16);

        //link bodiesBuffer with shader
        shader.SetBuffer(kernel, "Bodies", bodiesBuffer);
        shader.SetBuffer(kernel, "ResultColors", colorBuffer);

        //create texture to show results
        texture = new Texture2D(size, size, TextureFormat.RGB24, false);
        RawImage.texture = texture;
    }

    private void OnDisable()
    {
        bodiesBuffer.Release();
        colorBuffer.Release();
    }


    // Update is called once per frame
    void Update()
    {
        if (StateBus.ModelLoaded)
            InitShader();

        if (bodiesBuffer == null)
            return;

        //pass some parametr to shader
        var v = Time.time / 5;
        shader.SetVector("start", new Vector3(v, v, 0));
        shader.SetFloat("step", 10);
        shader.SetInt("size", size);
        shader.SetFloat("MapScale", (float)(StateBus.Model.MapScale));
        shader.SetInt("BodiesCount", bodies.Length);

        bodiesBuffer.SetData(bodies);

        //run shader
        shader.Dispatch(kernel, size / 16, size / 16, 1); // 512 / 16 = 32 (where 16 - group size in shader, 512 - size of data array)

        //get bodiesBuffer back
        colorBuffer.GetData(colors);

        //draw bodiesBuffer on texture
        //texture.SetPixels(colors);
        //texture.Apply();
    }
}
