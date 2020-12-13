using System.IO;

namespace Danganronpa_Another_Tool
{
    public static class LIN
    {
        public static void ExtractLIN(string LINAddress, string DestinationDir)
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
                        if (NSentences > 0 || (NSentences <= 0 && DRAT.ignoreLINWoTextToolStripMenuItem.Checked == false))
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

                                string RawTexAddress = Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(LINAddress) + ".rawtext");

                                // Create a file called "TextRaw.bin" and inserts all the contents of "SecondFileBody" in it.
                                using (FileStream TextRaw = new FileStream(RawTexAddress, FileMode.Create, FileAccess.Write))
                                    TextRaw.Write(SecondFileBody, 0, SecondFileBody.Length);

                                /* If the user has checked the "HIDE SPEAKERS box", then pass "null".
                                Null = don't store and don't print the speakers. */
                                if (DRAT.hIDESPEAKERSToolStripMenuItem.Checked == true)
                                    CommonTextStuff.MakePO(CommonTextStuff.TextExtractor(RawTexAddress), null, DestinationDir);
                                else
                                    CommonTextStuff.MakePO(CommonTextStuff.TextExtractor(RawTexAddress), BytecodeAddress, DestinationDir);

                                // Deletes FileName.rawtext.
                                File.Delete(RawTexAddress);
                                while (File.Exists(RawTexAddress)) { }
                            }
                        }
                    }
                }
            }
        }

        //If you are looking for the LIN's repack code, look at "CommonTextStuff.RePackText"
    }
}
