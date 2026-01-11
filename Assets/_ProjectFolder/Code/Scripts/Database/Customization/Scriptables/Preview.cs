using System.Collections.Generic;
using UnityEngine;

namespace Unity.Customization
{
    public static class Preview
    {
        //public static Sprite Build(IReadOnlyList<Sprite> images, int width = 1024, int height = 1024)
        //{
        //    Texture2D finalTex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        //    Debug.Log(images.Count);

        //    var clear = new Color(0, 0, 0, 0);
        //    var pixels = new Color[width * height];

        //    for (int i = 0; i < pixels.Length; i++) pixels[i] = clear;
        //    finalTex.SetPixels(pixels);

        //    foreach (var sprite in images) {
        //        var rect = sprite.rect;
        //        var src = sprite.texture;
        //        var srcPixels = src.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
        //        finalTex.SetPixels(0, 0, (int)rect.width, (int)rect.height, srcPixels);
        //    }

        //    finalTex.Apply();
        //    return Sprite.Create(finalTex, new(0, 0, width, height), new(0.5f, 0.5f));
        //}
    }
}