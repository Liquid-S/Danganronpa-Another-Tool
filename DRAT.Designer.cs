namespace Danganronpa_Another_Tool
{
    public partial class DRAT
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            tableLayoutPanelDR1PSP = new System.Windows.Forms.TableLayoutPanel();
            buttonExtractUmdImage = new System.Windows.Forms.Button();
            buttonRepackUmdImage = new System.Windows.Forms.Button();
            tabPage2 = new System.Windows.Forms.TabPage();
            tableLayoutPanelDR1PSVITA = new System.Windows.Forms.TableLayoutPanel();
            buttonExtractCPK = new System.Windows.Forms.Button();
            buttonRepackCPK = new System.Windows.Forms.Button();
            labelCompression = new System.Windows.Forms.Label();
            buttonDecompressFiles = new System.Windows.Forms.Button();
            buttonCompressFIles = new System.Windows.Forms.Button();
            tabPage3 = new System.Windows.Forms.TabPage();
            tableLayoutPanelDR1PC = new System.Windows.Forms.TableLayoutPanel();
            buttonRepackPakToPNG = new System.Windows.Forms.Button();
            buttonExtractPakToPNG = new System.Windows.Forms.Button();
            buttonRepackPakType1 = new System.Windows.Forms.Button();
            buttonRepacktPakWOConvert16 = new System.Windows.Forms.Button();
            buttonConvertToTGA = new System.Windows.Forms.Button();
            buttonExtractPakType2 = new System.Windows.Forms.Button();
            buttonConverBTX = new System.Windows.Forms.Button();
            buttonConvertToPng = new System.Windows.Forms.Button();
            buttonRepackPakType2 = new System.Windows.Forms.Button();
            buttonExtractPakWOConvert = new System.Windows.Forms.Button();
            buttonExtractPakType3 = new System.Windows.Forms.Button();
            buttonRepackPakType3 = new System.Windows.Forms.Button();
            buttonExtractPakType1 = new System.Windows.Forms.Button();
            buttonRepackLin = new System.Windows.Forms.Button();
            labelTextures = new System.Windows.Forms.Label();
            labelTexts = new System.Windows.Forms.Label();
            buttonExtractLin = new System.Windows.Forms.Button();
            labelUtilities = new System.Windows.Forms.Label();
            buttonConvertGXT = new System.Windows.Forms.Button();
            buttonConvertXmlToPo = new System.Windows.Forms.Button();
            buttonaPoToTranslateOtherPos = new System.Windows.Forms.Button();
            labelDataAssets = new System.Windows.Forms.Label();
            buttonUnpackWAD = new System.Windows.Forms.Button();
            buttonRepackWAD = new System.Windows.Forms.Button();
            tabPage4 = new System.Windows.Forms.TabPage();
            tableLayoutPanelDR2PSVITA = new System.Windows.Forms.TableLayoutPanel();
            tabPage5 = new System.Windows.Forms.TabPage();
            tableLayoutPanelDR2PC = new System.Windows.Forms.TableLayoutPanel();
            tabPage6 = new System.Windows.Forms.TabPage();
            tableLayoutPanelDRAEPSVITA = new System.Windows.Forms.TableLayoutPanel();
            buttonExtractBND = new System.Windows.Forms.Button();
            buttonRepackBND = new System.Windows.Forms.Button();
            tabPage7 = new System.Windows.Forms.TabPage();
            tableLayoutPanelDRAEPC = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            labelStatusText = new System.Windows.Forms.Label();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            repackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            chooseFreelyTheFolderToRepackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ignoreLINWoTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            eraseExtraLinefeedsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aDDJAPANESETEXTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            hIDESPEAKERSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            cLEANPSPCLTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toBTXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toBTXToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            toolStripComboBoxBTXType = new System.Windows.Forms.ToolStripComboBox();
            removeAdditionalDataFromExtensionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            labelStatusName = new System.Windows.Forms.Label();
            linkLabelCredits = new System.Windows.Forms.LinkLabel();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tableLayoutPanelDR1PSP.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanelDR1PSVITA.SuspendLayout();
            tabPage3.SuspendLayout();
            tableLayoutPanelDR1PC.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage6.SuspendLayout();
            tableLayoutPanelDRAEPSVITA.SuspendLayout();
            tabPage7.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 28);
            tabControl1.Margin = new System.Windows.Forms.Padding(0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(968, 380);
            tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.Transparent;
            tabPage1.BackgroundImage = global::Danganronpa_Another_Tool.Properties.Resources.save_bg1;
            tabPage1.Controls.Add(tableLayoutPanelDR1PSP);
            tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new System.Drawing.Size(960, 354);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "DR1 (PSP)";
            // 
            // tableLayoutPanelDR1PSP
            // 
            tableLayoutPanelDR1PSP.ColumnCount = 7;
            tableLayoutPanelDR1PSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR1PSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR1PSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSP.Controls.Add(buttonExtractUmdImage, 0, 1);
            tableLayoutPanelDR1PSP.Controls.Add(buttonRepackUmdImage, 0, 2);
            tableLayoutPanelDR1PSP.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelDR1PSP.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelDR1PSP.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelDR1PSP.Name = "tableLayoutPanelDR1PSP";
            tableLayoutPanelDR1PSP.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            tableLayoutPanelDR1PSP.RowCount = 12;
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSP.Size = new System.Drawing.Size(960, 354);
            tableLayoutPanelDR1PSP.TabIndex = 0;
            // 
            // buttonExtractUmdImage
            // 
            buttonExtractUmdImage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            buttonExtractUmdImage.BackColor = System.Drawing.SystemColors.InfoText;
            buttonExtractUmdImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractUmdImage.ForeColor = System.Drawing.Color.White;
            buttonExtractUmdImage.Location = new System.Drawing.Point(2, 23);
            buttonExtractUmdImage.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractUmdImage.Name = "buttonExtractUmdImage";
            buttonExtractUmdImage.Size = new System.Drawing.Size(180, 55);
            buttonExtractUmdImage.TabIndex = 9;
            buttonExtractUmdImage.Text = "EXTRACT FULL GAME\r\n\"UMDIMAGE.DAT\"";
            buttonExtractUmdImage.UseVisualStyleBackColor = false;
            buttonExtractUmdImage.Click += new System.EventHandler(Button5_Click);
            // 
            // buttonRepackUmdImage
            // 
            buttonRepackUmdImage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            buttonRepackUmdImage.BackColor = System.Drawing.SystemColors.InfoText;
            buttonRepackUmdImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackUmdImage.ForeColor = System.Drawing.Color.White;
            buttonRepackUmdImage.Location = new System.Drawing.Point(2, 82);
            buttonRepackUmdImage.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackUmdImage.Name = "buttonRepackUmdImage";
            buttonRepackUmdImage.Size = new System.Drawing.Size(180, 55);
            buttonRepackUmdImage.TabIndex = 28;
            buttonRepackUmdImage.Text = "EXTRACT DEMO\r\n\"UMDIMAGE.DAT\"";
            buttonRepackUmdImage.UseVisualStyleBackColor = false;
            buttonRepackUmdImage.Click += new System.EventHandler(Button6_Click);
            // 
            // tabPage2
            // 
            tabPage2.BackgroundImage = global::Danganronpa_Another_Tool.Properties.Resources.save_bg1;
            tabPage2.Controls.Add(tableLayoutPanelDR1PSVITA);
            tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tabPage2.Location = new System.Drawing.Point(4, 22);
            tabPage2.Name = "tabPage2";
            tabPage2.Size = new System.Drawing.Size(960, 354);
            tabPage2.TabIndex = 2;
            tabPage2.Text = "DR1 (PSVITA)";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelDR1PSVITA
            // 
            tableLayoutPanelDR1PSVITA.ColumnCount = 7;
            tableLayoutPanelDR1PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR1PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR1PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PSVITA.Controls.Add(buttonExtractCPK, 0, 1);
            tableLayoutPanelDR1PSVITA.Controls.Add(buttonRepackCPK, 0, 2);
            tableLayoutPanelDR1PSVITA.Controls.Add(labelCompression, 0, 3);
            tableLayoutPanelDR1PSVITA.Controls.Add(buttonDecompressFiles, 0, 4);
            tableLayoutPanelDR1PSVITA.Controls.Add(buttonCompressFIles, 0, 5);
            tableLayoutPanelDR1PSVITA.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelDR1PSVITA.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelDR1PSVITA.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelDR1PSVITA.Name = "tableLayoutPanelDR1PSVITA";
            tableLayoutPanelDR1PSVITA.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            tableLayoutPanelDR1PSVITA.RowCount = 12;
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PSVITA.Size = new System.Drawing.Size(960, 354);
            tableLayoutPanelDR1PSVITA.TabIndex = 1;
            // 
            // buttonExtractCPK
            // 
            buttonExtractCPK.BackColor = System.Drawing.Color.Black;
            buttonExtractCPK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractCPK.ForeColor = System.Drawing.Color.White;
            buttonExtractCPK.Location = new System.Drawing.Point(2, 23);
            buttonExtractCPK.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractCPK.Name = "buttonExtractCPK";
            buttonExtractCPK.Size = new System.Drawing.Size(180, 55);
            buttonExtractCPK.TabIndex = 7;
            buttonExtractCPK.Text = "EXTRACT \".CPK\"";
            buttonExtractCPK.UseVisualStyleBackColor = false;
            buttonExtractCPK.Click += new System.EventHandler(Button3_Click);
            // 
            // buttonRepackCPK
            // 
            buttonRepackCPK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepackCPK.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackCPK.ForeColor = System.Drawing.Color.White;
            buttonRepackCPK.Location = new System.Drawing.Point(2, 82);
            buttonRepackCPK.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackCPK.Name = "buttonRepackCPK";
            buttonRepackCPK.Size = new System.Drawing.Size(180, 55);
            buttonRepackCPK.TabIndex = 8;
            buttonRepackCPK.Text = "REPACK \".CPK\"";
            buttonRepackCPK.UseVisualStyleBackColor = false;
            buttonRepackCPK.Click += new System.EventHandler(Button4_Click);
            // 
            // labelCompression
            // 
            labelCompression.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            labelCompression.AutoSize = true;
            labelCompression.BackColor = System.Drawing.SystemColors.ControlLight;
            labelCompression.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelCompression.ForeColor = System.Drawing.Color.Black;
            labelCompression.Location = new System.Drawing.Point(3, 140);
            labelCompression.Name = "labelCompression";
            labelCompression.Size = new System.Drawing.Size(178, 15);
            labelCompression.TabIndex = 30;
            labelCompression.Text = "COMPRESSION";
            labelCompression.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonDecompressFiles
            // 
            buttonDecompressFiles.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonDecompressFiles.BackColor = System.Drawing.Color.Black;
            buttonDecompressFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonDecompressFiles.ForeColor = System.Drawing.Color.White;
            buttonDecompressFiles.Location = new System.Drawing.Point(2, 159);
            buttonDecompressFiles.Margin = new System.Windows.Forms.Padding(2);
            buttonDecompressFiles.Name = "buttonDecompressFiles";
            buttonDecompressFiles.Size = new System.Drawing.Size(180, 55);
            buttonDecompressFiles.TabIndex = 31;
            buttonDecompressFiles.Text = "DECOMPRESS FILES";
            buttonDecompressFiles.UseVisualStyleBackColor = false;
            buttonDecompressFiles.Click += new System.EventHandler(ButtonDecompressFiles_Click);
            // 
            // buttonCompressFIles
            // 
            buttonCompressFIles.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonCompressFIles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonCompressFIles.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonCompressFIles.ForeColor = System.Drawing.Color.White;
            buttonCompressFIles.Location = new System.Drawing.Point(2, 218);
            buttonCompressFIles.Margin = new System.Windows.Forms.Padding(2);
            buttonCompressFIles.Name = "buttonCompressFIles";
            buttonCompressFIles.Size = new System.Drawing.Size(180, 55);
            buttonCompressFIles.TabIndex = 32;
            buttonCompressFIles.Text = "COMPRESS FILES";
            buttonCompressFIles.UseVisualStyleBackColor = false;
            buttonCompressFIles.Click += new System.EventHandler(ButtonCompressFIles_Click);
            // 
            // tabPage3
            // 
            tabPage3.BackgroundImage = global::Danganronpa_Another_Tool.Properties.Resources.save_bg1;
            tabPage3.Controls.Add(tableLayoutPanelDR1PC);
            tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tabPage3.Location = new System.Drawing.Point(4, 22);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new System.Drawing.Size(960, 354);
            tabPage3.TabIndex = 1;
            tabPage3.Text = "DR1 (PC)";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelDR1PC
            // 
            tableLayoutPanelDR1PC.ColumnCount = 7;
            tableLayoutPanelDR1PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR1PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR1PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR1PC.Controls.Add(buttonRepackPakToPNG, 6, 2);
            tableLayoutPanelDR1PC.Controls.Add(buttonExtractPakToPNG, 5, 2);
            tableLayoutPanelDR1PC.Controls.Add(buttonRepackPakType1, 3, 2);
            tableLayoutPanelDR1PC.Controls.Add(buttonRepacktPakWOConvert16, 6, 1);
            tableLayoutPanelDR1PC.Controls.Add(buttonConvertToTGA, 6, 4);
            tableLayoutPanelDR1PC.Controls.Add(buttonExtractPakType2, 2, 4);
            tableLayoutPanelDR1PC.Controls.Add(buttonConverBTX, 6, 5);
            tableLayoutPanelDR1PC.Controls.Add(buttonConvertToPng, 5, 4);
            tableLayoutPanelDR1PC.Controls.Add(buttonRepackPakType2, 3, 4);
            tableLayoutPanelDR1PC.Controls.Add(buttonExtractPakWOConvert, 5, 1);
            tableLayoutPanelDR1PC.Controls.Add(buttonExtractPakType3, 2, 5);
            tableLayoutPanelDR1PC.Controls.Add(buttonRepackPakType3, 3, 5);
            tableLayoutPanelDR1PC.Controls.Add(buttonExtractPakType1, 2, 2);
            tableLayoutPanelDR1PC.Controls.Add(buttonRepackLin, 3, 1);
            tableLayoutPanelDR1PC.Controls.Add(labelTextures, 5, 0);
            tableLayoutPanelDR1PC.Controls.Add(labelTexts, 2, 0);
            tableLayoutPanelDR1PC.Controls.Add(buttonExtractLin, 2, 1);
            tableLayoutPanelDR1PC.Controls.Add(labelUtilities, 5, 6);
            tableLayoutPanelDR1PC.Controls.Add(buttonConvertGXT, 5, 5);
            tableLayoutPanelDR1PC.Controls.Add(buttonConvertXmlToPo, 5, 7);
            tableLayoutPanelDR1PC.Controls.Add(buttonaPoToTranslateOtherPos, 6, 7);
            tableLayoutPanelDR1PC.Controls.Add(labelDataAssets, 0, 0);
            tableLayoutPanelDR1PC.Controls.Add(buttonUnpackWAD, 0, 1);
            tableLayoutPanelDR1PC.Controls.Add(buttonRepackWAD, 0, 2);
            tableLayoutPanelDR1PC.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelDR1PC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tableLayoutPanelDR1PC.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelDR1PC.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelDR1PC.Name = "tableLayoutPanelDR1PC";
            tableLayoutPanelDR1PC.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            tableLayoutPanelDR1PC.RowCount = 12;
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR1PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR1PC.Size = new System.Drawing.Size(960, 354);
            tableLayoutPanelDR1PC.TabIndex = 0;
            // 
            // buttonRepackPakToPNG
            // 
            buttonRepackPakToPNG.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonRepackPakToPNG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepackPakToPNG.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackPakToPNG.ForeColor = System.Drawing.Color.White;
            buttonRepackPakToPNG.Location = new System.Drawing.Point(778, 82);
            buttonRepackPakToPNG.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackPakToPNG.Name = "buttonRepackPakToPNG";
            buttonRepackPakToPNG.Size = new System.Drawing.Size(180, 55);
            buttonRepackPakToPNG.TabIndex = 11;
            buttonRepackPakToPNG.Text = "REPACK TEXTURE\r\n\".PAK\" FROM PNG";
            buttonRepackPakToPNG.UseVisualStyleBackColor = false;
            buttonRepackPakToPNG.Click += new System.EventHandler(Button18_Click);
            // 
            // buttonExtractPakToPNG
            // 
            buttonExtractPakToPNG.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonExtractPakToPNG.BackColor = System.Drawing.SystemColors.InfoText;
            buttonExtractPakToPNG.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractPakToPNG.ForeColor = System.Drawing.Color.White;
            buttonExtractPakToPNG.Location = new System.Drawing.Point(594, 82);
            buttonExtractPakToPNG.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractPakToPNG.Name = "buttonExtractPakToPNG";
            buttonExtractPakToPNG.Size = new System.Drawing.Size(180, 55);
            buttonExtractPakToPNG.TabIndex = 10;
            buttonExtractPakToPNG.Text = "EXTRACT TEXTURE\r\n\".PAK\" TO PNG";
            buttonExtractPakToPNG.UseVisualStyleBackColor = false;
            buttonExtractPakToPNG.Click += new System.EventHandler(Button17_Click);
            // 
            // buttonRepackPakType1
            // 
            buttonRepackPakType1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonRepackPakType1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepackPakType1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackPakType1.ForeColor = System.Drawing.Color.White;
            buttonRepackPakType1.Location = new System.Drawing.Point(390, 82);
            buttonRepackPakType1.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackPakType1.Name = "buttonRepackPakType1";
            buttonRepackPakType1.Size = new System.Drawing.Size(180, 55);
            buttonRepackPakType1.TabIndex = 17;
            buttonRepackPakType1.Text = "REPACK \".PAK\" TYPE 1\r\n(SIMPLE TEXT)";
            buttonRepackPakType1.UseVisualStyleBackColor = false;
            buttonRepackPakType1.Click += new System.EventHandler(Button10_Click);
            // 
            // buttonRepacktPakWOConvert16
            // 
            buttonRepacktPakWOConvert16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonRepacktPakWOConvert16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepacktPakWOConvert16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepacktPakWOConvert16.ForeColor = System.Drawing.Color.White;
            buttonRepacktPakWOConvert16.Location = new System.Drawing.Point(778, 23);
            buttonRepacktPakWOConvert16.Margin = new System.Windows.Forms.Padding(2);
            buttonRepacktPakWOConvert16.Name = "buttonRepacktPakWOConvert16";
            buttonRepacktPakWOConvert16.Size = new System.Drawing.Size(180, 55);
            buttonRepacktPakWOConvert16.TabIndex = 9;
            buttonRepacktPakWOConvert16.Text = "REPACK TEXTURE\r\n\".PAK\" W/O CONVERT";
            buttonRepacktPakWOConvert16.UseVisualStyleBackColor = false;
            buttonRepacktPakWOConvert16.Click += new System.EventHandler(Button16_Click);
            // 
            // buttonConvertToTGA
            // 
            buttonConvertToTGA.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonConvertToTGA.BackColor = System.Drawing.SystemColors.InfoText;
            buttonConvertToTGA.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonConvertToTGA.ForeColor = System.Drawing.Color.White;
            buttonConvertToTGA.Location = new System.Drawing.Point(778, 159);
            buttonConvertToTGA.Margin = new System.Windows.Forms.Padding(2);
            buttonConvertToTGA.Name = "buttonConvertToTGA";
            buttonConvertToTGA.Size = new System.Drawing.Size(180, 55);
            buttonConvertToTGA.TabIndex = 13;
            buttonConvertToTGA.Text = "CONVERT TO\r\nTGA";
            buttonConvertToTGA.UseVisualStyleBackColor = false;
            buttonConvertToTGA.Click += new System.EventHandler(Button20_Click);
            // 
            // buttonExtractPakType2
            // 
            buttonExtractPakType2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonExtractPakType2.BackColor = System.Drawing.Color.Black;
            buttonExtractPakType2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractPakType2.ForeColor = System.Drawing.Color.White;
            buttonExtractPakType2.Location = new System.Drawing.Point(206, 159);
            buttonExtractPakType2.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractPakType2.Name = "buttonExtractPakType2";
            buttonExtractPakType2.Size = new System.Drawing.Size(180, 55);
            buttonExtractPakType2.TabIndex = 18;
            buttonExtractPakType2.Text = "EXTRACT \".PAK\" TYPE 2\r\n(\".LIN\"S INSIDE \".PAK\")";
            buttonExtractPakType2.UseVisualStyleBackColor = false;
            buttonExtractPakType2.Click += new System.EventHandler(Button11_Click);
            // 
            // buttonConverBTX
            // 
            buttonConverBTX.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonConverBTX.BackColor = System.Drawing.SystemColors.InfoText;
            buttonConverBTX.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonConverBTX.ForeColor = System.Drawing.Color.White;
            buttonConverBTX.Location = new System.Drawing.Point(778, 218);
            buttonConverBTX.Margin = new System.Windows.Forms.Padding(2);
            buttonConverBTX.Name = "buttonConverBTX";
            buttonConverBTX.Size = new System.Drawing.Size(180, 55);
            buttonConverBTX.TabIndex = 15;
            buttonConverBTX.Text = "CONVERT TO\r\nBTX";
            buttonConverBTX.UseVisualStyleBackColor = false;
            buttonConverBTX.Click += new System.EventHandler(Button22_Click);
            // 
            // buttonConvertToPng
            // 
            buttonConvertToPng.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonConvertToPng.BackColor = System.Drawing.SystemColors.InfoText;
            buttonConvertToPng.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonConvertToPng.ForeColor = System.Drawing.Color.White;
            buttonConvertToPng.Location = new System.Drawing.Point(594, 159);
            buttonConvertToPng.Margin = new System.Windows.Forms.Padding(2);
            buttonConvertToPng.Name = "buttonConvertToPng";
            buttonConvertToPng.Size = new System.Drawing.Size(180, 55);
            buttonConvertToPng.TabIndex = 12;
            buttonConvertToPng.Text = "CONVERT TO\r\nPNG";
            buttonConvertToPng.UseVisualStyleBackColor = false;
            buttonConvertToPng.Click += new System.EventHandler(Button19_Click);
            // 
            // buttonRepackPakType2
            // 
            buttonRepackPakType2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonRepackPakType2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepackPakType2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackPakType2.ForeColor = System.Drawing.Color.White;
            buttonRepackPakType2.Location = new System.Drawing.Point(390, 159);
            buttonRepackPakType2.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackPakType2.Name = "buttonRepackPakType2";
            buttonRepackPakType2.Size = new System.Drawing.Size(180, 55);
            buttonRepackPakType2.TabIndex = 19;
            buttonRepackPakType2.Text = "REPACK \".PAK\" TYPE 2\r\n(\".LIN\"S INSIDE \".PAK\")";
            buttonRepackPakType2.UseVisualStyleBackColor = false;
            buttonRepackPakType2.Click += new System.EventHandler(Button12_Click);
            // 
            // buttonExtractPakWOConvert
            // 
            buttonExtractPakWOConvert.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonExtractPakWOConvert.BackColor = System.Drawing.SystemColors.InfoText;
            buttonExtractPakWOConvert.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractPakWOConvert.ForeColor = System.Drawing.Color.White;
            buttonExtractPakWOConvert.Location = new System.Drawing.Point(594, 23);
            buttonExtractPakWOConvert.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractPakWOConvert.Name = "buttonExtractPakWOConvert";
            buttonExtractPakWOConvert.Size = new System.Drawing.Size(180, 55);
            buttonExtractPakWOConvert.TabIndex = 8;
            buttonExtractPakWOConvert.Text = "EXTRACT TEXTURE\r\n\".PAK\" W/O CONVERT";
            buttonExtractPakWOConvert.UseVisualStyleBackColor = false;
            buttonExtractPakWOConvert.Click += new System.EventHandler(Button15_Click);
            // 
            // buttonExtractPakType3
            // 
            buttonExtractPakType3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonExtractPakType3.BackColor = System.Drawing.Color.Black;
            buttonExtractPakType3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractPakType3.ForeColor = System.Drawing.Color.White;
            buttonExtractPakType3.Location = new System.Drawing.Point(206, 218);
            buttonExtractPakType3.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractPakType3.Name = "buttonExtractPakType3";
            buttonExtractPakType3.Size = new System.Drawing.Size(180, 55);
            buttonExtractPakType3.TabIndex = 20;
            buttonExtractPakType3.Text = "EXTRACT \".PAK\" TYPE 3\r\n(\".PAK\"S INSIDE \".PAK\")";
            buttonExtractPakType3.UseVisualStyleBackColor = false;
            buttonExtractPakType3.Click += new System.EventHandler(Button13_Click);
            // 
            // buttonRepackPakType3
            // 
            buttonRepackPakType3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonRepackPakType3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepackPakType3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackPakType3.ForeColor = System.Drawing.Color.White;
            buttonRepackPakType3.Location = new System.Drawing.Point(390, 218);
            buttonRepackPakType3.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackPakType3.Name = "buttonRepackPakType3";
            buttonRepackPakType3.Size = new System.Drawing.Size(180, 55);
            buttonRepackPakType3.TabIndex = 21;
            buttonRepackPakType3.Text = "REPACK \".PAK\" TYPE 3\r\n(\".PAK\"S INSIDE \".PAK\")";
            buttonRepackPakType3.UseVisualStyleBackColor = false;
            buttonRepackPakType3.Click += new System.EventHandler(Button14_Click);
            // 
            // buttonExtractPakType1
            // 
            buttonExtractPakType1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonExtractPakType1.BackColor = System.Drawing.Color.Black;
            buttonExtractPakType1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractPakType1.ForeColor = System.Drawing.Color.White;
            buttonExtractPakType1.Location = new System.Drawing.Point(206, 82);
            buttonExtractPakType1.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractPakType1.Name = "buttonExtractPakType1";
            buttonExtractPakType1.Size = new System.Drawing.Size(180, 55);
            buttonExtractPakType1.TabIndex = 16;
            buttonExtractPakType1.Text = "EXTRACT \".PAK\" TYPE 1\r\n(SIMPLE TEXT)";
            buttonExtractPakType1.UseVisualStyleBackColor = false;
            buttonExtractPakType1.Click += new System.EventHandler(Button9_Click);
            // 
            // buttonRepackLin
            // 
            buttonRepackLin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonRepackLin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepackLin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackLin.ForeColor = System.Drawing.Color.White;
            buttonRepackLin.Location = new System.Drawing.Point(390, 23);
            buttonRepackLin.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackLin.Name = "buttonRepackLin";
            buttonRepackLin.Size = new System.Drawing.Size(180, 55);
            buttonRepackLin.TabIndex = 15;
            buttonRepackLin.Text = "REPACK \".LIN\"";
            buttonRepackLin.UseVisualStyleBackColor = false;
            buttonRepackLin.Click += new System.EventHandler(Button8_Click);
            // 
            // labelTextures
            // 
            labelTextures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            labelTextures.AutoSize = true;
            labelTextures.BackColor = System.Drawing.SystemColors.ControlLight;
            tableLayoutPanelDR1PC.SetColumnSpan(labelTextures, 2);
            labelTextures.ForeColor = System.Drawing.Color.Black;
            labelTextures.Location = new System.Drawing.Point(595, 4);
            labelTextures.Name = "labelTextures";
            labelTextures.Size = new System.Drawing.Size(362, 15);
            labelTextures.TabIndex = 26;
            labelTextures.Text = "TEXTURES";
            labelTextures.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTexts
            // 
            labelTexts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            labelTexts.AutoSize = true;
            labelTexts.BackColor = System.Drawing.SystemColors.ControlLight;
            tableLayoutPanelDR1PC.SetColumnSpan(labelTexts, 2);
            labelTexts.ForeColor = System.Drawing.Color.Black;
            labelTexts.Location = new System.Drawing.Point(207, 4);
            labelTexts.Name = "labelTexts";
            labelTexts.Size = new System.Drawing.Size(362, 15);
            labelTexts.TabIndex = 25;
            labelTexts.Text = "TEXTS";
            labelTexts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonExtractLin
            // 
            buttonExtractLin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonExtractLin.BackColor = System.Drawing.Color.Black;
            buttonExtractLin.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractLin.ForeColor = System.Drawing.Color.White;
            buttonExtractLin.Location = new System.Drawing.Point(206, 23);
            buttonExtractLin.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractLin.Name = "buttonExtractLin";
            buttonExtractLin.Size = new System.Drawing.Size(180, 55);
            buttonExtractLin.TabIndex = 5;
            buttonExtractLin.Text = "EXTRACT \".LIN\"";
            buttonExtractLin.UseVisualStyleBackColor = false;
            buttonExtractLin.Click += new System.EventHandler(Button7_Click);
            // 
            // labelUtilities
            // 
            labelUtilities.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            labelUtilities.AutoSize = true;
            labelUtilities.BackColor = System.Drawing.SystemColors.ControlLight;
            tableLayoutPanelDR1PC.SetColumnSpan(labelUtilities, 2);
            labelUtilities.ForeColor = System.Drawing.Color.Black;
            labelUtilities.Location = new System.Drawing.Point(595, 276);
            labelUtilities.Name = "labelUtilities";
            labelUtilities.Size = new System.Drawing.Size(362, 15);
            labelUtilities.TabIndex = 32;
            labelUtilities.Text = "UTILITIES";
            labelUtilities.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonConvertGXT
            // 
            buttonConvertGXT.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonConvertGXT.BackColor = System.Drawing.SystemColors.InfoText;
            buttonConvertGXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonConvertGXT.ForeColor = System.Drawing.Color.White;
            buttonConvertGXT.Location = new System.Drawing.Point(594, 218);
            buttonConvertGXT.Margin = new System.Windows.Forms.Padding(2);
            buttonConvertGXT.Name = "buttonConvertGXT";
            buttonConvertGXT.Size = new System.Drawing.Size(180, 55);
            buttonConvertGXT.TabIndex = 14;
            buttonConvertGXT.Text = "CONVERT TO\r\nGXT";
            buttonConvertGXT.UseVisualStyleBackColor = false;
            buttonConvertGXT.Click += new System.EventHandler(Button21_Click);
            // 
            // buttonConvertXmlToPo
            // 
            buttonConvertXmlToPo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonConvertXmlToPo.BackColor = System.Drawing.SystemColors.InfoText;
            buttonConvertXmlToPo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonConvertXmlToPo.ForeColor = System.Drawing.Color.White;
            buttonConvertXmlToPo.Location = new System.Drawing.Point(594, 295);
            buttonConvertXmlToPo.Margin = new System.Windows.Forms.Padding(2);
            buttonConvertXmlToPo.Name = "buttonConvertXmlToPo";
            buttonConvertXmlToPo.Size = new System.Drawing.Size(180, 55);
            buttonConvertXmlToPo.TabIndex = 27;
            buttonConvertXmlToPo.Text = "CONVERT XML\r\nTO PO";
            buttonConvertXmlToPo.UseVisualStyleBackColor = false;
            buttonConvertXmlToPo.Click += new System.EventHandler(Button25_Click);
            // 
            // buttonaPoToTranslateOtherPos
            // 
            buttonaPoToTranslateOtherPos.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonaPoToTranslateOtherPos.BackColor = System.Drawing.SystemColors.InfoText;
            buttonaPoToTranslateOtherPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonaPoToTranslateOtherPos.ForeColor = System.Drawing.Color.White;
            buttonaPoToTranslateOtherPos.Location = new System.Drawing.Point(778, 295);
            buttonaPoToTranslateOtherPos.Margin = new System.Windows.Forms.Padding(2);
            buttonaPoToTranslateOtherPos.Name = "buttonaPoToTranslateOtherPos";
            buttonaPoToTranslateOtherPos.Size = new System.Drawing.Size(180, 55);
            buttonaPoToTranslateOtherPos.TabIndex = 36;
            buttonaPoToTranslateOtherPos.Text = "USE A \".PO\" TO TRANSLATE OTHERS \".PO\"";
            buttonaPoToTranslateOtherPos.UseVisualStyleBackColor = false;
            buttonaPoToTranslateOtherPos.Click += new System.EventHandler(buttonaPoToTranslateOtherPos_Click);
            // 
            // labelDataAssets
            // 
            labelDataAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            labelDataAssets.AutoSize = true;
            labelDataAssets.BackColor = System.Drawing.SystemColors.ControlLight;
            labelDataAssets.ForeColor = System.Drawing.Color.Black;
            labelDataAssets.Location = new System.Drawing.Point(3, 4);
            labelDataAssets.Name = "labelDataAssets";
            labelDataAssets.Size = new System.Drawing.Size(178, 15);
            labelDataAssets.TabIndex = 25;
            labelDataAssets.Text = "DATA ASSETS";
            labelDataAssets.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonUnpackWAD
            // 
            buttonUnpackWAD.BackColor = System.Drawing.Color.Black;
            buttonUnpackWAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonUnpackWAD.ForeColor = System.Drawing.Color.White;
            buttonUnpackWAD.Location = new System.Drawing.Point(2, 23);
            buttonUnpackWAD.Margin = new System.Windows.Forms.Padding(2);
            buttonUnpackWAD.Name = "buttonUnpackWAD";
            buttonUnpackWAD.Size = new System.Drawing.Size(180, 55);
            buttonUnpackWAD.TabIndex = 0;
            buttonUnpackWAD.Text = "EXTRACT \".WAD\"";
            buttonUnpackWAD.UseVisualStyleBackColor = false;
            buttonUnpackWAD.Click += new System.EventHandler(Button1_Click);
            // 
            // buttonRepackWAD
            // 
            buttonRepackWAD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepackWAD.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackWAD.ForeColor = System.Drawing.Color.White;
            buttonRepackWAD.Location = new System.Drawing.Point(2, 82);
            buttonRepackWAD.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackWAD.Name = "buttonRepackWAD";
            buttonRepackWAD.Size = new System.Drawing.Size(180, 55);
            buttonRepackWAD.TabIndex = 5;
            buttonRepackWAD.Text = "REPACK \".WAD\"";
            buttonRepackWAD.UseVisualStyleBackColor = false;
            buttonRepackWAD.Click += new System.EventHandler(Button2_Click);
            // 
            // tabPage4
            // 
            tabPage4.BackgroundImage = global::Danganronpa_Another_Tool.Properties.Resources.save_bg1;
            tabPage4.Controls.Add(tableLayoutPanelDR2PSVITA);
            tabPage4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tabPage4.Location = new System.Drawing.Point(4, 22);
            tabPage4.Name = "tabPage4";
            tabPage4.Size = new System.Drawing.Size(960, 354);
            tabPage4.TabIndex = 4;
            tabPage4.Text = "DR2 (PSVITA)";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelDR2PSVITA
            // 
            tableLayoutPanelDR2PSVITA.ColumnCount = 7;
            tableLayoutPanelDR2PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR2PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR2PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PSVITA.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelDR2PSVITA.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelDR2PSVITA.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelDR2PSVITA.Name = "tableLayoutPanelDR2PSVITA";
            tableLayoutPanelDR2PSVITA.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            tableLayoutPanelDR2PSVITA.RowCount = 12;
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PSVITA.Size = new System.Drawing.Size(960, 354);
            tableLayoutPanelDR2PSVITA.TabIndex = 1;
            // 
            // tabPage5
            // 
            tabPage5.BackgroundImage = global::Danganronpa_Another_Tool.Properties.Resources.save_bg1;
            tabPage5.Controls.Add(tableLayoutPanelDR2PC);
            tabPage5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tabPage5.Location = new System.Drawing.Point(4, 22);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new System.Drawing.Size(960, 354);
            tabPage5.TabIndex = 3;
            tabPage5.Text = "DR2 (PC)";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelDR2PC
            // 
            tableLayoutPanelDR2PC.ColumnCount = 7;
            tableLayoutPanelDR2PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR2PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDR2PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDR2PC.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelDR2PC.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelDR2PC.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelDR2PC.Name = "tableLayoutPanelDR2PC";
            tableLayoutPanelDR2PC.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            tableLayoutPanelDR2PC.RowCount = 12;
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDR2PC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDR2PC.Size = new System.Drawing.Size(960, 354);
            tableLayoutPanelDR2PC.TabIndex = 1;
            // 
            // tabPage6
            // 
            tabPage6.BackgroundImage = global::Danganronpa_Another_Tool.Properties.Resources.save_bg1;
            tabPage6.Controls.Add(tableLayoutPanelDRAEPSVITA);
            tabPage6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tabPage6.Location = new System.Drawing.Point(4, 22);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new System.Drawing.Size(960, 354);
            tabPage6.TabIndex = 6;
            tabPage6.Text = "DRAE (PSVITA)";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelDRAEPSVITA
            // 
            tableLayoutPanelDRAEPSVITA.ColumnCount = 7;
            tableLayoutPanelDRAEPSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDRAEPSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDRAEPSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPSVITA.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPSVITA.Controls.Add(buttonExtractBND, 2, 7);
            tableLayoutPanelDRAEPSVITA.Controls.Add(buttonRepackBND, 3, 7);
            tableLayoutPanelDRAEPSVITA.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelDRAEPSVITA.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelDRAEPSVITA.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelDRAEPSVITA.Name = "tableLayoutPanelDRAEPSVITA";
            tableLayoutPanelDRAEPSVITA.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            tableLayoutPanelDRAEPSVITA.RowCount = 12;
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPSVITA.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPSVITA.Size = new System.Drawing.Size(960, 354);
            tableLayoutPanelDRAEPSVITA.TabIndex = 1;
            // 
            // buttonExtractBND
            // 
            buttonExtractBND.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonExtractBND.BackColor = System.Drawing.Color.Black;
            buttonExtractBND.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonExtractBND.ForeColor = System.Drawing.Color.White;
            buttonExtractBND.Location = new System.Drawing.Point(206, 295);
            buttonExtractBND.Margin = new System.Windows.Forms.Padding(2);
            buttonExtractBND.Name = "buttonExtractBND";
            buttonExtractBND.Size = new System.Drawing.Size(180, 55);
            buttonExtractBND.TabIndex = 21;
            buttonExtractBND.Text = "EXTRACT \".BND\"";
            buttonExtractBND.UseVisualStyleBackColor = false;
            buttonExtractBND.Click += new System.EventHandler(Button23_Click);
            // 
            // buttonRepackBND
            // 
            buttonRepackBND.Anchor = System.Windows.Forms.AnchorStyles.Left;
            buttonRepackBND.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            buttonRepackBND.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            buttonRepackBND.ForeColor = System.Drawing.Color.White;
            buttonRepackBND.Location = new System.Drawing.Point(390, 295);
            buttonRepackBND.Margin = new System.Windows.Forms.Padding(2);
            buttonRepackBND.Name = "buttonRepackBND";
            buttonRepackBND.Size = new System.Drawing.Size(180, 55);
            buttonRepackBND.TabIndex = 22;
            buttonRepackBND.Text = "REPACK \".BND\"";
            buttonRepackBND.UseVisualStyleBackColor = false;
            buttonRepackBND.Click += new System.EventHandler(Button24_Click);
            // 
            // tabPage7
            // 
            tabPage7.BackgroundImage = global::Danganronpa_Another_Tool.Properties.Resources.save_bg1;
            tabPage7.Controls.Add(tableLayoutPanelDRAEPC);
            tabPage7.Location = new System.Drawing.Point(4, 22);
            tabPage7.Name = "tabPage7";
            tabPage7.Size = new System.Drawing.Size(960, 354);
            tabPage7.TabIndex = 7;
            tabPage7.Text = "DRAE (PC)";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanelDRAEPC
            // 
            tableLayoutPanelDRAEPC.ColumnCount = 7;
            tableLayoutPanelDRAEPC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDRAEPC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanelDRAEPC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPC.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 184F));
            tableLayoutPanelDRAEPC.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelDRAEPC.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelDRAEPC.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelDRAEPC.Name = "tableLayoutPanelDRAEPC";
            tableLayoutPanelDRAEPC.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            tableLayoutPanelDRAEPC.RowCount = 12;
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            tableLayoutPanelDRAEPC.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            tableLayoutPanelDRAEPC.Size = new System.Drawing.Size(960, 354);
            tableLayoutPanelDRAEPC.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = System.Drawing.SystemColors.Control;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            tableLayoutPanel2.Controls.Add(labelStatusText, 2, 0);
            tableLayoutPanel2.Controls.Add(menuStrip1, 0, 0);
            tableLayoutPanel2.Controls.Add(labelStatusName, 1, 0);
            tableLayoutPanel2.Controls.Add(linkLabelCredits, 3, 0);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new System.Drawing.Size(968, 28);
            tableLayoutPanel2.TabIndex = 25;
            // 
            // labelStatusText
            // 
            labelStatusText.AutoSize = true;
            labelStatusText.Dock = System.Windows.Forms.DockStyle.Fill;
            labelStatusText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelStatusText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            labelStatusText.Location = new System.Drawing.Point(483, 0);
            labelStatusText.Margin = new System.Windows.Forms.Padding(0);
            labelStatusText.Name = "labelStatusText";
            labelStatusText.Size = new System.Drawing.Size(161, 28);
            labelStatusText.TabIndex = 2;
            labelStatusText.Text = "Ready!";
            labelStatusText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            optionsToolStripMenuItem});
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(322, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            repackToolStripMenuItem,
            textToolStripMenuItem,
            toBTXToolStripMenuItem});
            optionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 24);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // repackToolStripMenuItem
            // 
            repackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            chooseFreelyTheFolderToRepackToolStripMenuItem});
            repackToolStripMenuItem.Name = "repackToolStripMenuItem";
            repackToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            repackToolStripMenuItem.Text = "Repack";
            // 
            // chooseFreelyTheFolderToRepackToolStripMenuItem
            // 
            chooseFreelyTheFolderToRepackToolStripMenuItem.CheckOnClick = true;
            chooseFreelyTheFolderToRepackToolStripMenuItem.Name = "chooseFreelyTheFolderToRepackToolStripMenuItem";
            chooseFreelyTheFolderToRepackToolStripMenuItem.Size = new System.Drawing.Size(251, 22);
            chooseFreelyTheFolderToRepackToolStripMenuItem.Text = "Choose freely the folder to repack";
            // 
            // textToolStripMenuItem
            // 
            textToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            ignoreLINWoTextToolStripMenuItem,
            eraseExtraLinefeedsToolStripMenuItem,
            aDDJAPANESETEXTToolStripMenuItem,
            hIDESPEAKERSToolStripMenuItem,
            cLEANPSPCLTToolStripMenuItem});
            textToolStripMenuItem.Name = "textToolStripMenuItem";
            textToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            textToolStripMenuItem.Text = "Text";
            // 
            // ignoreLINWoTextToolStripMenuItem
            // 
            ignoreLINWoTextToolStripMenuItem.Checked = true;
            ignoreLINWoTextToolStripMenuItem.CheckOnClick = true;
            ignoreLINWoTextToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            ignoreLINWoTextToolStripMenuItem.Name = "ignoreLINWoTextToolStripMenuItem";
            ignoreLINWoTextToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            ignoreLINWoTextToolStripMenuItem.Text = "Ignore \".LIN\" w/o text";
            // 
            // eraseExtraLinefeedsToolStripMenuItem
            // 
            eraseExtraLinefeedsToolStripMenuItem.Checked = true;
            eraseExtraLinefeedsToolStripMenuItem.CheckOnClick = true;
            eraseExtraLinefeedsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            eraseExtraLinefeedsToolStripMenuItem.Name = "eraseExtraLinefeedsToolStripMenuItem";
            eraseExtraLinefeedsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            eraseExtraLinefeedsToolStripMenuItem.Text = "Erase extra linefeeds";
            // 
            // aDDJAPANESETEXTToolStripMenuItem
            // 
            aDDJAPANESETEXTToolStripMenuItem.Checked = true;
            aDDJAPANESETEXTToolStripMenuItem.CheckOnClick = true;
            aDDJAPANESETEXTToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            aDDJAPANESETEXTToolStripMenuItem.Name = "aDDJAPANESETEXTToolStripMenuItem";
            aDDJAPANESETEXTToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            aDDJAPANESETEXTToolStripMenuItem.Text = "Add japanese text";
            // 
            // hIDESPEAKERSToolStripMenuItem
            // 
            hIDESPEAKERSToolStripMenuItem.CheckOnClick = true;
            hIDESPEAKERSToolStripMenuItem.Name = "hIDESPEAKERSToolStripMenuItem";
            hIDESPEAKERSToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            hIDESPEAKERSToolStripMenuItem.Text = "Hide speakers";
            // 
            // cLEANPSPCLTToolStripMenuItem
            // 
            cLEANPSPCLTToolStripMenuItem.CheckOnClick = true;
            cLEANPSPCLTToolStripMenuItem.Name = "cLEANPSPCLTToolStripMenuItem";
            cLEANPSPCLTToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            cLEANPSPCLTToolStripMenuItem.Text = "Clean PSP \"CLT\"";
            // 
            // toBTXToolStripMenuItem
            // 
            toBTXToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toBTXToolStripMenuItem1,
            removeAdditionalDataFromExtensionToolStripMenuItem});
            toBTXToolStripMenuItem.Name = "toBTXToolStripMenuItem";
            toBTXToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            toBTXToolStripMenuItem.Text = "Images";
            // 
            // toBTXToolStripMenuItem1
            // 
            toBTXToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripComboBoxBTXType});
            toBTXToolStripMenuItem1.Name = "toBTXToolStripMenuItem1";
            toBTXToolStripMenuItem1.Size = new System.Drawing.Size(282, 22);
            toBTXToolStripMenuItem1.Text = "To BTX (Choose BTX\'s type)";
            // 
            // toolStripComboBoxBTXType
            // 
            toolStripComboBoxBTXType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            toolStripComboBoxBTXType.Items.AddRange(new object[] {
            "AUTO",
            "SHTX",
            "SHTXFS",
            "SHTXFs",
            "SHTXFF",
            "SHTXFf"});
            toolStripComboBoxBTXType.Name = "toolStripComboBoxBTXType";
            toolStripComboBoxBTXType.Size = new System.Drawing.Size(80, 23);
            // 
            // removeAdditionalDataFromExtensionToolStripMenuItem
            // 
            removeAdditionalDataFromExtensionToolStripMenuItem.CheckOnClick = true;
            removeAdditionalDataFromExtensionToolStripMenuItem.Name = "removeAdditionalDataFromExtensionToolStripMenuItem";
            removeAdditionalDataFromExtensionToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            removeAdditionalDataFromExtensionToolStripMenuItem.Text = "Remove additional data from extension";
            // 
            // labelStatusName
            // 
            labelStatusName.AutoSize = true;
            labelStatusName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelStatusName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelStatusName.Location = new System.Drawing.Point(322, 0);
            labelStatusName.Margin = new System.Windows.Forms.Padding(0);
            labelStatusName.Name = "labelStatusName";
            labelStatusName.Size = new System.Drawing.Size(161, 28);
            labelStatusName.TabIndex = 1;
            labelStatusName.Text = "Status:";
            labelStatusName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabelCredits
            // 
            linkLabelCredits.AutoSize = true;
            linkLabelCredits.Dock = System.Windows.Forms.DockStyle.Fill;
            linkLabelCredits.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            linkLabelCredits.Location = new System.Drawing.Point(647, 0);
            linkLabelCredits.Name = "linkLabelCredits";
            linkLabelCredits.Size = new System.Drawing.Size(318, 28);
            linkLabelCredits.TabIndex = 3;
            linkLabelCredits.TabStop = true;
            linkLabelCredits.Text = "GITHUB WEBPAGE";
            linkLabelCredits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            linkLabelCredits.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(LinkLabel1_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tabControl1, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new System.Drawing.Size(968, 408);
            tableLayoutPanel1.TabIndex = 25;
            // 
            // DRAT
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            BackColor = System.Drawing.Color.Black;
            ClientSize = new System.Drawing.Size(968, 408);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(2);
            MaximizeBox = false;
            Name = "DRAT";
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            Text = "Danganronpa: Another Tool (Vers: 1.5)";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tableLayoutPanelDR1PSP.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tableLayoutPanelDR1PSVITA.ResumeLayout(false);
            tableLayoutPanelDR1PSVITA.PerformLayout();
            tabPage3.ResumeLayout(false);
            tableLayoutPanelDR1PC.ResumeLayout(false);
            tableLayoutPanelDR1PC.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage6.ResumeLayout(false);
            tableLayoutPanelDRAEPSVITA.ResumeLayout(false);
            tabPage7.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        static private System.Windows.Forms.TabPage tabPage1;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDR1PSP;
        static private System.Windows.Forms.Button buttonRepackUmdImage;
        static private System.Windows.Forms.Button buttonExtractUmdImage;
        static private System.Windows.Forms.TabPage tabPage3;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDR1PC;
        static private System.Windows.Forms.Button buttonRepackPakToPNG;
        static private System.Windows.Forms.Button buttonExtractPakToPNG;
        static private System.Windows.Forms.Button buttonRepackPakType1;
        static private System.Windows.Forms.Button buttonRepacktPakWOConvert16;
        static private System.Windows.Forms.Button buttonConvertGXT;
        static private System.Windows.Forms.Button buttonConvertToTGA;
        static private System.Windows.Forms.Button buttonExtractPakType2;
        static private System.Windows.Forms.Button buttonConverBTX;
        static private System.Windows.Forms.Button buttonConvertToPng;
        static private System.Windows.Forms.Button buttonRepackPakType2;
        static private System.Windows.Forms.Button buttonExtractPakWOConvert;
        static private System.Windows.Forms.Button buttonExtractPakType3;
        static private System.Windows.Forms.Button buttonRepackPakType3;
        static private System.Windows.Forms.Button buttonExtractPakType1;
        static private System.Windows.Forms.Button buttonRepackLin;
        static private System.Windows.Forms.Label labelTextures;
        static private System.Windows.Forms.Label labelTexts;
        static private System.Windows.Forms.Button buttonExtractLin;
        static private System.Windows.Forms.Label labelDataAssets;
        static private System.Windows.Forms.Button buttonUnpackWAD;
        static private System.Windows.Forms.Button buttonRepackWAD;
        static private System.Windows.Forms.TabPage tabPage2;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDR1PSVITA;
        static private System.Windows.Forms.Button buttonExtractCPK;
        static private System.Windows.Forms.Button buttonRepackCPK;
        static private System.Windows.Forms.TabPage tabPage5;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDR2PC;
        static private System.Windows.Forms.TabPage tabPage4;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDR2PSVITA;
        static private System.Windows.Forms.TabPage tabPage6;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDRAEPSVITA;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        static private System.Windows.Forms.MenuStrip menuStrip1;
        static private System.Windows.Forms.Label labelStatusName;
        static private System.Windows.Forms.LinkLabel linkLabelCredits;
        static private System.Windows.Forms.TabPage tabPage7;
        static private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDRAEPC;
        static private System.Windows.Forms.Button buttonExtractBND;
        static private System.Windows.Forms.Button buttonRepackBND;
        static private System.Windows.Forms.Button buttonConvertXmlToPo;
        static private System.Windows.Forms.Label labelCompression;
        static private System.Windows.Forms.Button buttonDecompressFiles;
        static private System.Windows.Forms.Button buttonCompressFIles;
        static private System.Windows.Forms.Label labelUtilities;
        static private System.Windows.Forms.ToolStripMenuItem toBTXToolStripMenuItem;
        static private System.Windows.Forms.ToolStripMenuItem toBTXToolStripMenuItem1;
        static private System.Windows.Forms.Button buttonaPoToTranslateOtherPos;
        static public System.Windows.Forms.ToolStripMenuItem removeAdditionalDataFromExtensionToolStripMenuItem;
        static public System.Windows.Forms.ToolStripComboBox toolStripComboBoxBTXType;
        static public System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        static public System.Windows.Forms.TabControl tabControl1;
        static public System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
        static public System.Windows.Forms.ToolStripMenuItem ignoreLINWoTextToolStripMenuItem;
        static public System.Windows.Forms.ToolStripMenuItem eraseExtraLinefeedsToolStripMenuItem;
        static public System.Windows.Forms.ToolStripMenuItem aDDJAPANESETEXTToolStripMenuItem;
        static public System.Windows.Forms.ToolStripMenuItem hIDESPEAKERSToolStripMenuItem;
        static public System.Windows.Forms.ToolStripMenuItem cLEANPSPCLTToolStripMenuItem;
        static public System.Windows.Forms.Label labelStatusText;
        static public System.Windows.Forms.ToolStripMenuItem repackToolStripMenuItem;
        static public System.Windows.Forms.ToolStripMenuItem chooseFreelyTheFolderToRepackToolStripMenuItem;
    }
}