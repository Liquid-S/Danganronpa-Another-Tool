using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Danganronpa_Another_Tool
{
    //Unused - abandoned
    public static class SHTXF
    {
        public static void PNGToSHTX(string Image, string DestinationDir, bool DeleteOriginal)
        {
            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            byte[] OrignalIMG = null;

            using (FileStream OriginalImage = new FileStream(Image, FileMode.Open, FileAccess.Read))
            {
                OrignalIMG = new byte[OriginalImage.Length];
                OriginalImage.Read(OrignalIMG, 0, OrignalIMG.Length);
            }

            Bitmap imgPNG = new Bitmap(Image);

            Bitmap imgtarget = new Bitmap(Image).Clone(new Rectangle(0, 0, imgPNG.Width, imgPNG.Height), PixelFormat.Format8bppIndexed);

            int pngWidth = imgPNG.Width, pngHeight = imgPNG.Height;

            ColorPalette Pal = imgtarget.Palette;

            using (FileStream newSHTX = new FileStream(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ".btx"), FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter NewBinaryWriter = new BinaryWriter(newSHTX))
                {
                    // START header creation
                    NewBinaryWriter.Write((uint)0x58544853); // MagicID 
                    NewBinaryWriter.Write((UInt16)pngWidth); // MaVersion 
                    NewBinaryWriter.Write((UInt16)pngHeight); // MiVersion 
                                                              // NewBinaryWriter.Write((uint)Math.Ceiling(Math.Log(pngWidth, 2)));
                                                              //  NewBinaryWriter.Write((uint)Math.Ceiling(Math.Log(pngHeight, 2)));

                    NewBinaryWriter.Write((uint)0x00100004); // MiVersion
                    NewBinaryWriter.Write((uint)0x0); // MiVersion


                    int c = 0;

                    while (c < 250)
                    {
                        if (c < Pal.Entries.Length)
                        {
                            NewBinaryWriter.Write(Pal.Entries[c].B);
                            NewBinaryWriter.Write(Pal.Entries[c + 1].G);
                            NewBinaryWriter.Write(Pal.Entries[c + 2].R);
                            NewBinaryWriter.Write(Pal.Entries[c + 3].A);
                        }
                        else
                            NewBinaryWriter.Write((uint)0x0);

                        c = c + 4;
                    }
                
                    NewBinaryWriter.Write(imgPNG.ToString());
                }

                // END header creation 
            }
        }
    }
}
