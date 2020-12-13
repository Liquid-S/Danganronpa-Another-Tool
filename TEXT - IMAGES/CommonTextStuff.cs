using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Yarhl.FileFormat;
using Yarhl.IO;
using Yarhl.Media.Text;

namespace Danganronpa_Another_Tool
{
    public static class CommonTextStuff
    {
        // It extracts sentences from files without bytecode (".raw" files), therefore made only by text.
        public static List<string> TextExtractor(string RawTextAddress)
        {
            List<string> Text = new List<string>();

            using (FileStream TEXTFILE = new FileStream(RawTextAddress, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader BinReaderText = new BinaryReader(TEXTFILE))
                {
                    int BOM = 2; /* Byte Order Mark (BOM): https://en.wikipedia.org/wiki/Byte_order_mark */

                    if (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE")) // "DRAE" doesn't use any BOM.
                    {
                        BOM = 0;
                    }

                    uint NPointers = BinReaderText.ReadUInt32(); // N# Pointers = N# sentenceses. 
                    uint[] Pointers = new uint[NPointers]; // It will contains all the pointers.

                    for (int i = 0; i < NPointers; i++)  // Reads and saves the pointers.
                    {
                        Pointers[i] = BinReaderText.ReadUInt32();
                    }

                    // START Reading sentences.
                    for (int i = 0; i < NPointers; i++)
                    {
                        Text.Add(ReadSentence(TEXTFILE, BinReaderText, Pointers[i] + BOM));
                    }
                    // END Reading sentences.
                }
            }
            return Text;
        }

        //Read one char at a time until it find "a zero" and save them all in a string.
        public static string ReadSentence(FileStream fs, BinaryReader br, long Pointer)
        {
            // Moves at the beginning of the sentence "x" and begins to read it.
            fs.Seek(Pointer, SeekOrigin.Begin);

            string Sentence = null;
            ushort Letter = 1;

            while ((fs.Position != fs.Length) && (Letter = br.ReadUInt16()) != 0)
            {
                Sentence += (char)Letter;
            }

            // The text is cleaned from the PSP codes.
            if (DRAT.cLEANPSPCLTToolStripMenuItem.Checked == true && Sentence != null && Path.GetExtension(fs.Name.ToLower()) != ".bnd")
            {
                Sentence = CleanTextFromPSPCodes(Sentence);
            }

            //Delete new extra lines and space.
            if (DRAT.eraseExtraLinefeedsToolStripMenuItem.Checked == true && Sentence != null && Path.GetExtension(fs.Name.ToLower()) != ".bnd")
            {
                Sentence = Sentence.Trim();
            }

            return Sentence;
        }

        // This is useful to people who has used as base the translation made by "Project Zetsubou" for DR1 (PSP).
        public static string CleanTextFromPSPCodes(string Sentence)
        {
            // START removing PZ codes for PSP.
            Sentence = Sentence.Replace("“", "\"");
            Sentence = Sentence.Replace("”", "\"");
            Sentence = Sentence.Replace("’", "'");

            int Position = 0, Flag = 1;

            string[] CLTToBeDeleted = new string[] { "<CLT 8>", "<CLT 12>", "<CLT 16>", "<CLT 21>", "<CLT 22>" };
            int[] CLTSize = new int[] { 7, 8, 8, 8, 8 };

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
        public static List<string> SpeakerExtractor(string BytecodeAddress)
        {
            using (FileStream FILECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader BinReaderSpeakers = new BinaryReader(FILECODE))
                {
                    List<string> Speakers = new List<string>();
                    List<byte> HexCodeSpeakers = new List<byte>();
                    string[] Names = null;

                    if (DRAT.tabControl1.SelectedTab.Text.Contains("DR1")) // If the user is working with DR1.
                    {
                        Names = new string[] { "MAKOTO NAEGI", "KIYOTAKA ISHIMARU", "BYAKUYA TOGAMI", "MONDO OOWADA", "LEON KUWATA", "HIFUMI YAMADA", "YASUHIRO HAGAKURE", "SAYAKA MAIZONO", "KYOUKO KIRIGIRI", "AOI ASAHINA", "TOUKO FUKAWA", "SAKURA OOGAMI", "CELES", "JUNKO ENOSHIMA (MUKURO)", "CHIHIRO FUJISAKI", "MONOKUMA", "JUNKO ENOSHIMA (REAL)", "ALTER EGO", "GENOCIDER SHOU", "HEADMASTER", "NAEGI'S MOTHER", "NAEGI'S FATHER", "NAEGI'S SISTER", "ERROR", "ISHIDA", "DAIA OOWADA", "ERROR", "ERROR", "LAST SPRITE", "EMPTY SPEAKER", "NO NAME", "NARRATION", "CHOICE/RE:ACT", "USAMI", "MONOKUMA BACKUP", "MONOKUMA BACKUP (R)", "MONOKUMA BACKUP (L)", "MONOKUMA BACKUP (M)", "ERROR1" };
                    }
                    else if (DRAT.tabControl1.SelectedTab.Text.Contains("DR2")) // If the user is working with DR2.
                    {
                        Names = new string[] { "HAJIME HINATA", "NAGITO KOMAEDA", "BYAKUYA TOGAMI", "GUNDAM TANAKA", "KAZUICHI SOUDA", "TERUTERU HANAMURA", "NEKOMARU NIDAI", "FUYUHIKO KUZURYUU", "AKANE OWARI", "CHIAKI NANAMI", "SONIA NEVERMIND", "HIYOKO SAIONJI", "MAHIRU KOIZUMI", "MIKAN TSUMIKI", "IBUKI MIODA", "PEKO PEKOYAMA", "MONOKUMA", "MONOMI", "JUNKO ENOSHIMA", "MECHA NIDAI", "MAKOTO NAEGI", "KYOUKO KIRIGIRI", "BYAKUYA TOGAMI (REAL)", "HANAMURA'S MOM", "ALTER EGO", "MINI NIDAI", "MONOKUMA MONOMI", "NARRATION", "ERROR", "EMPTY SPEAKER", "ERROR", "ERROR", "CHOICE/RE:ACT", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "USAMI", "KIRAKIRA", "NO NAME", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "JUNKO ENOSHIMA 2", "ERROR", "GIRL A", "GIRL B", "GIRL C", "GIRL D", "GIRL E", "BOY F", "NO NAME 2", "ERROR", "ERROR", "ERROR", "ERROR", "ERROR", "LAST SPRITE", "EMPTY SPEAKER", "ERROR1" };
                    }
                    else if (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE")) // If the user is working with DRAE.
                    {
                        Names = new string[] { "KOMARU NAEGI", "TOUKO FUKAWA", "GENOCIDER SHOU", "MASARU DAIMON", "JATARO KEMURI", "KOTOKO UTSUGI", "NAGISA SHINGETSU", "MONACA", "SERVANT", "KUROKUMA", "HAIJI TOUWA", "TOUICHI TOUWA", "SHIROKUMA", "YUUTA ASAHINA", "HIROKO HAGAKURE", "???", "MAKOTO NAEGI", "BYAKUYA TOGAMI", "MONOKUMA KID", "MONOKUMA KID", "FUTURE FONDATION", "FUTURE FONDATION A", "FUTURE FONDATION B", "FUTURE FONDATION C", "FUTURE FONDATION D", "FUTURE FONDATION E", "FUTURE FONDATION F", "ADULT", "ADULT A", "ADULT B", "ADULT C", "ADULT D", "ADULT E", "ADULT F", "ADULT G", "ADULT H", "ADULT I", "ADULT J", "ADULT K", "ADULT L", "ADULT M", "ADULT N", "ADULT O", "ADULT P", "ADULT Q", "ADULT R", "ADULT S", "ADULT T", "ADULT U", "ADULT V", "ADULT W", "ADULT X", "ADULT Y", "ADULT Z", "ADULTS", "ADULT", "ADULT", "DUMMY 1", "DUMMY 2", "DUMMY 3", "DUMMY 4", "DUMMY 5", "DUMMY 6", "DUMMY 7", "DUMMY 8", "DUMMY 9", "DUMMY 10", "DUMMY 11", "DUMMY 12", "JUNKO ENOSHIMA", "WARRIOS OF HOPE", "KID SHOP MANAGER", "EMPTY SPEAKER", "???", "???", "ERROR", "SYSTEM/CHOICE", "ERROR1" };
                    }

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
                    if (!DRAT.tabControl1.SelectedTab.Text.Contains("DRAE"))
                    {
                        if (DRAT.tabControl1.SelectedTab.Text.Contains("DR2")) // If the user is working with DR2.
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
                                    {
                                        LastSprite = SpeakerIndex = BinReaderSpeakers.ReadByte();
                                    }

                                    // 0x1C == "show the name of the last character displayed as sprites".
                                    else if (SpeakerIndex == LastSpriteCode)
                                    {
                                        SpeakerIndex = LastSprite;
                                    }
                                    else if (SpeakerIndex == 0x43)
                                    {
                                        SpeakerIndex = NarrationIndex;
                                    }
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
                                {
                                    HexCodeSpeakers.Add(SpeakerIndex); // Save speaker index.
                                }
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
                                {
                                    Narration = 1;
                                }
                            }
                        }

                        FILECODE.Seek(0x4, SeekOrigin.Begin);
                        int FileType = BinReaderSpeakers.ReadInt32();

                        if (Narration == 0 || FileType == 0x00011670 || FileType == 0x1F003670 || FileType == 0x04003370)
                        {
                            SpeakerCode = 0x07;
                        }

                        while (FILECODE.Position != FILECODE.Length)
                        {
                            TempVar = BinReaderSpeakers.ReadByte();
                            if (TempVar == 0x70)
                            {
                                TempVar = BinReaderSpeakers.ReadByte();

                                if (TempVar == SpeakerCode) // If the dialogue has a speaker.
                                {
                                    SpeakerIndex = BinReaderSpeakers.ReadByte();
                                }
                                else if (TempVar == 0x39) // If the dialogue is a System message or a Choice.
                                {
                                    SpeakerIndex = 76;
                                }
                                else if (TempVar == PrintSentenceCode) // PrintSentenceCode == Print dialogue on screen.
                                {
                                    HexCodeSpeakers.Add(SpeakerIndex); // Save speaker index.
                                }
                            }
                        }
                    }

                    for (int i = 0; i < HexCodeSpeakers.Count; i++) // Save the Names inside "Speakers".
                    {
                        int indexNames = HexCodeSpeakers[i];

                        if (indexNames >= Names.Length) // If indexNames >= Names.Length, then Speaker == "ERROR1".
                        {
                            indexNames = Names.Length - 1;
                        }

                        Speakers.Add(Names[indexNames]);
                    }
                    return Speakers;
                }
            }
        }

        public static List<string> ReadJAPText(string FileName)
        {
            List<string> JAPText = null;

            string JapanseTextFolder = null;

            if (DRAT.tabControl1.SelectedTab.Text.Contains("DR1 (PSP)") || DRAT.tabControl1.SelectedTab.Text.Contains("DR1 (PSVITA)"))
            {
                JapanseTextFolder = "DR1PSVITA";
            }

            if (DRAT.tabControl1.SelectedTab.Text.Contains("DR1 (PC)"))
            {
                JapanseTextFolder = "DR1PC";
            }

            if (DRAT.tabControl1.SelectedTab.Text.Contains("DR2 (PSVITA)"))
            {
                JapanseTextFolder = "DR2PSVITA";
            }

            if (DRAT.tabControl1.SelectedTab.Text.Contains("DR2 (PC)"))
            {
                JapanseTextFolder = "DR2PC";
            }

            if (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE (PSVITA)"))
            {
                JapanseTextFolder = "DRAEPSVITA";
            }

            if (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE (PC)"))
            {
                JapanseTextFolder = "DRAEPC";
            }

            if (File.Exists(Path.Combine("Ext\\JapText", JapanseTextFolder, Path.GetFileNameWithoutExtension(FileName) + ".rawtext")) == true)
            {
                return JAPText = CommonTextStuff.TextExtractor(Path.Combine("Ext\\JapText", JapanseTextFolder, Path.GetFileNameWithoutExtension(FileName) + ".rawtext"));
            }

            return JAPText;
        }

        //SECRET STUFF!
        public static List<string> ReadTranslatedRawText(string FileName)
        {
            List<string> TRText = null;

            if (File.Exists(Path.Combine("Ext\\EngText", Path.GetFileNameWithoutExtension(FileName) + ".rawtext")) == true)
            {
                return TRText = CommonTextStuff.TextExtractor(Path.Combine("Ext\\EngText", Path.GetFileNameWithoutExtension(FileName) + ".rawtext"));
            }

            return TRText;
        }


        // Extract the text from "FIleName" and the Speaker from "Bytcode" and save it in ".po".
        public static void MakePO(List<string> ExtractedText, string Bytecode, string DestinationDir)
        {
            if (!Directory.Exists(DestinationDir))
            {
                Directory.CreateDirectory(DestinationDir);
            }

            List<string> SpeakersExtracted = null;

            if (Bytecode != null)
            {
                SpeakersExtracted = SpeakerExtractor(Bytecode);
            }

            List<string> JAPText = null;

            // If the user has checked "Add japanase texts", then extract the japanese text. 
            if (DRAT.aDDJAPANESETEXTToolStripMenuItem.Checked == true)
            {
                JAPText = ReadJAPText(Path.GetFileNameWithoutExtension(DestinationDir));
            }

            //List<string> TRText = ExtractedText;
            //ExtractedText = ReadTranslatedRawText(Path.GetFileNameWithoutExtension(DestinationDir));

            //Read the language used by the user' OS, this way the editor can spellcheck the translation.
            System.Globalization.CultureInfo currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            Po po = new Po
            {
                Header = new PoHeader(DRAT.tabControl1.SelectedTab.Text, "your_email", currentCulture.Name)
            };

            for (int i = 0; i < ExtractedText.Count; i++)
            {

                PoEntry entry = new PoEntry();

                // Print the "Speaker".
                if (DRAT.hIDESPEAKERSToolStripMenuItem.Checked == false && SpeakersExtracted != null)
                {
                    if (i < SpeakersExtracted.Count)
                    {
                        entry.Context = $"{(i + 1).ToString("D4")} | {SpeakersExtracted[i]}";
                    }
                    else
                    {
                        entry.Context = $"{(i + 1).ToString("D4")} | {"ERROR"}";
                    }
                }
                else
                {
                    entry.Context = $"{i + 1:D4}"; // If there isn't a Speaker, then just print the sentence number
                }

                // Print the original sentence.
                if (ExtractedText[i] == "" || ExtractedText[i] == null)
                {
                    entry.Original = "[EMPTY_LINE]";
                    entry.Translated = "[EMPTY_LINE]";
                }
                else if (ExtractedText[i].Length == 1 || ExtractedText[i] == " \n" || ExtractedText[i] == "\n" || ExtractedText[i] == "..." || ExtractedText[i] == "…" || ExtractedText[i] == "...\n" || ExtractedText[i] == "…\n" || ExtractedText[i] == "\"...\"" || ExtractedText[i] == "\"…\"" || ExtractedText[i] == "\"...\n\"" || ExtractedText[i] == "\"…\n\"")
                {
                    entry.Original = ExtractedText[i];
                    entry.Translated = ExtractedText[i];
                }
                else
                {
                    entry.Original = ExtractedText[i];

                    //if (TRText[i] != "" || TRText[i] != null)
                    //    entry.Translated = TRText[i];
                }

                // Print the japanese sentence.
                if (DRAT.aDDJAPANESETEXTToolStripMenuItem.Checked == true && JAPText != null)
                {
                    if (i < JAPText.Count && JAPText != null && JAPText[i] != "")
                    {
                        entry.ExtractedComments = JAPText[i].Replace("\r\n", "\n#. ").Replace("\n\r", "\n#. ").Replace("\n", "\n#. ").Replace("\r", string.Empty); // The repalce is needed, otherwise PoEditor is not going to load correctly the jp text and the Repack is gonna crash.
                    }
                    else
                    {
                        entry.ExtractedComments = "JAPANESE LINE NOT FOUND";
                    }
                }

                po.Add(entry);
            }

            string NewPOAddress = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(DestinationDir) + ".po");

            ConvertFormat.To<BinaryFormat>(po).Stream.WriteTo(NewPOAddress);
        }

        // Read the all text from a file ".po".
        public static List<string> ExtracTextFromPo(string PoAddress)
        {
            List<string> Text = new List<string>();

            Po PoTranslated = null;
            string Sentence = null;

            using (DataStream name = DataStreamFactory.FromFile(PoAddress, FileOpenMode.Read))
            using (BinaryFormat binaryname = new BinaryFormat(name))
            {
                PoTranslated = (Po)ConvertFormat.With<Po2Binary>(binaryname);
            }

            foreach (PoEntry entry in PoTranslated.Entries)
            {
                if (entry.Original == "[EMPTY_LINE]" || entry.Translated == "[EMPTY_LINE]")
                {
                    Sentence = "";
                }
                else if (entry.Translated.Trim() != null && entry.Translated.Trim() != "")
                {
                    Sentence = entry.Translated;
                }
                else
                {
                    Sentence = entry.Original;
                }

                //Delete new extra lines and space. BND files are excluded from this.
                if (DRAT.eraseExtraLinefeedsToolStripMenuItem.Checked == true && Sentence != "" && Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(PoAddress))).ToLower() != "bnd")
                {
                    Sentence = Sentence.Trim();
                }

                if (Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(PoAddress))).ToLower() != "bnd" && Sentence != null && Sentence != "" && (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE") || Path.GetFileNameWithoutExtension(PoAddress).ToLower().Contains("novel"))) // If the user is working on AE or with some Novel.
                {
                    Sentence += "\n"; //Some files need it to be displayed in the right spot on the screen.
                }

                Text.Add(Sentence); //And finally save the sentence.
            }

            return Text; // Return all the sentences.
        }

        public static void TranslatePOwithAnotherPO(string BasePO, string TargetPo)
        {
            Po BPo = null, TPo = null;

            using (DataStream name = DataStreamFactory.FromFile(BasePO, FileOpenMode.Read))
            using (BinaryFormat binaryname = new BinaryFormat(name))
            {
                BPo = (Po)ConvertFormat.With<Po2Binary>(binaryname);
            }

            using (DataStream name = DataStreamFactory.FromFile(TargetPo, FileOpenMode.Read))
            using (BinaryFormat binaryname = new BinaryFormat(name))
            {
                TPo = (Po)ConvertFormat.With<Po2Binary>(binaryname);
            }

            foreach (PoEntry entryBPo in BPo.Entries)
            {
                foreach (PoEntry entryTPo in TPo.Entries)
                {
                    if (entryBPo.Original == entryTPo.Original && (entryBPo.Translated != null && entryBPo.Translated != ""))
                    {
                        entryTPo.Translated = entryBPo.Translated;

                        if (entryBPo.TranslatorComment != string.Empty && entryBPo.TranslatorComment != null && entryBPo.TranslatorComment.Trim() != "")
                        {
                            entryTPo.TranslatorComment = entryBPo.TranslatorComment;
                        }
                    }


                }
            }

            ConvertFormat.To<BinaryFormat>(TPo).Stream.WriteTo(TargetPo);
        }

        // 
        public static void ConverPoToTXT()
        {
            using (OpenFileDialog LinFiles = new OpenFileDialog())
            {
                LinFiles.Title = "Select one or more \"file.lin\"";
                LinFiles.Filter = ".lin|*.lin|All Files (*.*)|*.*";
                LinFiles.Multiselect = true;

                if (LinFiles.ShowDialog() == DialogResult.OK)
                {
                    Common.ChangeStatusLabelToWait(true); // Change "Ready!" to "Wait..."

                    if (Directory.Exists("ToTXT") == false)
                    {
                        Directory.CreateDirectory("ToTXT");
                    }

                    foreach (string SinglePO in LinFiles.FileNames)
                    {
                        List<string> Text = ExtracTextFromPo(SinglePO);

                        using (FileStream NewTXT = new FileStream(Path.Combine("ToTXT", Path.GetFileNameWithoutExtension(SinglePO) + ".txt"), FileMode.Create, FileAccess.Write))
                        using (BinaryWriter BinXML = new BinaryWriter(NewTXT), tx = new BinaryWriter(NewTXT, Encoding.Unicode))
                        {
                            BinXML.Write((ushort)0xFEFF); //BOM

                            for (int i = 0; i < Text.Count; i++)
                                tx.Write((Text[i]+ "\n").ToCharArray());
                        }
                    }

                    Common.ChangeStatusLabelToWait(false); // Change the "Status" to "Ready!".
                }
            }
        }


        /* I'M LEAVING THE XML STUFF FOR COMPATIBILITY WITH OLDER VERSIONS OF DRAT */

        public static void ConvertXmlToPo(string SingleXML)
        {
            List<string> XMLOriginalSentences = null, XMLTranslatedSentences = null, XMLSpeakers = null, XMLComments = null, JAPText = null;

            using (FileStream TRANSLATEDXML = new FileStream(SingleXML, FileMode.Open, FileAccess.Read))
            using (StreamReader XMLStreamReader = new StreamReader(TRANSLATEDXML, Encoding.Unicode))
            {
                string XmlText = XMLStreamReader.ReadToEnd();

                XMLOriginalSentences = CommonTextStuff.ExtractTextFromXML(XmlText, "<ORIGINAL N°", "</ORIGINAL N°", SingleXML);
                XMLTranslatedSentences = CommonTextStuff.ExtractTextFromXML(XmlText, "<TRANSLATED N°", "</TRANSLATED N°", SingleXML);
                XMLSpeakers = CommonTextStuff.ExtractTextFromXML(XmlText, "<SPEAKER N°", "</SPEAKER N°", SingleXML);
                XMLComments = CommonTextStuff.ExtractTextFromXML(XmlText, "<COMMENT N°", "</COMMENT N°", SingleXML);
            }

            // If the user has checked "Add japanase texts", then extract the japanese text. 
            if (DRAT.aDDJAPANESETEXTToolStripMenuItem.Checked == true)
            {
                JAPText = ReadJAPText(SingleXML);
            }

            //Read the language used by the user' OS, this way the editor can spellcheck the translation.
            System.Globalization.CultureInfo currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture;

            Po po = new Po
            {
                Header = new PoHeader(DRAT.tabControl1.SelectedTab.Text, "your_email", currentCulture.Name)
            };

            try
            {
                for (int i = 0; i < XMLOriginalSentences.Count; i++)
                {

                    PoEntry entry = new PoEntry();

                    // "Speaker"
                    if (DRAT.hIDESPEAKERSToolStripMenuItem.Checked == false && XMLSpeakers.Count > 0 && XMLSpeakers[i] != null && XMLSpeakers[i] != "")
                    {
                        entry.Context = $"{i + 1:D4} | {XMLSpeakers[i]}";
                    }
                    else
                    {
                        entry.Context = $"{i + 1:D4}";
                    }

                    // "Original"
                    if (XMLOriginalSentences.Count > 0 && XMLOriginalSentences[i] != null && XMLOriginalSentences[i] != "")
                    {
                        entry.Original = XMLOriginalSentences[i];
                    }
                    else
                    {
                        entry.Original = "[EMPTY_LINE]";
                        entry.Translated = "[EMPTY_LINE]";
                    }

                    // "Translated"
                    if (XMLTranslatedSentences.Count > 0 && XMLTranslatedSentences[i] != null && XMLTranslatedSentences[i] != "")
                    {
                        if (XMLOriginalSentences[i].Length == 1 || XMLOriginalSentences[i] == " \n" || XMLOriginalSentences[i] == "\n" || XMLOriginalSentences[i] == "..." || XMLOriginalSentences[i] == "…" || XMLOriginalSentences[i] == "...\n" || XMLOriginalSentences[i] == "…\n" || XMLOriginalSentences[i] == "\"...\"" || XMLOriginalSentences[i] == "\"…\"" || XMLOriginalSentences[i] == "\"...\n\"" || XMLOriginalSentences[i] == "\"…\n\"")
                        {
                            entry.Translated = XMLOriginalSentences[i];
                        }
                        else if (XMLTranslatedSentences[i] != XMLOriginalSentences[i])
                        {
                            entry.Translated = XMLTranslatedSentences[i];
                        }
                    }

                    // "Comments"
                    if (XMLComments.Count > 0 && XMLComments[i] != null && XMLComments[i] != "")
                    {
                        entry.TranslatorComment = XMLComments[i].Replace("\n", "\n# "); //The repalce is needed, otherwise PoEditor is not going to load correctly the jp text and the Repack is gonna crash.;
                    }

                    // "Japanese"
                    if (DRAT.aDDJAPANESETEXTToolStripMenuItem.Checked == true && JAPText != null && JAPText.Count > 0 && i < JAPText.Count)
                    {
                        if (JAPText[i] != "" && JAPText[i] != null)
                        {
                            entry.ExtractedComments = JAPText[i].Replace("\n", "\n#. "); //The repalce is needed, otherwise PoEditor is not going to load correctly the jp text and the Repack is gonna crash.
                        }
                        else
                        {
                            entry.ExtractedComments = "[EMPTY_LINE]";
                        }
                    }
                    else
                    {
                        entry.ExtractedComments = "JAPANESE LINE NOT FOUND";
                    }

                    po.Add(entry);
                }

                string NewPoAddress = Path.Combine(Path.GetDirectoryName(SingleXML), Path.GetFileNameWithoutExtension(SingleXML) + ".po");

                ConvertFormat.To<BinaryFormat>(po).Stream.WriteTo(NewPoAddress);
            }
            catch
            {
                MessageBox.Show("\"" + SingleXML + "\"'s structure appears to be corrupted. Please check the code tags' integrity.\n\nYou can use an older version of DRAT to extract again the XML from the original English or Japanese file and compare it with yours.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void MakeXML(List<string> ExtractedText, List<string> SpeakersExtracted, string SingleXML)
        {
            if (Directory.Exists(Path.GetDirectoryName(SingleXML)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SingleXML));
            }

            List<string> JAPText = null;

            // If the user has checked "Add japanase texts", then extract the japanese text. 
            if (DRAT.aDDJAPANESETEXTToolStripMenuItem.Checked == true)
            {
                JAPText = ReadJAPText(SingleXML);
            }

            using (FileStream NuovoXML = new FileStream(SingleXML, FileMode.Create, FileAccess.Write))
            using (BinaryWriter BinXML = new BinaryWriter(NuovoXML), tx = new BinaryWriter(NuovoXML, Encoding.Unicode))
            {
                BinXML.Write((ushort)0xFEFF); //BOM

                for (int i = 0; i < ExtractedText.Count; i++)
                {
                    // Print the "Speaker".
                    if (DRAT.hIDESPEAKERSToolStripMenuItem.Checked == false && SpeakersExtracted != null)
                    {
                        if (i < SpeakersExtracted.Count)
                        {
                            tx.Write(("<SPEAKER N°" + (i + 1).ToString("D3") + ">" + SpeakersExtracted[i] + "</SPEAKER N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                        }
                        else
                        {
                            tx.Write(("<SPEAKER N°" + (i + 1).ToString("D3") + ">ERROR</SPEAKER N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                        }
                    }

                    // Print the original sentence.
                    tx.Write(("<ORIGINAL N°" + (i + 1).ToString("D3") + ">\n" + ExtractedText[i] + "\n</ORIGINAL N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());

                    // Print the japanese sentence.
                    if (DRAT.aDDJAPANESETEXTToolStripMenuItem.Checked == true && JAPText != null)
                    {
                        if (i < JAPText.Count)
                        {
                            tx.Write(("<JAPANESE N°" + (i + 1).ToString("D3") + ">\n" + JAPText[i] + "\n</JAPANESE N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                        }
                        else
                        {
                            tx.Write(("<JAPANESE N°" + (i + 1).ToString("D3") + ">\nNOT FOUND\n</JAPANESE N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                        }
                    }

                    tx.Write(("<TRANSLATED N°" + (i + 1).ToString("D3") + ">\n\n</TRANSLATED N°" + (i + 1).ToString("D3") + ">\n").ToCharArray());
                    tx.Write(("<COMMENT N°" + (i + 1).ToString("D3") + ">\n\n</COMMENT N°" + (i + 1).ToString("D3") + ">\n------------------------------------------------------------\n").ToCharArray());
                }
            }
        }

        // Read and extract the text from the XML. XmlText contains all the XML's text.
        public static List<string> ExtractTextFromXML(string XmlText, string OpeningTagTEMP, string EndingTagTEMP, string XMLAddress)
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
                    temp2 = temp2.Replace(OpTag + "\n", OpTag).Replace("\n" + EdTag, EdTag);

                    // Removes the TAGs from the sentence.
                    temp2 = temp2.Replace(OpTag, null).Replace(EdTag, null);

                    //Delete new extra lines and space.
                    if (DRAT.eraseExtraLinefeedsToolStripMenuItem.Checked == true)
                    {
                        temp2 = temp2.Trim();
                    }

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
                            temp2 = temp2.Replace(OpTag + "\n", OpTag).Replace("\n" + EdTag, EdTag);

                            // Removes the TAGs from the sentence.
                            temp2 = temp2.Replace(OpTag, null).Replace(EdTag, null);

                            //Delete new extra lines and space.
                            if (DRAT.eraseExtraLinefeedsToolStripMenuItem.Checked == true)
                            {
                                temp2 = temp2.Trim();
                            }
                        }
                    }

                    if (OpeningTagTEMP == "< TRANSLATED N°" && Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(XMLAddress))).ToLower() != "bnd" && temp2 != null && (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE") || Path.GetFileNameWithoutExtension(XMLAddress).ToLower().Contains("novel"))) // If the user is working on AE or with some Novel.
                    {
                        temp2 += "\n"; //Some files need it to be displayed in the right spot on the screen.
                    }

                    Text.Add(temp2); // And finally saves the cleaned sentence.
                }
                SentencesN++; // Index increment.
            }
            return Text; // Return all the sentences.
        }

        /* I'M LEAVING THE XML STUFF FOR COMPATIBILITY WITH OLDER VERSIONS OF DRAT */



        // Repack the text to ".LIN" or ".PAK". If the dir contains the "bytecode", then it's a ".LIN" file, otherwise it's a ".PAK".
        public static void RePackText(string DirTextToBeRepacked, string DestinationDir)
        {
            string NewFileExtension = ".pak",
            BytecodeAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".bytecode");

            // If there is a ".bytecode" file, it means it's a ".LIN" file.
            if (File.Exists(BytecodeAddress) == true)
            {
                NewFileExtension = ".lin";
            }

            List<string> TranslatedSentences = null;

            uint NParts = 0x02,
                HeaderSize = 0x10;

            string PoAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".po");

            // Checkout if there is a text file and consequently extracts the sentences.
            if (File.Exists(PoAddress) == true)
            {
                /* It extracts all the phrases translated from the XML and stores them in "TranslatedSentences".
                 XMLStreamReader.ReadToEnd () = Read all the text within the XML and stores it in a string.
                using (FileStream TRANSLATEDXML = new FileStream(PoAddress, FileMode.Open, FileAccess.Read))
                using (StreamReader XMLStreamReader = new StreamReader(TRANSLATEDXML, Encoding.Unicode))
                    TranslatedSentences = ExtractTextFromXML(XMLStreamReader.ReadToEnd(), "<TRANSLATED N°", "</TRANSLATED N°", XMLAddress); */

                TranslatedSentences = ExtracTextFromPo(PoAddress);
            }
            // If there are no text files, and you the user is working on a different game from DR1.
            else if (File.Exists(PoAddress) == false && !DRAT.tabControl1.SelectedTab.Text.Contains("DR1"))
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
                            {
                                LINBinaryWriter.Write((uint)(HeaderSize + BYTECODE.Length));
                            }

                            FileSizePos = (int)REPACKEDFILE.Position;
                            LINBinaryWriter.Write((uint)(0x0));
                            BYTECODE.CopyTo(REPACKEDFILE, (int)HeaderSize);
                        }
                    }
                    // END INSERTING BYCODE - ONLY IF IT'S A LIN.

                    // START INSERTING TEXT - Only if there is text to be processed.
                    if (File.Exists(PoAddress) == true)
                    {
                        // "SentencesOffset" will contain the offset of each phrase.
                        uint[] SentencesOffset = new uint[TranslatedSentences.Count + 1];
                        byte Padding = 0x04; // DR1 and AE padding is 4.

                        if (DRAT.tabControl1.SelectedTab.Text.Contains("DR2")) // If the user is working on DR2.
                        {
                            Padding = 0x02; // DR2 padding is 2.
                        }

                        // Write down the n# of sentences.
                        LINBinaryWriter.Write((uint)TranslatedSentences.Count);

                        // Stores the current position so that we can come back later and enter the correct offsets.
                        int pos = (int)REPACKEDFILE.Position;

                        // Fills the pointers area with zeros. At the end of the process the area will be overwritten with the correct data.
                        for (int i = 0; i < SentencesOffset.Length; i++)
                        {
                            LINBinaryWriter.Write((uint)0x00);
                        }

                        /* The "- ((uint) pos - 4)" is due to the fact that the offsets do not take into account everything that
                        is before the number of sentences, ergo the bytecode and the first 0x10. */
                        SentencesOffset[0] = (uint)REPACKEDFILE.Position - ((uint)pos - 4);

                        for (int i = 0; i < TranslatedSentences.Count; i++)
                        {
                            // If the user is NOT working on AE, then write down the BOM.
                            if (!DRAT.tabControl1.SelectedTab.Text.Contains("DRAE"))
                            {
                                LINBinaryWriter.Write((ushort)0xFEFF);
                            }

                            // Write the sentence n# [i] in the repacked file.
                            LINBinUnicode.Write(TranslatedSentences[i].ToCharArray());

                            // Write down the null string terminator.
                            LINBinaryWriter.Write((ushort)0x00);

                            // If it's a "PAK" that means that we must padding.
                            if (NewFileExtension == ".pak")
                            {
                                if (REPACKEDFILE.Position % 0x04 != 0)
                                {
                                    while (REPACKEDFILE.Position % 0x04 != 0)
                                    {
                                        LINBinaryWriter.Write((byte)0x0);
                                    }
                                }

                                SentencesOffset[i + 1] = (uint)REPACKEDFILE.Position;
                            }
                            else
                            {
                                SentencesOffset[i + 1] = (uint)REPACKEDFILE.Position - ((uint)pos - 4);
                            }
                        }

                        // Padding at the end of the file.
                        if (REPACKEDFILE.Position % Padding != 0)
                        {
                            while (REPACKEDFILE.Position % Padding != 0)
                            {
                                LINBinaryWriter.Write((byte)0x0);
                            }
                        }

                        if (DRAT.tabControl1.SelectedTab.Text.Contains("DRAE")) // If the user is working on AE.
                        {
                            SentencesOffset[SentencesOffset.Length - 1] = 0; // Last one is always zero on AE files.
                        }

                        // Comes back in the area dedicated to the offsets and overwrites all the zeros with the correct offsets.
                        LINBinaryWriter.Seek(pos, SeekOrigin.Begin);
                        for (int i = 0; i < SentencesOffset.Length; i++)
                        {
                            LINBinaryWriter.Write(SentencesOffset[i]);
                        }
                    }
                    // END INSERTING TEXT - Only if there is text to be processed.

                    // If it's a "LIN", returns to file beginning and writes the exact size of the LIN.
                    if (NewFileExtension == ".lin")
                    {
                        /* If there insn't a text file and the header size is equal to 0x10,
                        it means that this is a file containing only a bytecode. */
                        if (File.Exists(PoAddress) == false && HeaderSize == 0x10)
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

    }
}
