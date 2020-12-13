using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

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
            tabControl1.Selecting += new TabControlCancelEventHandler(TabControl1_Selecting);

            toolStripComboBoxBTXType.SelectedIndex = 0;
        }

        public static bool GreyScaleValue = false;

        // Clone the original buttons to the new tablelayout's tab when the user change tab.
        private void TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            //.GetControlFromPosition(0, 0) == null... serve to avoid cloning the buttons in the same tab more than once.
            if (tabControl1.SelectedIndex == 0 && tableLayoutPanelDR1PSP.GetControlFromPosition(0, 0) == null)
            {
                CloneTab(ref tableLayoutPanelDR1PSP);
            }
            else if (tabControl1.SelectedIndex == 1 && tableLayoutPanelDR1PSVITA.GetControlFromPosition(0, 0) == null)
            {
                CloneTab(ref tableLayoutPanelDR1PSVITA);
            }
            else if (tabControl1.SelectedIndex == 3 && tableLayoutPanelDR2PSVITA.GetControlFromPosition(0, 0) == null)
            {
                CloneTab(ref tableLayoutPanelDR2PSVITA);
            }
            else if (tabControl1.SelectedIndex == 4 && tableLayoutPanelDR2PC.GetControlFromPosition(0, 0) == null)
            {
                CloneTab(ref tableLayoutPanelDR2PC);
            }
            else if (tabControl1.SelectedIndex == 5 && tableLayoutPanelDRAEPSVITA.GetControlFromPosition(0, 0) == null)
            {
                CloneTab(ref tableLayoutPanelDRAEPSVITA);
            }
            else if (tabControl1.SelectedIndex == 6 && tableLayoutPanelDRAEPC.GetControlFromPosition(0, 0) == null)
            {
                CloneTab(ref tableLayoutPanelDRAEPC);
            }
        }

        public void CloneTab(ref TableLayoutPanel TableLayoutToFill)
        {
            CreaLabel(ref TableLayoutToFill, ref labelDataAssets);
            CreaLabel(ref TableLayoutToFill, ref labelTexts);
            CreaLabel(ref TableLayoutToFill, ref labelTextures);
            CreaLabel(ref TableLayoutToFill, ref labelUtilities);
            CloneButton(ref TableLayoutToFill, ref buttonExtractLin);
            CloneButton(ref TableLayoutToFill, ref buttonRepackLin);
            CloneButton(ref TableLayoutToFill, ref buttonExtractPakType1);
            CloneButton(ref TableLayoutToFill, ref buttonRepackPakType1);
            CloneButton(ref TableLayoutToFill, ref buttonExtractPakType2);
            CloneButton(ref TableLayoutToFill, ref buttonRepackPakType2);
            CloneButton(ref TableLayoutToFill, ref buttonExtractPakType3);
            CloneButton(ref TableLayoutToFill, ref buttonRepackPakType3);
            CloneButton(ref TableLayoutToFill, ref buttonExtractPakWOConvert);
            CloneButton(ref TableLayoutToFill, ref buttonRepacktPakWOConvert16);
            CloneButton(ref TableLayoutToFill, ref buttonExtractPakToPNG);
            CloneButton(ref TableLayoutToFill, ref buttonConvertToPng);
            CloneButton(ref TableLayoutToFill, ref buttonConvertXmlToPo);
            CloneButton(ref TableLayoutToFill, ref buttonaPoToTranslateOtherPos);

            if (!tabControl1.SelectedTab.Text.Contains("PSP"))
            {
                CloneButton(ref TableLayoutToFill, ref buttonConvertToTGA);
                CloneButton(ref TableLayoutToFill, ref buttonConvertGXT);
                CloneButton(ref TableLayoutToFill, ref buttonConverBTX);

                /* The tool doesn't convert images from PNG to GIM, so there is no point in cloning
             the "REPACK TEXTURE .PAK FROM PNG" button in the PSP tab. */
                CloneButton(ref TableLayoutToFill, ref buttonRepackPakToPNG);
            }

            if (tabControl1.SelectedTab.Text.Contains("DRAE (PC)"))
            {
                CloneButton(ref TableLayoutToFill, ref buttonExtractBND);
                CloneButton(ref TableLayoutToFill, ref buttonRepackBND);
            }

            // Clone the "Unpack/Repack .WAD" buttons only if the new tab is about a PC game.
            if (tabControl1.SelectedTab.Text.Contains("PC") && !tabControl1.SelectedTab.Text.Contains("DRAE (PC)"))
            {
                CloneButton(ref TableLayoutToFill, ref buttonUnpackWAD);
                CloneButton(ref TableLayoutToFill, ref buttonRepackWAD);
            }

            /* Clone the "Unpack/Repack .CPK" buttons only if the new tab is about a PSVITA game.
            "DR1 (PSVITA)"'s tab contains the original buttons, so there is no need to clone the buttons there. */
            else if ((tabControl1.SelectedTab.Text.Contains("PSVITA") || tabControl1.SelectedTab.Text.Contains("DRAE (PC)")) && !tabControl1.SelectedTab.Text.Contains("DR1 (PSVITA)"))
            {
                CloneButton(ref TableLayoutToFill, ref buttonExtractCPK);
                CloneButton(ref TableLayoutToFill, ref buttonRepackCPK);
            }

            if ((tabControl1.SelectedTab.Text.Contains("PSVITA") || tabControl1.SelectedTab.Text.Contains("DRAE (PC)")) && !tabControl1.SelectedTab.Text.Contains("DR1 (PSVITA)"))
            {
                CreaLabel(ref TableLayoutToFill, ref labelCompression);
                CloneButton(ref TableLayoutToFill, ref buttonDecompressFiles);
                CloneButton(ref TableLayoutToFill, ref buttonCompressFIles);
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

            FieldInfo eventsField = typeof(Component).GetField("events", BindingFlags.NonPublic | BindingFlags.Instance);
            object eventHandlerList = eventsField.GetValue(OriginalButton);
            eventsField.SetValue(Gril.GetControlFromPosition(Column, Row), eventHandlerList);
        }

        private void Button1_Click(object sender, EventArgs e) // EXTRACT .WAD 
        {
            using (OpenFileDialog WAD = new OpenFileDialog())
            {
                WAD.Title = "Select one or more \"file.wad\"";
                WAD.Filter = ".wad|*.wad|All Files (*.*)|*.*";
                WAD.Multiselect = true;

                if (WAD.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    foreach (string SingleWAD in WAD.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\WAD\\" + Path.GetFileNameWithoutExtension(SingleWAD);

                        Danganronpa_Another_Tool.WAD.ExtractWAD(SingleWAD, DestinationDir);
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e) // REPACK WAD 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\WAD";

            if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
            {
                OriginalDir = Common.ChooseFolder(OriginalDir);
            }

            // If the folder "EXTRACTED WAD" exists and is not empty.
            if (Common.CheckDirExistence(OriginalDir) == true)
            {
                Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\WAD";

                // For each folder to be reassembled in .WAD
                foreach (string FolderToRebuildToWAD in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                {
                    WAD.RePackWAD(FolderToRebuildToWAD, DestinationDir);
                }

                Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
            }
        }

        private void Button3_Click(object sender, EventArgs e) // UNPACK .CPK
        {
            if (Common.CheckIfEXEExist("YACpkTool.exe"))
            {
                using (OpenFileDialog CPK = new OpenFileDialog())
                {
                    CPK.Title = "Select one or more \"file.cpk\"";
                    CPK.Filter = ".cpk|*.cpk|All Files (*.*)|*.*";
                    CPK.Multiselect = true;

                    if (CPK.ShowDialog() == DialogResult.OK)
                    {
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        foreach (string SingleCPK in CPK.FileNames)
                        {
                            string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\CPK\\" + Path.GetFileNameWithoutExtension(SingleCPK);

                            Danganronpa_Another_Tool.CPK.ExtractCPK(SingleCPK, DestinationDir);

                        }

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e) // REPACK .CPK
        {
            if (Common.CheckIfEXEExist("YACpkTool.exe"))
            {
                string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\CPK";

                if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
                {
                    OriginalDir = Common.ChooseFolder(OriginalDir);
                }

                // If the folder "EXTRACTED CPK" exists and is not empty.
                if (Common.CheckDirExistence(OriginalDir) == true)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\CPK";

                    // If the destination folder does not exist, create it.
                    if (Directory.Exists(DestinationDir) == false)
                    {
                        Directory.CreateDirectory(DestinationDir);
                    }

                    // For each folder to be reassembled in .CPK
                    foreach (string FolderToRebuildToCPK in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                    {
                        string CodeLine = "-P -i \"" + FolderToRebuildToCPK + "\" --codec LAYLA --align 2048 -o \"" + Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(FolderToRebuildToCPK)) + ".cpk\"";

                        Images.UseEXEToConvert("Ext\\YACpkTool.exe", CodeLine);
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e) // EXTRACT FULL GAME UMDIMAGE.DAT (PSP ONLY) 
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
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        string DestinationDir = "DR1 (PSP) [MANUAL MODE]\\EXTRACTED\\" + Path.GetFileNameWithoutExtension(UMDIMAGE.FileName).ToUpper() + " (FULL GAME)";

                        if (Directory.Exists(DestinationDir) == false)
                        {
                            Directory.CreateDirectory(DestinationDir);
                        }

                        DATpsp.ExtractDAT(UMDIMAGE.FileName, DestinationDir, 0xF5A38, EBOOT.FileName, "FULL");

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }
        }

        private void Button6_Click(object sender, EventArgs e) // EXTRACT DEMO UMDIMAGE.DAT (PSP ONLY) 
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
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        string DestinationDir = "DR1 (PSP) [MANUAL MODE]\\EXTRACTED\\" + Path.GetFileNameWithoutExtension(UMDIMAGE.FileName).ToUpper() + " (DEMO)";

                        if (Directory.Exists(DestinationDir) == false)
                        {
                            Directory.CreateDirectory(DestinationDir);
                        }

                        DATpsp.ExtractDAT(UMDIMAGE.FileName, DestinationDir, 0x145c1c, EBOOT.FileName, "DEMO");

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }
        }

        private void Button7_Click(object sender, EventArgs e) // EXTRACT .LIN 
        {
            //CommonTextStuff.ConverPoToTXT();

            using (OpenFileDialog LinFiles = new OpenFileDialog())
            {
                LinFiles.Title = "Select one or more \"file.lin\"";
                LinFiles.Filter = ".lin|*.lin|All Files (*.*)|*.*";
                LinFiles.Multiselect = true;

                if (LinFiles.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    foreach (string SingleLIN in LinFiles.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\LIN\\" + Path.GetFileNameWithoutExtension(SingleLIN);

                        LIN.ExtractLIN(SingleLIN, DestinationDir);
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button8_Click(object sender, EventArgs e) // REPACK .LIN 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\LIN";

            if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
            {
                OriginalDir = Common.ChooseFolder(OriginalDir);
            }

            // If "EXTRACTED LIN" exists and it's not empty.
            if (Common.CheckDirExistence(OriginalDir) == true)
            {
                Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\LIN";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                {
                    Directory.CreateDirectory(DestinationDir);
                }

                // For each LIN folder to be repacked.
                foreach (string DirLinToBeRepacked in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                {
                    // Continue only if there is a text file or if the user has chosen to work with files composed only by the bytecode too.
                    if (File.Exists(Path.Combine(DirLinToBeRepacked, Path.GetFileNameWithoutExtension(DirLinToBeRepacked) + ".po")) == true
                        || (File.Exists(Path.Combine(DirLinToBeRepacked, Path.GetFileNameWithoutExtension(DirLinToBeRepacked) + ".bytecode")) == true
                        && File.Exists(Path.Combine(DirLinToBeRepacked, Path.GetFileNameWithoutExtension(DirLinToBeRepacked) + ".po")) == false
                        && ignoreLINWoTextToolStripMenuItem.Checked == false))
                    {
                        CommonTextStuff.RePackText(DirLinToBeRepacked, DestinationDir);
                    }
                }

                Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
            }
        }

        private void Button9_Click(object sender, EventArgs e) // EXTRACT TEXT PAK TYPE 1 ".PAK" --> Simple text
        {
            using (OpenFileDialog PAK = new OpenFileDialog())
            {
                PAK.Title = "Select one or more \"file.pak\"";
                PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PAK.Multiselect = true;

                if (PAK.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    foreach (string SinglePAK in PAK.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 1\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        CommonTextStuff.MakePO(CommonTextStuff.TextExtractor(SinglePAK), null, DestinationDir);
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button10_Click(object sender, EventArgs e) // REPACK TEXT PAK TYPE 1 ".PAK" --> Simple text 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 1";

            if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
            {
                OriginalDir = Common.ChooseFolder(OriginalDir);
            }

            // If "EXTRACTED TEXT PAK TYPE 1" exists and it's not empty.
            if (Common.CheckDirExistence(OriginalDir) == true)
            {
                Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXT PAK TYPE 1";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                {
                    Directory.CreateDirectory(DestinationDir);
                }

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                {
                    CommonTextStuff.RePackText(PakDirToBeRepacked, DestinationDir);
                }

                Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
            }
        }

        private void Button11_Click(object sender, EventArgs e) // EXTRACT TEXT PAK TYPE 2 ".PAK" --> LIN inside a PAK 
        {
            using (OpenFileDialog PakDialog = new OpenFileDialog())
            {
                PakDialog.Title = "Select one or more \"file.pak\"";
                PakDialog.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PakDialog.Multiselect = true;

                if (PakDialog.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    foreach (string SinglePAK in PakDialog.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 2\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        // 0 = Don't convert the images, leave them like they are.
                        Danganronpa_Another_Tool.PAK.ExtractPAK(SinglePAK, DestinationDir, false);

                        // If the folder exists it means that contains the files texts, then search for ".lin"s and extract their texts.
                        if (Directory.Exists(DestinationDir) == true)
                        {
                            foreach (string SingleLIN in Directory.GetFiles(DestinationDir, "*.lin", SearchOption.TopDirectoryOnly))
                            {
                                LIN.ExtractLIN(SingleLIN, Path.Combine(Path.GetDirectoryName(SingleLIN), Path.GetFileNameWithoutExtension(SingleLIN)));

                                // Remove the LIN after having extracted the text.
                                if ((Directory.Exists(Path.Combine(Path.GetDirectoryName(SingleLIN), Path.GetFileNameWithoutExtension(SingleLIN))) == true))
                                {
                                    File.Delete(SingleLIN);
                                    while (File.Exists(SingleLIN)) { }
                                }
                            }
                        }
                    }
                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button12_Click(object sender, EventArgs e) // REPACK TEXT PAK TYPE 2 ".PAK" --> LIN inside a PAK 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 2",
                TEMPFolder = "TEMP001";

            if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
            {
                OriginalDir = Common.ChooseFolder(OriginalDir);
            }

            // If "EXTRACTED PAK" exists and it's not empty.
            if (Common.CheckDirExistence(OriginalDir) == true)
            {
                Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                // false = Don't convert the images, leave them like they are.
                Common.CloneDirectory(OriginalDir, TEMPFolder, "TEXT", false);

                // For each LIN folder to be repacked.
                foreach (string DirsWithinTEMPFolder in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                {
                    foreach (string DirLinToBeRepacked in Directory.GetDirectories(DirsWithinTEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    {
                        CommonTextStuff.RePackText(DirLinToBeRepacked, DirsWithinTEMPFolder);
                    }
                }

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXT PAK TYPE 2";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                {
                    Directory.CreateDirectory(DestinationDir);
                }

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                {
                    Danganronpa_Another_Tool.PAK.RePackPAK(PakDirToBeRepacked, DestinationDir, false);
                }

                // Remove the temp folder as it is no longer needed.
                Directory.Delete(TEMPFolder, true);
                while (Directory.Exists(TEMPFolder)) { }

                Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
            }
        }

        private void Button13_Click(object sender, EventArgs e) // EXTRACT TEXT PAK TYPE 3 ".PAK" --> PAK inside a PAK 
        {
            using (OpenFileDialog PAK = new OpenFileDialog())
            {
                PAK.Title = "Select one or more \"file.pak\"";
                PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PAK.Multiselect = true;

                if (PAK.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    foreach (string SinglePAK in PAK.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 3\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        // False = Don't convert the images, leave them like they are.
                        Danganronpa_Another_Tool.PAK.ExtractPAK(SinglePAK, DestinationDir, false);

                        // If the folder exists it means that contains the files texts, then search for ".pak"s and extract their texts.
                        if (Directory.Exists(DestinationDir) == true)
                        {
                            foreach (string SottoPAK in Directory.GetFiles(DestinationDir, "*.pak", SearchOption.TopDirectoryOnly))
                            {
                                // Extract the text from each Pak.
                                CommonTextStuff.MakePO(CommonTextStuff.TextExtractor(SottoPAK), null, Path.Combine(Path.GetDirectoryName(SottoPAK), Path.GetFileNameWithoutExtension(SottoPAK)));

                                // Remove the PAK after having extracted the text.
                                File.Delete(SottoPAK);
                                while (File.Exists(SottoPAK)) { }
                            }
                        }
                    }
                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button14_Click(object sender, EventArgs e) // REPACK TEXT PAK TYPE 3 ".PAK" --> PAK inside a PAK 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXT PAK TYPE 3",
           TEMPFolder = "TEMP001";

            if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
            {
                OriginalDir = Common.ChooseFolder(OriginalDir);
            }

            // If "EXTRACTED PAK" exists and it's not empty.
            if (Common.CheckDirExistence(OriginalDir) == true)
            {
                Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                // false = Don't convert the images, leave them like they are.
                Common.CloneDirectory(OriginalDir, TEMPFolder, "TEXT", false);

                // For each LIN folder to be repacked.
                foreach (string DirsWithinTEMPFolder in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                {
                    foreach (string DirLinToBeRepacked in Directory.GetDirectories(DirsWithinTEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    {
                        CommonTextStuff.RePackText(DirLinToBeRepacked, DirsWithinTEMPFolder);
                    }
                }

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXT PAK TYPE 3";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                {
                    Directory.CreateDirectory(DestinationDir);
                }

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                {
                    Danganronpa_Another_Tool.PAK.RePackPAK(PakDirToBeRepacked, DestinationDir, false);
                }

                // Remove the temp folder as it is no longer needed.
                Directory.Delete(TEMPFolder, true);
                while (Directory.Exists(TEMPFolder)) { }

                Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
            }
        }

        private void Button15_Click(object sender, EventArgs e) // EXTRACT TEXTURE ".PAK" W/O CONVERT 
        {
            using (OpenFileDialog PAK = new OpenFileDialog())
            {
                PAK.Title = "Select one or more \"file.pak\"";
                PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                PAK.Multiselect = true;

                if (PAK.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    foreach (string SinglePAK in PAK.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXTURE PAK (W-O CONVERT)\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                        // 0 = Don't convert the images, leave them like they are.
                        Danganronpa_Another_Tool.PAK.ExtractPAK(SinglePAK, DestinationDir, false);
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button16_Click(object sender, EventArgs e) // REPACK TEXTURE ".PAK" W/O CONVERT 
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXTURE PAK (W-O CONVERT)",
                TEMPFolder = "TEMP001";

            if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
            {
                OriginalDir = Common.ChooseFolder(OriginalDir);
            }

            // If "EXTRACTED PAK" exists and it's not empty.
            if (Common.CheckDirExistence(OriginalDir) == true)
            {
                Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                // false = Don't convert the images, leave them like they are.
                Common.CloneDirectory(OriginalDir, TEMPFolder, "IMAGES", false);

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXTURE PAK (W-O CONVERT)";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                {
                    Directory.CreateDirectory(DestinationDir);
                }

                // For each PAK folder to be repacked.
                foreach (string PakDirToBeRepacked in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                {
                    Danganronpa_Another_Tool.PAK.RePackPAK(PakDirToBeRepacked, DestinationDir, true); // true == convert to ".pak" the subdirs too. 
                }

                // Remove the temp folder as it is no longer needed.
                Directory.Delete(TEMPFolder, true);
                while (Directory.Exists(TEMPFolder)) { }

                Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
            }
        }

        private void Button17_Click(object sender, EventArgs e) // EXTRACT TEXTURE ".PAK" TO PNG 
        {
            //Check if the EXEs needed exits.
            bool proceed = false;

            if (tabControl1.SelectedTab.Text.Contains("DRAE") || tabControl1.SelectedTab.Text.Contains("PSVITA"))
            {
                proceed = Common.CheckIfEXEExist("ScarletTestApp.exe");
            }
            else if (tabControl1.SelectedTab.Text.Contains("PC"))
            {
                proceed = Common.CheckIfEXEExist("convert.exe");
            }
            else if (tabControl1.SelectedTab.Text.Contains("PSP"))
            {
                proceed = Common.CheckIfEXEExist("GIM2PNG.exe");
            }

            if (proceed == true)
            {
                using (OpenFileDialog PAK = new OpenFileDialog())
                {
                    PAK.Title = "Select one or more \"file.pak\"";
                    PAK.Filter = ".pak|*.pak|All Files (*.*)|*.*";
                    PAK.Multiselect = true;

                    if (PAK.ShowDialog() == DialogResult.OK)
                    {
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        foreach (string SinglePAK in PAK.FileNames)
                        {
                            string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXTURE PAK (TO PNG)\\" + Path.GetFileNameWithoutExtension(SinglePAK);

                            // true = Convert all the extracted images to ".png".
                            Danganronpa_Another_Tool.PAK.ExtractPAK(SinglePAK, DestinationDir, true);
                        }

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }
        }

        private void Button18_Click(object sender, EventArgs e) // REPACK TEXTURE ".PAK" FROM PNG 
        {
            //Check if the EXEs needed exits.
            bool proceed = false;

            if (tabControl1.SelectedTab.Text.Contains("DRAE"))
            {
                proceed = Common.CheckIfEXEExist("convert.exe") && Common.CheckIfEXEExist("to_BTX.py") && Common.CheckIfEXEExist("pngquant.exe") && Common.CheckIfEXEExist("util.py");
            }
            else if (tabControl1.SelectedTab.Text.Contains("PC"))
            {
                proceed = Common.CheckIfEXEExist("convert.exe");
            }
            else if (tabControl1.SelectedTab.Text.Contains("PSVITA") && Common.CheckIfEXEExist("psp2gxt.exe") == true && Common.CheckIfEXEExist("convert.exe") == true)
            {
                proceed = true;
            }

            if (proceed == true)
            {
                string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\TEXTURE PAK (TO PNG)",
               TEMPFolder = "TEMP001";

                if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
                {
                    OriginalDir = Common.ChooseFolder(OriginalDir);
                }

                // If "EXTRACTED PAK" exists and it's not empty.
                if (Common.CheckDirExistence(OriginalDir) == true)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    /* Folder to be cloned - folder where the files will be copied - File type being worked on.
                     * true = Convert the images to GXT, TGA or BXT (it depends from the game). */
                    Common.CloneDirectory(OriginalDir, TEMPFolder, "IMAGES", true);

                    string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\TEXTURE PAK (TO PNG)";

                    // If the destination folder does not exist, create it.
                    if (Directory.Exists(DestinationDir) == false)
                    {
                        Directory.CreateDirectory(DestinationDir);
                    }

                    // For each PAK folder to be repacked.
                    foreach (string PakDirToBeRepacked in Directory.GetDirectories(TEMPFolder, "*", SearchOption.TopDirectoryOnly))
                    {
                        Danganronpa_Another_Tool.PAK.RePackPAK(PakDirToBeRepacked, DestinationDir, true); // true == Convert the subdirs to ".pak"

                        if (tabControl1.SelectedTab.Text.Contains("DRAE") == true && (Path.GetFileName(PakDirToBeRepacked) == "file_img_ehon" || Path.GetFileName(PakDirToBeRepacked) == "file_img_kill"))
                        {
                            Common.CompressFileSpikeCompression(Path.Combine(DestinationDir, Path.GetFileName(PakDirToBeRepacked) + ".pak"), DestinationDir, true); // true = replace the original.
                        }
                    }
                    // Remove the temp folder as it's no longer needed.
                    Directory.Delete(TEMPFolder, true);
                    while (Directory.Exists(TEMPFolder)) { }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button19_Click(object sender, EventArgs e) // CONVERT TO PNG 
        {
            using (OpenFileDialog IMG = new OpenFileDialog())
            {
                IMG.Title = "Select one or more files.";
                IMG.Filter = "Compatible files|*.tga; *.gxt; *.btx; *.gim; *.SHTX; *.SHTXFS; *.dec; *.cmp|.tga|*.tga|.gxt|*.gxt|.btx|*.btx|.gim|*.gim|.SHTX|*.SHTX|.SHTXFS|*.SHTXFS|.compressed|*.cmp|.decompressed|*.dec|All Files (*.*)|*.*";
                IMG.Multiselect = true;

                if (IMG.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    foreach (string SingleImage in IMG.FileNames)
                    {
                        //Check if the EXEs needed exits.
                        string fileExtension = Path.GetExtension(SingleImage.Replace(".cmp", null).Replace(".dec", null));

                        if (fileExtension == ".tga" && !Common.CheckIfEXEExist("convert.exe"))
                        {
                            break;
                        }
                        else if ((fileExtension == ".gxt" || fileExtension == ".btx" || fileExtension == ".SHTX" || fileExtension == ".SHTXFS") && !Common.CheckIfEXEExist("ScarletTestApp.exe"))
                        {
                            break;
                        }
                        else if (fileExtension == ".gim" && !Common.CheckIfEXEExist("GIM2PNG.exe"))
                        {
                            break;
                        }

                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\CONVERTED TO PNG\\FROM " + Path.GetExtension(SingleImage).ToUpper().TrimStart('.') + " TO PNG";

                        Images.ConvertToPNG(SingleImage, DestinationDir.Replace(".", null), false); // false = don't delete the original image
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button20_Click(object sender, EventArgs e) // CONVERT TO TGA 
        {
            using (OpenFileDialog IMG = new OpenFileDialog())
            {
                if (Common.CheckIfEXEExist("convert.exe") == true)
                {
                    IMG.Title = "Select one or more files.";
                    IMG.Filter = "Compatible files|*.png; *.gxt; *.btx; *.SHTX; *.SHTXFS; *.dec; *.cmp|.png|*.png|.gxt|*.gxt|.btx|*.btx|.SHTX|*.SHTX|.SHTXFS|*.SHTXFS|.compressed|*.cmp|.decompressed|*.dec|All Files (*.*)|*.*";
                    IMG.Multiselect = true;

                    if (IMG.ShowDialog() == DialogResult.OK)
                    {
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait...".

                        foreach (string SingleImage in IMG.FileNames)
                        {
                            //Check if the EXEs needed exits.
                            string fileExtension = Path.GetExtension(SingleImage.Replace(".cmp", null).Replace(".dec", null));

                            if ((fileExtension == ".gxt" || fileExtension == ".btx" || fileExtension == ".SHTX" || fileExtension == ".SHTXFS") && !Common.CheckIfEXEExist("ScarletTestApp.exe"))
                            {
                                break;
                            }
                            else if (fileExtension == ".gim" && !Common.CheckIfEXEExist("GIM2PNG.exe"))
                            {
                                break;
                            }

                            string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\CONVERTED TO TGA\\FROM " + Path.GetExtension(SingleImage).ToUpper().TrimStart('.') + " TO TGA";

                            Images.ConvertToTGA(SingleImage, DestinationDir.Replace(".", null), false); // false = don't delete the original image
                        }

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }

        }

        private void Button21_Click(object sender, EventArgs e) // CONVERT TO GXT
        {
            if (Common.CheckIfEXEExist("psp2gxt.exe") == true)
            {
                using (OpenFileDialog IMG = new OpenFileDialog())
                {
                    IMG.Title = "Select one or more files.";
                    IMG.Filter = "Compatible files|*.png; *.tga; *.btx; *.SHTX; *.SHTXFS|.png|*.png|.tga|*.tga|.btx|*.btx|.SHTX|*.SHTX|.SHTXFS|*.SHTXFS|All Files (*.*)|*.*";
                    IMG.Multiselect = true;

                    if (IMG.ShowDialog() == DialogResult.OK)
                    {
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        foreach (string SingleImage in IMG.FileNames)
                        {
                            //Check if the EXEs needed exits.
                            string fileExtension = Path.GetExtension(SingleImage.Replace(".cmp", null).Replace(".dec", null));

                            if (fileExtension == ".tga" && !Common.CheckIfEXEExist("convert.exe"))
                            {
                                break;
                            }
                            else if ((fileExtension == ".btx" || fileExtension == ".SHTX" || fileExtension == ".SHTXFS") && !Common.CheckIfEXEExist("ScarletTestApp.exe"))
                            {
                                break;
                            }
                            else if (fileExtension == ".gim" && !Common.CheckIfEXEExist("GIM2PNG.exe"))
                            {
                                break;
                            }

                            string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\CONVERTED TO GXT\\FROM " + Path.GetExtension(SingleImage).ToUpper().TrimStart('.') + " TO GXT";

                            Images.ConvertToGXT(SingleImage, DestinationDir.Replace(".", null), false); // false = don't delete the original image
                        }

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }
        }

        private void Button22_Click(object sender, EventArgs e) // CONVERT TO BTX 
        {
            if (Common.CheckIfEXEExist("to_BTX.py") == true && Common.CheckIfEXEExist("pngquant.exe") == true && Common.CheckIfEXEExist("util.py") == true)
            {
                using (OpenFileDialog IMG = new OpenFileDialog())
                {
                    IMG.Title = "Select one or more files.";
                    IMG.Filter = "Compatible files|*.png; *.tga; *.gxt; *.dec; *.cmp|.png|*.png|.tga|*.tga|.gxt|*.gxt|.compressed|*.cmp|.decompressed|*.dec|All Files (*.*)|*.*";
                    IMG.Multiselect = true;

                    if (IMG.ShowDialog() == DialogResult.OK)
                    {
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        foreach (string SingleImage in IMG.FileNames)
                        {
                            /*
                            foreach (string SingleImage in IMG.FileNames)
                            {
                                string newdir = Path.Combine(Path.GetDirectoryName(SingleImage), Path.GetFileNameWithoutExtension(SingleImage));
                                string newFilename = Path.GetFileName(SingleImage);
                                if (!Directory.Exists(newdir))
                                    Directory.CreateDirectory(newdir);

                                File.Move(SingleImage, Path.Combine(newdir, newFilename));
                            } */

                            //Check if the EXEs needed exits.
                            string fileExtension = Path.GetExtension(SingleImage.Replace(".cmp", null).Replace(".dec", null));

                            if (fileExtension == ".tga" && !Common.CheckIfEXEExist("convert.exe"))
                            {
                                break;
                            }
                            else if (fileExtension == ".gxt" && !Common.CheckIfEXEExist("ScarletTestApp.exe"))
                            {
                                break;
                            }
                            else if (fileExtension == ".gim" && !Common.CheckIfEXEExist("GIM2PNG.exe"))
                            {
                                break;
                            }

                           string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\CONVERTED TO BXT\\FROM " + Path.GetExtension(SingleImage).ToUpper().TrimStart('.') + " TO BTX";
                          //  string DestinationDir = Path.GetDirectoryName(SingleImage);

                            Images.ConvertToBTX(SingleImage, DestinationDir.Replace(".", null), false); // false = don't delete the original image
                        }

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Liquid-S");
        }

        private void Button23_Click(object sender, EventArgs e) // EXTRACT ".BND"
        {
            using (OpenFileDialog BNDFile = new OpenFileDialog())
            {
                BNDFile.Title = "Select one or more \"file.bnd\"";
                BNDFile.Filter = "Supported files|item.bnd; kotodama.bnd; oneshot.bnd; skill.bnd; strings.bnd; item64.bnd; kotodama64.bnd; oneshot64.bnd; skill64.bnd; strings64.bnd|All Files (*.*)|*.*";
                BNDFile.Multiselect = true;

                if (BNDFile.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    foreach (string SingleBND in BNDFile.FileNames)
                    {
                        string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\BND\\" + Path.GetFileNameWithoutExtension(SingleBND);

                        BND.ExtractBND(SingleBND, DestinationDir);
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void Button24_Click(object sender, EventArgs e) // REPACK ".BND"
        {
            string OriginalDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\EXTRACTED\\BND";

            if (chooseFreelyTheFolderToRepackToolStripMenuItem.Checked == true)
            {
                OriginalDir = Common.ChooseFolder(OriginalDir);
            }

            // If "EXTRACTED LIN" exists and it's not empty.
            if (Common.CheckDirExistence(OriginalDir) == true)
            {
                Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\REPACKED\\BND";

                // If the destination folder does not exist, create it.
                if (Directory.Exists(DestinationDir) == false)
                {
                    Directory.CreateDirectory(DestinationDir);
                }

                // For each LIN folder to be repacked.
                foreach (string DirBNDToBeRepacked in Directory.GetDirectories(OriginalDir, "*", SearchOption.TopDirectoryOnly))
                {
                    // Continue only if there is a text file or if the user has chosen to work with files composed only by the bytecode too.
                    if (File.Exists(Path.Combine(DirBNDToBeRepacked, Path.GetFileNameWithoutExtension(DirBNDToBeRepacked) + ".original")) == true
                        && File.Exists(Path.Combine(DirBNDToBeRepacked, Path.GetFileNameWithoutExtension(DirBNDToBeRepacked) + ".po")) == true)
                    {
                        BND.RePackBND(DirBNDToBeRepacked, DestinationDir);
                    }
                }

                Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
            }
        }

        private void Button25_Click(object sender, EventArgs e) // Convert Xml ToPo
        {
            using (OpenFileDialog XML = new OpenFileDialog())
            {
                XML.Title = "Select one or more \"file.xml\"";
                XML.Filter = ".xml|*.xml|All Files (*.*)|*.*";
                XML.Multiselect = true;

                if (XML.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait...".

                    foreach (string SingleXML in XML.FileNames)
                    {
                        CommonTextStuff.ConvertXmlToPo(SingleXML);
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }

        private void ButtonCompressFIles_Click(object sender, EventArgs e)
        {
            if (Common.CheckIfEXEExist("../Kontract.dll") == true)
            {
                using (OpenFileDialog filesToBeCompressed = new OpenFileDialog())
                {
                    filesToBeCompressed.Title = "Select one or more files to compress";
                    filesToBeCompressed.Filter = "All Files (*.*)|*.*";
                    filesToBeCompressed.Multiselect = true;

                    if (filesToBeCompressed.ShowDialog() == DialogResult.OK)
                    {
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        foreach (string SingleFile in filesToBeCompressed.FileNames)
                        {
                            string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\COMPRESSED FILES\\" + Path.GetExtension(SingleFile).ToUpper().TrimStart('.');

                            Common.CompressFileSpikeCompression(SingleFile, DestinationDir, false);
                            // false = don't replace the original.
                        }

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }
        }

        private void ButtonDecompressFiles_Click(object sender, EventArgs e)
        {
            if (Common.CheckIfEXEExist("../Kontract.dll") == true)
            {
                using (OpenFileDialog filesToBeDecompressed = new OpenFileDialog())
                {
                    filesToBeDecompressed.Title = "Select one or more files to decompress";
                    filesToBeDecompressed.Filter = "All Files (*.*)|*.*";
                    filesToBeDecompressed.Multiselect = true;

                    if (filesToBeDecompressed.ShowDialog() == DialogResult.OK)
                    {
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        foreach (string SingleFile in filesToBeDecompressed.FileNames)
                        {
                            string DestinationDir = tabControl1.SelectedTab.Text + " [MANUAL MODE]\\DECOMPRESSED FILES\\" + Path.GetExtension(SingleFile).ToUpper().TrimStart('.');

                            Common.DecompressFileSpikeCompression(SingleFile, DestinationDir, false);
                            // false = don't replace the original.
                        }

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                    }
                }
            }
        }

        private void buttonUnpackWADAutoMode_Click(object sender, EventArgs e)
        {

        }

        private void buttonRepackWADAutoMode_Click(object sender, EventArgs e)
        {

        }

        private void buttonExtractCPKAutoMode_Click(object sender, EventArgs e)
        {

        }

        private void buttonRepackCPKAutoMode_Click(object sender, EventArgs e)
        {

        }

        private void buttonaPoToTranslateOtherPos_Click(object sender, EventArgs e)
        {

                string ToTranslateDir = string.Empty;
            //ToTranslateDir = "D:\\DRAT\\DRAE (PC) [MANUAL MODE]\\EXTRACTED\\BND";
            ToTranslateDir = Common.ChooseFolder(ToTranslateDir);

            string OriginalDir = string.Empty;
            //OriginalDir = "C:\\Users\\Acer\\Desktop\\cccc";
            OriginalDir = Common.ChooseFolder(OriginalDir);

            DialogResult dialogResult = MessageBox.Show("Are you sure you sure you want to proceed? This is going to translate or retranslate the target \".PO\"", "Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.Yes)
                    {
                        Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                        foreach (string ToTranslate in Directory.GetFiles(ToTranslateDir, "*.po", SearchOption.AllDirectories))
                        {
                            string originalFile = Path.Combine(OriginalDir, Path.GetFileNameWithoutExtension(ToTranslate) + ".po");

                            if (File.Exists(originalFile))
                            CommonTextStuff.TranslatePOwithAnotherPO(originalFile, ToTranslate);
                        }

                        Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
            }

            // Code used for my projects
             //using (OpenFileDialog BasePO = new OpenFileDialog())
             //{
             //    BasePO.Title = "Select the \".PO\" you want to use to translate the others PO.";
             //    BasePO.Filter = ".po|*.po|All Files (*.*)|*.*";
             //    BasePO.Multiselect = false;

             //    if (BasePO.ShowDialog() == DialogResult.OK)
             //        using (OpenFileDialog TargetPO = new OpenFileDialog())
             //        {
             //            TargetPO.Title = "Select the target or targets.";
             //            TargetPO.Filter = ".po|*.po|All Files (*.*)|*.*";
             //            TargetPO.Multiselect = false;

             //            if (TargetPO.ShowDialog() == DialogResult.OK)
             //            {
             //                DialogResult dialogResult = MessageBox.Show("Are you sure you sure you want to proceed? This is going to translate or retranslate the target \".PO\"", "Proceed?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

             //                if (dialogResult == DialogResult.Yes)
             //                {
             //                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

             //                    foreach (string SinglePO in TargetPO.FileNames)
             //                        CommonTextStuff.TranslatePOwithAnotherPO(BasePO.FileName, SinglePO);


             //                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
             //                }

             //            }
             //        }
             //}
        }
    }
}