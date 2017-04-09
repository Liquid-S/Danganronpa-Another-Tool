using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Danganronpa_Steam_Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog cdfile = new OpenFileDialog();
            cdfile.Filter = "PSP umdimage.dat |*.dat|All Files (*.*)|*.*";
            cdfile.FileName = "PSP umdimage.dat";

            OpenFileDialog ebootfile = new OpenFileDialog();
            ebootfile.Filter = "Decrypted PSP eboot.bin |*.bin|All Files (*.*)|*.*";
            ebootfile.FileName = "Decrypted PSP eboot.bin";

            if ((cdfile.ShowDialog() == DialogResult.OK) && (ebootfile.ShowDialog() == DialogResult.OK))
            {
                if (comboBox3.SelectedIndex == 0)
                {
                    UnpackDAT(cdfile.FileName, "DRumdimage-PSP", 0xF5A38, ebootfile.FileName);
                }
                else if (comboBox3.SelectedIndex == 1)
                {
                    UnpackDEMODAT(cdfile.FileName, "DRumdimage-DEMOPSP", 0x145c1c, ebootfile.FileName);
                }
                cdfile.Dispose();
                ebootfile.Dispose();
                MessageBox.Show("Done!");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog cdfile = new OpenFileDialog();
            cdfile.Filter = "PSP umdimage2.dat |*.dat|All Files (*.*)|*.*";
            cdfile.FileName = "PSP umdimage2.dat";

            OpenFileDialog ebootfile = new OpenFileDialog();
            ebootfile.Filter = "Decrypted PSP eboot.bin |*.bin|All Files (*.*)|*.*";
            ebootfile.FileName = "Decrypted PSP eboot.bin";
            if ((cdfile.ShowDialog() == DialogResult.OK) && (ebootfile.ShowDialog() == DialogResult.OK))
            {
                UnpackDAT(cdfile.FileName, "DRumdimage2-PSP", 0xF5220, ebootfile.FileName);
                cdfile.Dispose();
                ebootfile.Dispose();
                MessageBox.Show("Done!");
            }
        }

        private void UnpackDEMODAT(string cdfile, string directory, long pos, string EbootDR)
        {
            FileStream cdimg = new FileStream(cdfile, FileMode.Open, FileAccess.Read);
            BinaryReader idx = new BinaryReader(cdimg, Encoding.Default);
            FileStream Eboot = new FileStream(EbootDR, FileMode.Open, FileAccess.Read);

            UInt32 sector = 0;

            BinaryReader Ebt = new BinaryReader(Eboot);
            Directory.CreateDirectory(directory);

            Eboot.Seek(pos, SeekOrigin.Begin);
            sector = Ebt.ReadUInt32();
            Ebt.ReadUInt32();
            uint[] offset = new uint[sector];
            uint[] filesize = new uint[sector];
            string[] Nomifile = new string[sector];
            uint[] offsetNomifile = new uint[sector];
            for (int i = 0; i < sector; i++)
            {
                offsetNomifile[i] = Ebt.ReadUInt32() + 0xC0;
                offset[i] = Ebt.ReadUInt32();
                filesize[i] = Ebt.ReadUInt32();
            }

            for (int i = 0; i < offsetNomifile.Length; i++)
            {
                Eboot.Seek(offsetNomifile[i], SeekOrigin.Begin);
                Nomifile[i] = null;
                byte Fine = Ebt.ReadByte();

                while (Fine != 0)
                {
                    Nomifile[i] += (char)Fine;
                    Fine = Ebt.ReadByte();
                }
            }

            for (int i = 0; i < offset.Length; i++)
            {
                cdimg.Seek(offset[i], SeekOrigin.Begin);

                byte[] buffer = new byte[filesize[i]];
                cdimg.Read(buffer, 0, buffer.Length);

                FileStream Extract = new FileStream(directory + "\\" + Nomifile[i], FileMode.Create, FileAccess.Write);
                Extract.Write(buffer, 0, buffer.Length);
                Extract.Close();
            }

            cdimg.Close();
            Ebt.Close();
        }

        private void UnpackDAT(string cdfile, string directory, long pos, string EbootDR)
        {
            FileStream cdimg = new FileStream(cdfile, FileMode.Open, FileAccess.Read);
            BinaryReader idx = new BinaryReader(cdimg, Encoding.Default);
            FileStream Eboot = new FileStream(EbootDR, FileMode.Open, FileAccess.Read);

            UInt32 sector = 0;

            BinaryReader Ebt = new BinaryReader(Eboot);
            Directory.CreateDirectory(directory);

            sector = idx.ReadUInt32();
            uint[] offset = new uint[sector];
            uint[] filesize = new uint[sector];
            String[] Nomifile = new String[sector];

            for (int i = 0; i < sector; i++)
            {
                cdimg.Seek(i * 0x4 + 0x4, SeekOrigin.Begin);
                offset[i] = idx.ReadUInt32();
                filesize[i] = idx.ReadUInt32();

                if (filesize[i] == 0)
                    filesize[i] = (UInt32)cdimg.Length - offset[i];

                else
                    filesize[i] -= offset[i];

                Eboot.Seek(pos, SeekOrigin.Begin);
                uint Puntatore = Ebt.ReadUInt32() + 0xC0;
                pos = Eboot.Position + 8;

                Eboot.Seek(Puntatore, SeekOrigin.Begin);
                Nomifile[i] = null;
                byte Fine = Ebt.ReadByte();

                while (Fine != 0)
                {
                    Nomifile[i] += (char)Fine;
                    Fine = Ebt.ReadByte();
                }
            }

            Array.Sort(Nomifile, new AlphanumComparatorFast());

            for (int i = 0; i < offset.Length; i++)
            {
                cdimg.Seek(offset[i], SeekOrigin.Begin);

                byte[] buffer = new byte[filesize[i]];
                cdimg.Read(buffer, 0, buffer.Length);

                FileStream Extract = new FileStream(directory + "\\" + Nomifile[i], FileMode.Create, FileAccess.Write);
                Extract.Write(buffer, 0, buffer.Length);
                Extract.Close();
            }

            cdimg.Close();
            Ebt.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog cdfile = new OpenFileDialog();
            cdfile.Filter = ".wad |*.wad|All Files (*.*)|*.*";
            cdfile.Multiselect = true;

            if (cdfile.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in cdfile.FileNames)
                {
                    UnpackWAD(file);
                }
                cdfile.Dispose();
                MessageBox.Show("Done!");
            }
        }

        private void UnpackWAD(string filename)
        {
            FileStream wad = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader wadbin = new BinaryReader(wad);

            uint MagicID = wadbin.ReadUInt32();

            if (MagicID == 0x52414741)
            {
                uint MaVersion = wadbin.ReadUInt32();
                uint MiVersion = wadbin.ReadUInt32();
                uint Boh = wadbin.ReadUInt32();
                uint numfiles = wadbin.ReadUInt32();

                ulong[] ASize = new ulong[numfiles];
                ulong[] AOffset = new ulong[numfiles];
                String[] NomiFile = new String[numfiles];

                for (int i = 0; i < numfiles; i++)
                {
                    uint Filename1_Length = wadbin.ReadUInt32();
                    byte[] FuturaStringa = new byte[Filename1_Length];
                    wadbin.Read(FuturaStringa, 0, FuturaStringa.Length);
                    ASize[i] = wadbin.ReadUInt64();
                    AOffset[i] = wadbin.ReadUInt64();
                    NomiFile[i] = Encoding.Default.GetString(FuturaStringa);
                }

                uint numcartelle = wadbin.ReadUInt32();

                for (int i = 0; i < numcartelle; i++)
                {
                    uint NomeCartella_Length = wadbin.ReadUInt32();

                    if (NomeCartella_Length != 0x0)
                    {
                        byte[] FuturaStringa2 = new byte[NomeCartella_Length];
                        wadbin.Read(FuturaStringa2, 0, FuturaStringa2.Length);
                    }

                    uint Figli = wadbin.ReadUInt32();

                    for (int a = 0; a < Figli; a++)
                    {
                        uint NomeSubFile_Length = wadbin.ReadUInt32();

                        if (NomeSubFile_Length != 0x0)
                        {
                            byte[] FuturaStringa3 = new byte[NomeSubFile_Length];
                            wadbin.Read(FuturaStringa3, 0, FuturaStringa3.Length);
                        }

                        byte Tipologia = wadbin.ReadByte();
                    }

                }

                long pos = wad.Position;

                string directory = "Unpack WAD " + comboBox2.SelectedItem + "\\" + Path.GetFileNameWithoutExtension(filename);

                for (int i = 0; i < numfiles; i++)
                {
                    Directory.CreateDirectory(directory + "\\" + Path.GetDirectoryName(NomiFile[i]));
                    wad.Seek((long)AOffset[i] + pos, SeekOrigin.Begin);
                    byte[] DatiFile = new byte[ASize[i]];
                    FileStream Extracted = new FileStream(directory + "\\" + NomiFile[i], FileMode.Create, FileAccess.Write);
                    wadbin.Read(DatiFile, 0, (int)ASize[i]);
                    Extracted.Write(DatiFile, 0, (int)ASize[i]);
                    Extracted.Close();
                }
            }
            wad.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("Unpack WAD " + comboBox2.SelectedItem))
            {
                foreach (string cartella in Directory.GetDirectories("Unpack WAD " + comboBox2.SelectedItem))
                {
                    PackWAD(cartella);
                }
                MessageBox.Show("Done!");
            }
        }

        private string[] ReadFiles(string cartella)
        {
            string[] temp = Directory.GetFiles(cartella, "*", SearchOption.TopDirectoryOnly);
            Array.Sort(temp, new AlphanumComparatorFast());
            return temp;
        }

        private string[] ReadDirectories(string cartella)
        {
            string[] temp = Directory.GetDirectories(cartella, "*", SearchOption.TopDirectoryOnly);
            Array.Sort(temp, new AlphanumComparatorFast());
            return temp;
        }

        private string[] PulisciIndirizzo(String[] stringa, string cartella)
        {
            Array.Sort(stringa, new AlphanumComparatorFast());
            for (int i = 0; i < stringa.Length; i++)
                stringa[i] = stringa[i].Replace(cartella + "\\", null).Replace("\\", "/");

            return stringa;
        }

        private void PackWAD(string cartella)
        {
            string directory = "RePack WAD " + comboBox2.SelectedItem;
            Directory.CreateDirectory(directory);

            FileStream NewWAD = new FileStream(directory + "\\" + Path.GetFileNameWithoutExtension(cartella) + ".wad", FileMode.Create, FileAccess.Write);
            BinaryWriter WAD = new BinaryWriter(NewWAD);

            string[] IndirizzoCompletoFiles = Directory.GetFiles(cartella, "*", SearchOption.AllDirectories);
            string[] IndirizzoCompletoCartelle = Directory.GetDirectories(cartella, "*", SearchOption.AllDirectories);
            string[] IndirizzoPulitoFiles = PulisciIndirizzo(Directory.GetFiles(cartella, "*", SearchOption.AllDirectories), cartella);
            string[] IndirizzoPulitoCartelle = PulisciIndirizzo(Directory.GetDirectories(cartella, "*", SearchOption.AllDirectories), cartella);

            Array.Sort(IndirizzoCompletoCartelle, new AlphanumComparatorFast());
            Array.Sort(IndirizzoCompletoFiles, new AlphanumComparatorFast());

            /* Inizio creazione header */
            WAD.Write((UInt32)0x52414741);
            WAD.Write((UInt32)0x01);
            WAD.Write((UInt32)0x01);
            WAD.Write((UInt32)0x0);
            /* Fine creazione header */

            /* Inizio sezione nomi */
            WAD.Write((UInt32)IndirizzoPulitoFiles.Length);

            long TempOffset = 0x0;

            for (int i = 0; i < IndirizzoCompletoFiles.Length; i++)
            {
                FileStream FileTemp = new FileStream(IndirizzoCompletoFiles[i], FileMode.Open, FileAccess.Read);
                WAD.Write((UInt32)IndirizzoPulitoFiles[i].Length);
                WAD.Write(IndirizzoPulitoFiles[i].ToCharArray());
                WAD.Write((long)FileTemp.Length);
                WAD.Write(TempOffset);
                TempOffset += FileTemp.Length;
                FileTemp.Close();
            }
            /* Fine sezione nomi */

            /* Inizio sezione cartelle e conteggio contenuto */
            WAD.Write((UInt32)IndirizzoPulitoCartelle.Length + 1);
            WAD.Write((UInt32)0x0);
            WAD.Write((UInt32)(ReadDirectories(cartella).Length + ReadFiles(cartella).Length));

            if (ReadDirectories(cartella).Length != 0)
            {
                string[] tempC = PulisciIndirizzo(ReadDirectories(cartella), cartella);
                Array.Sort(tempC, new AlphanumComparatorFast());
                for (int i = 0; i < tempC.Length; i++)
                {
                    WAD.Write((UInt32)tempC[i].Length);
                    WAD.Write(tempC[i].ToCharArray());
                    WAD.Write((byte)0x01);
                }
            }

            if (ReadFiles(cartella).Length != 0)
            {
                string[] tempF = PulisciIndirizzo(ReadFiles(cartella), cartella);
                Array.Sort(tempF, new AlphanumComparatorFast());
                for (int i = 0; i < tempF.Length; i++)
                {
                    WAD.Write((UInt32)tempF[i].Length);
                    WAD.Write(tempF[i].ToCharArray());
                    WAD.Write((byte)0x00);
                }
            }

            for (int i = 0; i < IndirizzoPulitoCartelle.Length; i++)
            {
                WAD.Write((UInt32)IndirizzoPulitoCartelle[i].Length);
                WAD.Write(IndirizzoPulitoCartelle[i].ToCharArray());
                WAD.Write((UInt32)(ReadDirectories(IndirizzoCompletoCartelle[i]).Length + ReadFiles(IndirizzoCompletoCartelle[i]).Length));

                if (ReadDirectories(IndirizzoCompletoCartelle[i]).Length != 0)
                {
                    string[] tempC = PulisciIndirizzo(ReadDirectories(IndirizzoCompletoCartelle[i]), cartella);

                    for (int a = 0; a < tempC.Length; a++)
                    {
                        tempC[a] = tempC[a].Replace(IndirizzoPulitoCartelle[i] + "/", null);
                    }

                    Array.Sort(tempC, new AlphanumComparatorFast());

                    for (int a = 0; a < tempC.Length; a++)
                    {
                        WAD.Write((UInt32)tempC[a].Length);
                        WAD.Write(tempC[a].ToCharArray());
                        WAD.Write((byte)0x01);
                    }
                }

                if (ReadFiles(IndirizzoCompletoCartelle[i]).Length != 0)
                {
                    string[] tempF = PulisciIndirizzo(ReadFiles(IndirizzoCompletoCartelle[i]), cartella);

                    for (int a = 0; a < tempF.Length; a++)
                    {
                        tempF[a] = tempF[a].Replace(IndirizzoPulitoCartelle[i] + "/", null);
                    }

                    Array.Sort(tempF, new AlphanumComparatorFast());

                    for (int a = 0; a < tempF.Length; a++)
                    {
                        WAD.Write((UInt32)tempF[a].Length);
                        WAD.Write(tempF[a].ToCharArray());
                        WAD.Write((byte)0x00);
                    }
                }
            }
            /* Fine sezione cartelle e conteggio contenuto */

            /* Inizio inserimento corpo dei files */

            for (int i = 0; i < IndirizzoCompletoFiles.Length; i++)
            {
                FileStream FileTemp = new FileStream(IndirizzoCompletoFiles[i], FileMode.Open, FileAccess.Read);
                byte[] DatiFile = new byte[FileTemp.Length];
                FileTemp.Read(DatiFile, 0, DatiFile.Length);
                NewWAD.Write(DatiFile, 0, DatiFile.Length);
            }
            /* Fine inserimento corpo dei files */

            NewWAD.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog cdfile = new OpenFileDialog(); /* Apre la finestra di selezione dei file */
            cdfile.Filter = ".lin |*.lin|All Files (*.*)|*.*"; /* Filtra i file e permette la selezione solo dei ".lin" */
            cdfile.Multiselect = true; /* Attiva la selezione multipla */

            if (cdfile.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in cdfile.FileNames) /* Per ogni stringa/file contenuta in cdfiles */
                {
                    UnpackLIN(file, "Unpack LIN " + comboBox2.SelectedItem + "\\" + Path.GetFileNameWithoutExtension(file));
                }
                cdfile.Dispose();
                GC.Collect();
                MessageBox.Show("Done!");
            }
        }

        private void UnpackLIN(string filename, string directory)
        {
            FileStream LIN = new FileStream(filename, FileMode.Open, FileAccess.Read); /* Apre il file selezionato in lettura */
            BinaryReader ln = new BinaryReader(LIN); /* Il binary reader pemette l'utilizzo di tutto ciò che non è un array, quindi byte, int, short... */

            uint TipologiaFile = ln.ReadUInt32(); /* Numero di parti che compongono il file */
            if (TipologiaFile == 0x02)
            {/* Il Header contiene il numero di file che lo compongono, in questo caso due (bytecode e testo), dove sono posizionati e quanto è lungo (quanto "pesa") */
                uint GrandezzaHeader = ln.ReadUInt32();
                uint InizioSecondoFile = ln.ReadUInt32(); /* Il secondo file è il testo, il primo file è il bytecode */

                /* Posizioniamo il Reader all'inizio del secondo file, ossia il testo per leggere il numero di frasi presenti nel file.
                QUesto serve a tenere conto di quanti Speaker bisognerà trovare nel bytecode */
                LIN.Seek(InizioSecondoFile, SeekOrigin.Begin);
                uint NPuntatori = ln.ReadUInt32(); /* Numero di frasi */

                /* Per evitare di elaborare inutilmente i file senza testo, si effettua un controllo sul numero di puntatori (=frasi) e l'inizio del file dei testi */
                if (NPuntatori != 0 && InizioSecondoFile != 0)
                { /* Se si entra nel IF significa che è presente del testo, quindi ci si posiziona all'inizio del primo file, ossia il bytecode  */
                    LIN.Seek(0x10, SeekOrigin.Begin);

                    Directory.CreateDirectory(directory);
                    /* Crea la cartella "Unpack LIN" con al suo interno una cartella con lo stesso nome del file scelto*/

                    uint GrandezzaTotale = (uint)LIN.Length; /* Invece di leggere la grandezza totale del file, la calcola direttamente. Questo serve a far funzionare il programma anche coi LIN per PSP */
                    byte[] Corpo1 = new byte[InizioSecondoFile - GrandezzaHeader]; /* Vettore che conterrà tutta la sezione dedicata ai puntatori delle frasi */
                    byte[] Corpo2 = new byte[GrandezzaTotale - InizioSecondoFile]; /* Vetterore che conterrà tutta la sezione dedicata alle frasi vere e proprie */

                    LIN.Read(Corpo1, 0, Corpo1.Length); /* Inserisce in Corpo1 tutta la parte dei puntatori */
                    LIN.Read(Corpo2, 0, Corpo2.Length); /* Inserisce in Corpo2 tutta la parte delle frasi */

                    FileStream bytecode = new FileStream(directory + "\\" + "Bytecode.bin", FileMode.Create, FileAccess.Write);
                    bytecode.Write(Corpo1, 0, Corpo1.Length);
                    /* Crea un file chiamato "Bytecode.bin" in modalità scrittura e vi inserisce il contenuto di "Corpo1" */

                    FileStream TestoRaw = new FileStream(directory + "\\" + "TextRaw.bin", FileMode.Create, FileAccess.Write);
                    TestoRaw.Write(Corpo2, 0, Corpo2.Length);
                    /* Crea un file chiamato "TextRaw.bin" in modalità scrittura e vi inserisce tutto il contenuto di "Corpo2" */

                    bytecode.Close(); /* Chiude il filestrem di bytecode, quindi chiude la "comuncazione" con tale file */
                    TestoRaw.Close();

                    EstrattoreTesto(filename, directory); /* Richiamo il metodo per estrarre in ".txt" i testi */
                    File.Delete(directory + "\\" + "TextRaw.bin");
                }
            }
            LIN.Close();
            LIN.Dispose();
        }

        private void EstrattoreTesto(string filename, string directory)
        {
            FileStream TestoRaw = new FileStream(directory + "\\" + "TextRaw.bin", FileMode.Open, FileAccess.Read);
            BinaryReader tr = new BinaryReader(TestoRaw, Encoding.Unicode);

            FileStream Bytecode = new FileStream(directory + "\\" + "Bytecode.bin", FileMode.Open, FileAccess.Read);
            BinaryReader bc = new BinaryReader(Bytecode);

            FileStream Testo = new FileStream(directory + "\\" + Path.GetFileNameWithoutExtension(filename) + ".txt", FileMode.Create, FileAccess.Write);
            BinaryWriter txt = new BinaryWriter(Testo, Encoding.Unicode);

            bc.ReadInt16(); /* I primi due byte di solito sono "70 00", 70 sta per "inizio istruzione", mentre "00" sta per "header" */
            short NomiDaTrovare = bc.ReadInt16(); /* Il terzo e il quarto byte indicano la quantità di dialoghi presenti nel file e a cui assegnare uno "Speaker" */
            byte[] Speaker = new byte[NomiDaTrovare]; /* Conterrà il codice in esadecimale del PG parlante per ogni battuta e servirà da indice durante la creazione del ".txt" */
            string[] Nomi = null;

            if (checkBox2.CheckState == CheckState.Unchecked)
            {
                if (comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 3) /* Se l'utente ha scelto "Working on DR2" o "DR2 PSVITA" */
                {
                    Nomi = new string[] { "HINATA", "KOMAEDA", "TOGAMI", "TANAKA", "SOUDA", "HANAMURA", "NIDAI", "KUZURYUU", "OWARI", "NANAMI", "SONIA", "SAIONJI", "KOIZUMI", "TSUMIKI", "MIODA", "PEKOYAMA", "MONOKUMA", "MONOMI", "ENOSHIMA", "MECHA NIDAI", "NAEGI", "KIRIGIRI", "TOGAMI REAL", "HANAMURA MOM", "ALTER EGO", "MINI NIDAI", "MONOKUMA MONOMI", "NARRATION", "ERROR", "EMPTY SPEAKER", "ERROR", "ERROR", "CHOICE/RE:ACT", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "USAMI", "KIRAKIRA", "NO NAME", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ENOSHIMA 2", "ERROR", "GIRL A", "GIRL B", "GIRL C", "GIRL D", "GIRL E", "BOY F", "NO NAME 2", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "LAST SPRITE", "EMPTY SPEAKER" };
                }
                else if (comboBox2.SelectedIndex == 0 || comboBox2.SelectedIndex == 2) /* Se l'utente ha scelto "Working on DR1" o "DR1 PSVITA" */
                {
                    Nomi = new string[] { "NAEGI", "ISHIMARU", "TOGAMI", "OOWADA", "KUWATA", "YAMADA", "HAGAKURE", "MAIZONO", "KIRIGIRI", "ASAHINA", "FUKAWA", "OOGAMI", "CELES", "ENOSHIMA (MUKURO)", "FUJISAKI", "MONOKUMA", "ENOSHIMA (REAL)", "ALTER EGO", "GENOCIDER", "HEADMASTER", "NAEGI MOTHER", "NAEGI FATHER", "NAEGI SISTER", "ERROR", "ISHIDA", "DAIA OOWADA", "ERROR", "ERROR", "LAST SPRITE", "EMPTY SPEAKER", "NO NAME", "NARRATION", "CHOICE/RE:ACT", "USAMI", "MONOKUMA BACKUP", "MONOKUMA BACKUP (R)", "MONOKUMA BACKUP (L)", "MONOKUMA BACKUP (M)", "ERROR" };
                }
                else if (comboBox2.SelectedIndex == 4) /* Se l'utente ha scelto "Working on DR AE PSVITA" */
                {
                    Nomi = new string[] { "KOMARU NAEGI", "TOUKO FUKAWA", "GENOCIDER SHOU", "MASARU DAIMON", "JATARO KEMURI", "KOTOKO UTSUGI", "NAGISA SHINGETSU", "MONACA", "SERVANT", "KUROKUMA", "HAIJI TOUWA", "TOUICHI TOUWA", "SHIROKUMA", "YUUTA ASAHINA", "HIROKO HAGAKURE", "???", "MAKOTO NAEGI", "BYAKUYA TOGAMI", "MONOKUMA KID", "MONOKUMA KID", "FUTURE FONDATION", "FUTURE FONDATION A", "FUTURE FONDATION B", "FUTURE FONDATION C", "FUTURE FONDATION D", "FUTURE FONDATION E", "FUTURE FONDATION F", "ADULT", "ADULT A", "ADULT B", "ADULT C", "ADULT D", "ADULT E", "ADULT F", "ADULT G", "ADULT H", "ADULT I", "ADULT J", "ADULT K", "ADULT L", "ADULT M", "ADULT N", "ADULT O", "ADULT P", "ADULT Q", "ADULT R", "ADULT S", "ADULT T", "ADULT U", "ADULT V", "ADULT W", "ADULT X", "ADULT Y", "ADULT Z", "ADULTS", "ADULT", "ADULT", "DUMMY 1", "DUMMY 2", "DUMMY 3", "DUMMY 4", "DUMMY 5", "DUMMY 6", "DUMMY 7", "DUMMY 8", "DUMMY 9", "DUMMY 10", "DUMMY 11", "DUMMY 12", "JUNKO ENOSHIMA", "WARRIOS OF HOPE", "KID SHOP MANAGER", "EMPTY SPEAKER", "???", "???", "ERROR", "SYSTEM/CHOICE" };
                }

                /* Inizio lettera dei nomi dal bytecode.bin */

                int IDSpeaker = 0; /* Variabile utile a leggere e contenere momentaneamente i vari letti di volta in volta */
                int NomiTrovati = 0; /* Indica la quantità di nomi trovati */
                byte Parlante = 0; /* Contiene il codice in esadecimale del personaggio parlante */
                byte lastsprite = 0; /* Tiene memorizzato il codice dell'ultimo personaggio apparse su schermo */
                byte CodiceScelta = 0x2B;
                byte IndiceLastSprite = 0x1C;
                int CodiceDiErrore = 38;

                if (comboBox2.SelectedIndex == 4)
                {
                    Parlante = 72;
                    byte CodiceParlante = 0x15;
                    byte CodiceStampa = 0x01;
                    int Narrazione = 0;

                    while (Bytecode.Position != Bytecode.Length && Narrazione == 0)
                    {
                        IDSpeaker = bc.ReadByte();
                        if (IDSpeaker == 0x70) /* Se il valore letto è uguale a 0x70, ovvero a "inizio istruzione", allora... */
                        {
                            IDSpeaker = bc.ReadByte();
                            if (IDSpeaker == 0x15) /* Se il valore letto è uguale a "Speaker" */
                            {
                                Narrazione = 1;
                            }
                        }
                    }
                    Bytecode.Seek(0x4, SeekOrigin.Begin);
                    int TipologiaDiFile = bc.ReadInt32();

                    if (Narrazione == 0 || TipologiaDiFile == 0x00011670 || TipologiaDiFile == 0x1F003670 || TipologiaDiFile == 0x04003370)
                    {
                        CodiceParlante = 0x07;
                    }

                    while (NomiTrovati < NomiDaTrovare)
                    {
                        if (Bytecode.Position == Bytecode.Length)
                        { /* Se il Reader ha superato la lunghezza del file */
                            Speaker[NomiTrovati] = 75; /* Allora assegna "Errore" allo Speaker, in questo modo sarà possibile trovare con facilità i file con problemi*/
                            NomiTrovati++; /* Il contatore contierà a incrementare finché il while sarà attivo */
                        }
                        else
                        {
                            IDSpeaker = bc.ReadByte();
                            if (IDSpeaker == 0x70) /* Se il valore letto è uguale a 0x70, ovvero a "inizio istruzione", allora... */
                            {
                                IDSpeaker = bc.ReadByte();
                                if (IDSpeaker == CodiceParlante) /* Se il valore letto è uguale a "Speaker" */
                                {
                                    Parlante = bc.ReadByte();
                                }
                                else if (IDSpeaker == 0x39) /* Se il valore letto è uguale a "System/Choice" */
                                {
                                    Parlante = 76;
                                }
                                else if (IDSpeaker == CodiceStampa) /* Se "stampa riga" */
                                {
                                    Speaker[NomiTrovati] = Parlante; /* Allora segna il codice del parlante nell'indice*/
                                    NomiTrovati++; /* Avendo trovato un nome per la riga numero "NomiTrovati", passa alla casella successiva di "NumeriTrovati" */
                                }
                                else if (IDSpeaker == 0x01)
                                {
                                    bc.ReadByte();
                                    bc.ReadByte();
                                }
                            }
                        }
                    }

                    CodiceDiErrore = 75;
                }


                else
                { /* Se l'utente sta lavorando su un qualsiasi altro gioco che non sia AE */

                    if (comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 3)
                    {
                        CodiceScelta = 0x32;
                        IndiceLastSprite = 0x3E;
                    }

                    while (NomiTrovati < NomiDaTrovare)
                    {
                        if (Bytecode.Position == Bytecode.Length)
                        { /* Se il Reader ha superato la lunghezza del file */
                            Speaker[NomiTrovati] = 38; /* Allora assegna "Errore" allo Speaker, in questo modo sarà possibile trovare con facilità i file con problemi*/
                            NomiTrovati++; /* Il contatore contierà a incrementare finché il while sarà attivo */
                        }
                        else
                        {
                            IDSpeaker = bc.ReadByte();
                            if (IDSpeaker == 0x70) /* Se il valore letto è uguale a 0x70, ovvero a "inizio istruzione", allora... */
                            {
                                IDSpeaker = bc.ReadByte();
                                if (IDSpeaker == 0x21 || IDSpeaker == 0x1E) /* Se il valore letto è uguale a "Speaker" o a "Sprite" */
                                {
                                    Parlante = bc.ReadByte();
                                    if (IDSpeaker == 0x1E) { Parlante = bc.ReadByte(); lastsprite = Parlante; } /* Se "Sprite", allora memorizza il codice del PG e salvalo anche in lastsprite */
                                    if (Parlante == IndiceLastSprite)
                                    {
                                        Parlante = lastsprite;
                                        /* 0x1C sta per "mostra il nome dell'ultimo PG mostrato come sprite, quindi bisogna ripescare il codice del PG contenuto nella variabile "lastsprite" */
                                    }
                                }
                                else if (IDSpeaker == CodiceScelta) /* Se il dialogo in realtà è una scelta */
                                {
                                    Parlante = 0x20; /* Allora segna "tale dialogo" come scleta */
                                    bc.ReadByte(); /* Salta l'offset per evitare pasticci */
                                }
                                else if (IDSpeaker == 0x03) /* Se il dialogo in realtà non ha un parlate */
                                {
                                    IDSpeaker = bc.ReadByte(); /* Leggi l'indice di CLT" */
                                    if (IDSpeaker == 0x17) /* Se l'indice di CLT è pari a 17 significa "Pulisci Speaker" */
                                    {
                                        Parlante = 0x1D; /* Allora segna "tale dialogo" come "CLT" */
                                    }

                                    else if (IDSpeaker == 0x04) /* Se l'indice di CLT è pari a 17 significa "Pulisci Speaker" */
                                    {
                                        Parlante = 0x00; /* Allora segna "tale dialogo" come "Naegi" */
                                    }

                                }
                                else if (IDSpeaker == 0x02) /* Se "stampa riga" */
                                {
                                    Speaker[NomiTrovati] = Parlante; /* Allora segna il codice del parlante nell'indice*/
                                    NomiTrovati++; /* Avendo trovato un nome per la riga numero "NomiTrovati", passa alla casella successiva di "NumeriTrovati" */
                                }
                            }
                        }
                    }
                }


                for (int i = 0; i < Speaker.Length; i++)
                {
                    if (Speaker[i] >= Nomi.Length)
                    {
                        Speaker[i] = (byte)CodiceDiErrore;
                    }
                }

            }

            /* Inizio estrazione testo dal textraw.bin */
            UInt32 Npuntatori = tr.ReadUInt32(); /* Numero di puntatori alle frasi, quindi numero di frasi presenti nel file */
            UInt32[] Puntatori = new UInt32[Npuntatori]; /* Vettore che conterrà il valore dei puntatori trovati */

            for (int i = 0; i < Npuntatori; i++)
            {
                Puntatori[i] = tr.ReadUInt32();
            }

            TestoRaw.Seek(Puntatori[0], SeekOrigin.Begin);

            int BOMPRESENTE = 0;

            if (comboBox2.SelectedIndex != 4) /* Se l'utente NON ha scelto "DR AE PSVITA" */
            {
                BOMPRESENTE = 2;
                txt.Write(tr.ReadUInt16());
            }
            else /* Se l'utente ha scelto "DR AE PSVITA" */
            {
                txt.Write((UInt16)0xFEFF);
            }

            for (int i = 0; i < Npuntatori; i++)
            {

                TestoRaw.Seek(Puntatori[i] + BOMPRESENTE, SeekOrigin.Begin);

                string Riga = null;

                if (checkBox2.CheckState == CheckState.Unchecked)
                { /* Se l'utente non ha scelto di eliminare i nomi e non sta lavorando a DR AE, allora stampa i nomi */
                    if (i < Speaker.Length)
                    {
                        Riga = "[" + Nomi[Speaker[i]] + "]\n";
                    }
                    else
                    {
                        Riga = "[ERROR]\n";
                    }
                }
                ushort Lettera = 1;

                while (TestoRaw.Position != TestoRaw.Length)
                {
                    Lettera = tr.ReadUInt16();

                    if (Lettera != 0)
                    {
                        Riga += (char)Lettera;
                    }

                    else
                        break;
                }

                Riga += "[END]\n\n";

                if (checkBox1.CheckState == CheckState.Checked)
                {
                    Riga = PulisciRigaDaiCodiciPerPSP(Riga);
                }
                txt.Write(Riga.ToCharArray());
            }

            Bytecode.Close();
            Bytecode.Dispose();
            TestoRaw.Close();
            TestoRaw.Dispose();
            Testo.Close();
            Testo.Dispose();
        }

        private string PulisciRigaDaiCodiciPerPSP(string Riga)
        {
            /* Inizio rimozione dei TAG inseriti dai PZ e non presenti su Steam */

            Riga = Riga.Replace("“", "\"");
            Riga = Riga.Replace("”", "\"");
            Riga = Riga.Replace("’", "'");

            int Posizione12 = 0;
            int Posizione8 = 0;
            int Posizione16 = 0;
            int Posizione21 = 0;

            while (Posizione12 != -1 || Posizione8 != -1 || Posizione16 != -1 || Posizione21 != -1)
            {
                Posizione12 = Riga.IndexOf("<CLT 12>");
                if (Posizione12 >= 0)
                {
                    Riga = Riga.Remove(Posizione12, 8);
                    Posizione12 = Riga.IndexOf("<CLT>", Posizione12);
                    if (Posizione12 >= 0)
                    {
                        Riga = Riga.Remove(Posizione12, 5);
                    }
                }

                Posizione8 = Riga.IndexOf("<CLT 8>");
                if (Posizione8 >= 0)
                {
                    Riga = Riga.Remove(Posizione8, 7);
                    Posizione8 = Riga.IndexOf("<CLT>", Posizione8);
                    if (Posizione8 >= 0)
                    {
                        Riga = Riga.Remove(Posizione8, 5);
                    }
                }

                Posizione16 = Riga.IndexOf("<CLT 16>");
                if (Posizione16 >= 0)
                {
                    Riga = Riga.Remove(Posizione16, 8);
                    Posizione16 = Riga.IndexOf("<CLT>", Posizione16);
                    if (Posizione16 >= 0)
                    {
                        Riga = Riga.Remove(Posizione16, 5);
                    }
                }

                Posizione21 = Riga.IndexOf("<CLT 21>");
                if (Posizione21 >= 0)
                {
                    Riga = Riga.Remove(Posizione21, 8);
                    Posizione21 = Riga.IndexOf("<CLT>", Posizione21);
                    if (Posizione21 >= 0)
                    {
                        Riga = Riga.Remove(Posizione21, 5);
                    }
                }
            }
            /* Fine rimozione dei TAG inseriti dai PZ e non presenti su Steam */

            return Riga;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog cdfile = new OpenFileDialog();
            cdfile.Filter = ".pak |*.pak|font.pak|font.pak|All Files (*.*)|*.*";
            cdfile.Multiselect = true;

            if (cdfile.ShowDialog() == DialogResult.OK)
            {

                if (comboBox1.SelectedIndex == 0) /* Se l'utente ha selezionato TextMenu */
                {
                    foreach (string file in cdfile.FileNames)
                    {
                        UnpackTextMenuPAK(file, "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                    }
                }

                else if (comboBox1.SelectedIndex == 1) /* Se l'utente ha selezionato ScriptText Type 1 */
                {
                    foreach (string file in cdfile.FileNames)
                    {
                        string directory = "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem + "\\" + Path.GetFileNameWithoutExtension(file);
                        UnpackTexturePAK(file, directory, 1);
                        foreach (string files in Directory.GetFiles(directory))
                        {
                            UnpackLIN(files, directory + "\\" + Path.GetFileNameWithoutExtension(files));
                        }
                    }
                }

                else if (comboBox1.SelectedIndex == 2) /* Se l'utente ha selezionato ScriptText Type 2 */
                {
                    foreach (string file in cdfile.FileNames)
                    {
                        string directory = "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem + "\\" + Path.GetFileNameWithoutExtension(file);
                        UnpackTexturePAK(file, directory, 2);
                        foreach (string files in Directory.GetFiles(directory))
                        {
                            UnpackTextMenuPAK(files, "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem + "\\" + Path.GetFileNameWithoutExtension(file) + "\\" + Path.GetFileNameWithoutExtension(files));
                        }
                    }
                }

                else if (comboBox1.SelectedIndex == 4 || comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 3) /* Se l'utente ha scelto "Texture (TGA)" o "Texture (PNG)" o "Font" */
                {
                    foreach (string file in cdfile.FileNames) /* Il programma spacchetta completamente tutto il contenuto del PAK, ma non esegue nessuna conversione */
                    {
                        string directory = "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem + "\\" + Path.GetFileNameWithoutExtension(file);

                        /* In DR AE per PSVITA sono presenti un paio di file PAK criptati, quindi per evitare dei crash il programa controlla se sono criptati o no*/
                        if (comboBox2.SelectedIndex == 4)
                        {
                            FileStream PAK = new FileStream(file, FileMode.Open, FileAccess.Read);
                            BinaryReader PAB = new BinaryReader(PAK);

                            if (PAB.ReadUInt32() == 0xA755AAFC)
                            {
                                PAK.Close();
                                PAK.Dispose();
                                string temp = file.Replace("\\", "/");
                                Decrypt("Ext\\dr_dec.py", "\u0022" + temp + "\u0022");
                            }
                        }

                        UnpackTexturePAK(file, directory, 0);
                        foreach (string files in Directory.GetFiles(directory, "*.pak"))
                        {
                            directory = "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem + "\\" + Path.GetFileNameWithoutExtension(file) + "\\" + Path.GetFileNameWithoutExtension(files);
                            UnpackTexturePAK(files, directory, 3);

                            /* In DR2 sono presenti dei PAK dentro i PAK dentro altri PAK, quindi se l'utente ha scelto "DR2" o "DR2 PSVITA", verrà fatto un ciclo extra per estrarre i files dai pak */
                            if ((comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 3) && Directory.Exists(directory) == true)
                            {
                                foreach (string filesub in Directory.GetFiles(directory, "*.pak"))
                                {
                                    directory = "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem + "\\" + Path.GetFileNameWithoutExtension(file) + "\\" + Path.GetFileNameWithoutExtension(files) + "\\" + Path.GetFileNameWithoutExtension(filesub);
                                    UnpackTexturePAK(filesub, directory, 3);
                                }
                            }
                        }
                    } /* "In questo momento" i file si trovano in formato originale (infatti non sono ancora stati convertiti),
                          quindi sono in formato TGA se sono di Steam, altrimenti sono in formato GXT (criptato) se provengono da PSVITA */

                    /* Inizio conversione da TGA a PNG dei file provenienti da Steam */
                    if ((comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 3) && (comboBox2.SelectedIndex == 0 || comboBox2.SelectedIndex == 1)) /* Se l'utente ha scelto "Texture(PNG)" o "Font" e ha scelto "Working on: DR1/2" NON PER VITA */
                    {   /* Seleziona OGNI singolo file.tga dalla cartalla scelta e dalle sue sottocartelle */
                        foreach (string tga in Directory.GetFiles("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem, "*.tga", SearchOption.AllDirectories))
                        {   /* Passa il file.tga prelevato al programma "conver.exe" insieme alla riga di comando per convertire il TGA in un PNG compresso mantenendo il massimo della qualità */
                            string rigadapassare = "convert \u0022" + tga + "\u0022 " + "-quality 100" + " \u0022" + Path.GetDirectoryName(tga) + "\\" + Path.GetFileNameWithoutExtension(tga) + ".png\u0022";
                            rigadapassare = rigadapassare.Replace("\\", "/");
                            Decrypt("Ext\\convert.exe", rigadapassare);
                            File.Delete(tga);
                        }
                    }

                    /* Inizio decriptaggio e conversione dei files provienienti dai DR per PSVITA */
                    else if (comboBox2.SelectedIndex == 2 || comboBox2.SelectedIndex == 3 || comboBox2.SelectedIndex == 4)
                    {   /* Decripta tutti i file.GXT e i file.BTX presenti nella cartella "Unpack..." */
                        Decrypt("Ext\\dr_dec.py", "\u0022Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem + "\u0022");
                        /* Se l'utente sta lavorando su DR AE per PSVITA */
                        if (comboBox2.SelectedIndex == 4)
                        {
                            /* Converte in PNG ogni singolo file.BTX nella cartella "Unpack..." */
                            foreach (string btx in Directory.GetFiles("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem, "*.btx", SearchOption.AllDirectories))
                            {
                                string rigadapassare = "convert \u0022" + btx + "\u0022";
                                rigadapassare = rigadapassare.Replace("\\", "/");
                                Decrypt("Ext\\shtx_conv.py", rigadapassare);
                                if (File.Exists(Path.GetDirectoryName(btx) + "\\" + Path.GetFileNameWithoutExtension(btx) + ".png") == true)
                                {
                                    File.Delete(btx); /* Cancella il file.gxt dopo averlo convertito in PNG */
                                }
                            }

                            /* Converte in PNG ogni singolo file.GXT (che in realtà è un BTX) nella cartella "Unpack..." */
                            foreach (string gxt in Directory.GetFiles("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem, "*.gxt", SearchOption.AllDirectories))
                            {
                                string rigadapassare = "convert \u0022" + gxt + "\u0022";
                                rigadapassare = rigadapassare.Replace("\\", "/");
                                Decrypt("Ext\\shtx_conv.py", rigadapassare);
                                if (File.Exists(Path.GetDirectoryName(gxt) + "\\" + Path.GetFileNameWithoutExtension(gxt) + ".png") == true)
                                {
                                    File.Delete(gxt); /* Cancella il file.gxt dopo averlo convertito in PNG */
                                }
                            }

                            /* Converte in PNG ogni singolo file.BTX decriptato (che in realtà è un GXT) nella cartella "Unpack..." */
                            foreach (string btx in Directory.GetFiles("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem, "*.btx", SearchOption.AllDirectories))
                            {
                                string rigadapassare = "convert \u0022" + btx + "\u0022";
                                rigadapassare = rigadapassare.Replace("\\", "/");
                                Decrypt("Ext\\GXTConvert.exe", rigadapassare);
                                if (File.Exists(Path.GetDirectoryName(btx) + "\\" + Path.GetFileNameWithoutExtension(btx) + ".png") == true)
                                {
                                    File.Delete(btx); /* Cancella il file.gxt dopo averlo convertito in PNG */
                                }
                            }
                        }
                        /* Converte in PNG ogni singolo file.GXT decriptato nella cartella "Unpack..." */
                        foreach (string gxt in Directory.GetFiles("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem, "*.gxt", SearchOption.AllDirectories))
                        {
                            string rigadapassare = "convert \u0022" + gxt + "\u0022";
                            rigadapassare = rigadapassare.Replace("\\", "/");
                            Decrypt("Ext\\GXTConvert.exe", rigadapassare);
                            File.Delete(gxt); /* Cancella il file.gxt dopo averlo convertito in PNG */
                        }

                        if (comboBox1.SelectedIndex == 4) /* Se l'utente ha scelto "Texture (TGA)" e "Working on DR1/DR2 PSVITA" */
                        {
                            foreach (string png in Directory.GetFiles("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem, "*.png", SearchOption.AllDirectories))
                            {
                                string rigadapassare = "convert \u0022" + png + "\u0022 " + "-compress RLE" + " \u0022" + Path.GetDirectoryName(png) + "\\" + Path.GetFileNameWithoutExtension(png) + ".tga\u0022";
                                rigadapassare = rigadapassare.Replace("\\", "/");
                                Decrypt("Ext\\convert.exe", rigadapassare);
                                File.Delete(png);
                            }
                        }
                    }
                }
                cdfile.Dispose();
                GC.Collect();
                MessageBox.Show("Done!");
            }
        }

        public static void Decrypt(string programma, string filename)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.FileName = programma;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = filename;

            try
            {
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }

            catch
            {
                MessageBox.Show("Unable to run " + programma + ".\nAre you sure that you have it in the same folder of this tool?", programma + " not found!");
            }
        }

        private void UnpackTextMenuPAK(string filename, string directory)
        {
            FileStream PAK = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader pk = new BinaryReader(PAK, Encoding.Unicode);

            Directory.CreateDirectory(directory);

            FileStream Testo = new FileStream(directory + "\\" + Path.GetFileNameWithoutExtension(filename) + ".txt", FileMode.Create, FileAccess.Write);
            BinaryWriter txt = new BinaryWriter(Testo, Encoding.Unicode);

            /* Inizio estrazione testo dal textraw.bin */
            UInt32 Npuntatori = pk.ReadUInt32(); /* Numero di puntatori alle frasi, quindi numero di frasi presenti nel file */
            UInt32[] Puntatori = new UInt32[Npuntatori]; /* Vettore che conterrà il valore dei puntatori trovati */

            for (int i = 0; i < Npuntatori; i++)
            {
                Puntatori[i] = pk.ReadUInt32();
            }

            PAK.Seek(Puntatori[0], SeekOrigin.Begin);
            txt.Write(pk.ReadUInt16());

            for (int i = 0; i < Npuntatori; i++)
            {
                PAK.Seek(Puntatori[i] + 2, SeekOrigin.Begin);

                string Riga = null;
                ushort Lettera = 1;

                while (PAK.Position != PAK.Length)
                {
                    Lettera = pk.ReadUInt16();

                    if (Lettera != 0)
                    {
                        Riga += (char)Lettera;
                    }

                    else
                        break;
                }

                Riga += "[END]\n\n";

                if (checkBox1.CheckState == CheckState.Checked)
                {
                    Riga = PulisciRigaDaiCodiciPerPSP(Riga);
                }

                txt.Write(Riga.ToCharArray());
            }

            PAK.Close();
            PAK.Dispose();
            Testo.Close();
            Testo.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Directory.Exists("Unpack LIN " + comboBox2.SelectedItem))
            {
                foreach (string cartella in Directory.GetDirectories("Unpack LIN " + comboBox2.SelectedItem))
                {
                    PackLIN(cartella, "RePack LIN " + comboBox2.SelectedItem);
                }
                GC.Collect();
                MessageBox.Show("Done!");
            }
        }

        private void PackLIN(string cartella, string directoryRePack)
        {
            FileStream Bytecode = new FileStream(cartella + "\\" + "Bytecode.bin", FileMode.Open, FileAccess.Read);
            FileStream Testo = new FileStream(cartella + "\\" + Path.GetFileNameWithoutExtension(cartella) + ".txt", FileMode.Open, FileAccess.Read);
            StreamReader txt = new StreamReader(Testo, Encoding.Unicode);

            Directory.CreateDirectory(directoryRePack);

            FileStream NewLIN = new FileStream(directoryRePack + "\\" + Path.GetFileNameWithoutExtension(cartella) + ".lin", FileMode.Create, FileAccess.Write);
            BinaryWriter Nin = new BinaryWriter(NewLIN);
            BinaryWriter Ni = new BinaryWriter(NewLIN, Encoding.Unicode);

            string[] Nomi = null;


            if (comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 3) /* Se l'utente ha scelto "Working on DR2" o "DR2 PSVITA" */
            {
                Nomi = new string[] { "HINATA", "KOMAEDA", "TOGAMI", "TANAKA", "SOUDA", "HANAMURA", "NIDAI", "KUZURYUU", "OWARI", "NANAMI", "SONIA", "SAIONJI", "KOIZUMI", "TSUMIKI", "MIODA", "PEKOYAMA", "MONOKUMA", "MONOMI", "ENOSHIMA", "MECHA NIDAI", "NAEGI", "KIRIGIRI", "TOGAMI REAL", "HANAMURA MOM", "ALTER EGO", "MINI NIDAI", "MONOKUMA MONOMI", "NARRATION", "ERROR", "EMPTY SPEAKER", "ERROR", "ERROR", "CHOICE/RE:ACT", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "USAMI", "KIRAKIRA", "NO NAME", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ENOSHIMA 2", "ERROR", "GIRL A", "GIRL B", "GIRL C", "GIRL D", "GIRL E", "BOY F", "NO NAME 2", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "LAST SPRITE", "EMPTY SPEAKER" };
            }
            else if (comboBox2.SelectedIndex == 0 || comboBox2.SelectedIndex == 2) /* Se l'utente ha scelto "Working on DR1" o "DR1 PSVITA" */
            {
                Nomi = new string[] { "NAEGI", "ISHIMARU", "TOGAMI", "OOWADA", "KUWATA", "YAMADA", "HAGAKURE", "MAIZONO", "KIRIGIRI", "ASAHINA", "FUKAWA", "OOGAMI", "CELES", "ENOSHIMA (MUKURO)", "FUJISAKI", "MONOKUMA", "ENOSHIMA (REAL)", "ALTER EGO", "GENOCIDER", "HEADMASTER", "NAEGI MOTHER", "NAEGI FATHER", "NAEGI SISTER", "ERROR", "ISHIDA", "DAIA OOWADA", "ERROR", "ERROR", "LAST SPRITE", "EMPTY SPEAKER", "NO NAME", "NARRATION", "CHOICE/RE:ACT", "USAMI", "MONOKUMA BACKUP", "MONOKUMA BACKUP (R)", "MONOKUMA BACKUP (L)", "MONOKUMA BACKUP (M)", "ERROR" };
            }
            else if (comboBox2.SelectedIndex == 4) /* Se l'utente ha scelto "Working on DR AE PSVITA" */
            {
                Nomi = new string[] { "KOMARU NAEGI", "TOUKO FUKAWA", "GENOCIDER SHOU", "MASARU DAIMON", "JATARO KEMURI", "KOTOKO UTSUGI", "NAGISA SHINGETSU", "MONACA", "SERVANT", "KUROKUMA", "HAIJI TOUWA", "TOUICHI TOUWA", "SHIROKUMA", "YUUTA ASAHINA", "HIROKO HAGAKURE", "???", "MAKOTO NAEGI", "BYAKUYA TOGAMI", "MONOKUMA KID", "MONOKUMA KID", "FUTURE FONDATION", "FUTURE FONDATION A", "FUTURE FONDATION B", "FUTURE FONDATION C", "FUTURE FONDATION D", "FUTURE FONDATION E", "FUTURE FONDATION F", "ADULT", "ADULT A", "ADULT B", "ADULT C", "ADULT D", "ADULT E", "ADULT F", "ADULT G", "ADULT H", "ADULT I", "ADULT J", "ADULT K", "ADULT L", "ADULT M", "ADULT N", "ADULT O", "ADULT P", "ADULT Q", "ADULT R", "ADULT S", "ADULT T", "ADULT U", "ADULT V", "ADULT W", "ADULT X", "ADULT Y", "ADULT Z", "ADULTS", "ADULT", "ADULT", "DUMMY 1", "DUMMY 2", "DUMMY 3", "DUMMY 4", "DUMMY 5", "DUMMY 6", "DUMMY 7", "DUMMY 8", "DUMMY 9", "DUMMY 10", "DUMMY 11", "DUMMY 12", "JUNKO ENOSHIMA", "WARRIOS OF HOPE", "KID SHOP MANAGER", "EMPTY SPEAKER", "???", "???", "ERROR", "SYSTEM/CHOICE" };
            }

            string line = txt.ReadToEnd();
            Testo.Close();
            Testo.Dispose();

            for (int i = 0; i < Nomi.Length; i++)
            {
                line = line.Replace("[" + Nomi[i] + "]\n", null);
            }

            line = line.Replace("[END]\n\n", "\0");
            String[] lines = line.Split('\0');

            int DimensioneZonaPuntatori = (lines.Length + 1) * 4;

            Nin.Write((UInt32)0x02);
            Nin.Write((UInt32)0x10);
            Nin.Write((UInt32)(0x10 + Bytecode.Length));
            long pos = NewLIN.Position;
            Nin.Write((UInt32)(0x0));

            Bytecode.CopyTo(NewLIN, 0x10);

            Bytecode.Close();
            Bytecode.Dispose();
            Nin.Write((UInt32)lines.Length - 1);
            Nin.Write((UInt32)DimensioneZonaPuntatori);

            int BOMPRESENTE = 2;

            if (comboBox2.SelectedIndex == 4) /* Se l'utente ha scelto "DR AE PSVITA" */
            {
                BOMPRESENTE = 1; /* DR AE non ha i BOM nei file, quindi non va contato. Si conta solo il terminatore di stringhe */
            }

            for (int i = 0; i < lines.Length - 1; i++) /* Calcola lo spazio necessario per i puntatori */
            {
                DimensioneZonaPuntatori += (((int)lines[i].Length + BOMPRESENTE) * 2);

                Nin.Write((UInt32)DimensioneZonaPuntatori);
            }

            for (int i = 0; i < lines.Length - 1; i++)
            {
                if (comboBox2.SelectedIndex != 4) /* Se l'utente NON ha scelto "DR AE PSVITA", allora non inserire il BOM */
                {
                    Nin.Write((UInt16)0xFEFF);
                }
                Ni.Write(lines[i].ToCharArray()); /* Inserisci la riga "i" nel nuovo LIN */

                Nin.Write((UInt16)0x00); /* Inserisci i zero di chiusura */
            }

            if (comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 3) /* Se l'utente ha scelto "Working on DR2" o "DR2 PSVITA" */
            {
                if (NewLIN.Position % 0x02 != 0)
                {
                    while (NewLIN.Position % 0x02 != 0) /* DR2 usa un padding di 2 */
                    {
                        Nin.Write((byte)0x0);
                    }
                }
            }

            else
            {
                if (NewLIN.Position % 0x04 != 0)
                {
                    while (NewLIN.Position % 0x04 != 0) /* DR1 e AE usano un padding di 4 */
                    {
                        Nin.Write((byte)0x0);
                    }
                }
            }

            Nin.Seek((int)pos, SeekOrigin.Begin);
            Nin.Write((UInt32)NewLIN.Length);

            NewLIN.Close();
            NewLIN.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            /* Se l'utente ha scelto RePack MenuText ed esiste la cartella aposita */
            if (comboBox1.SelectedIndex == 0 && Directory.Exists("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem))
            {
                /* Converti in PAK ogni file presente in "Unpack..." */
                foreach (string files in Directory.GetFiles("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem))
                {
                    PackMenuTextPAK(files, "RePack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                }
            }

            /* Se l'utente ha scelto RePack ScripText Type 1 */
            else if (comboBox1.SelectedIndex == 1 && Directory.Exists("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem))
            {
                string directory = "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem;
                /* Per ogni cartella in Unpack... */
                foreach (string cartella in Directory.GetDirectories(directory))
                { /* Passa la sottocartella a PackLIN affinché tutti i file nella sotto cartella vengano convertiti in LIN e inseriti
                    nella cartella che contiene la sottocartella*/
                    foreach (string sottocartella in Directory.GetDirectories(cartella))
                    {
                        PackLIN(sottocartella, cartella);
                    }
                }

                Directory.CreateDirectory("RePack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                /* Prende il contenuto di tutte le cartelle in "Unpack" e lo converte in un PAK */
                foreach (string cartella in Directory.GetDirectories(directory))
                {
                    PackTexturePAK(cartella, "RePack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                }
            }

            /* Se l'utente ha scelto RePack ScripText Type 2 */
            else if (comboBox1.SelectedIndex == 2 && Directory.Exists("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem))
            {
                string directory = "Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem;
                /* Per ogni cartella in Unpack... */
                foreach (string cartella in Directory.GetDirectories(directory))
                { /* Passa la sottocartella a PackLIN affinché tutti i file nella sotto cartella vengano convertiti in LIN e inseriti
                    nella cartella che contiene la sottocartella*/
                    foreach (string sottocartella in Directory.GetDirectories(cartella))
                    {
                        PackMenuTextPAK(sottocartella + "\\" + Path.GetFileNameWithoutExtension(sottocartella) + ".txt", cartella);
                    }
                }

                Directory.CreateDirectory("RePack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                /* Prende il contenuto di tutte le cartelle in "Unpack" e lo converte in un PAK */
                foreach (string cartella in Directory.GetDirectories(directory))
                {
                    PackTexturePAK(cartella, "RePack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                }
            }

            /* Se l'utente ha scelto Repack Texture (PNG) o (TGA) */
            else if ((comboBox1.SelectedIndex == 4 || comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 3) && Directory.Exists("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem))
            {
                DirectoryInfo diSource = new DirectoryInfo("Unpack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                DirectoryInfo diTarget = new DirectoryInfo("temp");
                /* Ho optato per creare una copia della cartella "Unpack..." e di chiarmarla "temp" per evitare di combinare guai in caso di errori.
                So che rallenta il lavoro del programma, ma penso sia meglio andare lenti e sicuri. Inoltre la cartella "temp" è obbligatoria se l'utente
                sta lavorando coi PNG. In tal caso i PNG vanno convertiti in TGA e, dopo la conversione, cancellati per evitare che finiscano dentro il PAK */
                CopyAll(diSource, diTarget);
                string directory = "temp";

                /* Cancella dalla cartella "temp" tutti i progetti di Phostoshop e GIMP per evitare di inserirli all'interno del PAK */
                foreach (string psd in Directory.GetFiles(directory, "*.psd", SearchOption.AllDirectories))
                {
                    File.Delete(psd);
                }

                foreach (string xcf in Directory.GetFiles(directory, "*.xcf", SearchOption.AllDirectories))
                {
                    File.Delete(xcf);
                }

                foreach (string pdn in Directory.GetFiles(directory, "*.pdn", SearchOption.AllDirectories))
                {
                    File.Delete(pdn);
                }

                /* Se l'utente ha scelto RePack Texture (PNG) o Font, allora inizia la conversione dei PNG a TGA per i PAK di Steam */
                if (comboBox1.SelectedIndex == 5 || comboBox1.SelectedIndex == 3)
                {
                    foreach (string png in Directory.GetFiles(directory, "*.png", SearchOption.AllDirectories))
                    {
                        string rigadapassare = "convert \u0022" + png + "\u0022 " + "-compress RLE" + " \u0022" + Path.GetDirectoryName(png) + "\\" + Path.GetFileNameWithoutExtension(png) + ".tga\u0022";
                        rigadapassare = rigadapassare.Replace("\\", "/");
                        Decrypt("Ext\\convert.exe", rigadapassare);
                        File.Delete(png);
                    }
                }

                /* Se si sta lavorando su DR2 o DR2 PSVITA, allora il Repack comincia da qui, dalle sottocartelle delle sottocartelle delle cartelle */
                if (comboBox2.SelectedIndex == 1 || comboBox2.SelectedIndex == 3)
                {  /* Esegue il RePack delle sottocartelle delle sottocartelle delle cartelle all'interno di "Unpack...." */
                    foreach (string cartella in Directory.GetDirectories(directory))
                    { /* Per ogni cartella presente nella cartella principale */
                        foreach (string sottocartella in Directory.GetDirectories(cartella))
                        { /* Per ogni sottocartella presente nella cartella */
                            foreach (string sottosottocartella in Directory.GetDirectories(sottocartella))
                            { /* Prendi la sotto-sottocartella e passala a PackTexturePAK in modo che la sotto-sottocartella venga convertita in PAK */
                                PackTexturePAK(sottosottocartella, sottocartella);
                            }
                        }
                    }
                }

                /* Inizio Repack vero e proprio in caso non si sta lavorando a DR2 o DR2 PSVITA */

                /* Esegue il RePack delle sottocartelle delle cartelle all'interno di "Unpack...." */
                foreach (string cartella in Directory.GetDirectories(directory))
                { /* Per ogni cartella presente nella cartella principale */
                    foreach (string sottocartella in Directory.GetDirectories(cartella))
                    { /* Prendi la sottocartella e passala a PackTexturePAK in modo che la sottocartella venga convertita in PAK */
                        PackTexturePAK(sottocartella, cartella);
                    }
                }

                Directory.CreateDirectory("RePack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                /* Finalmente avviene la conversione in PAK delle cartelle principale contenute in "Unpack..." */
                foreach (string cartella in Directory.GetDirectories(directory))
                { /* I Pak delle cartelle principali vengono salvate nella "nuova cartella" chiavamata "Repack..." */
                    PackTexturePAK(cartella, "RePack " + comboBox1.SelectedItem + " " + comboBox2.SelectedItem);
                }
                /* La cartella "temp" non serve più, quindi va cancellata */
                if (Directory.Exists("temp") == true)
                {
                    Directory.Delete("temp", true);
                }
            }
            MessageBox.Show("Done!");
        }

        private void PackMenuTextPAK(string file, string directory)
        {
            FileStream Testo = new FileStream(file, FileMode.Open, FileAccess.Read);
            StreamReader txt = new StreamReader(Testo, Encoding.Unicode);

            Directory.CreateDirectory(directory);

            FileStream NewPAK = new FileStream(directory + "\\" + Path.GetFileNameWithoutExtension(file) + ".pak", FileMode.Create, FileAccess.Write);
            BinaryWriter pak = new BinaryWriter(NewPAK);
            BinaryWriter pk = new BinaryWriter(NewPAK, Encoding.Unicode);

            string line = txt.ReadToEnd();
            Testo.Close();
            Testo.Dispose();
            line = line.Replace("[END]\n\n", "\0");

            String[] lines = line.Split('\0');

            UInt32[] DimensioneZonaPuntatori = new UInt32[lines.Length];

            for (int i = 0; i < lines.Length + 1; i++)
            {
                pak.Write((UInt32)0x00);
            }

            DimensioneZonaPuntatori[0] = (UInt32)NewPAK.Position;

            for (int i = 0; i < lines.Length - 1; i++)
            {
                pak.Write((UInt16)0xFEFF);
                pk.Write(lines[i].ToCharArray());
                pak.Write((UInt16)0x00);

                if (NewPAK.Position % 0x04 != 0)
                {
                    while (NewPAK.Position % 0x04 != 0)
                    {
                        pak.Write((byte)0x0);
                    }
                }

                if (i < DimensioneZonaPuntatori.Length - 1)
                {
                    DimensioneZonaPuntatori[i + 1] = (UInt32)NewPAK.Position;
                }
            }

            NewPAK.Seek(0x0, SeekOrigin.Begin);
            pak.Write(lines.Length - 1);

            for (int i = 0; i < lines.Length; i++)
            {
                pak.Write(DimensioneZonaPuntatori[i]);
            }

            NewPAK.Close();
            NewPAK.Dispose();
        }

        private void UnpackTexturePAK(string filename, string directory, int flag)
        {
            FileStream PAK = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader PAB = new BinaryReader(PAK);

            PAK.Seek(0x0, SeekOrigin.Begin);

            Directory.CreateDirectory(directory);

            try
            {
                UInt32 numfiles = PAB.ReadUInt32();

                for (int i = 0; i < numfiles; i++)
                {
                    UInt32 filesize = 0;
                    UInt32 Offset1 = PAB.ReadUInt32();

                    long pos = PAK.Seek(0x00, SeekOrigin.Current);

                    if (i != numfiles - 1)
                    {
                        UInt32 Offset2 = PAB.ReadUInt32();
                        PAK.Seek(-0x04, SeekOrigin.Current);
                        filesize = Offset2 - Offset1;
                    }

                    else
                    {
                        filesize = (UInt32)(PAK.Length - Offset1);
                    }

                    PAK.Seek(Offset1, SeekOrigin.Begin);
                    FileStream Extract = null;

                    byte[] data = new byte[filesize];
                    PAK.Read(data, 0, data.Length);

                    if (data[0] == 0xF0 && data[1] == 0x30 && data[2] == 0x60 && data[3] == 0x90)
                    {
                        Array.Copy(data, 4, data, 0, data.Length - 4);
                    }

                    if (comboBox1.SelectedIndex == 1 && flag == 1)
                    {
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".lin", FileMode.Create, FileAccess.Write);
                    }
                    else if (comboBox1.SelectedIndex == 2 && flag == 2)
                    {
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".pak", FileMode.Create, FileAccess.Write);
                    }
                    else if (data[0] == 0x4C && data[1] == 0x4C && data[2] == 0x46 && data[3] == 0x53)
                    {
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".llfs", FileMode.Create, FileAccess.Write);
                    }
                    else if (data[0] == 0x4F && data[1] == 0x4D && data[2] == 0x47 && data[3] == 0x2E && data[4] == 0x30 && data[5] == 0x30)
                    {
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".gmo", FileMode.Create, FileAccess.Write);
                    }
                    else if (data[0] == 0xFC && data[1] == 0xAA && data[2] == 0x55 && data[3] == 0xA7 || data[0] == 0x47 && data[1] == 0x58 && data[2] == 0x54 && data[3] == 0x00)
                    { /* I file gxt sono un formato immagine usato in DR1 e DR2 per PSVITA */
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".gxt", FileMode.Create, FileAccess.Write);
                    }
                    else if (data[0] == 0x53 && data[1] == 0x48 && data[2] == 0x54 && data[3] == 0x58)
                    { /* I file btx sono un formato immagine usato in DR AE PSVITA */
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".btx", FileMode.Create, FileAccess.Write);
                    }
                    else if (data[0] == 0x74 && data[1] == 0x46 && data[2] == 0x70 && data[3] == 0x53)
                    { /* I file font sono un formato usato in DR per i font su Steam */
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".font", FileMode.Create, FileAccess.Write);
                    }
                    else if (data[0] == 0x4D && data[1] == 0x49 && data[2] == 0x47 && data[3] == 0x2E && data[4] == 0x30 && data[5] == 0x30)
                    { /* I file gim sono un formato immagine usato in DR per PSP */
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".gim", FileMode.Create, FileAccess.Write);
                    }
                    else if (data.Length < 0x70 || data[0] == 0xFF && data[1] == 0xFE)
                    {
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".unknown", FileMode.Create, FileAccess.Write);
                    }
                    else if (data[0] == 0x00 && data[1] == 0x01 && data[2] == 0x01 && data[3] == 0x00 || data[0] == 0x00 && data[1] == 0x00 && data[2] == 0x02 && data[3] == 0x00 || data[0] == 0x01 && data[1] == 0x00 && data[2] == 0x02 && data[3] == 0x00 || data[0] == 0x00 && data[1] == 0x00 && data[2] == 0x0A && data[3] == 0x00 || data[0] == 0x00 && data[1] == 0x01 && data[2] == 0x09 && data[3] == 0x00 || data[0] == 0x00 && data[1] == 0x00 && data[2] == 0x0B && data[3] == 0x00)
                    {
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".tga", FileMode.Create, FileAccess.Write);
                    }
                    else
                    {
                        Extract = new FileStream(directory + "\\" + i.ToString() + ".pak", FileMode.Create, FileAccess.Write);
                    }
                    Extract.Write(data, 0, data.Length);
                    Extract.Close();

                    PAK.Seek(pos, SeekOrigin.Begin);
                }
                PAK.Close();
                PAK.Dispose();
                /* Se la cartella esiste e non contiene cartelle o files*/
                if (Directory.Exists(directory) == true && Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                { /* Cancella la cartella */
                    Directory.Delete(directory);
                    if (flag == 3) /* Se il programma NON sta lavorando direttamente coi file principali scelti dall'utente */
                    { /* Rinomina il file */
                        File.Move(filename, Path.ChangeExtension(filename, ".unknown"));
                    }
                } /* Altrimenti se la cartella esiste, NON si sta lavorando coi file principali e la cartella contiene almeno un file grafico */
                else if ((Directory.Exists(directory) == true && flag == 3) && (Directory.GetFiles(directory, "*.llfs").Length != 0 || Directory.GetFiles(directory, "*.pak").Length != 0 || Directory.GetFiles(directory, "*.tga").Length != 0 || Directory.GetFiles(directory, "*.gxt").Length != 0 || Directory.GetFiles(directory, "*.gmo").Length != 0 || Directory.GetFiles(directory, "*.lin").Length != 0 || Directory.GetFiles(directory, "*.gim").Length != 0))
                {/* Cancella il file da cui si è estratto tutto */
                    File.Delete(filename);
                }
                else if (flag == 3)
                { /* Altrimenti, se la cartella esiste e non contiene nulla di rilevante, cancellala */
                    if (Directory.Exists(directory) == true)
                    {
                        Directory.Delete(directory, true);
                    } /* E se NON si sta lavorando coi file principali, rinomina l'estensione del file in "sconosciuto" */
                    File.Move(filename, Path.ChangeExtension(filename, ".unknown"));
                }
            }
            catch
            {
                PAK.Close();
                PAK.Dispose();
                if (Directory.Exists(directory) == true)
                { /* Se la cartella esiste, cancellala */
                    Directory.Delete(directory, true);
                }
                if (flag == 3)
                { /* E se NON si sta lavorando coi file principali, rinomina l'estensione del file in sconosciuto */
                    if (File.Exists(Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + ".unknown") == true)
                    { /* Se la cartella esiste, cancellala */
                        File.Delete(Path.GetDirectoryName(filename) + "\\" + Path.GetFileNameWithoutExtension(filename) + ".unknown");
                    }
                    File.Move(filename, Path.ChangeExtension(filename, ".unknown"));
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PackTexturePAK(string cartella, string directory)
        {
            FileStream NewPAK = new FileStream(directory + "\\" + Path.GetFileNameWithoutExtension(cartella) + ".pak", FileMode.Create, FileAccess.Write);
            BinaryWriter pak = new BinaryWriter(NewPAK);

            string[] QuantitaFile = Directory.GetFiles(cartella, "*");
            Array.Sort(QuantitaFile, new AlphanumComparatorFast());
            UInt32[] DimensioneZonaPuntatori = new UInt32[QuantitaFile.Length];

            for (int i = 0; i < QuantitaFile.Length + 1; i++)
            {
                pak.Write((UInt32)0x00);
            }

            if (NewPAK.Position % 0x10 != 0)
            {
                while (NewPAK.Position % 0x10 != 0)
                {
                    pak.Write((byte)0x0);
                }
            }

            DimensioneZonaPuntatori[0] = (UInt32)NewPAK.Position;

            for (int i = 0; i < QuantitaFile.Length; i++)
            {
                FileStream FileTemp = new FileStream(QuantitaFile[i], FileMode.Open, FileAccess.Read);
                byte[] DatiFile = new byte[FileTemp.Length];
                FileTemp.Read(DatiFile, 0, DatiFile.Length);
                FileTemp.Close();
                FileTemp.Dispose();
                NewPAK.Write(DatiFile, 0, DatiFile.Length);
                if (NewPAK.Position % 0x10 != 0 && i < QuantitaFile.Length - 1)
                {
                    while (NewPAK.Position % 0x10 != 0)
                    {
                        pak.Write((byte)0x0);
                    }
                }

                if (i < DimensioneZonaPuntatori.Length - 1)
                {
                    DimensioneZonaPuntatori[i + 1] = (UInt32)NewPAK.Position;
                }
            }

            NewPAK.Seek(0x0, SeekOrigin.Begin);
            pak.Write(QuantitaFile.Length);

            for (int i = 0; i < QuantitaFile.Length; i++)
            {
                pak.Write(DimensioneZonaPuntatori[i]);
            }

            NewPAK.Close();
            NewPAK.Dispose();
        }

        public class AlphanumComparatorFast : IComparer
        {
            public int Compare(object x, object y)
            {
                string s1 = x as string;
                if (s1 == null)
                {
                    return 0;
                }
                string s2 = y as string;
                if (s2 == null)
                {
                    return 0;
                }

                int len1 = s1.Length;
                int len2 = s2.Length;
                int marker1 = 0;
                int marker2 = 0;

                // Walk through two the strings with two markers.
                while (marker1 < len1 && marker2 < len2)
                {
                    char ch1 = s1[marker1];
                    char ch2 = s2[marker2];

                    // Some buffers we can build up characters in for each chunk.
                    char[] space1 = new char[len1];
                    int loc1 = 0;
                    char[] space2 = new char[len2];
                    int loc2 = 0;

                    // Walk through all following characters that are digits or
                    // characters in BOTH strings starting at the appropriate marker.
                    // Collect char arrays.
                    do
                    {
                        space1[loc1++] = ch1;
                        marker1++;

                        if (marker1 < len1)
                        {
                            ch1 = s1[marker1];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                    do
                    {
                        space2[loc2++] = ch2;
                        marker2++;

                        if (marker2 < len2)
                        {
                            ch2 = s2[marker2];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                    // If we have collected numbers, compare them numerically.
                    // Otherwise, if we have strings, compare them alphabetically.
                    string str1 = new string(space1);
                    string str2 = new string(space2);

                    int result;

                    if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                    {
                        int thisNumericChunk = int.Parse(str1);
                        int thatNumericChunk = int.Parse(str2);
                        result = thisNumericChunk.CompareTo(thatNumericChunk);
                    }
                    else
                    {
                        result = str1.CompareTo(str2);
                    }

                    if (result != 0)
                    {
                        return result;
                    }
                }
                return len1 - len2;
            }
        }

        private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            if (source.FullName.ToLower() == target.FullName.ToLower())
            {
                return;
            }

            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == true)
            {
                Directory.Delete(target.FullName, true);
                Directory.CreateDirectory(target.FullName);
                DirectoryInfo dir = Directory.CreateDirectory(target.FullName);
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            else if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
                DirectoryInfo dir = Directory.CreateDirectory(target.FullName);
                dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog cdfile = new OpenFileDialog();
            cdfile.Filter = ".tga .gxt .btx |*.tga; *.gxt; *.btx|.tga|*.tga|.gxt|*.gxt|.btx|*.btx|All Files (*.*)|*.*";
            cdfile.Multiselect = true;

            if (cdfile.ShowDialog() == DialogResult.OK)
            {
                string directory = "Convert TGA-GXT-BXT to PNG (" + comboBox2.SelectedItem + ")";
                if (Directory.Exists(directory) == false)
                {
                    Directory.CreateDirectory(directory);
                }

                foreach (string file in cdfile.FileNames)
                { /* Passa i file.tga selezionati e li passa al programma "conver.exe" insieme alla riga di comando per convertire il TGA in un PNG compresso mantenendo il massimo della qualità */
                    if (Path.GetExtension(file) == ".tga")
                    {
                        string rigadapassare = "convert \u0022" + file + "\u0022 " + "-quality 100" + " \u0022" + directory + "\\" + Path.GetFileNameWithoutExtension(file) + ".png\u0022";
                        rigadapassare = rigadapassare.Replace("\\", "/");
                        Decrypt("Ext\\convert.exe", rigadapassare);
                    }

                    else if (Path.GetExtension(file) == ".gxt" || Path.GetExtension(file) == ".btx")
                    {
                        FileInfo fi = new FileInfo(file);
                        fi.CopyTo(Path.Combine(directory, Path.GetFileNameWithoutExtension(file) + Path.GetExtension(file)), true);
                        string newfile = directory + "\\" + Path.GetFileNameWithoutExtension(file) + Path.GetExtension(file);
                        newfile = newfile.Replace("\\", "/");

                        Decrypt("Ext\\dr_dec.py", " \u0022" + newfile + "\u0022");

                        if (Path.GetExtension(file) == ".btx")
                        {
                            Decrypt("Ext\\shtx_conv.py", "convert \u0022" + newfile + "\u0022");
                        }

                        if (File.Exists(directory + "\\" + Path.GetFileNameWithoutExtension(file) + ".png") == false)
                        {
                            Decrypt("Ext\\GXTConvert.exe", "convert \u0022" + newfile + "\u0022");
                        }

                        File.Delete(newfile); /* Cancella il file dopo averlo convertito in PNG */
                    }

                }
                cdfile.Dispose();
                MessageBox.Show("Done!");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog cdfile = new OpenFileDialog();
            cdfile.Filter = ".png |*.png";
            cdfile.Multiselect = true;

            if (cdfile.ShowDialog() == DialogResult.OK)
            {
                string directory = "Convert PNG to TGA (" + comboBox2.SelectedItem + ")";
                if (Directory.Exists(directory) == false)
                {
                    Directory.CreateDirectory(directory);
                }
                foreach (string png in cdfile.FileNames)
                {
                    /* Passa i file.tga selezionati e li passa al programma "conver.exe" insieme alla riga di comando per convertire il PNG in un TGA compresso con RLE */
                    string rigadapassare = "convert \u0022" + png + "\u0022 " + "-compress RLE" + " \u0022" + directory + "\\" + Path.GetFileNameWithoutExtension(png) + ".tga\u0022";
                    rigadapassare = rigadapassare.Replace("\\", "/");
                    Decrypt("Ext\\convert.exe", rigadapassare);
                }
                cdfile.Dispose();
                MessageBox.Show("Done!");
            }
        }

    }
}