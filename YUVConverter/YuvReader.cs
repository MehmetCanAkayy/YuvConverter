using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;

namespace YUVConverter
{
    class YuvReader
    {
        string filename;
        int width, height, type;
        public List<byte[,]> frameY;
        public List<Bitmap> bitmapsY;
        //public List<byte[,]> frameU;
        //public List<byte[,]> frameV;
        //public List<Color[,]> frameRGB;

        public YuvReader(string f, int w, int h, int t)
        {
            type = t;
            filename = f;
            width = w;
            height = h;
            frameY = new List<byte[,]>();
            bitmapsY = new List<Bitmap>();
            //frameU = new List<byte[,]>();
            //frameV = new List<byte[,]>();
            //frameRGB = new List<Color[,]>();
        }

        private Color yuvToRGB(byte y, byte u, byte v)
        {
            byte r = (byte)(1.164 * (y - 16) + 1.596 * (v - 128));
            byte g = (byte)(1.164 * (y - 16) - 0.813 * (v - 128) - 0.391 * (u - 128));
            byte b = (byte)(1.164 * (y - 16) + 2.018 * (u - 128));

            Color rgb = Color.FromArgb(255, r, g, b);
            return rgb;
        }

        //public Bitmap mapRGB(Color[,] data)
        //{
        //    Bitmap rgb = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        //    for (int w = 0; w < width; w++)
        //        for (int h = 0; h < height; h++)
        //        {
        //            rgb.SetPixel(w, h, data[w, h]);
        //        }
        //    return rgb;
        //}

        public Bitmap mapYUV(byte[,] data)
        {
            Bitmap rgb = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int w = 0; w < width; w++)
                for (int h = 0; h < height; h++)
                {
                    byte p = data[w, h];
                    rgb.SetPixel(w, h, Color.FromArgb(255, p, p, p));
                }
            return rgb;
        }

        public void saveBMP(Bitmap bmp, string filename)
        {
            bmp.Save(filename);
            bitmapsY.Add(bmp);
            Console.WriteLine("saved to: " + filename);
        }

        public void readFrames()
        {
            FileStream fstream = File.OpenRead(filename);
            byte[] buffer = new byte[1];
            int b = 0, frameCount = 0;
            try
            {
                while ((b = fstream.ReadByte()) != -1)
                {
                    byte[,] frame = new byte[width, height];
                    for (int h = 0; h < height; h++)
                        for (int w = 0; w < width; w++)
                        {
                            if (!(h == 0 && w == 0))
                            {
                                b = fstream.ReadByte();
                                frame[w, h] = (byte)b;
                            }
                        }
                    if (type == 0)
                    {
                        int a = frameCount % 3;
                        if (a == 0)
                            frameY.Add(frame);
                        //else if (a == 1)
                        //    frameU.Add(frame);
                        //else
                        //    frameV.Add(frame); 
                    }
                    else if (type == 1)
                    {
                        int a = frameCount % 4;
                        if ((a == 0) || (a == 2))
                            frameY.Add(frame);
                        //else if (a == 1)
                        //    frameU.Add(frame);
                        //else
                        //    frameV.Add(frame);
                    }
                    else if (type == 2)
                    {
                        frameY.Add(frame);
                        for (int i = 0; i < (width * height) / 2; i++)
                        {
                            fstream.ReadByte();
                        }
                    }
                    Console.WriteLine("frame: " + frameCount);
                    frameCount++;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                fstream.Close();
            }

            //for (int i = 0; i < (frameCount + 1) / 3; i++)
            //{
            //    Color[,] currentFrame = new Color[width,height];
            //    for (int h = 0; h < height; h++)
            //    {
            //        for (int w = 0; w < width; w++)
            //        {
            //            currentFrame[w,h] = yuvToRGB(frameY[0][w,h], frameU[0][w, h], frameV[0][w, h]);
            //        }
            //    }
            //    frameRGB.Add(currentFrame);
            //    Console.WriteLine("frame: " + i + ", rgb0: " + currentFrame[0,0].ToArgb());
            //}
            //saveBMP(mapRGB(frameRGB[0]), "a.bmp");
            int realFrameCount = 0;
            if (type == 0)
                realFrameCount = (frameCount + 1) / 3;
            else if (type == 1)
                realFrameCount = (frameCount + 1) / 2;
            else
                realFrameCount = frameCount;
            Console.WriteLine("total frame count: " + realFrameCount);
            Directory.CreateDirectory(filename.Substring(0, filename.Length - 4) + "_bitmaps");
            for (int i = 0; i < realFrameCount; i++)
                saveBMP(mapYUV(frameY[i]), filename.Substring(0, filename.Length - 4) + "_bitmaps/y_" + i + ".bmp");
            //saveBMP(mapYUV(frameU[0]), "u.bmp");
            //saveBMP(mapYUV(frameV[0]), "v.bmp");
        }
    }
}