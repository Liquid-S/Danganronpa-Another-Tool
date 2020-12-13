using System;
using System.IO;
using System.Text;

namespace Danganronpa_Another_Tool
{
    public static class WAD
    {
        public static void ExtractWAD(string WADdress, string DestinationDir)
        {
            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            using (FileStream FILEWAD = new FileStream(WADdress, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader WadBinReader = new BinaryReader(FILEWAD))
                {
                    // The first 0x10 byte can be skipped because they contain: MagicID (= 0x52414741), MaVersion, MiVersion and unknown.
                    // Yes, I could do the ID check, but... nah.
                    FILEWAD.Seek(0x10, SeekOrigin.Begin);

                    // Saves the amount of files contained in the WAD, ie the number of files to extract.
                    uint AmountOfFiles = WadBinReader.ReadUInt32(),
                         NameLength = 0;

                    int[] FilesSizes = new int[AmountOfFiles], OffsetFiles = new int[AmountOfFiles];
                    string[] FilesNames = new string[AmountOfFiles];

                    // START memorization name, size and offset of each file contained in the WAD.
                    for (int i = 0; i < AmountOfFiles; i++)
                    {
                        NameLength = WadBinReader.ReadUInt32(); // It stores the length of the filename.
                        byte[] TempName = new byte[NameLength];
                        WadBinReader.Read(TempName, 0, TempName.Length); // It reads the filename and stores it temporarily in "TempName".
                        FilesSizes[i] = WadBinReader.ReadInt32(); // It stores the file size.
                        WadBinReader.ReadInt32(); // Unk. It's always zero.
                        OffsetFiles[i] = WadBinReader.ReadInt32(); // It stores the file offset.
                        WadBinReader.ReadInt32(); // Unk. It's always zero.
                        FilesNames[i] = Encoding.Default.GetString(TempName); // It stores the filename in FilesNames[].
                    }
                    // END memorization.

                    /* START reading (without memorization) the names of the folders, subfolders and EVEN their content (files).
                     This step serves only to arrive at the beginning of the body of the files in order to begin with the extraction. */
                    uint AmountOfDirs = WadBinReader.ReadUInt32(); // It stores the amount of directories.

                    for (int i = 0; i < AmountOfDirs; i++)
                    {
                        NameLength = WadBinReader.ReadUInt32(); // It stores the length of the directory name.
                        FILEWAD.Seek(NameLength, SeekOrigin.Current);

                        uint AmountOfSubDirs = WadBinReader.ReadUInt32(); // It stores the number of subfolders in the "current" folder.

                        for (int a = 0; a < AmountOfSubDirs; a++)
                        {
                            NameLength = WadBinReader.ReadUInt32(); // It stores the length of the subdirectory name.
                            FILEWAD.Seek(NameLength + 1, SeekOrigin.Current);
                            /* The "+1" stands for byte specifying whether it is a file or folder (Typology).
                             If byte == "01", then it's a folder, if == "00" then it's a file.
                             In this case all of them are folders, so there is no need to memorize it. */
                        }

                    }
                    // END reading (without memorization).

                    // START files creation.
                    long pos = FILEWAD.Position;

                    for (int i = 0; i < AmountOfFiles; i++)
                    {
                        Directory.CreateDirectory(Path.Combine(DestinationDir, Path.GetDirectoryName(FilesNames[i])));
                        FILEWAD.Seek((long)OffsetFiles[i] + pos, SeekOrigin.Begin);
                        byte[] BodyFile = new byte[FilesSizes[i]];
                        WadBinReader.Read(BodyFile, 0, (int)FilesSizes[i]);

                        using (FileStream Extracted = new FileStream(Path.Combine(DestinationDir, FilesNames[i]), FileMode.Create, FileAccess.Write))
                            Extracted.Write(BodyFile, 0, (int)FilesSizes[i]);
                    }
                    // END files creation.
                }
            }
        }

        public static void RePackWAD(string FolderToRebuildToWAD, string DestinationDir)
        {
            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            using (FileStream FILEWAD = new FileStream(Path.Combine(DestinationDir, Path.GetFileName(FolderToRebuildToWAD) + ".wad"), FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter WADBinaryWriter = new BinaryWriter(FILEWAD))
                {
                    // It stores in "FullFilesAddress" the full address of ALL the files in the folder WAD.
                    string[] FullFilesAddress = Directory.GetFiles(FolderToRebuildToWAD, "*", SearchOption.AllDirectories);
                    // It stores in "FullDirsAddress" the full address of ALL the directories in the folder WAD to be reassembled.
                    string[] FullDirsAddress = Directory.GetDirectories(FolderToRebuildToWAD, "*", SearchOption.AllDirectories);
                    Array.Sort(FullDirsAddress, new DRAT.AlphanumComparatorFast()); // Order alphanumerically the variable contents.

                    /* It stores in "CleanedFilesAddress" in alphanumeric order the clean address of ALL the files in the folder WAD.
                     With "clean" I mean convert "X:\DRAT\DR1(PC)\[MANUAL MODE]\EXTRACTED\EXTRACTED WAD\dr1_data_keyboard.wad\Dr1\data\all\bin\mtb.pak"
                     to "Dr1\data\all\bin\mtb.pak" which is the actual address which should be saved in the new WAD. */
                    string[] CleanedFilesAddress = Directory.GetFiles(FolderToRebuildToWAD, "*", SearchOption.AllDirectories);
                    // It stores in "CleanedDirsAddress" in alphanumeric order the clean address of ALL the direcotires in the folder WAD.
                    string[] CleanedDirsAddress = Common.CleanAddress(Directory.GetDirectories(FolderToRebuildToWAD, "*", SearchOption.AllDirectories), FolderToRebuildToWAD);

                    // Delete the address that come before the folder's name. Ex: "D:/Data/bin_mtb.pak" --> "Data/bin_mtb.pak" 
                    for (int i = 0; i < CleanedFilesAddress.Length; i++)
                        CleanedFilesAddress[i] = CleanedFilesAddress[i].Replace(FolderToRebuildToWAD + "\\", null).Replace("\\", "/");

                    // IMPORTANT: To recreate the WAD exactly like the original, we have to reorder alphanumerically ONLY the folders, NOT THE FILES.

                    // START header creation
                    WADBinaryWriter.Write((uint)0x52414741); // MagicID 
                    WADBinaryWriter.Write((uint)0x01); // MaVersion 
                    WADBinaryWriter.Write((uint)0x01); // MiVersion 
                    WADBinaryWriter.Write((uint)0x0); // Unknown
                    // END header creation 

                    // START Write the name, size and offset of each file that will form the new WAD.
                    WADBinaryWriter.Write((uint)CleanedFilesAddress.Length); // Writes the amount of files that will make up the new WAD. 

                    uint TempOffset = 0; // This variable will contains the offset of each file. The first file has the offset "0". 

                    for (int i = 0; i < FullFilesAddress.Length; i++)
                    { // Opens, in alphanumeric order, all the files inside "FullFilesAddress".
                        using (FileStream TempFile = new FileStream(FullFilesAddress[i], FileMode.Open, FileAccess.Read))
                        { // and adds the data to the new WAD.
                            WADBinaryWriter.Write((uint)CleanedFilesAddress[i].Length); // Writes the cleaned address length of the file. Ex: "Dr1/script/001.lin" = 18 
                            WADBinaryWriter.Write(CleanedFilesAddress[i].ToCharArray()); // Writes the cleaned address of the file.
                            WADBinaryWriter.Write((uint)TempFile.Length); // Writes the file size.
                            WADBinaryWriter.Write((uint)0x0); // Unknown
                            WADBinaryWriter.Write(TempOffset); // Writes the file offeset. The first file offset is "0".
                            WADBinaryWriter.Write((uint)0x0); // Unknown
                            TempOffset += (uint)TempFile.Length; /* The second file offset (as well as subsequent) is calculated by summing the offset to the size of the current file.
                            This offset will point at the beginning of the next file and not to the previous one. */
                        }
                    }
                    // START Write the name, size and offset of each file that will form the new WAD.

                    // START Write the names of folders, sub-folders and their contents.
                    WADBinaryWriter.Write((uint)CleanedDirsAddress.Length + 1); // Amount of directories + 1 ("+1" = "+root") 

                    // START dedicated to the root zone ONLY.
                    WADBinaryWriter.Write((uint)0x0); // Length of the name of the first folder. Since the root doesn't have a name, we must write "00 00 00 00".

                    // Amount of subdirectories and files inside the root.
                    WADBinaryWriter.Write((uint)(Directory.GetDirectories(FolderToRebuildToWAD, "*", SearchOption.TopDirectoryOnly).Length + Directory.GetFiles(FolderToRebuildToWAD, "*", SearchOption.TopDirectoryOnly).Length));

                    // If the current folder contains one or more subfolders...
                    if (Directory.GetDirectories(FolderToRebuildToWAD, "*", SearchOption.TopDirectoryOnly).Length != 0)
                    {
                        // Saves in "tempD" the cleaned address of the directories of the current folder.
                        string[] tempD = Common.CleanAddress(Common.ReadOnlyRootDirs(FolderToRebuildToWAD), FolderToRebuildToWAD);

                        for (int a = 0; a < tempD.Length; a++) // For each subfolder we have to write...
                        {   // Remove the address before the current directory. Es: "Dr1/Data" --> "Data"
                            tempD[a] = tempD[a].Replace(FolderToRebuildToWAD + "/", null);
                            WADBinaryWriter.Write((uint)tempD[a].Length); // Length of the subfolder name.
                            WADBinaryWriter.Write(tempD[a].ToCharArray()); // Subfolder name.
                            WADBinaryWriter.Write((byte)0x01); // Byte which specify the type. "0x01" = folder, "0x00" = file.
                        }
                    }

                    // If the current folder contains one or more files...
                    if (Directory.GetFiles(FolderToRebuildToWAD, "*", SearchOption.TopDirectoryOnly).Length != 0)
                    {
                        // Saves in "tempF" the cleaned address of the files of the current folder.
                        string[] tempF = Directory.GetFiles(FolderToRebuildToWAD, "*", SearchOption.TopDirectoryOnly);

                        for (int a = 0; a < tempF.Length; a++) // For each file we have to write...
                        { // Remove the address before the current file. Es: "Data/bin_mtb.pak" --> "bin_mtb.pak".
                            tempF[a] = tempF[a].Replace(FolderToRebuildToWAD + "\\", null).Replace("\\", "/");
                            WADBinaryWriter.Write((uint)tempF[a].Length); // Length of the filename.
                            WADBinaryWriter.Write(tempF[a].ToCharArray()); // File name.
                            WADBinaryWriter.Write((byte)0x00); // Byte which specify the type. "0x01" = folder, "0x00" = file.
                        }
                    }
                    // END dedicated to the root zone ONLY.

                    // START zone dedicated to the ALL the root's subfolders. 
                    for (int i = 0; i < CleanedDirsAddress.Length; i++)
                    {
                        WADBinaryWriter.Write((uint)CleanedDirsAddress[i].Length); // Length of the folder name.
                        WADBinaryWriter.Write(CleanedDirsAddress[i].ToCharArray()); // Folder name.
                        // Number of folders and files in the current folder.
                        WADBinaryWriter.Write((uint)(Directory.GetDirectories(FullDirsAddress[i], "*", SearchOption.TopDirectoryOnly).Length + Directory.GetFiles(FullDirsAddress[i], "*", SearchOption.TopDirectoryOnly).Length));

                        // If the current folder contains one or more subfolders...
                        if (Directory.GetDirectories(FullDirsAddress[i], "*", SearchOption.TopDirectoryOnly).Length != 0)
                        {
                            // Saves in "tempD" the cleaned address of the directories of the current folder.
                            string[] tempD = Common.CleanAddress(Common.ReadOnlyRootDirs(FullDirsAddress[i]), FolderToRebuildToWAD);

                            for (int a = 0; a < tempD.Length; a++) // For each subfolder we have to write...
                            {   // Remove the address before the current directory. Es: "Dr1/Data" --> "Data"
                                tempD[a] = tempD[a].Replace(CleanedDirsAddress[i] + "/", null);
                                WADBinaryWriter.Write((uint)tempD[a].Length); // Length of the subfolder name.
                                WADBinaryWriter.Write(tempD[a].ToCharArray()); // Subfolder name.
                                WADBinaryWriter.Write((byte)0x01); // Byte which specify the type. "0x01" = folder, "0x00" = file.
                            }
                        }

                        // If the current folder contains one or more files...
                        if (Directory.GetFiles(FullDirsAddress[i], "*", SearchOption.TopDirectoryOnly).Length != 0)
                        {
                            // Saves in "tempF" the cleaned address of the files of the current folder.
                            string[] tempF = Directory.GetFiles(FullDirsAddress[i], "*", SearchOption.TopDirectoryOnly);

                            for (int a = 0; a < tempF.Length; a++) // For each file we have to write...
                            {// Remove the address before the current file. Ex: "Data/bin_mtb.pak" --> "bin_mtb.pak".
                                tempF[a] = tempF[a].Replace(FullDirsAddress[i] + "\\", null).Replace("\\", "/");
                                WADBinaryWriter.Write((uint)tempF[a].Length); // Length of the folder name.
                                WADBinaryWriter.Write(tempF[a].ToCharArray()); // File name.
                                WADBinaryWriter.Write((byte)0x00); // Byte which specify the type. "0x01" = folder, "0x00" = file.
                            }
                        }
                    }
                    // END zone dedicated to the ALL the root's subfolders.

                    // END Write the names of folders, sub-folders and their contents.

                    // START inserting the body of each file inside the new WAD.
                    for (int i = 0; i < FullFilesAddress.Length; i++)
                    { // Opens EVERY file and put its body within the new WAD.
                        using (FileStream TempFile = new FileStream(FullFilesAddress[i], FileMode.Open, FileAccess.Read))
                        {
                            byte[] BodyFile = new byte[TempFile.Length];
                            TempFile.Read(BodyFile, 0, BodyFile.Length);
                            FILEWAD.Write(BodyFile, 0, BodyFile.Length);
                        }
                    }
                    // END inserting the body of each file inside the new WAD.
                }
            }
        }
    }
}
