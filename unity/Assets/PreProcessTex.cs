using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreProcessTex {

    private Texture2D texture;
    private List<Vector4> res = new List<Vector4>();
    private List<float> resAreas = new List<float>();

    public PreProcessTex (Texture2D texture)
    {
        this.texture = texture;
    }

    public List<Vector4> GetVectors()
    {
        return res;
    }

    public List<float> GetAreas()
    {
        return resAreas;
    }

    public void PreProcessTexture()
    {
        // Preprocess texture
        bool[,] done = new bool[texture.width, texture.height];

        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.width; y++)
            {
                // Find neightbors
                if (!done[x,y])
                {
                    List<Vector4> n = GetNeightbors(x, y, done);
                    if (n.Count > 0)
                    {
                        res.AddRange(n);
                        resAreas.Add(n.Count);
                    }
                } 
            }
        }
        Debug.Log(res.Count);

    }

    private List<Vector4> GetNeightbors(int x, int y, bool[,] done)
    {
        List<Vector4> res = new List<Vector4>();
        List<Vector4> todo = new List<Vector4>();
        todo.Add(new Vector2(x, y));

        while (todo.Count > 0)
        {
            int currentX = (int)todo[0].x;
            int currentY = (int)todo[0].y;
            todo.RemoveAt(0);
            if (currentX >= 0 && currentX < texture.width && currentY >= 0 && currentY < texture.height && !done[currentX, currentY])
            {
                if (texture.GetPixel(currentX, currentY) != Color.white)
                {
                    res.Add(new Vector2(currentX, currentY));
                    todo.Add(new Vector2(currentX + 1, currentY));
                    todo.Add(new Vector2(currentX, currentY + 1));
                    todo.Add(new Vector2(currentX - 1, currentY));
                    todo.Add(new Vector2(currentX, currentY - 1));
                }
                done[currentX, currentY] = true;
            }
        }
        return res;
    }

}
