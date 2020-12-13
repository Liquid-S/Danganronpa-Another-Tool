using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static Danganronpa_Another_Tool.Images;

namespace Danganronpa_Another_Tool
{
    public static class Common
    {
        // Clone the directories, this way the tool can delete, change and repack everything without worrying about damage the user work.
        public static void CloneDirectory(string OriginalDir, string TEMPFolder, string PAKType, bool ConvertOrNotToConvert)
        {
            DirectoryInfo source = new DirectoryInfo(OriginalDir),
                target = new DirectoryInfo(TEMPFolder);

            // Delte the TEMPDir if it already exist.
            if (Directory.Exists(target.FullName) == true)
            {
                Directory.Delete(target.FullName, true);
                while (Directory.Exists(target.FullName)) { }
            }

            // Create the TEMPDir and make it invisible.
            DirectoryInfo NewTEMPDir = Directory.CreateDirectory(target.FullName);
            NewTEMPDir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

            string[] extensions = null;

            // The button pressed by the user decide what extensions are going to be accepted.
            if (PAKType == "IMAGES")
            {
                extensions = new[] { ".llfs", ".gmo", ".gxt", ".btx", ".SHTXFS", ".SHTXFs", ".SHTXFF", ".SHTXFf", ".SHTX", ".font", ".gim", ".unknown", ".tga", ".pak", ".png", ".cmp", ".gx3", ".bmp" };
            }
            else if (PAKType == "TEXT")
            {
                extensions = new[] { ".lin", ".pak", ".po", ".bytecode", ".unknown" };
            }

            // Copy the files to the TEMP folder. Images are converted only if the user has requested.
            foreach (FileInfo fi in source.EnumerateFiles().Where(f => extensions.Contains(f.Extension.ToLower())).ToArray())
            {
                // Converts the images if that's what the user want and is not working with the PSP version.
                if (ConvertOrNotToConvert == true && !DRAT.tabControl1.SelectedTab.Text.Contains("PSP") && (fi.Extension == ".png" || fi.Extension == ".tga"))
                {
                    // Converts ".png" images to ".tga" saving them directly in the TEMP folder.
                    if (DRAT.tabControl1.SelectedTab.Text.Contains("PC") && !DRAT.tabControl1.SelectedTab.Text.Contains("DRAE") && fi.Extension != ".tga")
                    {
                        Images.ConvertToTGA(fi.FullName, target.ToString(), false); // false = don't delete the original image
                    }
                    else if (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE"))
                    {
                        ConvertToBTX(fi.FullName, target.ToString(), false); // false = don't delete the original image
                    }
                    else if (DRAT.tabControl1.SelectedTab.Text.Contains("PSVITA") && !DRAT.tabControl1.SelectedTab.Text.Contains("DRAE"))
                    {
                        ConvertToGXT(fi.FullName, target.ToString(), false); // false = don't delete the original image
                    }
                }
                else // Otherwise if there is nothing to convert, copy the file to the "TEMP" folder without changes.
                {
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                }
            }

            // Copy the subfolders and their contents.
            foreach (string SubDir in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
            {
                CloneDirectory(SubDir, Path.Combine(TEMPFolder, Path.GetFileName(SubDir)), PAKType, ConvertOrNotToConvert);
            }
        }

        // Reads all the folders in the root (no files or subdirectories) and sorts them alphanumerically.
        public static string[] ReadOnlyRootDirs(string folder)
        {
            string[] temp = Directory.GetDirectories(folder, "*", SearchOption.TopDirectoryOnly);
            Array.Sort(temp, new DRAT.AlphanumComparatorFast());
            return temp;
        }

        // Clean all that is before the folder's name, turns "\\" to "/" and order the string[] alphanumerically.
        public static string[] CleanAddress(string[] sentence, string folder)
        {
            Array.Sort(sentence, new DRAT.AlphanumComparatorFast());

            for (int i = 0; i < sentence.Length; i++)
            {
                sentence[i] = sentence[i].Replace(folder + "\\", null).Replace("\\", "/");
            }

            return sentence;
        }

        // Let the user choose what folder to repack.
        public static string ChooseFolder(string OriginalDir)
        {
            FolderBrowserDialog FolderChosenByTheUser = new FolderBrowserDialog();

            if (FolderChosenByTheUser.ShowDialog() == DialogResult.OK)
            {
                OriginalDir = FolderChosenByTheUser.SelectedPath; // Write the Path inside the textbox.
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Repack the default folder \"" + OriginalDir + "\"?", "Folder to repack", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.No)
                {
                    OriginalDir = string.Empty;
                }
            }

            return OriginalDir;

        }

        // Check if the Folder exist and if it contains other folders. If it doesn't exist ask the user if they want to create.
        public static bool CheckDirExistence(string DestinationDir)
        {
            bool Existence = false;

            if (Directory.Exists(DestinationDir) && Directory.EnumerateDirectories(DestinationDir).Any() == true)
            {
                Existence = true;
            }
            else if (!Directory.Exists(DestinationDir))
            {
                DialogResult dialogResult = MessageBox.Show("The directory \"" + DestinationDir + "\" does not exist, do you want to create it?", "Directory not found!", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (dialogResult == DialogResult.Yes)
                {
                    Directory.CreateDirectory(DestinationDir);
                }
                else
                {
                    DestinationDir = string.Empty;
                }
            }
            else if (!Directory.EnumerateDirectories(DestinationDir).Any())
            {
                MessageBox.Show("The directory \"" + DestinationDir + "\" it's empty.", "The directory is empty!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return Existence;

        }

        // Compress the gxt with the code from Kuriimu
        public static string CompressFileSpikeCompression(string originalFile, string DestinationDir, bool ReplaceOriginal)
        {
            if (Directory.Exists(DestinationDir) == false)
            {
                Directory.CreateDirectory(DestinationDir);
            }

            string newFile = Path.Combine(DestinationDir, Path.GetFileName(originalFile).Replace(".gx3dec", ".gx3").Replace(".dec", ".cmp"));

            if (originalFile == newFile)
            {
                ReplaceOriginal = true;
            }

            if (ReplaceOriginal == true)
            {
                newFile += "CMP";
            }

            // Let's check if the file already exist and delete it.
            if (File.Exists(newFile))
            {
                File.Delete(newFile);
                while (File.Exists(newFile)) { }
            }

            using (FileStream FileToCompress = new FileStream(originalFile, FileMode.Open, FileAccess.Read))
            using (FileStream CompressedFile = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            using (Kontract.IO.BinaryWriterX outFs = new Kontract.IO.BinaryWriterX(CompressedFile))
            {
                //Double compression!
                if (Path.GetFileName(newFile).Contains(".gx3"))
                {
                    byte[] BodyFile = null;
                    BodyFile = Kontract.Compression.PSVSpikeChun.Compress(FileToCompress);
                    Array.Resize(ref BodyFile, BodyFile.Length + 4);
                    Array.Copy(BodyFile, 0, BodyFile, 4, BodyFile.Length - 4);

                    //Files with double compresion have this signature
                    BodyFile[0] = 0x47;
                    BodyFile[1] = 0x58;
                    BodyFile[2] = 0x33;
                    BodyFile[3] = 0x0;

                    outFs.Write(Kontract.Compression.PSVSpikeChun.Compress(new MemoryStream(BodyFile)));
                }
                else
                {
                    outFs.Write(Kontract.Compression.PSVSpikeChun.Compress(FileToCompress));
                }
            }

            // Replace the original file with the compressed one.
            if (ReplaceOriginal == true)
            {
                File.Delete(originalFile);
                while (File.Exists(originalFile)) { }

                File.Delete(originalFile.Replace(".gx3dec", ".gx3").Replace(".dec", ".cmp"));
                while (File.Exists(originalFile.Replace(".gx3dec", ".gx3").Replace(".dec", ".cmp"))) { }

                File.Move(newFile, originalFile.Replace(".gx3dec", ".gx3").Replace(".dec", ".cmp"));
                newFile = originalFile.Replace(".gx3dec", ".gx3").Replace(".dec", ".cmp");
            }

            return newFile;
        }

        public static string DecompressFileSpikeCompression(string originalFile, string DestinationDir, bool ReplaceOriginal)
        {
            if (Directory.Exists(DestinationDir) == false)
            {
                Directory.CreateDirectory(DestinationDir);
            }

            string newFile = Path.Combine(DestinationDir, Path.GetFileName(originalFile));

            byte[] BodyFile = null;

            using (FileStream file = new FileStream(originalFile, FileMode.Open, FileAccess.Read))
            {
                BodyFile = new byte[file.Length];
                file.Read(BodyFile, 0, BodyFile.Length);
            }

            // We don't know if the "originalFile" extension is "complete" or not, so we just read it again from the file.
            // DRAT use the extension part of each file to rebuild them exactly as the original. PAK files doesn't need extra info.
            if (Path.GetExtension(originalFile) != ".pak" && !Path.GetFileName(originalFile).Contains(".dec") && !Path.GetFileName(originalFile).Contains(".gt3") && !Path.GetFileName(originalFile).Contains(".cmp"))
            {
                string name = Path.GetFileName(originalFile).Substring(0, Path.GetFileName(originalFile).IndexOf(".")),
                ext = GetMagicID(ref BodyFile);

                newFile = Path.Combine(DestinationDir, name + ext);

                // Some files are "GXT" files, but some reason their original extension is BTX, so we need to save that, so the user will know it.
                if (Path.GetExtension(originalFile) != Path.GetExtension(newFile))
                {
                    newFile += Path.GetExtension(originalFile);
                }
            }
            else if (ReplaceOriginal == false && Path.GetExtension(originalFile) == ".pak" && !Path.GetFileName(originalFile).Contains(".dec") && !Path.GetFileName(originalFile).Contains(".gt3") && !Path.GetFileName(originalFile).Contains(".cmp"))
            {
                newFile += (".dec");
            }

                newFile = newFile.Replace(".gx3", ".gx3dec").Replace(".cmp", ".dec");

            // Remove any dec file previously created.
            if (File.Exists(newFile))
            {
                File.Delete(newFile);
                while (File.Exists(newFile)) { }
            }

            using (FileStream DecompressedFile = new FileStream(newFile, FileMode.Create, FileAccess.Write))
            using (Kontract.IO.BinaryWriterX outFs = new Kontract.IO.BinaryWriterX(DecompressedFile))
            {
                if (Path.GetFileName(newFile).Contains(".gx3"))
                {
                    BodyFile = Kontract.Compression.PSVSpikeChun.Decompress(new MemoryStream(BodyFile));
                    Array.Copy(BodyFile, 4, BodyFile, 0, BodyFile.Length - 4);
                    Array.Resize(ref BodyFile, BodyFile.Length - 4);
                }

                outFs.Write(Kontract.Compression.PSVSpikeChun.Decompress(new MemoryStream(BodyFile)));
            }

            // Replace the original file with the decompressed one.
            if (ReplaceOriginal == true)
            {
                if (File.Exists(originalFile))
                {
                    File.Delete(originalFile);
                    while (File.Exists(originalFile)) { }
                }

                File.Move(newFile, originalFile);
            }

            return newFile;
        }

        public static bool CheckIfEXEExist(string fileExe)
        {
            if (File.Exists("Ext\\" + fileExe))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Unable to run " + fileExe + ".\nAre you sure that you have it in \"Ext\" folder?", fileExe + " not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static void ChangeStatusLabelToWait(bool status)
        {
            if (status == true)
            {
                DRAT.labelStatusText.Text = "Wait..."; // Change "Ready!" to "Wait..."
                DRAT.labelStatusText.Refresh(); // Refresh the Status label.
            }
            else
            {
                DRAT.labelStatusText.Text = "Ready!"; // Change the "Status" to "Ready!".
                MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static string GetMagicID(ref byte[] BodyFile)
        {
            string NewFileExtension;

            if (BodyFile[0] == 0xFF && BodyFile[1] == 0xFE) // = Text file.
            {
                NewFileExtension = ".unknown";
            }
            // Check the beginning of the file with the purpose of discovering its extension.
            else if (BodyFile[0] == 0xF0 && BodyFile[1] == 0x30 && BodyFile[2] == 0x60 && BodyFile[3] == 0x90) //I'm not sure about what is this.
            {
                Array.Copy(BodyFile, 4, BodyFile, 0, BodyFile.Length - 4);
                NewFileExtension = ".pak";
            }
            // ".gx3" files are doubly compressed files for PSVITA.
            else if (BodyFile.Length > 16 &&((BodyFile[13] == 0x47 && BodyFile[14] == 0x58 && BodyFile[15] == 0x33 && BodyFile[16] == 0) || (BodyFile[14] == 0x47 && BodyFile[15] == 0x58 && BodyFile[16] == 0x33 && BodyFile[17] == 0)))
            {
                if ((BodyFile[30] == 0x47 && BodyFile[31] == 0x58 && BodyFile[32] == 0x54) || (BodyFile[31] == 0x47 && BodyFile[32] == 0x58 && BodyFile[33] == 0x54))
                {
                    // ".gxt" is an image format used for PSVITA.
                    NewFileExtension = ".gx3.gxt";
                }
                else if (BodyFile[30] == 0x53 && BodyFile[31] == 0x48 && BodyFile[32] == 0x54 && BodyFile[33] == 0x58 && BodyFile[34] == 0x46)
                {
                    // ".SHTXF?" is an image format used for PSVITA.
                    NewFileExtension = ".gx3.SHTXF" + Convert.ToChar(BodyFile[35]) + ".btx";
                }
                else if (BodyFile[31] == 0x53 && BodyFile[32] == 0x48 && BodyFile[33] == 0x54 && BodyFile[34] == 0x58 && BodyFile[35] == 0x46)
                {
                    // ".SHTXF?" is an image format used for PSVITA.
                    NewFileExtension = ".gx3.SHTXF" + Convert.ToChar(BodyFile[36]) + ".btx";
                }
                else if ((BodyFile[30] == 0x53 && BodyFile[31] == 0x48 && BodyFile[32] == 0x54 && BodyFile[33] == 0x58) || (BodyFile[31] == 0x53 && BodyFile[32] == 0x48 && BodyFile[33] == 0x54 && BodyFile[34] == 0x58))
                {
                    // ".SHTX" is an image format used for PSVITA.
                    NewFileExtension = ".gx3.SHTX.btx";
                }
                else if ((BodyFile[30] == 0x44 && BodyFile[31] == 0x44 && BodyFile[32] == 0x53 && BodyFile[33] == 0x31) || (BodyFile[31] == 0x44 && BodyFile[32] == 0x44 && BodyFile[33] == 0x53 && BodyFile[34] == 0x31))
                {
                    // ".dds"  is an image format used for AE (PC).
                    NewFileExtension = ".gx3.dds.btx";
                }
                else
                {
                    NewFileExtension = ".gx3";
                }
            }
            // The ".cmp" files are compressed files.
            else if (BodyFile[0] == 0xFC && BodyFile[1] == 0xAA && BodyFile[2] == 0x55 && BodyFile[3] == 0xA7)
            {
                if ((BodyFile[13] == 0x47 && BodyFile[14] == 0x58 && BodyFile[15] == 0x54) || (BodyFile[14] == 0x47 && BodyFile[15] == 0x58 && BodyFile[16] == 0x54))
                {
                    // ".gxt" is an image format used for PSVITA.
                    NewFileExtension = ".cmp.gxt";
                }
                else if (BodyFile[13] == 0x53 && BodyFile[14] == 0x48 && BodyFile[15] == 0x54 && BodyFile[16] == 0x58 && BodyFile[17] == 0x46)
                {
                    // ".SHTXF?" is an image format used for PSVITA.
                    NewFileExtension = ".cmp.SHTXF" + Convert.ToChar(BodyFile[18]) + ".btx";
                }
                else if (BodyFile[14] == 0x53 && BodyFile[15] == 0x48 && BodyFile[16] == 0x54 && BodyFile[17] == 0x58 && BodyFile[18] == 0x46)
                {
                    // ".SHTXF?" is an image format used for PSVITA.
                    NewFileExtension = ".cmp.SHTXF" + Convert.ToChar(BodyFile[19]) + ".btx";
                }
                else if (BodyFile[13] == 0x53 && BodyFile[14] == 0x48 && BodyFile[15] == 0x54 && BodyFile[16] == 0x58)
                {
                    // ".SHTX" is an image format used for PSVITA.
                    NewFileExtension = ".cmp.SHTX.btx";
                }
                else if ((BodyFile[13] == 0x44 && BodyFile[14] == 0x44 && BodyFile[15] == 0x53 && BodyFile[16] == 0x31) || (BodyFile[14] == 0x44 && BodyFile[15] == 0x44 && BodyFile[16] == 0x53 && BodyFile[17] == 0x31))
                {
                    // ".dds"  is an image format used for AE (PC).
                    NewFileExtension = ".cmp.dds.btx";
                }
                else
                {
                    NewFileExtension = ".cmp";
                }
            }
            else if (BodyFile[0] == 0x4C && BodyFile[1] == 0x4C && BodyFile[2] == 0x46 && BodyFile[3] == 0x53)
            {
                NewFileExtension = ".llfs";
            }
            else if (BodyFile[0] == 0x4F && BodyFile[1] == 0x4D && BodyFile[2] == 0x47 && BodyFile[3] == 0x2E && BodyFile[4] == 0x30 && BodyFile[5] == 0x30)
            {
                NewFileExtension = ".gmo";
            }
            else if (BodyFile[0] == 0x47 && BodyFile[1] == 0x58 && BodyFile[2] == 0x54)
            {
                // ".gxt" is an image format used for PSVITA.
                NewFileExtension = ".gxt";
            }
            else if (BodyFile[0] == 0x53 && BodyFile[1] == 0x48 && BodyFile[2] == 0x54 && BodyFile[3] == 0x58 && BodyFile[4] == 0x46)
            {
                //  There are three types of ".btx" ".SHTX", ".SHTXFS" and ".SHTXFF". This one is used for AE (PSVITA).
                NewFileExtension = ".SHTXF" + Convert.ToChar(BodyFile[5]) + ".btx";
            }
            else if (BodyFile[0] == 0x53 && BodyFile[1] == 0x48 && BodyFile[2] == 0x54 && BodyFile[3] == 0x58)
            {
                NewFileExtension = ".SHTX.btx";
            }
            else if (BodyFile[0] == 0x74 && BodyFile[1] == 0x46 && BodyFile[2] == 0x70 && BodyFile[3] == 0x53)
            {
                // Files.font descrives the font. Letters position, height, ect...
                NewFileExtension = ".font";
            }
            else if (BodyFile[0] == 0x4D && BodyFile[1] == 0x49 && BodyFile[2] == 0x47 && BodyFile[3] == 0x2E && BodyFile[4] == 0x30 && BodyFile[5] == 0x30)
            {
                // ".gim"  is an image format used for PSP.
                NewFileExtension = ".gim";
            }
            else if (BodyFile[0] == 0x44 && BodyFile[1] == 0x44 && BodyFile[2] == 0x53 && BodyFile[3] == 0x31)
            {
                // ".dds"  is an image format used for AE (PC).
                NewFileExtension = ".dds";
            }
            else if ((BodyFile[0] == 0x01 || BodyFile[0] == 0x02) && BodyFile[1] == 0x00 && BodyFile[2] == 0x00 && BodyFile[3] == 0x00 && (BodyFile[4] == 0x10 || BodyFile[4] == 0x0C) && BodyFile[5] == 0x00 && BodyFile[6] == 0x00 && BodyFile[7] == 0x00 && BodyFile[16] == 0x70)
            {
                NewFileExtension = ".lin";
            }
            else if (BodyFile.Length < 0x70 || (BodyFile[0] == 0xFF && BodyFile[1] == 0xFE))
            {
                NewFileExtension = ".unknown";
            }
            else if ((BodyFile[0] == 0x00 && BodyFile[1] == 0x01 && BodyFile[2] == 0x01 && BodyFile[3] == 0x00) || (BodyFile[0] == 0x00 && BodyFile[1] == 0x00 && BodyFile[2] == 0x02 && BodyFile[3] == 0x00) || (BodyFile[0] == 0x01 && BodyFile[1] == 0x00 && BodyFile[2] == 0x02 && BodyFile[3] == 0x00) || (BodyFile[0] == 0x00 && BodyFile[1] == 0x00 && BodyFile[2] == 0x0A && BodyFile[3] == 0x00) || (BodyFile[0] == 0x00 && BodyFile[1] == 0x01 && BodyFile[2] == 0x09 && BodyFile[3] == 0x00) || (BodyFile[0] == 0x00 && BodyFile[1] == 0x00 && BodyFile[2] == 0x0B && BodyFile[3] == 0x00))
            {
                NewFileExtension = ".tga";
            }
            else if ((BodyFile[0] == 0x42 && BodyFile[1] == 0x4D))
            {
                NewFileExtension = ".bmp";
            }
            else if ((BodyFile[0] != 0x00 || BodyFile[1] != 0x00) && BodyFile[2] == 0x00 && BodyFile[3] == 0x00 && (BodyFile[4] != 0x00 || BodyFile[5] != 0x00) && BodyFile[6] == 0x00 && BodyFile[7] == 0x00)
            {
                NewFileExtension = ".pak";
            }
            else
            {
                NewFileExtension = ".unknown";
            }

            return NewFileExtension;
        }

        //Clean the file's extension. "Image.dec.SHTXFS.btx" --> "Image.btx"
        public static string CleanExtension(string OriginalFile)
        {
            string CleanedName = Path.Combine(Path.GetDirectoryName(OriginalFile), Path.GetFileName(OriginalFile).Substring(0, Path.GetFileName(OriginalFile).IndexOf(".")) + Path.GetExtension(OriginalFile));

            if (CleanedName != OriginalFile)
            {
                if (File.Exists(CleanedName))
                {
                    File.Delete(CleanedName);
                    while (File.Exists(CleanedName)) { }
                }

                File.Move(OriginalFile, CleanedName);
            }

            return CleanedName;
        }
    }
}