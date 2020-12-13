using System.Diagnostics;
using System.IO;

namespace Danganronpa_Another_Tool
{
    public static class Images
    {
        public static string UseEXEToConvert(string FileEXE, string CodeLine)
        {
            //Scarlet changes the output file's name. We need the new name to check if exist and perform other operations with it.
            string NewFilesName = null;

            if (File.Exists(FileEXE))
            {
                CodeLine = CodeLine.Replace("\\", "/");

                ProcessStartInfo startInfo = null;

                if (Path.GetExtension(FileEXE) == ".py")
                {
                    startInfo = new ProcessStartInfo
                    {
                        FileName = FileEXE,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        Arguments = CodeLine
                    };
                }
                else
                {
                    startInfo = new ProcessStartInfo
                    {
                        FileName = FileEXE,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        Arguments = CodeLine
                    };
                }


                using (Process exeProcess = Process.Start(startInfo))
                {
                    if (FileEXE == "Ext\\ScarletTestApp.exe")
                        NewFilesName = exeProcess.StandardOutput.ReadLine();

                    exeProcess.WaitForExit();
                }
            }

            return NewFilesName;
        }

        public static string ConvertFromPNGToTGA(string Image, string DestinationDir)
        {
            string NewTGA = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image)) + ".tga";

            string CodeLine = "convert \"" + Image + "\" -compress RLE \"" + NewTGA + "\"";

            if (DRAT.GreyScaleValue == true) // [ -colorspace sRGB ] psp2gxt.exe can't hanlde grayscale, so we need to disable it. PSVITA ONLY
                CodeLine = "convert \"" + Image + "\" -define colorspace:auto-grayscale=off -type truecolor -compress RLE \"" + Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image)) + ".tga\"";

            UseEXEToConvert("Ext\\convert.exe", CodeLine);

            return NewTGA;
        }

        public static string ConvertFromPNGToDDS(string Image, string DestinationDir)
        {
            string NewDDS = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ".dds"),
                FinalDDS = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image)),
                CodeLine = "convert -format dds -define dds:compression=dxt5 \"" + Image + "\" \"" + NewDDS + "\"";

            UseEXEToConvert("Ext\\convert.exe", CodeLine);

            //For some reason AE's DDSs start with "DDS1", so we need to reinsert that.
            using (FileStream OriginalDDS = new FileStream(NewDDS, FileMode.Open, FileAccess.Read))
            using (FileStream FDDS = new FileStream(FinalDDS, FileMode.Create, FileAccess.Write))
            using (BinaryWriter DDSWriter = new BinaryWriter(FDDS))
            {
                DDSWriter.Write((uint)0x31534444);
                OriginalDDS.CopyTo(FDDS, 0x4);
            }

            // We don't need anymore the original DDS.
            if (File.Exists(NewDDS))
            {
                File.Delete(NewDDS);
                while (File.Exists(NewDDS)) { }
            }

            if (File.Exists(FinalDDS) && (Path.GetFileName(FinalDDS).ToLower().Contains(".gx3dec") || Path.GetFileName(FinalDDS).ToLower().Contains(".dec")))
            {
                // Compress the btx with the code from Kuriimu
                FinalDDS = Common.CompressFileSpikeCompression(FinalDDS, DestinationDir, true);
            }

            return FinalDDS;
        }

        public static string ConvertFromPNGToBTX(string Image, string DestinationDir, bool Delete)
        {
            // -s = silent
            string CodeLine = "-o \"" + DestinationDir + "\" -s";

            // If the user want a specific type.
            if (DRAT.toolStripComboBoxBTXType.SelectedIndex != 0)
                CodeLine = CodeLine + " --" + DRAT.toolStripComboBoxBTXType.SelectedItem;

            // Tell to the py that it need to use the PC's palette.
            if (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE (PC)"))
                CodeLine += " -PC";

            if (Delete == true) //Delete the original png.
                CodeLine += " -d";

            CodeLine += " \"" + Image + "\"";

            UseEXEToConvert("Ext\\to_BTX.py", CodeLine);

            string NewFile = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image));

            /* Images extracted with DRAT already have the ".btx" as extension: "image.SHTX.btx.png"
              while other images doesn't: image.png */
            if (!File.Exists(NewFile))
                NewFile = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ".btx");

            if (Path.GetFileName(Image).ToLower().Contains(".gx3dec") || Path.GetFileName(Image).ToLower().Contains(".dec"))
            {
                // Compress the btx with the code from Kuriimu
                NewFile = Common.CompressFileSpikeCompression(NewFile, DestinationDir, true);
            }

            return NewFile;
        }

        public static void ConvertFromTGAToPNG(string Image, string DestinationDir)
        {
            string CodeLine = "convert \"" + Image + "\" -alpha Background -quality 100 \"" + Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image)) + ".png\"";
            UseEXEToConvert("Ext\\convert.exe", CodeLine);
        }

        public static string ConvertFromTGAToGXT(string Image, string DestinationDir)
        {
            string NewGXT = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image)) + ".gxt";

            //AE PSVITA have some BTX files that in reality are just GXT files, so if that's the case, let's rename the new file and make things easier to the user
            if (Path.GetFileName(NewGXT).Contains(".gxt.btx"))
                NewGXT = NewGXT.Replace(".btx.gxt", ".btx");

            string CodeLine = "-i \"" + Image + "\" -o \"" + NewGXT + "\"";
            UseEXEToConvert("Ext\\psp2gxt.exe", CodeLine);

            return NewGXT;
        }

        public static string ConvertGxtBtxToPNG(string Image, string DestinationDir)
        {
            string CodeLine = "\"" + Image + "\" --output \"" + DestinationDir + "\"";

            if (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE (PC)"))
                CodeLine += " --PCAE";

            return UseEXEToConvert("Ext\\ScarletTestApp.exe", CodeLine);
        }

        public static void ConvertFromGIMToPNG(string Image, string DestinationDir)
        {
            string CodeLine = "\"" + Image + "\"";
            UseEXEToConvert("Ext\\GIM2PNG.exe", CodeLine);

            //Since GIM2PNG.exe creates the image.png in the same folder as the original, we have to manually move the .png to the destination folder.
            File.Move(Path.Combine(Path.GetDirectoryName(Image), Path.GetFileNameWithoutExtension(Image) + ".png"), Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ".png"));
        }

        public static string ConvertToPNG(string Image, string DestinationDir, bool DeleteOriginal)
        {
            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            string imgExtension = Path.GetExtension(Image).ToLower(),
                NewPNG = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ".png");

            if (imgExtension == ".tga")
                ConvertFromTGAToPNG(Image, DestinationDir);
            else if (imgExtension == ".gxt" || imgExtension == ".btx" || imgExtension == ".SHTX" || imgExtension == ".SHTXFS")
                NewPNG = ConvertGxtBtxToPNG(Image, DestinationDir);
            else if (imgExtension == ".gim") //gim are used only in the PSP games.
                ConvertFromGIMToPNG(Image, DestinationDir);

            // Delete the original image.
            if (DeleteOriginal == true && File.Exists(NewPNG))
            {
                File.Delete(Image);
                while (File.Exists(Image)) { }
            }

            return NewPNG;
        }

        public static string ConvertToTGA(string Image, string DestinationDir, bool DeleteOriginal)
        {
            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            string imgExtension = Path.GetExtension(Image).ToLower(),
                NewTGA = null,
            ImageToConvert = Image;

            // First we have to convert btx and gxt files to png
            if (imgExtension != ".png")
                ImageToConvert = ConvertToPNG(Image, DestinationDir, false); // false = don't delete the original image

            // Convert the image.png to .tga.
            NewTGA = ConvertFromPNGToTGA(ImageToConvert, DestinationDir);

            // Delete the image.png generated from btx ang gxt files.
            if (ImageToConvert != Image)
            {
                File.Delete(ImageToConvert);
                while (File.Exists(ImageToConvert)) { }
            }

            // Delete the original image.
            if (DeleteOriginal == true && File.Exists(NewTGA))
            {
                File.Delete(Image);
                while (File.Exists(Image)) { }
            }

            return NewTGA;
        }

        public static void ConvertToGXT(string Image, string DestinationDir, bool DeleteOriginal)
        {
            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            string imgExtension = Path.GetExtension(Image).ToLower(),
                NewGXT = null,
                ImageToConvert = Image;

            // We have to convert btx images to png, then to tga and finally to gxt.
            if (imgExtension != ".tga")
                ImageToConvert = ConvertToTGA(ImageToConvert, DestinationDir, false); // false = don't delete the original image

            // Convert tga images to gxt saving them directly in the TEMP folder.
            NewGXT = ConvertFromTGAToGXT(ImageToConvert, DestinationDir);

            // If the gxt doesn't exit, that means that the tga's palette is wrong, so we need to rebuild the tga.
            if (!File.Exists(NewGXT))
            {
                DRAT.GreyScaleValue = true;

                // Delete the tga generated from btx and png files.
                if (File.Exists(ImageToConvert) && ImageToConvert != Image)
                {
                    File.Delete(ImageToConvert);
                    while (File.Exists(ImageToConvert)) { }
                }

                if (imgExtension != ".tga")
                    ImageToConvert = ConvertToTGA(Image, DestinationDir, false); // false = don't delete the original image
                else if (imgExtension == ".tga")
                {
                    ImageToConvert = ConvertToPNG(Image, DestinationDir, false); // false = don't delete the original image
                    ImageToConvert = ConvertToTGA(ImageToConvert, DestinationDir, true); // true = delete the original image
                }

                // Convert tga images to gxt saving them directly in the TEMP folder.
                NewGXT = ConvertFromTGAToGXT(ImageToConvert, DestinationDir);

                DRAT.GreyScaleValue = false;
            }

            // Delete the tga generated from btx and png files.
            if (ImageToConvert != Image)
            {
                File.Delete(ImageToConvert);
                while (File.Exists(ImageToConvert)) { }
            }

            // Compress the gxt with the code from Kuriimu
            NewGXT = Common.CompressFileSpikeCompression(NewGXT, DestinationDir, true);

            // Delete the original image.
            if (DeleteOriginal == true && File.Exists(NewGXT))
            {
                File.Delete(Image);
                while (File.Exists(Image)) { }
            }

            //Clean the file's extension. "Image.dec.SHTXFS.btx" --> "Image.btx"
            if (File.Exists(NewGXT) && DRAT.removeAdditionalDataFromExtensionToolStripMenuItem.Checked == true)
                Common.CleanExtension(NewGXT);
        }

        public static void ConvertToBTX(string Image, string DestinationDir, bool DeleteOriginal)
        {
            string FinalImage = null;

            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            if (Path.GetFileName(Image).Contains(".gxt.btx"))
                ConvertToGXT(Image, DestinationDir, DeleteOriginal);
            else if (Path.GetFileName(Image).Contains(".dds.btx"))
                FinalImage = ConvertFromPNGToDDS(Image, DestinationDir);
            else
            {
                string imgExtension = Path.GetExtension(Image).ToLower(), ImageToConvert = Image;

                // First we have to convert the image to png
                if (imgExtension != ".png")
                    ImageToConvert = ConvertToPNG(Image, DestinationDir, false); // false = don't delete the original image     

                // Convert the png to btx.
                FinalImage = ConvertFromPNGToBTX(ImageToConvert, DestinationDir, DeleteOriginal);

                // Delete the tga generated from btx and png files.
                if (ImageToConvert != Image)
                {
                    File.Delete(ImageToConvert);
                    while (File.Exists(ImageToConvert)) { }
                }
            }

            //Clean the file's extension. "Image.dec.SHTXFS.btx" --> "Image.btx"
            if(FinalImage != null && File.Exists(FinalImage) && DRAT.removeAdditionalDataFromExtensionToolStripMenuItem.Checked == true)
                    Common.CleanExtension(FinalImage);
        }
    }
}
