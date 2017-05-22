using System;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Reflection;

namespace Danganronpa_Another_Tool
{
    public partial class DRAT : Form
    {
        public DRAT()
        {
            InitializeComponent();

            // Change the main tab to "DR1 (PC)". So whenever the user open the tool, you will see the "DR1 (PC)" tab instead the PSP one. 
            tabControl1.SelectedIndex = 2;

            // When the user change tab "tabControl1_Selecting" will occour.
            tabControl1.Selecting += new TabControlCancelEventHandler(tabControl1_Selecting);
        }

        // Clone the original buttons to the new tablelayout's tab when the user change tab.
        void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //.GetControlFromPosition(0, 0) == null... serve to avoid cloning the buttons in the same tab more than once.
            if (tabControl1.SelectedIndex == 0 && tableLayoutPanel3.GetControlFromPosition(0, 0) == null)
                CloneTab(ref tableLayoutPanel3);
            else if (tabControl1.SelectedIndex == 1 && tableLayoutPanel4.GetControlFromPosition(0, 0) == null)
                CloneTab(ref tableLayoutPanel4);
            else if (tabControl1.SelectedIndex == 3 && tableLayoutPanel6.GetControlFromPosition(0, 0) == null)
                CloneTab(ref tableLayoutPanel6);
            else if (tabControl1.SelectedIndex == 4 && tableLayoutPanel7.GetControlFromPosition(0, 0) == null)
                CloneTab(ref tableLayoutPanel7);
            else if (tabControl1.SelectedIndex == 5 && tableLayoutPanel8.GetControlFromPosition(0, 0) == null)
                CloneTab(ref tableLayoutPanel8);
            else if (tabControl1.SelectedIndex == 6 && tableLayoutPanel9.GetControlFromPosition(0, 0) == null)
                CloneTab(ref tableLayoutPanel9);
        }

        public void CloneTab(ref TableLayoutPanel TableLayoutToFill)
        {
            CreaLabel(ref TableLayoutToFill, ref label1);
            CreaLabel(ref TableLayoutToFill, ref label2);
            CreaLabel(ref TableLayoutToFill, ref label3);
            CloneButton(ref TableLayoutToFill, ref button7);
            CloneButton(ref TableLayoutToFill, ref button8);
            CloneButton(ref TableLayoutToFill, ref button9);
            CloneButton(ref TableLayoutToFill, ref button10);
            CloneButton(ref TableLayoutToFill, ref button11);
            CloneButton(ref TableLayoutToFill, ref button12);
            CloneButton(ref TableLayoutToFill, ref button13);
            CloneButton(ref TableLayoutToFill, ref button14);
            CloneButton(ref TableLayoutToFill, ref button15);
            CloneButton(ref TableLayoutToFill, ref button16);
            CloneButton(ref TableLayoutToFill, ref button17);

            /* The tool doesn't convert images from PNG to GIM/BTX, so there is no point in cloning
             the "REPACK TEXTURE .PAK FROM PNG" button in the PSP tab and AE tabs. */
            if (!tabControl1.SelectedTab.Text.Contains("PSP") && !tabControl1.SelectedTab.Text.Contains("DRAE (PSVITA)"))
                CloneButton(ref TableLayoutToFill, ref button18);

            CloneButton(ref TableLayoutToFill, ref button19);

            if (!tabControl1.SelectedTab.Text.Contains("PSP"))
                CloneButton(ref TableLayoutToFill, ref button20);

            if (!tabControl1.SelectedTab.Text.Contains("PSP"))
                CloneButton(ref TableLayoutToFill, ref button21);

            // if (!tabControl1.SelectedTab.Text.Contains("PSP")) Button22 doesn't do anything for now, so...
            // CloneButton(ref TableLayoutToFill, ref button22);

            // Clone the "Unpack/Repack .WAD" buttons only if the new tab is about a PC game.
            if (tabControl1.SelectedTab.Text.Contains("PC"))
            {
                CloneButton(ref TableLayoutToFill, ref button1);
                CloneButton(ref TableLayoutToFill, ref button2);
            }
            /* Clone the "Unpack/Repack .CPK" buttons only if the new tab is about a PSVITA game.
            "DR1 (PSVITA)"'s tab contains the original buttons, so there is no need to clone the buttons there. */
            else if (tabControl1.SelectedTab.Text.Contains("PSVITA") && !tabControl1.SelectedTab.Text.Contains("DR1 (PSVITA)"))
            {
                CloneButton(ref TableLayoutToFill, ref button3);
                CloneButton(ref TableLayoutToFill, ref button4);
            }
        }

        // Clone the chosen Label to the new TableLayout
        public void CreaLabel(ref TableLayoutPanel Gril, ref Label OriginalLabel)
        {
            int Column = Gril.GetColumn(OriginalLabel), Row = Gril.GetRow(OriginalLabel);
            Gril.Controls.Add(new Label()
            {
                Anchor = OriginalLabel.Anchor,
                AutoSize = OriginalLabel.AutoSize,
                BackColor = OriginalLabel.BackColor,
                ForeColor = OriginalLabel.ForeColor,
                Size = OriginalLabel.Size,
                Text = OriginalLabel.Text,
                TextAlign = OriginalLabel.TextAlign,
                Font = OriginalLabel.Font,
                Dock = OriginalLabel.Dock,
                Margin = OriginalLabel.Margin,

            }, Column, Row);
            Gril.SetColumnSpan(Gril.GetControlFromPosition(Column, Row), Gril.GetColumnSpan(OriginalLabel));
        }

        // Clone the chosen Button to the new TableLayout
        public void CloneButton(ref TableLayoutPanel Gril, ref Button OriginalButton)
        {
            int Column = Gril.GetColumn(OriginalButton), Row = Gril.GetRow(OriginalButton);

            Gril.Controls.Add(new Button()
            {
                Anchor = OriginalButton.Anchor,
                AutoSize = OriginalButton.AutoSize,
                BackColor = OriginalButton.BackColor,
                ForeColor = OriginalButton.ForeColor,
                Size = OriginalButton.Size,
                Text = OriginalButton.Text,
                TextAlign = OriginalButton.TextAlign,
                Font = OriginalButton.Font,
                Dock = OriginalButton.Dock,
                Margin = OriginalButton.Margin,
                UseVisualStyleBackColor = OriginalButton.UseVisualStyleBackColor
            }, Column, Row);

            var eventsField = typeof(Component).GetField("events", BindingFlags.NonPublic | BindingFlags.Instance);
            var eventHandlerList = eventsField.GetValue(OriginalButton);
            eventsField.SetValue(Gril.GetControlFromPosition(Column, Row), eventHandlerList);
        }       

        // Clone the directories, this way the tool can delete, change and repack everything without worrying about damage the user work.
        private void CloneDirectory(string OriginalDir, string TEMPFolder, string PAKType, int ConvertOrNotToConvert)
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
                extensions = new[] { ".llfs", ".gmo", ".gxt", ".btx", ".font", ".gim", ".unknown", ".tga", ".pak", ".png", ".cmp", ".gx3" };
            else if (PAKType == "TEXT")
                extensions = new[] { ".lin", ".pak", ".xml", ".bytecode", ".unknown" };

            // Copy the files to the TEMP folder. Images are converted only if the user has requested.
            foreach (FileInfo fi in source.EnumerateFiles().Where(f => extensions.Contains(f.Extension.ToLower())).ToArray())
            {
                string SingleImage = fi.FullName;

                // Converts the images if the user want it and is not working with the PSP version...
                if (ConvertOrNotToConvert == 1 && !tabControl1.SelectedTab.Text.Contains("PSP") && (fi.Extension == ".png" || fi.Extension == ".tga"))
                {
                    // Converts ".png" images to ".tga" saving them directly in the TEMP folder.
                    if (fi.Extension == ".png")
                    {
                        ConvertFromPNGToTGA(fi.FullName, target.ToString());
                        SingleImage = Path.Combine(target.ToString(), Path.GetFileNameWithoutExtension(fi.Name) + ".tga");
                    }

                    // If the user is working with the games for PSVITA convert the image to .gxt.
                    if (tabControl1.SelectedTab.Text.Contains("PSVITA"))
                    {
                        // Converts ".tga" images to ".gxt" saving them directly in the TEMP folder.
                        ConvertFromTGAToGXT(SingleImage, target.ToString());

                        // Delete the image in TGA as it's not longer needed.
                        File.Delete(SingleImage);
                        while (File.Exists(SingleImage)) { }
                    }

                }
                else // Otherwise if there is nothing to convert, copy the file to the "TEMP" folder without changes.
                    fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy the subfolders and their contents.
            foreach (string SubDir in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                CloneDirectory(SubDir, Path.Combine(TEMPFolder, Path.GetFileName(SubDir)), PAKType, ConvertOrNotToConvert);
        }

        public static void UseEXEToConvert(string FileEXE, string CodeLine)
        {
            CodeLine = CodeLine.Replace("\\", "/");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.FileName = FileEXE;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = CodeLine;

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                    exeProcess.WaitForExit();
            }

            catch
            {
                MessageBox.Show("Unable to run " + FileEXE + ".\nAre you sure that you have it in \"Ext\" folder?", FileEXE + " not found!");
            }
        }

        // Reads all the folders in the root (no files or subdirectories) and sorts them alphanumerically.
        private string[] ReadOnlyRootDirs(string folder)
        {
            string[] temp = Directory.GetDirectories(folder, "*", SearchOption.TopDirectoryOnly);
            Array.Sort(temp, new AlphanumComparatorFast());
            return temp;
        }

        // Clean all that is before the folder's name, turns "\\" to "/" and order the string[] alphanumerically.
        private string[] CleanAddress(string[] stringa, string folder)
        {
            Array.Sort(stringa, new AlphanumComparatorFast());

            for (int i = 0; i < stringa.Length; i++)
                stringa[i] = stringa[i].Replace(folder + "\\", null).Replace("\\", "/");

            return stringa;
        }

        private void button1_Click(object sender, EventArgs e) // EXTRACT .WAD 
        {
            using (OpenFileDialog WAD = new OpenFileDialog())
            {
                WAD.Title = "Select one or more \"file.wad\"";
                WAD.Filter = ".wad|*.wad|All Files (*.*)|*.*";
                WAD.FileName = "*.wad";
                WAD.Multiselect = true;

                if (WAD.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SingleWAD in WAD.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\WAD\\" + Path.GetFileNameWithoutExtension(SingleWAD);

                        if (Directory.Exists(DestinationDir) == false)
                            Directory.CreateDirectory(DestinationDir);

                        ExtractWAD(SingleWAD, DestinationDir);
                    }

                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".
                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ExtractWAD(string WADdress, string DestinationDir)
        {
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

                    ulong[] FilesSizes = new ulong[AmountOfFiles], OffsetFiles = new ulong[AmountOfFiles];
                    string[] FilesNames = new string[AmountOfFiles];

                    // START memorization name, size and offset of each file contained in the WAD.
                    for (int i = 0; i < AmountOfFiles; i++)
                    {
                        NameLength = WadBinReader.ReadUInt32(); // It stores the length of the filename.
                        byte[] TempName = new byte[NameLength];
                        WadBinReader.Read(TempName, 0, TempName.Length); // It reads the filename and stores it temporarily in "TempName".
                        FilesSizes[i] = WadBinReader.ReadUInt64(); // It stores the file size.
                        OffsetFiles[i] = WadBinReader.ReadUInt64(); // It stores the file offset.
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

        private void button2_Click(object sender, EventArgs e) // REPACK WAD 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\WAD";

            // If the folder "EXTRACTED WAD" exists and is not empty.
            if (Directory.Exists(OriginalDir) && Directory.EnumerateDirectories(OriginalDir).Any() == true)
            {
                label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                label5.Refresh(); // Refresh the Status label.

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\WAD";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                    Directory.CreateDirectory(DestinationDir);

                // For each folder to be reassembled in .WAD
                foreach (string FolderToRebuildToWAD in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                    RePackWAD(FolderToRebuildToWAD, DestinationDir);

                label5.Text = "Ready!"; // Change the "Status" to "Ready!".
                MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RePackWAD(string FolderToRebuildToWAD, string DestinationDir)
        {
            using (FileStream FILEWAD = new FileStream(Path.Combine(DestinationDir, Path.GetFileName(FolderToRebuildToWAD) + ".wad"), FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter WADBinaryWriter = new BinaryWriter(FILEWAD))
                {
                    // It stores in "FullFilesAddress" the full address of ALL the files in the folder WAD.
                    string[] FullFilesAddress = Directory.GetFiles(FolderToRebuildToWAD, "*", SearchOption.AllDirectories);
                    // It stores in "FullDirsAddress" the full address of ALL the directories in the folder WAD to be reassembled.
                    string[] FullDirsAddress = Directory.GetDirectories(FolderToRebuildToWAD, "*", SearchOption.AllDirectories);
                    Array.Sort(FullDirsAddress, new AlphanumComparatorFast()); // Order alphanumerically the variable contents.

                    /* It stores in "CleanedFilesAddress" in alphanumeric order the clean address of ALL the files in the folder WAD.
                     With "clean" I mean convert "X:\DRAT\DR1(PC)\[MANUAL MODE]\EXTRACTED\EXTRACTED WAD\dr1_data_keyboard.wad\Dr1\data\all\bin\mtb.pak"
                     to "Dr1\data\all\bin\mtb.pak" which is the actual address which should be saved in the new WAD. */
                    string[] CleanedFilesAddress = Directory.GetFiles(FolderToRebuildToWAD, "*", SearchOption.AllDirectories);
                    // It stores in "CleanedDirsAddress" in alphanumeric order the clean address of ALL the direcotires in the folder WAD.
                    string[] CleanedDirsAddress = CleanAddress(Directory.GetDirectories(FolderToRebuildToWAD, "*", SearchOption.AllDirectories), FolderToRebuildToWAD);

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

                    long TempOffset = 0; // This variable will contains the offset of each file. The first file has the offset "0". 

                    for (int i = 0; i < FullFilesAddress.Length; i++)
                    { // Opens, in alphanumeric order, all the files inside "FullFilesAddress".
                        using (FileStream TempFile = new FileStream(FullFilesAddress[i], FileMode.Open, FileAccess.Read))
                        { // and adds the data to the new WAD.
                            WADBinaryWriter.Write((uint)CleanedFilesAddress[i].Length); // Writes the cleaned address length of the file. Ex: "Dr1/script/001.lin" = 18 
                            WADBinaryWriter.Write(CleanedFilesAddress[i].ToCharArray()); // Writes the cleaned address of the file.
                            WADBinaryWriter.Write((long)TempFile.Length); // Writes the file size.
                            WADBinaryWriter.Write(TempOffset); // Writes the file offeset. The first file offset is "0".
                            TempOffset += TempFile.Length; /* The second file offset (as well as subsequent) is calculated by summing the offset to the size of the current file.
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
                        string[] tempD = CleanAddress(ReadOnlyRootDirs(FolderToRebuildToWAD), FolderToRebuildToWAD);

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
                            string[] tempD = CleanAddress(ReadOnlyRootDirs(FullDirsAddress[i]), FolderToRebuildToWAD);

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

        private void button3_Click(object sender, EventArgs e) // UNPACK .CPK
        {
            Process.Start("https://github.com/s1cp/VitaGuide/wiki#unpacking-the-files");
        }

        private void button4_Click(object sender, EventArgs e) // REPACK .CPK
        {
            Process.Start("https://github.com/s1cp/VitaGuide/wiki#unpacking-the-files");
        }

        private void button5_Click(object sender, EventArgs e) // EXTRACT FULL GAME UMDIMAGE.DAT (PSP ONLY) 
        {
            using (OpenFileDialog UMDIMAGE = new OpenFileDialog())
            {
                UMDIMAGE.Title = "Select \"umdimage.dat\" or \"umdimage2.dat\" (PSP ONLY)";
                UMDIMAGE.Filter = "umdimage.dat (PSP)|*.dat|All Files (*.*)|*.*";
                UMDIMAGE.FileName = "umdimage.dat";

                using (OpenFileDialog EBOOT = new OpenFileDialog())
                {
                    EBOOT.Title = "Open decrypted \"eboot.bin\" (PSP ONLY)";
                    EBOOT.Filter = "Decrypted eboot.bin (PSP)|eboot.bin|All \".bin\" (*.bin)|*.bin|All Files (*.*)|*.*";
                    EBOOT.FileName = "eboot.bin";

                    if (UMDIMAGE.ShowDialog() == DialogResult.OK && EBOOT.ShowDialog() == DialogResult.OK)
                    {
                        label5.Text = "Wait...";
                        label5.Refresh();

                        string DestinationDir = "DR1 (PSP) [MANUAL MODE]\\EXTRACTED\\" + Path.GetFileNameWithoutExtension(UMDIMAGE.FileName).ToUpper() + " (FULL GAME)";

                        if (Directory.Exists(DestinationDir) == false)
                            Directory.CreateDirectory(DestinationDir);

                        ExtractDAT(UMDIMAGE.FileName, DestinationDir, 0xF5A38, EBOOT.FileName, "FULL");

                        label5.Text = "Ready!";
                        MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e) // EXTRACT DEMO UMDIMAGE.DAT (PSP ONLY) 
        {
            using (OpenFileDialog UMDIMAGE = new OpenFileDialog())
            {
                UMDIMAGE.Title = "Select \"umdimage.dat\" or \"umdimage2.dat\" (PSP ONLY)";
                UMDIMAGE.Filter = "umdimage.dat (PSP)|*.dat|All Files (*.*)|*.*";
                UMDIMAGE.FileName = "umdimage.dat";

                using (OpenFileDialog EBOOT = new OpenFileDialog())
                {
                    EBOOT.Title = "Open decrypted \"eboot.bin\" (PSP ONLY)";
                    EBOOT.Filter = "Decrypted eboot.bin (PSP)|eboot.bin|All \".bin\" (*.bin)|*.bin|All Files (*.*)|*.*";
                    EBOOT.FileName = "eboot.bin";

                    if (UMDIMAGE.ShowDialog() == DialogResult.OK && EBOOT.ShowDialog() == DialogResult.OK)
                    {
                        label5.Text = "Wait...";
                        label5.Refresh();

                        string DestinationDir = "DR1 (PSP) [MANUAL MODE]\\EXTRACTED\\" + Path.GetFileNameWithoutExtension(UMDIMAGE.FileName).ToUpper() + " (DEMO)";

                        if (Directory.Exists(DestinationDir) == false)
                            Directory.CreateDirectory(DestinationDir);

                        ExtractDAT(UMDIMAGE.FileName, DestinationDir, 0x145c1c, EBOOT.FileName, "DEMO");

                        label5.Text = "Ready!";
                        MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void ExtractDAT(string IndirizzoUMDIMAGE, string DestinationDir, long pos, string FILEBOOT, string GameVersion)
        {
            using (FileStream FILEDAT = new FileStream(IndirizzoUMDIMAGE, FileMode.Open, FileAccess.Read), EBOOT = new FileStream(FILEBOOT, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader LettoreDAT = new BinaryReader(FILEDAT, Encoding.Default), LettoreEBOOT = new BinaryReader(EBOOT))
                {
                    uint AmountOfFiles = 0;

                    if (GameVersion == "FULL") // If the user is working with the full game files.
                        AmountOfFiles = LettoreDAT.ReadUInt32(); // Reads the number of files inside the UMDIMAGE.DAT.
                    else // If the user has chosen to work with the demo files.
                    {
                        EBOOT.Seek(pos, SeekOrigin.Begin);
                        AmountOfFiles = LettoreEBOOT.ReadUInt32(); // Reads the number of files inside the UMDIMAGE.DAT.
                        LettoreEBOOT.ReadUInt32(); // Unknown and "useless".
                    }

                    string[] FilesNames = new string[AmountOfFiles];
                    uint[] OffsetFiles = new uint[AmountOfFiles], FilesSizes = new uint[AmountOfFiles];

                    if (GameVersion == "FULL") // If the user is working with the full game files.
                    {
                        for (int i = 0; i < AmountOfFiles; i++)
                        {
                            FILEDAT.Seek(i * 0x4 + 0x4, SeekOrigin.Begin); // Move at the beginning of the info of each file inside the DAT. 
                            OffsetFiles[i] = LettoreDAT.ReadUInt32(); // Saves the file offset involved.
                            FilesSizes[i] = LettoreDAT.ReadUInt32(); // Saves the offset of the next file in order to calculate the size of the current file.

                            if (FilesSizes[i] == 0) // If the size of the next file is 0, it means that it's the very latest file.
                                FilesSizes[i] = (uint)FILEDAT.Length - OffsetFiles[i]; // So the file size will be obtained taking into account the end of the DAT.
                            else
                                FilesSizes[i] -= OffsetFiles[i]; // File size = starting point of the next file - starting point of the current file. 

                            EBOOT.Seek(pos, SeekOrigin.Begin); // Moves at the beginning of the offset of the filename.
                            uint Puntatore = LettoreEBOOT.ReadUInt32() + 0xC0; // Saves the offset of the filename.
                            pos = EBOOT.Position + 8; /* pos now points to the offset of the next filename.
                            The +8 is due to the fact that in the between an offset and the next there is some "unnecessary" data. */

                            EBOOT.Seek(Puntatore, SeekOrigin.Begin); // Move at the beginning of the filename.

                            // START Read and save the filename.
                            byte Fine = LettoreEBOOT.ReadByte();
                            while (Fine != 0)
                            {
                                FilesNames[i] += (char)Fine;
                                Fine = LettoreEBOOT.ReadByte();
                            } //END Read and save the filename.
                        }

                        Array.Sort(FilesNames, new AlphanumComparatorFast()); // Order filenames alphanumetically.
                    }
                    else // If the user is working with the demo files.
                    {
                        uint[] OffsetNamesfiles = new uint[AmountOfFiles];

                        // Reads from the eboot the offsets of the file names and body. It also reads the size of each file.
                        for (int i = 0; i < AmountOfFiles; i++)
                        {
                            OffsetNamesfiles[i] = LettoreEBOOT.ReadUInt32() + 0xC0;
                            OffsetFiles[i] = LettoreEBOOT.ReadUInt32();
                            FilesSizes[i] = LettoreEBOOT.ReadUInt32();
                        }

                        for (int i = 0; i < AmountOfFiles; i++)
                        {
                            EBOOT.Seek(OffsetNamesfiles[i], SeekOrigin.Begin); // Move at the beginning of the filename.

                            // START Read and save the filename.
                            byte Fine = LettoreEBOOT.ReadByte();
                            while (Fine != 0)
                            {
                                FilesNames[i] += (char)Fine;
                                Fine = LettoreEBOOT.ReadByte();
                            } //END Read and save the filename.
                        }
                    }

                    for (int i = 0; i < AmountOfFiles; i++) // Files extraction start now.
                    {
                        FILEDAT.Seek(OffsetFiles[i], SeekOrigin.Begin); // Move at the beginning of the body's file within the file.DAT.

                        byte[] CorpoDelFile = new byte[FilesSizes[i]];
                        FILEDAT.Read(CorpoDelFile, 0, CorpoDelFile.Length); // Save the body's file.

                        using (FileStream Extract = new FileStream(Path.Combine(DestinationDir, FilesNames[i]), FileMode.Create, FileAccess.Write))
                            Extract.Write(CorpoDelFile, 0, CorpoDelFile.Length); // Makes and saves the new file.
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e) // EXTRACT .LIN 
        {
            using (OpenFileDialog LIN = new OpenFileDialog())
            {
                LIN.Title = "Select one or more \"file.lin\"";
                LIN.Filter = ".lin|*.lin|All Files (*.*)|*.*";
                LIN.FileName = "*.lin";
                LIN.Multiselect = true;

                if (LIN.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SingleLIN in LIN.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\LIN\\" + Path.GetFileNameWithoutExtension(SingleLIN);

                        ExtractLIN(SingleLIN, DestinationDir);
                    }

                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ExtractLIN(string LINAddress, string DestinationDir)
        {
            // Open file.LIN in FileStream.
            using (FileStream LIN = new FileStream(LINAddress, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader BinReaderLIN = new BinaryReader(LIN))
                {
                    uint Nparti = BinReaderLIN.ReadUInt32(); // Number of parts that make up the LIN.

                    // If the LIN has of 1 or 2 parts, continue.
                    // If the LIN has just 1 file that means that there is not text, just bytecode.
                    if (Nparti == 0x02 || Nparti == 0x01)
                    {
                        uint HeaderSize = BinReaderLIN.ReadUInt32(),
                        SecondFileOffset = BinReaderLIN.ReadUInt32(), // The second file contains the text, the first file contains the bytecode.
                        LINSize = (uint)LIN.Length, // Calculate directly the size of the LIN instead of reading it from the file, this way the tool can work with LINs from PSP (PZ) as they are "incorrect".
                        NSentences = 0; // Senteces within the file. 

                        // If the beginning of the second file corresponds to the size of the file or equal to 0, than there are no sentences.
                        if (SecondFileOffset != LINSize && SecondFileOffset > 0)
                        {
                            LIN.Seek(SecondFileOffset, SeekOrigin.Begin);
                            NSentences = BinReaderLIN.ReadUInt32();
                        }

                        // If the file contains text or the user has nevertheless chosen to process files with no text, then continue.
                        if (NSentences > 0 || (NSentences <= 0 && ignoreLINWoTextToolStripMenuItem.Checked == false))
                        {
                            if (Directory.Exists(DestinationDir) == false)
                                Directory.CreateDirectory(DestinationDir);

                            LIN.Seek(HeaderSize, SeekOrigin.Begin); // Moves at the beginning of the first file, the bytecode, which is located immediately after the header.
                            byte[] FirstFileBody = new byte[SecondFileOffset - HeaderSize]; // Will contains the bytecode.

                            LIN.Read(FirstFileBody, 0, FirstFileBody.Length); // Insert the bytecode inside "FirstFileBody".

                            // File.bytecode's address.
                            string BytecodeAddress = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(LINAddress) + ".bytecode");

                            // Makes a file called "FileName.bytecode" e saves the "FirstFileBody" in it.
                            using (FileStream bytecode = new FileStream(BytecodeAddress, FileMode.Create, FileAccess.Write))
                                bytecode.Write(FirstFileBody, 0, FirstFileBody.Length);

                            if (NSentences > 0) // If there is text to extract, extract it.
                            {
                                byte[] SecondFileBody = new byte[LINSize - SecondFileOffset]; // Will contains all the text.

                                LIN.Read(SecondFileBody, 0, SecondFileBody.Length); // Insert the text inside "SecondFileBody".

                                string RawTexAddress = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(LINAddress) + ".rawtext"),
                                NewXMLAddress = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(LINAddress) + ".xml");

                                // Create a file called "TextRaw.bin" and inserts all the contents of "SecondFileBody" in it.
                                using (FileStream TextRaw = new FileStream(RawTexAddress, FileMode.Create, FileAccess.Write))
                                    TextRaw.Write(SecondFileBody, 0, SecondFileBody.Length);

                                /* If the user has checked the "HIDE SPEAKERS box", then pass "null".
                                Null = don't store and don't print the speakers. */

                                // As for now I don't know how the DRV3's bytecode works, extract the all V3 texts without speakrs.
                                if (hIDESPEAKERSToolStripMenuItem.Checked == true || tabControl1.SelectedTab.Text.Contains("DRV3"))
                                    MakeXML(TextExtractor(RawTexAddress), null, NewXMLAddress);
                                else
                                    MakeXML(TextExtractor(RawTexAddress), SpeakerExtractor(BytecodeAddress), NewXMLAddress);

                                // Deletes FileName.rawtext.
                                File.Delete(RawTexAddress);
                                while (File.Exists(RawTexAddress)) { }
                            }
                        }
                    }
                }
            }
        }

        // It extracts sentences from files without bytecode, therefore made only by text.
        private List<string> TextExtractor(string RawTextAddress)
        {
            List<string> Text = new List<string>();

            using (FileStream TEXTFILE = new FileStream(RawTextAddress, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader BinReaderText = new BinaryReader(TEXTFILE))
                {
                    int BOM = 2; /* Byte Order Mark (BOM): https://en.wikipedia.org/wiki/Byte_order_mark */

                    if (tabControl1.SelectedTab.Text.Contains("DRAE")) // "DRAE" doesn't use any BOM.
                        BOM = 0;

                    uint NPointers = BinReaderText.ReadUInt32(); // N# Pointers = N# sentenceses. 
                    uint[] Pointers = new uint[NPointers]; // It will contains all the pointers.

                    for (int i = 0; i < NPointers; i++)  // Reads and saves the pointers.
                        Pointers[i] = BinReaderText.ReadUInt32();

                    // START Reading sentences.
                    for (int i = 0; i < NPointers; i++)
                    { // Moves at the beginning of the sentence "i" and begins to read it.
                        TEXTFILE.Seek(Pointers[i] + BOM, SeekOrigin.Begin);

                        string Sentence = null;
                        ushort Letter = 1;

                        while (TEXTFILE.Position != TEXTFILE.Length)
                        {
                            Letter = BinReaderText.ReadUInt16();

                            if (Letter != 0)
                                Sentence += (char)Letter;
                            else
                                break;
                        }

                        // The text is cleaned from the PSP codes.
                        if (cLEANPSPCLTToolStripMenuItem.Checked == true && Sentence != null)
                            Sentence = CleanTextFromPSPCodes(Sentence);

                        //START Removes extra \n.
                        if (eraseExtraLinefeedsToolStripMenuItem.Checked == true && Sentence != null)
                        {
                            Sentence += "</TEMP>";
                            while (Sentence.IndexOf("\n</TEMP>") >= 0)
                                Sentence = Sentence.Replace("\n</TEMP>", "</TEMP>");

                            Sentence = Sentence.Replace("\n<CLT></TEMP>", "<CLT></TEMP>");
                            Sentence = Sentence.Replace("</TEMP>", null);
                        }
                        //END Removes extra \n.

                        Text.Add(Sentence);
                    }
                    // END Reading sentences.
                }
            }
            return Text;
        }

        private string CleanTextFromPSPCodes(string Sentence)
        {
            // START removing PZ codes for PSP.
            Sentence = Sentence.Replace("“", "\"");
            Sentence = Sentence.Replace("”", "\"");
            Sentence = Sentence.Replace("’", "'");

            int Position = 0, Flag = 1;

            string[] CLTToBeDeleted = new string[] { "<CLT 8>", "<CLT 12>", "<CLT 16>", "<CLT 21>" };
            int[] CLTSize = new int[] { 7, 8, 8, 8 };

            while (Flag != 0)
            {
                Flag = 0;

                for (int i = 0; i < CLTToBeDeleted.Length; i++)
                {
                    Position = Sentence.IndexOf(CLTToBeDeleted[i]);
                    if (Position >= 0)
                    {
                        Flag++;
                        Sentence = Sentence.Remove(Position, CLTSize[i]);
                        Position = Sentence.IndexOf("<CLT>", Position);
                        if (Position >= 0)
                        {
                            Sentence = Sentence.Remove(Position, 5);
                            Flag++;
                        }
                    }
                }
            }
            // END removing PZ codes for PSP.

            return Sentence;
        }

        // Read the data forum FileName.bytecode and saves the Speakers.
        private List<string> SpeakerExtractor(string BytecodeAddress)
        {
            using (FileStream FILECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader BinReaderSpeakers = new BinaryReader(FILECODE))
                {
                    List<string> Speakers = new List<string>();
                    List<byte> HexCodeSpeakers = new List<byte>();
                    string[] Names = null;

                    if (tabControl1.SelectedTab.Text.Contains("DR1")) // If the user is working with DR1.
                        Names = new string[] { "MAKOTO NAEGI", "KIYOTAKA ISHIMARU", "BYAKUYA TOGAMI", "MONDO OOWADA", "LEON KUWATA", "HIFUMI YAMADA", "YASUHIRO HAGAKURE", "SAYAKA MAIZONO", "KYOUKO KIRIGIRI", "AOI ASAHINA", "TOUKO FUKAWA", "SAKURA OOGAMI", "CELES", "JUNKO ENOSHIMA (MUKURO)", "CHIHIRO FUJISAKI", "MONOKUMA", "JUNKO ENOSHIMA (REAL)", "ALTER EGO", "GENOCIDER SHOU", "HEADMASTER", "NAEGI'S MOTHER", "NAEGI'S FATHER", "NAEGI'S SISTER", "ERROR", "ISHIDA", "DAIA OOWADA", "ERROR", "ERROR", "LAST SPRITE", "EMPTY SPEAKER", "NO NAME", "NARRATION", "CHOICE/RE:ACT", "USAMI", "MONOKUMA BACKUP", "MONOKUMA BACKUP (R)", "MONOKUMA BACKUP (L)", "MONOKUMA BACKUP (M)", "ERROR1" };

                    else if (tabControl1.SelectedTab.Text.Contains("DR2")) // If the user is working with DR2.
                        Names = new string[] { "HAJIME HINATA", "NAGITO KOMAEDA", "BYAKUYA TOGAMI", "GUNDAM TANAKA", "KAZUICHI SOUDA", "TERUTERU HANAMURA", "NEKOMARU NIDAI", "FUYUHIKO KUZURYUU", "AKANE OWARI", "CHIAKI NANAMI", "SONIA NEVERMIND", "HIYOKO SAIONJI", "MAHIRU KOIZUMI", "MIKAN TSUMIKI", "IBUKI MIODA", "PEKO PEKOYAMA", "MONOKUMA", "MONOMI", "JUNKO ENOSHIMA", "MECHA NIDAI", "MAKOTO NAEGI", "KYOUKO KIRIGIRI", "BYAKUYA TOGAMI (REAL)", "HANAMURA'S MOM", "ALTER EGO", "MINI NIDAI", "MONOKUMA MONOMI", "NARRATION", "ERROR", "EMPTY SPEAKER", "ERROR", "ERROR", "CHOICE/RE:ACT", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "USAMI", "KIRAKIRA", "NO NAME", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "JUNKO ENOSHIMA 2", "ERROR", "GIRL A", "GIRL B", "GIRL C", "GIRL D", "GIRL E", "BOY F", "NO NAME 2", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "LAST SPRITE", "EMPTY SPEAKER", "ERROR1" };

                    else if (tabControl1.SelectedTab.Text.Contains("DRAE")) // If the user is working with DRAE.
                        Names = new string[] { "KOMARU NAEGI", "TOUKO FUKAWA", "GENOCIDER SHOU", "MASARU DAIMON", "JATARO KEMURI", "KOTOKO UTSUGI", "NAGISA SHINGETSU", "MONACA", "SERVANT", "KUROKUMA", "HAIJI TOUWA", "TOUICHI TOUWA", "SHIROKUMA", "YUUTA ASAHINA", "HIROKO HAGAKURE", "???", "MAKOTO NAEGI", "BYAKUYA TOGAMI", "MONOKUMA KID", "MONOKUMA KID", "FUTURE FONDATION", "FUTURE FONDATION A", "FUTURE FONDATION B", "FUTURE FONDATION C", "FUTURE FONDATION D", "FUTURE FONDATION E", "FUTURE FONDATION F", "ADULT", "ADULT A", "ADULT B", "ADULT C", "ADULT D", "ADULT E", "ADULT F", "ADULT G", "ADULT H", "ADULT I", "ADULT J", "ADULT K", "ADULT L", "ADULT M", "ADULT N", "ADULT O", "ADULT P", "ADULT Q", "ADULT R", "ADULT S", "ADULT T", "ADULT U", "ADULT V", "ADULT W", "ADULT X", "ADULT Y", "ADULT Z", "ADULTS", "ADULT", "ADULT", "DUMMY 1", "DUMMY 2", "DUMMY 3", "DUMMY 4", "DUMMY 5", "DUMMY 6", "DUMMY 7", "DUMMY 8", "DUMMY 9", "DUMMY 10", "DUMMY 11", "DUMMY 12", "JUNKO ENOSHIMA", "WARRIOS OF HOPE", "KID SHOP MANAGER", "EMPTY SPEAKER", "???", "???", "ERROR", "SYSTEM/CHOICE", "ERROR1" };

                    // START reading names from FileName.bytecode. 

                    BinReaderSpeakers.ReadInt32(); /* The first two bytes are usually "70 00", 70 stands for "beginning of an instruction", while "00" stands for "header".
                                                   The third and fourth bytes indicate the amount of Speakers. */
                    int TempVar = 0; // Variable that momentarily will contain the values read each time.
                    byte SpeakerIndex = 0x1E; // Will contains the code of the SpeakerIndex. (0x1E == NO NAME --> DR1 and DR2 ONLY)
                    byte LastSprite = 0; // Will contains the code of the last sprite appeared.
                    byte ChoiceCode = 0x2B; // (DR1 ONLY)
                    byte LastSpriteCode = 0x1C; // (DR1 ONLY)
                    byte NarrationIndex = 31; // (DR1 ONLY)

                    // If the user is NOT working with DR AE.
                    if (!tabControl1.SelectedTab.Text.Contains("DRAE"))
                    {
                        if (tabControl1.SelectedTab.Text.Contains("DR2")) // If the user is working with DR2.
                        {
                            ChoiceCode = 0x32; // (DR2 ONLY)
                            LastSpriteCode = 0x3E; // (DR2 ONLY)
                            NarrationIndex = 27; // (DR2 ONLY)
                        }

                        while (FILECODE.Position != FILECODE.Length)
                        {
                            TempVar = BinReaderSpeakers.ReadByte();

                            // These three lines are used for debbuging.
                            // int dfdsfds = 0;
                            // if (HexCodeSpeakers.Count == 45)
                            // dfdsfds = 0;

                            if (TempVar == 0x70) // If TempVar == "beginning of an instruction"... 
                            {
                                TempVar = BinReaderSpeakers.ReadByte();
                                if (TempVar == 0x21 || TempVar == 0x1E) // If the HexCode indicates a "Speaker" or a "Sprite".
                                {
                                    SpeakerIndex = BinReaderSpeakers.ReadByte();

                                    if (TempVar == 0x1E)  // Saves the HexCode in SpeakerIndex and LastSprite.
                                        LastSprite = SpeakerIndex = BinReaderSpeakers.ReadByte();

                                    // 0x1C == "show the name of the last character displayed as sprites".
                                    else if (SpeakerIndex == LastSpriteCode)
                                        SpeakerIndex = LastSprite;

                                    else if (SpeakerIndex == 0x43)
                                        SpeakerIndex = NarrationIndex;
                                }
                                else if (TempVar == ChoiceCode) // If the dialogue is actually a choice.
                                {
                                    SpeakerIndex = 0x20; // Then save it as a choice.
                                    BinReaderSpeakers.ReadByte(); // Jump the extra code to avoid trouble.
                                }
                                // If the dialogue really doesn't has a speaker.
                                else if (TempVar == 0x03)
                                {
                                    TempVar = BinReaderSpeakers.ReadByte(); // Saves the CLT index. CLT == Cleaner. 

                                    //     if (TempVar == 0x17 || TempVar == 0x03) // 0x17 == "Remove speaker tag".
                                    //     SpeakerIndex = 0x1D; // 0x1D == "NO SPEAKER".

                                    //     else if (TempVar == 0x04 || TempVar == 0x00)
                                    //             SpeakerIndex = 0x00; // 0x00 == "Naegi/Hinata".
                                }
                                else if (TempVar == 0x02) // 0x02 == Print dialogue on screen.
                                    HexCodeSpeakers.Add(SpeakerIndex); // Save speaker index.
                            }
                        }
                    }
                    else  // If the user IS working with DR AE.
                    {
                        SpeakerIndex = 72;
                        byte SpeakerCode = 0x15;
                        byte PrintSentenceCode = 0x01;
                        int Narration = 0;

                        /* The bytcode doesn't seem to follow the same logic in each game file,
                        that's why first we need to check what kind of bytecode it. */
                        while (FILECODE.Position != FILECODE.Length && Narration == 0)
                        {
                            TempVar = BinReaderSpeakers.ReadByte();
                            if (TempVar == 0x70)
                            {
                                TempVar = BinReaderSpeakers.ReadByte();
                                if (TempVar == 0x15) // If this bytecode use "0x15" to mark the speakers...
                                    Narration = 1;
                            }
                        }

                        FILECODE.Seek(0x4, SeekOrigin.Begin);
                        int FileType = BinReaderSpeakers.ReadInt32();

                        if (Narration == 0 || FileType == 0x00011670 || FileType == 0x1F003670 || FileType == 0x04003370)
                            SpeakerCode = 0x07;

                        while (FILECODE.Position != FILECODE.Length)
                        {
                            TempVar = BinReaderSpeakers.ReadByte();
                            if (TempVar == 0x70)
                            {
                                TempVar = BinReaderSpeakers.ReadByte();

                                if (TempVar == SpeakerCode) // If the dialogue has a speaker.
                                    SpeakerIndex = BinReaderSpeakers.ReadByte();
                                else if (TempVar == 0x39) // If the dialogue is a System message or a Choice.
                                    SpeakerIndex = 76;
                                else if (TempVar == PrintSentenceCode) // PrintSentenceCode == Print dialogue on screen.
                                    HexCodeSpeakers.Add(SpeakerIndex); // Save speaker index.
                            }
                        }
                    }

                    for (int i = 0; i < HexCodeSpeakers.Count; i++) // Save the Names inside "Speakers".
                    {
                        int indexNames = HexCodeSpeakers[i];

                        if (indexNames >= Names.Length) // If indexNames >= Names.Length, then Speaker == "ERROR1".
                            indexNames = Names.Length - 1;

                        Speakers.Add(Names[indexNames]);
                    }
                    return Speakers;
                }
            }
        }

        private void MakeXML(List<string> TextsExtracted, List<string> SpeakersExtracted, string NewXMLAddress)
        {
            if (Directory.Exists(Path.GetDirectoryName(NewXMLAddress)) == false)
                Directory.CreateDirectory(Path.GetDirectoryName(NewXMLAddress));

            List<string> JAPText = null;

            // If the user has checked "Add japanase texts", then extract the japanese text. 
            if (aDDJAPANESETEXTToolStripMenuItem.Checked == true)
            {
                string CartellaCoiTestiJap = null;
                if (tabControl1.SelectedTab.Text.Contains("DR1 (PSP)") || tabControl1.SelectedTab.Text.Contains("DR1 (PSVITA)"))
                    CartellaCoiTestiJap = "DR1PSVITA";
                if (tabControl1.SelectedTab.Text.Contains("DR1 (PC)"))
                    CartellaCoiTestiJap = "DR1PC";
                if (tabControl1.SelectedTab.Text.Contains("DR2 (PSVITA)"))
                    CartellaCoiTestiJap = "DR2PSVITA";
                if (tabControl1.SelectedTab.Text.Contains("DR2 (PC)"))
                    CartellaCoiTestiJap = "DR2PC";
                if (tabControl1.SelectedTab.Text.Contains("DRAE (PSVITA)"))
                    CartellaCoiTestiJap = "DRAEPSVITA";
                if (tabControl1.SelectedTab.Text.Contains("DRAE (PC)"))
                    CartellaCoiTestiJap = "DRAEPC";
                if (tabControl1.SelectedTab.Text.Contains("DRV3 DEMO (PSVITA)"))
                    CartellaCoiTestiJap = "DRV3DEMOPSVITA";
                if (tabControl1.SelectedTab.Text.Contains("DRV3 (PSVITA)"))
                    CartellaCoiTestiJap = "DRV3PSVITA";
                if (tabControl1.SelectedTab.Text.Contains("DRV3 (PC)"))
                    CartellaCoiTestiJap = "DRV3PC";

                if (File.Exists(Path.Combine("Ext\\JapText", CartellaCoiTestiJap, Path.GetFileNameWithoutExtension(NewXMLAddress) + ".rawtext")) == true)
                    JAPText = TextExtractor(Path.Combine("Ext\\JapText", CartellaCoiTestiJap, Path.GetFileNameWithoutExtension(NewXMLAddress) + ".rawtext"));
            }

            using (FileStream NuovoXML = new FileStream(NewXMLAddress, FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter BinXML = new BinaryWriter(NuovoXML), tx = new BinaryWriter(NuovoXML, Encoding.Unicode))
                {
                    BinXML.Write((UInt16)0xFEFF); //BOM

                    for (int i = 0; i < TextsExtracted.Count; i++)
                    {
                        // Print the "Speaker".
                        if (hIDESPEAKERSToolStripMenuItem.Checked == false && SpeakersExtracted != null)
                        {
                            if (i < SpeakersExtracted.Count)
                                tx.Write(("<SPEAKER N°" + (i + 1).ToString("D3") + ">" + SpeakersExtracted[i] + "</SPEAKER N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                            else
                                tx.Write(("<SPEAKER N°" + (i + 1).ToString("D3") + ">ERROR</SPEAKER N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                        }

                        // Print the original sentence.
                        tx.Write(("<ORIGINAL N°" + (i + 1).ToString("D3") + ">\n" + TextsExtracted[i] + "\n</ORIGINAL N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());

                        // Print the japanese sentence.
                        if (aDDJAPANESETEXTToolStripMenuItem.Checked == true && JAPText != null)
                        {
                            if (i < JAPText.Count)
                                tx.Write(("<JAPANESE N°" + (i + 1).ToString("D3") + ">\n" + JAPText[i] + "\n</JAPANESE N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                            else
                                tx.Write(("<JAPANESE N°" + (i + 1).ToString("D3") + ">\nNOT FOUND\n</JAPANESE N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                        }

                        tx.Write(("<TRANSLATED N°" + (i + 1).ToString("D3") + ">\n\n</TRANSLATED N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                        tx.Write(("<COMMENT N°" + (i + 1).ToString("D3") + ">\n\n</COMMENT N°" + (i + 1).ToString("D3") + ">\n------------------------------------------------------------\n").ToCharArray());
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e) // REPACK .LIN 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\LIN";

            // If "EXTRACTED LIN" exists and it's not empty.
            if (Directory.Exists(OriginalDir) && Directory.EnumerateDirectories(OriginalDir).Any() == true)
            {
                label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                label5.Refresh(); // Refresh the Status label.

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\LIN";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                    Directory.CreateDirectory(DestinationDir);

                // For each LIN folder to be repacked.
                foreach (string DirLinToBeRepacked in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                    // Continue only if there is a text file or if the user has chosen to work with files composed only by the bytecode too.
                    if (File.Exists(Path.Combine(DirLinToBeRepacked, Path.GetFileNameWithoutExtension(DirLinToBeRepacked) + ".xml")) == true
                        || (File.Exists(Path.Combine(DirLinToBeRepacked, Path.GetFileNameWithoutExtension(DirLinToBeRepacked) + ".bytecode")) == true
                        && File.Exists(Path.Combine(DirLinToBeRepacked, Path.GetFileNameWithoutExtension(DirLinToBeRepacked) + ".xml")) == false
                        && ignoreLINWoTextToolStripMenuItem.Checked == false))
                        RePackText(DirLinToBeRepacked, DestinationDir);

                label5.Text = "Ready!"; // Change the "Status" to "Ready!".
                MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RePackText(string DirTextToBeRepacked, string DestinationDir)
        {
            string NewFileExtension = ".pak",
            BytecodeAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".bytecode");

            // If there is a ".bytecode" file, it means it's a ".LIN" file.
            if (File.Exists(BytecodeAddress) == true)
                NewFileExtension = ".lin";

            List<string> TranslatedSentences = null;

            uint NParts = 0x02,
                HeaderSize = 0x10;

            string XMLAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".xml");

            // Checkout if there is a text file and consequently extracts the sentences.
            if (File.Exists(XMLAddress) == true)
            {
                /* It extracts all the phrases translated from the XML and stores them in "TranslatedSentences".
                 XMLStreamReader.ReadToEnd () = Read all the text within the XML and stores it in a string. */
                using (FileStream TRANSLATEDXML = new FileStream(XMLAddress, FileMode.Open, FileAccess.Read))
                using (StreamReader XMLStreamReader = new StreamReader(TRANSLATEDXML, Encoding.Unicode))
                    TranslatedSentences = TextBetweenTAGsReader(XMLStreamReader.ReadToEnd(), "<TRANSLATED N°", "</TRANSLATED N°");
            }
            // If there are no text files, and you the user is working on a different game from DR1.
            else if (File.Exists(XMLAddress) == false && !tabControl1.SelectedTab.Text.Contains("DR1"))
            {
                NParts = 0x01;
                HeaderSize = 0x0C;
            }

            using (FileStream REPACKEDFILE = new FileStream(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + NewFileExtension), FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter LINBinaryWriter = new BinaryWriter(REPACKEDFILE), LINBinUnicode = new BinaryWriter(REPACKEDFILE, Encoding.Unicode))
                {
                    int FileSizePos = 0;

                    // START INSERTING BYCODE - ONLY IF IT'S A LIN.
                    if (NewFileExtension == ".lin") // If it's a "LIN" that means there is a ".bytecode" file.
                    {
                        LINBinaryWriter.Write(NParts);
                        LINBinaryWriter.Write(HeaderSize);

                        using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
                        {
                            /* If the header sizes is equal to 0x10 that means that are two files
                            and therefore we must write the offset of the second file. */
                            if (HeaderSize == 0x10)
                                LINBinaryWriter.Write((uint)(HeaderSize + BYTECODE.Length));

                            FileSizePos = (int)REPACKEDFILE.Position;
                            LINBinaryWriter.Write((uint)(0x0));
                            BYTECODE.CopyTo(REPACKEDFILE, (int)HeaderSize);
                        }
                    }
                    // END INSERTING BYCODE - ONLY IF IT'S A LIN.

                    // START INSERTING TEXT - Only if there is text to be processed.
                    if (File.Exists(XMLAddress) == true)
                    {
                        // "SentencesOffset" will contain the offset of each phrase.
                        uint[] SentencesOffset = new uint[TranslatedSentences.Count + 1];
                        byte Padding = 0x04; // DR1 and AE padding is 4.

                        if (tabControl1.SelectedTab.Text.Contains("DR2")) // If the user is working on DR2.
                            Padding = 0x02; // DR2 padding is 2.

                        // Write down the n# of sentences.
                        LINBinaryWriter.Write((uint)TranslatedSentences.Count);

                        // Stores the current position so that we can come back later and enter the correct offsets.
                        int pos = (int)REPACKEDFILE.Position;

                        // Fills the pointers area with zeros. At the end of the process the area will be overwritten with the correct data.
                        for (int i = 0; i < SentencesOffset.Length; i++)
                            LINBinaryWriter.Write((uint)0x00);

                        /* The "- ((uint) pos - 4)" is due to the fact that the offsets do not take into account everything that
                        is before the number of sentences, ergo the bytecode and the first 0x10. */
                        SentencesOffset[0] = (uint)REPACKEDFILE.Position - ((uint)pos - 4);

                        for (int i = 0; i < TranslatedSentences.Count; i++)
                        {
                            // If the user is NOT working on AE, then write down the BOM.
                            if (!tabControl1.SelectedTab.Text.Contains("DRAE"))
                                LINBinaryWriter.Write((ushort)0xFEFF);

                            // Write the sentence n# [i] in the repacked file.
                            LINBinUnicode.Write(TranslatedSentences[i].ToCharArray());

                            // Write down the null string terminator.
                            LINBinaryWriter.Write((ushort)0x00);

                            // If it's a "PAK" that means that we must padding.
                            if (NewFileExtension == ".pak")
                            {
                                if (REPACKEDFILE.Position % 0x04 != 0)
                                    while (REPACKEDFILE.Position % 0x04 != 0)
                                        LINBinaryWriter.Write((byte)0x0);

                                SentencesOffset[i + 1] = (uint)REPACKEDFILE.Position;
                            }
                            else
                                SentencesOffset[i + 1] = (uint)REPACKEDFILE.Position - ((uint)pos - 4);
                        }

                        // Padding at the end of the file.
                        if (REPACKEDFILE.Position % Padding != 0)
                            while (REPACKEDFILE.Position % Padding != 0)
                                LINBinaryWriter.Write((byte)0x0);

                        // Comes back in the area dedicated to the offsets and overwrites all the zeros with the correct offsets.
                        LINBinaryWriter.Seek(pos, SeekOrigin.Begin);
                        for (int i = 0; i < SentencesOffset.Length; i++)
                            LINBinaryWriter.Write(SentencesOffset[i]);
                    }
                    // END INSERTING TEXT - Only if there is text to be processed.

                    // If it's a "LIN", returns to file beginning and writes the exact size of the LIN.
                    if (NewFileExtension == ".lin")
                    {
                        /* If there insn't a text file and the header size is equal to 0x10,
                        it means that this is a file containing only a bytecode. */
                        if (File.Exists(XMLAddress) == false && HeaderSize == 0x10)
                        {
                            LINBinaryWriter.Write((uint)0x00);
                            LINBinaryWriter.Write((uint)0x08);
                        }

                        LINBinaryWriter.Seek(FileSizePos, SeekOrigin.Begin);
                        LINBinaryWriter.Write((uint)REPACKEDFILE.Length);
                    }
                }
            }
        }

        // XmlText contains all the XML's text.
        private List<string> TextBetweenTAGsReader(string XmlText, string OpeningTagTEMP, string EndingTagTEMP)
        {
            int OffsetOpTAG = 0; // Stores the location where the opening TAG begins.
            int OffsetEdTAG = 0; // Stores the location where the ending TAG begins.
            int SentencesN = 1; // Index to collect each sentence by its number: (ES: <SPEAKER N°015>???</SPEAKER N°015>)
            string OpTag = null; // It will contain the opening tag, this way it will be easier for us to know its total length.
            string EdTag = null; // It will contain the ending tag, this way it will be easier for us to know its total length.
            List<string> Text = new List<string>();

            while (OffsetOpTAG != -1 || OffsetEdTAG != -1)
            {
                OpTag = OpeningTagTEMP + SentencesN.ToString("D3") + ">";
                EdTag = EndingTagTEMP + SentencesN.ToString("D3") + ">";

                OffsetOpTAG = XmlText.IndexOf(OpTag); // Saves the opening TAG location.
                OffsetEdTAG = XmlText.IndexOf(EdTag); // Saves the ending TAG location.

                if (OffsetOpTAG >= 0 && OffsetEdTAG >= 0)
                {
                    int SentenceSize = (OffsetEdTAG + EdTag.Length) - OffsetOpTAG;

                    char[] temp = new char[SentenceSize];

                    /* Copy the phrase chosen within TEMP.
                    (Start point of reading, variable where the phrase will be saved, start point of writing, sentences size. */
                    XmlText.CopyTo(OffsetOpTAG, temp, 0, SentenceSize);

                    // TEMP2 allows us to easily remove the new lines and the tags before store them permanently.
                    string temp2 = new string(temp);

                    // Remove the new lines created by the DRAT while unpacking to xml.
                    temp2 = temp2.Replace(OpTag + "\n", OpTag);
                    temp2 = temp2.Replace("\n" + EdTag, EdTag);

                    //START deleting extra new lines.
                    if (eraseExtraLinefeedsToolStripMenuItem.Checked == true)
                    {
                        while (temp2.IndexOf(OpTag + "\n") >= 0)
                            temp2 = temp2.Replace(OpTag + "\n", OpTag);

                        while (temp2.IndexOf("\n" + EdTag) >= 0)
                            temp2 = temp2.Replace("\n" + EdTag, EdTag);
                    }
                    //END deleting extra new lines.

                    // Removes the TAGs from the sentence.
                    temp2 = temp2.Replace(OpTag, null);
                    temp2 = temp2.Replace(EdTag, null);

                    if (temp2.Length == 0 || temp2 == "\n\n") // If temp2 == it's empty, then read the original sentence.
                    {
                        temp = null;
                        temp2 = null;

                        // Replaces "TRANSLATED" with "ORIGINAL" in order to pick the original sentence.
                        OpTag = OpTag.Replace("TRANSLATED", "ORIGINAL");
                        EdTag = EdTag.Replace("TRANSLATED", "ORIGINAL");

                        OffsetOpTAG = XmlText.IndexOf(OpTag); // Saves the opening TAG location.
                        OffsetEdTAG = XmlText.IndexOf(EdTag); // Saves the ending TAG location.

                        if (OffsetOpTAG >= 0 && OffsetEdTAG >= 0)
                        {
                            SentenceSize = (OffsetEdTAG + EdTag.Length) - OffsetOpTAG;

                            temp = new char[SentenceSize];

                            /* Copy the phrase chosen within TEMP.
                            (Start point of reading, variable where the phrase will be saved, start point of writing, sentences size. */
                            XmlText.CopyTo(OffsetOpTAG, temp, 0, SentenceSize);

                            temp2 = new string(temp);

                            // Remove the new lines created by the DRAT while unpacking to xml.
                            temp2 = temp2.Replace(OpTag + "\n", OpTag);
                            temp2 = temp2.Replace("\n" + EdTag, EdTag);

                            //START deleting extra new lines.
                            if (eraseExtraLinefeedsToolStripMenuItem.Checked == true)
                            {
                                while (temp2.IndexOf(OpTag + "\n") >= 0)
                                    temp2 = temp2.Replace(OpTag + "\n", OpTag);

                                while (temp2.IndexOf("\n" + EdTag) >= 0)
                                    temp2 = temp2.Replace("\n" + EdTag, EdTag);
                            }
                            //END deleting extra new lines.

                            // Removes the TAGs from the sentence.
                            temp2 = temp2.Replace(OpTag, null);
                            temp2 = temp2.Replace(EdTag, null);
                        }
                    }
                    Text.Add(temp2); // And finally saves the cleaned sentence.
                }
                SentencesN++; // Index increment.
            }
            return Text; // Return all the sentences.
        }

        private void button9_Click(object sender, EventArgs e) // EXTRACT TEXT PAK TYPE 1 ".PAK" --> Simple text
        {
            using (OpenFileDialog PAK = new OpenFileDialog())
            {
                PAK.Title = "Select one or more \"file.pak\"";
                PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PAK.FileName = "*.pak";
                PAK.Multiselect = true;

                if (PAK.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SinglePAK in PAK.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 1\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        MakeXML(TextExtractor(SinglePAK), null, Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SinglePAK) + ".xml"));
                    }

                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e) // REPACK TEXT PAK TYPE 1 ".PAK" --> Simple text 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 1";

            // If "EXTRACTED TEXT PAK TYPE 1" exists and it's not empty.
            if (Directory.Exists(OriginalDir) && Directory.EnumerateDirectories(OriginalDir).Any() == true)
            {
                label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                label5.Refresh(); // Refresh the Status label.

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXT PAK TYPE 1";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                    Directory.CreateDirectory(DestinationDir);

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                    RePackText(PakDirToBeRepacked, DestinationDir);

                label5.Text = "Ready!"; // Change the "Status" to "Ready!".
                MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button11_Click(object sender, EventArgs e) // EXTRACT TEXT PAK TYPE 2 ".PAK" --> LIN inside a PAK 
        {
            using (OpenFileDialog PAK = new OpenFileDialog())
            {
                PAK.Title = "Select one or more \"file.pak\"";
                PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PAK.FileName = "*.pak";
                PAK.Multiselect = true;

                if (PAK.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SinglePAK in PAK.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 2\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        // 0 = Don't convert the images, leave them like they are.
                        ExtractPAK(SinglePAK, DestinationDir, 0);

                        // If the folder exists it means that contains the files texts, then search for ".lin"s and extract their texts.
                        if (Directory.Exists(DestinationDir) == true)
                            foreach (string SingleLIN in Directory.GetFiles(DestinationDir, "*.lin", SearchOption.TopDirectoryOnly))
                            {
                                ExtractLIN(SingleLIN, Path.Combine(Path.GetDirectoryName(SingleLIN), Path.GetFileNameWithoutExtension(SingleLIN)));

                                // Remove the LIN after having extracted the text.
                                if ((Directory.Exists(Path.Combine(Path.GetDirectoryName(SingleLIN), Path.GetFileNameWithoutExtension(SingleLIN))) == true))
                                {
                                    File.Delete(SingleLIN);
                                    while (File.Exists(SingleLIN)) { }
                                }
                            }
                    }
                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button12_Click(object sender, EventArgs e) // REPACK TEXT PAK TYPE 2 ".PAK" --> LIN inside a PAK 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 2",
                TEMPFolder = "TEMP001";

            // If "EXTRACTED PAK" exists and it's not empty.
            if (Directory.Exists(OriginalDir) && Directory.EnumerateDirectories(OriginalDir).Any() == true)
            {
                label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                label5.Refresh(); // Refresh the Status label.

                // 0 = Don't convert the images, leave them like they are.
                CloneDirectory(OriginalDir, TEMPFolder, "TEXT", 0);

                // For each LIN folder to be repacked.
                foreach (string DirsWithinTEMPFolder in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    foreach (string DirLinToBeRepacked in Directory.GetDirectories(DirsWithinTEMPFolder, "*", SearchOption.TopDirectoryOnly))
                        RePackText(DirLinToBeRepacked, DirsWithinTEMPFolder);

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXT PAK TYPE 2";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                    Directory.CreateDirectory(DestinationDir);

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    RePackPAK(PakDirToBeRepacked, DestinationDir, 0);

                // Remove the temp folder as it is no longer needed.
                Directory.Delete(TEMPFolder, true);
                while (Directory.Exists(TEMPFolder)) { }

                label5.Text = "Ready!"; // Change the "Status" to "Ready!".
                MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button13_Click(object sender, EventArgs e) // EXTRACT TEXT PAK TYPE 3 ".PAK" --> PAK inside a PAK 
        {
            using (OpenFileDialog PAK = new OpenFileDialog())
            {
                PAK.Title = "Select one or more \"file.pak\"";
                PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PAK.FileName = "*.pak";
                PAK.Multiselect = true;

                if (PAK.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SinglePAK in PAK.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 3\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        // 0 = Don't convert the images, leave them like they are.
                        ExtractPAK(SinglePAK, DestinationDir, 0);

                        // If the folder exists it means that contains the files texts, then search for ".pak"s and extract their texts.
                        if (Directory.Exists(DestinationDir) == true)
                            foreach (string SottoPAK in Directory.GetFiles(DestinationDir, "*.pak", SearchOption.TopDirectoryOnly))
                            {
                                // Extract the text from each Pak.
                                MakeXML(TextExtractor(SottoPAK), null, Path.Combine(Path.GetDirectoryName(SottoPAK), Path.GetFileNameWithoutExtension(SottoPAK), Path.GetFileNameWithoutExtension(SottoPAK) + ".xml"));

                                // Remove the PAK after having extracted the text.
                                File.Delete(SottoPAK);
                                while (File.Exists(SottoPAK)) { }
                            }
                    }
                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button14_Click(object sender, EventArgs e) // REPACK TEXT PAK TYPE 3 ".PAK" --> PAK inside a PAK 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 3",
           TEMPFolder = "TEMP001";

            // If "EXTRACTED PAK" exists and it's not empty.
            if (Directory.Exists(OriginalDir) && Directory.EnumerateDirectories(OriginalDir).Any() == true)
            {
                label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                label5.Refresh(); // Refresh the Status label.

                // 0 = Don't convert the images, leave them like they are.
                CloneDirectory(OriginalDir, TEMPFolder, "TEXT", 0);

                // For each LIN folder to be repacked.
                foreach (string DirsWithinTEMPFolder in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    foreach (string DirLinToBeRepacked in Directory.GetDirectories(DirsWithinTEMPFolder, "*", SearchOption.TopDirectoryOnly))
                        RePackText(DirLinToBeRepacked, DirsWithinTEMPFolder);

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXT PAK TYPE 3";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                    Directory.CreateDirectory(DestinationDir);

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    RePackPAK(PakDirToBeRepacked, DestinationDir, 0);

                // Remove the temp folder as it is no longer needed.
                Directory.Delete(TEMPFolder, true);
                while (Directory.Exists(TEMPFolder)) { }

                label5.Text = "Ready!"; // Change the "Status" to "Ready!".
                MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button15_Click(object sender, EventArgs e) // EXTRACT TEXTURE ".PAK" W/O CONVERT 
        {
            using (OpenFileDialog PAK = new OpenFileDialog())
            {
                PAK.Title = "Select one or more \"file.pak\"";
                PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PAK.FileName = "*.pak";
                PAK.Multiselect = true;

                if (PAK.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SinglePAK in PAK.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXTURE PAK (W-O CONVERT)\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        // 0 = Don't convert the images, leave them like they are.
                        ExtractPAK(SinglePAK, DestinationDir, 0);
                    }

                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // If ToPNG == 1 then convert the images.
        private void ExtractPAK(string PAKFileAddress, string DestinationDir, int ToPNG)
        {
            if (Directory.Exists(DestinationDir) == false)
                Directory.CreateDirectory(DestinationDir);

            /* If it's one of these two files.pak, then we have to decompress it.
            These two file.pak are present in DRAE and are compressed only in the PSVITA version. */
            if (Path.GetFileName(PAKFileAddress) == "file_img_ehon.pak" || Path.GetFileName(PAKFileAddress) == "file_img_kill.pak")
            {
                using (FileStream FilePAK = new FileStream(PAKFileAddress, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader PAKBinReader = new BinaryReader(FilePAK))
                    {
                        // Checks out if the file.pak is compressed.
                        if (PAKBinReader.ReadUInt32() == 0xA755AAFC)
                        {
                            // Remove any file.dec previously created.
                            if (File.Exists(PAKFileAddress + ".dec"))
                            {
                                File.Delete(PAKFileAddress + ".dec");
                                while (File.Exists(PAKFileAddress)) { }
                            }

                            // Uses Scalet to decompress the file.pak.
                            UseEXEToConvert("Ext\\ScarletTestApp.exe", "\"" + PAKFileAddress + "\" --output \"" + Path.GetDirectoryName(PAKFileAddress) + "\"");

                            // The file.pak on which we will work is now the file.dec generated by Scarlet.
                            PAKFileAddress += ".dec";
                        }
                    }
                }
            }

            // Start extracting the files contained within the file.pak.
            using (FileStream FilePAK = new FileStream(PAKFileAddress, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader PAKBinReader = new BinaryReader(FilePAK))
                {
                    // Stores the number of files in the PAK.
                    uint AmountOfFiles = PAKBinReader.ReadUInt32();

                    // Start a For to extract all the files in the PAK.
                    for (int i = 0; i < AmountOfFiles; i++)
                    {
                        // In "Offset1" is stored the offset to the file we are going to extract.
                        uint FileToBeExtractedSize = 0, Offset1 = PAKBinReader.ReadUInt32();

                        // Stores in "pos" the current position in order to read later next offset.
                        long pos = FilePAK.Position;

                        // If this is not the last file...
                        if (i != AmountOfFiles - 1)
                        {
                            uint Offset2 = PAKBinReader.ReadUInt32();
                            FileToBeExtractedSize = Offset2 - Offset1;
                        }
                        else // If this IS the last file...
                            FileToBeExtractedSize = (uint)(FilePAK.Length - Offset1);

                        // Move at the beginning of the file to be extracted.
                        FilePAK.Seek(Offset1, SeekOrigin.Begin);

                        // Read the file and stores it in "BodyFile".
                        byte[] BodyFile = new byte[FileToBeExtractedSize];
                        FilePAK.Read(BodyFile, 0, BodyFile.Length);

                        string NewFileExtension = null;

                        if (BodyFile[0] == 0xF0 && BodyFile[1] == 0x30 && BodyFile[2] == 0x60 && BodyFile[3] == 0x90)
                            Array.Copy(BodyFile, 4, BodyFile, 0, BodyFile.Length - 4);
                        // Check the beginning of the file with the purpose of discovering its extension.
                        else if (BodyFile[0] == 0xFC && BodyFile[1] == 0xAA && BodyFile[2] == 0x55 && BodyFile[3] == 0xA7)
                        {
                            // The ".cmp" files are compressed files for PSVITA.
                            if (BodyFile[13] == 0x47 && BodyFile[14] == 0x58 && BodyFile[15] == 0x54 && BodyFile[16] == 0x00)
                                // ".gxt" is an image format used for PSVITA.
                                NewFileExtension = ".cmp.gxt";
                            else if (BodyFile[13] == 0x53 && BodyFile[14] == 0x48 && BodyFile[15] == 0x54 && BodyFile[16] == 0x58 && BodyFile[17] == 0x46 && BodyFile[18] == 0x53)
                                // ".shtxfs" is an image format used for PSVITA.
                                NewFileExtension = ".cmp.shtxfs.btx";
                            else if (BodyFile[13] == 0x53 && BodyFile[14] == 0x48 && BodyFile[15] == 0x54 && BodyFile[16] == 0x58)
                                // ".shtx" is an image format used for PSVITA.
                                NewFileExtension = ".cmp.shtx.btx";
                            else
                                NewFileExtension = ".cmp";
                        }
                        else if (BodyFile[0] == 0x47 && BodyFile[1] == 0x58 && BodyFile[2] == 0x33 && BodyFile[3] == 0x00)
                        {
                            // ".gx3" files are doubly compressed files for PSVITA.
                            if (BodyFile[13] == 0x47 && BodyFile[14] == 0x58 && BodyFile[15] == 0x54 && BodyFile[16] == 0x00)
                                // ".gxt" is an image format used for PSVITA.
                                NewFileExtension = ".gx3.gxt";
                            else if (BodyFile[13] == 0x53 && BodyFile[14] == 0x48 && BodyFile[15] == 0x54 && BodyFile[16] == 0x58 && BodyFile[17] == 0x46 && BodyFile[18] == 0x53)
                                // ".shtxfs" is an image format used for PSVITA.
                                NewFileExtension = ".gx3.shtxfs.btx";
                            else if (BodyFile[13] == 0x53 && BodyFile[14] == 0x48 && BodyFile[15] == 0x54 && BodyFile[16] == 0x58)
                                // ".shtx" is an image format used for PSVITA.
                                NewFileExtension = ".gx3.shtx.btx";
                            else
                                NewFileExtension = ".gx3";
                        }
                        else if (BodyFile[0] == 0x4C && BodyFile[1] == 0x4C && BodyFile[2] == 0x46 && BodyFile[3] == 0x53)
                            NewFileExtension = ".llfs";
                        else if (BodyFile[0] == 0x4F && BodyFile[1] == 0x4D && BodyFile[2] == 0x47 && BodyFile[3] == 0x2E && BodyFile[4] == 0x30 && BodyFile[5] == 0x30)
                            NewFileExtension = ".gmo";
                        else if (BodyFile[0] == 0x47 && BodyFile[1] == 0x58 && BodyFile[2] == 0x54 && BodyFile[3] == 0x00)
                            // ".gxt" is an image format used for PSVITA.
                            NewFileExtension = ".gxt";
                        else if (BodyFile[0] == 0x53 && BodyFile[1] == 0x48 && BodyFile[2] == 0x54 && BodyFile[3] == 0x58 && BodyFile[2] == 0x46 && BodyFile[3] == 0x53)
                            //  There are two types of ".btx"" ".shtxfs" and ".shtxfs". This is image format used for PSVITA and mainly for AE.
                            NewFileExtension = ".shtxfs.btx";
                        else if (BodyFile[0] == 0x53 && BodyFile[1] == 0x48 && BodyFile[2] == 0x54 && BodyFile[3] == 0x58)
                            NewFileExtension = ".shtx.btx";
                        else if (BodyFile[0] == 0x74 && BodyFile[1] == 0x46 && BodyFile[2] == 0x70 && BodyFile[3] == 0x53)
                            // Files.font descrives the font. Letters position, height, ect...
                            NewFileExtension = ".font";
                        else if (BodyFile[0] == 0x4D && BodyFile[1] == 0x49 && BodyFile[2] == 0x47 && BodyFile[3] == 0x2E && BodyFile[4] == 0x30 && BodyFile[5] == 0x30)
                            // ".gim"  is an image format used for PSP.
                            NewFileExtension = ".gim";
                        else if ((BodyFile[0] == 0x01 || BodyFile[0] == 0x02) && BodyFile[1] == 0x00 && BodyFile[2] == 0x00 && BodyFile[3] == 0x00 && (BodyFile[4] == 0x10 || BodyFile[4] == 0x0C) && BodyFile[5] == 0x00 && BodyFile[6] == 0x00 && BodyFile[7] == 0x00 && BodyFile[16] == 0x70)
                            NewFileExtension = ".lin";
                        else if (BodyFile.Length < 0x70 || (BodyFile[0] == 0xFF && BodyFile[1] == 0xFE))
                            NewFileExtension = ".unknown";
                        else if ((BodyFile[0] == 0x00 && BodyFile[1] == 0x01 && BodyFile[2] == 0x01 && BodyFile[3] == 0x00) || (BodyFile[0] == 0x00 && BodyFile[1] == 0x00 && BodyFile[2] == 0x02 && BodyFile[3] == 0x00) || (BodyFile[0] == 0x01 && BodyFile[1] == 0x00 && BodyFile[2] == 0x02 && BodyFile[3] == 0x00) || (BodyFile[0] == 0x00 && BodyFile[1] == 0x00 && BodyFile[2] == 0x0A && BodyFile[3] == 0x00) || (BodyFile[0] == 0x00 && BodyFile[1] == 0x01 && BodyFile[2] == 0x09 && BodyFile[3] == 0x00) || (BodyFile[0] == 0x00 && BodyFile[1] == 0x00 && BodyFile[2] == 0x0B && BodyFile[3] == 0x00))
                            NewFileExtension = ".tga";
                        else if ((BodyFile[0] != 0x00 || BodyFile[1] != 0x00) && BodyFile[2] == 0x00 && BodyFile[3] == 0x00 && (BodyFile[4] != 0x00 || BodyFile[5] != 0x00) && BodyFile[6] == 0x00 && BodyFile[7] == 0x00)
                            NewFileExtension = ".pak";
                        else
                            NewFileExtension = ".unknown";

                        string NewFileAddress = Path.Combine(DestinationDir, Path.GetFileName(DestinationDir) + "-[" + i.ToString("D4") + "]");

                        // After establishing its extension, the file is created and saved.
                        using (FileStream Extract = new FileStream(NewFileAddress + NewFileExtension, FileMode.Create, FileAccess.Write))
                            Extract.Write(BodyFile, 0, BodyFile.Length);

                        // If the user has decided to convert images to ".png", then convert them.
                        if (ToPNG == 1 && NewFileExtension == ".tga") // From TGA to PNG.
                        {
                            string CodeLine = "convert \"" + NewFileAddress + NewFileExtension + "\" -alpha Background -quality 100 \"" + NewFileAddress + ".png\"";
                            UseEXEToConvert("Ext\\convert.exe", CodeLine);

                            // Remove the original image after the conversion.
                            File.Delete(NewFileAddress + NewFileExtension);
                        }
                        // From GXT or BTX to PNG.       
                        else if (ToPNG == 1 && (NewFileExtension.Contains(".gxt") || NewFileExtension.Contains(".btx")))
                        {
                            string CodeLine = "\"" + NewFileAddress + NewFileExtension + "\"";
                            UseEXEToConvert("Ext\\ScarletTestApp.exe", CodeLine);
                            // Remove the original image after the conversion.
                            File.Delete(NewFileAddress + NewFileExtension);

                            /* If it exists, delete the decompressed file. "Scarlet" automatically decompress
                            the compressed images but does not erase the decompressed files. */
                            if (File.Exists(NewFileAddress + NewFileExtension + ".dec"))
                            {
                                File.Delete(NewFileAddress + NewFileExtension + ".dec");
                                while (File.Exists(NewFileAddress + NewFileExtension + ".dec")) { }
                            }
                            if (File.Exists(NewFileAddress + NewFileExtension + ".dec.dec"))
                            {
                                File.Delete(NewFileAddress + NewFileExtension + ".dec.dec");
                                while (File.Exists(NewFileAddress + NewFileExtension + ".dec.dec")) { }
                            }
                        }
                        else if (ToPNG == 1 && NewFileExtension == ".gim") // From GIM to PNG.
                        {
                            string CodeLine = "\"" + NewFileAddress + NewFileExtension + "\"";
                            UseEXEToConvert("Ext\\GIM2PNG.exe", CodeLine);

                            // Remove the original image.gim after the conversion.
                            File.Delete(NewFileAddress + NewFileExtension);
                        }

                        // Return into the pointer area and takes care of the next file.
                        FilePAK.Seek(pos, SeekOrigin.Begin);
                    }
                }
            }

            // Delete file.pak.dec since it is no longer needed.
            if (Path.GetExtension(PAKFileAddress) == ".dec")
            {
                File.Delete(PAKFileAddress);
                while (File.Exists(PAKFileAddress)) { }
            }

            // If the folder contains only "files.unknown", then delete the folder.
            if (Directory.Exists(DestinationDir) == true && Directory.GetFiles(DestinationDir, "*.png").Length == 0 && Directory.GetFiles(DestinationDir, "*.cmp").Length == 0 && Directory.GetFiles(DestinationDir, "*.gx3").Length == 0
            && Directory.GetFiles(DestinationDir, "*.btx").Length == 0 && Directory.GetFiles(DestinationDir, "*.llfs").Length == 0 && Directory.GetFiles(DestinationDir, "*.gmo").Length == 0
            && Directory.GetFiles(DestinationDir, "*.gxt").Length == 0 && Directory.GetFiles(DestinationDir, "*.tga").Length == 0 && Directory.GetFiles(DestinationDir, "*.font").Length == 0 && Directory.GetFiles(DestinationDir, "*.gim").Length == 0
            && Directory.GetFiles(DestinationDir, "*.lin").Length == 0 && Directory.GetFiles(DestinationDir, "*.pak").Length == 0)
            {
                Directory.Delete(DestinationDir, true);
                while (Directory.Exists(DestinationDir)) { }

                /* If it's not the original.pak, but a sub-pak, rename the pak in ".unknown" because it doesn't contain any known files.
                In the case of Files.PAK containing LINs or PAKs with text, don't change the file extension ortherwise the program will not be able to extract the text from them. */
                if (PAKFileAddress.Contains("MODE]\\EXTRACTED\\") && !PAKFileAddress.Contains("TEXT PAK TYPE 2") && !PAKFileAddress.Contains("TEXT PAK TYPE 3"))
                {
                    // File.Move is not able to overwrite files, so we have to delete the file.unknown if it already exists.
                    if (File.Exists(Path.ChangeExtension(PAKFileAddress, ".unknown")))
                    {
                        File.Delete(Path.ChangeExtension(PAKFileAddress, ".unknown"));
                        while (File.Exists(Path.ChangeExtension(PAKFileAddress, ".unknown"))) { }
                    }

                    File.Move(PAKFileAddress, Path.ChangeExtension(PAKFileAddress, ".unknown"));
                }
            }
            // Otherwise, if the folder contains file.pak, unpack them.
            else if (Directory.GetFiles(DestinationDir, "*.pak").Length != 0)
                foreach (string SinglePAK in Directory.GetFiles(DestinationDir, "*.pak", SearchOption.TopDirectoryOnly))
                {
                    ExtractPAK(SinglePAK, Path.Combine(Path.GetDirectoryName(SinglePAK), Path.GetFileNameWithoutExtension(SinglePAK)), ToPNG);

                    // If it was possible to extract at least one file different from .unknown, delete the original pak.
                    if ((Directory.Exists(Path.Combine(Path.GetDirectoryName(SinglePAK), Path.GetFileNameWithoutExtension(SinglePAK))) == true))
                    {
                        File.Delete(SinglePAK);
                        while (File.Exists(SinglePAK)) { }
                    }
                }
        }

        private void button16_Click(object sender, EventArgs e) // REPACK TEXTURE ".PAK" W/O CONVERT 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXTURE PAK (W-O CONVERT)",
                TEMPFolder = "TEMP001";

            // If "EXTRACTED PAK" exists and it's not empty.
            if (Directory.Exists(OriginalDir) && Directory.EnumerateDirectories(OriginalDir).Any() == true)
            {
                label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                label5.Refresh(); // Refresh the Status label.

                // 0 = Don't convert the images, leave them like they are.
                CloneDirectory(OriginalDir, TEMPFolder, "IMAGES", 0);

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXTURE PAK (W-O CONVERT)";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                    Directory.CreateDirectory(DestinationDir);

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    RePackPAK(PakDirToBeRepacked, DestinationDir, 1); // 1 == converti in ".pak" anche le sotto cartelle. 

                // Remove the temp folder as it is no longer needed.
                Directory.Delete(TEMPFolder, true);
                while (Directory.Exists(TEMPFolder)) { }

                label5.Text = "Ready!"; // Change the "Status" to "Ready!".
                MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // RepackSubDirs: 1 == convert to ".pak" subdirectories too, 0 = don't repack the subdirs.
        private void RePackPAK(string PakDirToBeRepacked, string DestinationDir, int RepackSubDirs)
        {
            // Check if there are any subfolders and converts them into ".pak".
            if (RepackSubDirs == 1 && Directory.GetDirectories(PakDirToBeRepacked, "*", SearchOption.TopDirectoryOnly).Length != 0)
                foreach (string SottoCartella in Directory.GetDirectories(PakDirToBeRepacked, "*", SearchOption.TopDirectoryOnly))
                    RePackPAK(SottoCartella, PakDirToBeRepacked, 1);

            using (FileStream NEWPAK = new FileStream(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(PakDirToBeRepacked) + ".pak"), FileMode.Create, FileAccess.Write))
            {
                using (BinaryWriter PAKBinaryWriter = new BinaryWriter(NEWPAK))
                {
                    // Stores the full address of all files included into the folder and orders them alphanumerically.
                    string[] FilesAddress = Directory.GetFiles(PakDirToBeRepacked, "*");
                    Array.Sort(FilesAddress, new AlphanumComparatorFast());

                    // Write down the n# of the sentences.
                    PAKBinaryWriter.Write((uint)FilesAddress.Length);

                    //  "SentencesOffset" will contain the offset of each file. 
                    uint[] FilesOffset = new uint[FilesAddress.Length];
                    byte Padding = 0x10; // The padding is "0x10" for every DR. 

                    // Stores the current position so that we can come back later and enter the correct offsets.
                    int pos = (int)NEWPAK.Position;

                    // Fills the pointers area with zeros. At the end of the process the area will be overwritten with the correct data.
                    for (int i = 0; i < FilesOffset.Length; i++)
                        PAKBinaryWriter.Write((uint)0x00);

                    // Padding after the pointers zone.
                    if (NEWPAK.Position % Padding != 0)
                        while (NEWPAK.Position % Padding != 0)
                            PAKBinaryWriter.Write((byte)0x0);

                    // Store the size of the area dedicated to pointers. 
                    FilesOffset[0] = (uint)NEWPAK.Position;

                    for (int i = 0; i < FilesAddress.Length; i++)
                    {
                        // It opens every single file, stores it in "BodyFile" and then inserts it into the new ".pak".
                        using (FileStream TempFile = new FileStream(FilesAddress[i], FileMode.Open, FileAccess.Read))
                        {
                            byte[] BodyFile = new byte[TempFile.Length];
                            TempFile.Read(BodyFile, 0, BodyFile.Length);
                            NEWPAK.Write(BodyFile, 0, BodyFile.Length);
                        }

                        if (i < FilesAddress.Length - 1)
                        {
                            // Inserts the padding after each file, except the last.
                            if (NEWPAK.Position % Padding != 0)
                                while (NEWPAK.Position % Padding != 0)
                                    PAKBinaryWriter.Write((byte)0x0);

                            // No need to memorize the last offset because it would point to the EOF. 
                            FilesOffset[i + 1] = (uint)NEWPAK.Position;
                        }
                    }

                    // Comes back in the area dedicated to the offsets and overwrites all the zeros with the correct offsets.
                    PAKBinaryWriter.Seek(pos, SeekOrigin.Begin);
                    for (int i = 0; i < FilesAddress.Length; i++)
                        PAKBinaryWriter.Write(FilesOffset[i]);
                }
            }
        }

        private void button17_Click(object sender, EventArgs e) // EXTRACT TEXTURE ".PAK" TO PNG 
        {
            using (OpenFileDialog PAK = new OpenFileDialog())
            {
                PAK.Title = "Select one or more \"file.pak\"";
                PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PAK.FileName = "*.pak";
                PAK.Multiselect = true;

                if (PAK.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SinglePAK in PAK.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXTURE PAK (TO PNG)\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        // 1 = Convert all the extracted images to ".png".
                        ExtractPAK(SinglePAK, DestinationDir, 1);
                    }

                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button18_Click(object sender, EventArgs e) // REPACK TEXTURE ".PAK" TO PNG 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXTURE PAK (TO PNG)",
               TEMPFolder = "TEMP001";

            // If "EXTRACTED PAK" exists and it's not empty.
            if (Directory.Exists(OriginalDir) && Directory.EnumerateDirectories(OriginalDir).Any() == true)
            {
                label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                label5.Refresh(); // Refresh the Status label.

                /* Folder to be cloned - folder where the files will be copied - File type being worked on.
                 * - 1 = Convert the images to GXT, TGA or BXT (it depends from the game). */
                CloneDirectory(OriginalDir, TEMPFolder, "IMAGES", 1);

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXTURE PAK (TO PNG)";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                    Directory.CreateDirectory(DestinationDir);

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    RePackPAK(PakDirToBeRepacked, DestinationDir, 1); // 1 == Convert the subdirs to ".pak". 

                // Remove the temp folder as it's no longer needed.
                Directory.Delete(TEMPFolder, true);
                while (Directory.Exists(TEMPFolder)) { }

                label5.Text = "Ready!"; // Change the "Status" to "Ready!".
                MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button19_Click(object sender, EventArgs e) // CONVERT TO PNG 
        {
            using (OpenFileDialog IMAGES = new OpenFileDialog())
            {
                IMAGES.Title = "Select one or more files.";
                IMAGES.Filter = "Compatible files|*.tga; *.gxt; *.btx; *.gim|.tga|*.tga|.gxt|*.gxt|.btx|*.btx|.gim|*.gim|All Files (*.*)|*.*";
                IMAGES.Multiselect = true;

                if (IMAGES.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SingleImage in IMAGES.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\CONVERT TO PNG\\FROM " + Path.GetExtension(SingleImage).ToUpper() + " TO PNG",
                            ExtesionIMG = Path.GetExtension(SingleImage);

                        DestinationDir = DestinationDir.Replace(".", null);

                        if (Directory.Exists(DestinationDir) == false)
                            Directory.CreateDirectory(DestinationDir);

                        if (ExtesionIMG == ".tga")
                            ConvertFromTGAToPNG(SingleImage, DestinationDir);
                        else if ((ExtesionIMG == ".gxt" || ExtesionIMG == ".btx"))
                            ConvertGxtBtxToPNG(SingleImage, DestinationDir);
                        else if (ExtesionIMG == ".gim") //Images.gim are used only in the PSP games.
                            ConvertFromGIMToPNG(SingleImage, DestinationDir);
                    }

                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button20_Click(object sender, EventArgs e) // CONVERT TO TGA 
        {
            using (OpenFileDialog IMAGES = new OpenFileDialog())
            {
                IMAGES.Title = "Select one or more files.";
                IMAGES.Filter = "Compatible files|*.png; *.gxt; *.btx|.png|*.png|.gxt|*.gxt|.btx|*.btx|All Files (*.*)|*.*";
                IMAGES.Multiselect = true;

                if (IMAGES.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SingleImage in IMAGES.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\CONVERT TO TGA\\FROM " + Path.GetExtension(SingleImage).ToUpper() + " TO TGA",
                            ExtesionIMG = Path.GetExtension(SingleImage), ImageToConvert = SingleImage;

                        DestinationDir = DestinationDir.Replace(".", null);

                        if (Directory.Exists(DestinationDir) == false)
                            Directory.CreateDirectory(DestinationDir);

                        // We have to convert Images.btx && .gxt to .png and finally to .tga.
                        if (ExtesionIMG == ".gxt" || ExtesionIMG == ".btx")
                        {
                            ConvertGxtBtxToPNG(SingleImage, DestinationDir);
                            ImageToConvert = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".png");
                        }

                        // Convert the image.png to .tga.
                        ConvertFromPNGToTGA(ImageToConvert, DestinationDir);

                        // Delete the image.png generated from .btx files.
                        if (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".png")))
                        {
                            File.Delete(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".png"));
                            while (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".png"))) { }
                        }
                    }

                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button21_Click(object sender, EventArgs e) // CONVERT TO GXT 
        {
            using (OpenFileDialog IMAGES = new OpenFileDialog())
            {
                IMAGES.Title = "Select one or more files.";
                IMAGES.Filter = "Compatible files|*.png; *.tga; *.btx|.png|*.png|.tga|*.tga|.btx|*.btx|All Files (*.*)|*.*";
                IMAGES.Multiselect = true;

                if (IMAGES.ShowDialog() == DialogResult.OK)
                {
                    label5.Text = "Wait..."; // Change "Ready!" to "Wait..."
                    label5.Refresh(); // Refresh the Status label.

                    foreach (string SingleImage in IMAGES.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\CONVERT TO GXT\\FROM " + Path.GetExtension(SingleImage).ToUpper() + " TO GXT",
                            ExtesionIMG = Path.GetExtension(SingleImage), ImageToConvert = SingleImage;

                        DestinationDir = DestinationDir.Replace(".", null);

                        if (Directory.Exists(DestinationDir) == false)
                            Directory.CreateDirectory(DestinationDir);

                        string CodeLine = "-i \"" + SingleImage + "\" -o \"" + Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage)) + ".gxt\"";

                        if (ExtesionIMG == ".btx" || ExtesionIMG == ".png")
                        {
                            // We have to convert images.btx to .png, then to .tga and finally to .gxt.
                            if (ExtesionIMG == ".btx")
                            {
                                // Convert the immage.btx to .png
                                ConvertGxtBtxToPNG(SingleImage, DestinationDir);
                                ImageToConvert = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".png");
                            }

                            // Convert the image.png to .tga.
                            ConvertFromPNGToTGA(ImageToConvert, DestinationDir);

                            // Delete the image.png generated from .btx files.
                            if (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".png")))
                            {
                                File.Delete(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".png"));
                                while (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".png"))) { }
                            }

                            ImageToConvert = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".tga");
                        }

                        ConvertFromTGAToGXT(ImageToConvert, DestinationDir);

                        // Delte the image.tga generated from .btx and .png files.
                        if (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".tga")))
                        {
                            File.Delete(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".tga"));
                            while (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(SingleImage) + ".tga"))) { }
                        }
                    }

                    label5.Text = "Ready!"; // Change the "Status" to "Ready!".

                    MessageBox.Show("Done!", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button22_Click(object sender, EventArgs e) // CONVERT TO BXT 
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Liquid-S");
        }

        private void ConvertFromPNGToTGA(string Image, string DestinationDir)
        {
            string CodeLine = "convert \"" + Image + "\" -compress RLE \"" + Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image)) + ".tga\"";
            UseEXEToConvert("Ext\\convert.exe", CodeLine);
        }

        private void ConvertFromTGAToPNG(string Image, string DestinationDir)
        {
            string CodeLine = "convert \"" + Image + "\" -alpha Background -quality 100 \"" + Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image)) + ".png\"";
            UseEXEToConvert("Ext\\convert.exe", CodeLine);
        }

        private void ConvertFromTGAToGXT(string Image, string DestinationDir)
        {
            string CodeLine = "-i \"" + Image + "\" -o \"" + Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image)) + ".gxt\"";
            UseEXEToConvert("Ext\\psp2gxt.exe", CodeLine);
        }

        private void ConvertGxtBtxToPNG(string Image, string DestinationDir)
        {
            string ExtesionIMG = Path.GetExtension(Image),
            CodeLine = "\"" + Image + "\" --output \"" + DestinationDir + "\"";

            UseEXEToConvert("Ext\\ScarletTestApp.exe", CodeLine);

            /* If it exists, delete the decompressed file. "Scarlet" automatically decompress
            the compressed images but doesn't erase the decompressed files after having used them. */
            if (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ExtesionIMG + ".dec")))
            {
                File.Delete(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ExtesionIMG + ".dec"));
                while (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ExtesionIMG + ".dec"))) { }
            }
            if (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ExtesionIMG + ".dec.dec")))
            {
                File.Delete(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ExtesionIMG + ".dec.dec"));
                while (File.Exists(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ExtesionIMG + ".dec.dec"))) { }
            }
        }
        
        private void ConvertFromGIMToPNG(string Image, string DestinationDir)
        {
            string CodeLine = "\"" + Image + "\"";
            UseEXEToConvert("Ext\\GIM2PNG.exe", CodeLine);

            //Since GIM2PNG.exe creates the image.png in the same folder as the original, we have to manually move the .png to the destination folder.
            File.Move(Path.Combine(Path.GetDirectoryName(Image), Path.GetFileNameWithoutExtension(Image) + ".png"), Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(Image) + ".png"));
        }
    }
}
