using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Drawing.Imaging;
using UnityEngine.UI;

public class GitTestScript : MonoBehaviour
{

    string path;
    List<Texture2D> t2d;
    UnityEngine.UI.Image uImage;
    int i;
    float timer;

    private void Start()
    {
        uImage = this.gameObject.GetComponent<UnityEngine.UI.Image>();

        path = Application.dataPath + "/Resources/gifTest.gif";
        System.Drawing.Image image = System.Drawing.Image.FromFile(path);
        t2d = new List<Texture2D>();
        FrameDimension fd = new FrameDimension(image.FrameDimensionsList[0]);
        int count = image.GetFrameCount(fd);
        for (int i = 0; i < count; i++)
        {
            image.SelectActiveFrame(fd, i);
            Bitmap tmpBm = new Bitmap(image.Width, image.Height);
            System.Drawing.Graphics.FromImage(tmpBm).DrawImage(image, Point.Empty);
            Texture2D tmpT2d = new Texture2D(tmpBm.Width, tmpBm.Height);
            for (int j = 0; j < tmpBm.Width; j++)
            {
                for (int k = 0; k < tmpBm.Height; k++)
                {
                    System.Drawing.Color color = tmpBm.GetPixel(j, k);
                    tmpT2d.SetPixel(j, tmpBm.Height - 1 - k, new Color32(color.R, color.G, color.B, color.A));
                }
            }
            tmpT2d.Apply();
            t2d.Add(tmpT2d);
        }

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            uImage.sprite = Sprite.Create(t2d[20], new Rect(0, 0, 400, 400), new Vector2(0, 0));
            i++;
            if (i == t2d.Count) i = 0;
            timer = 0;
        }


    }
}


