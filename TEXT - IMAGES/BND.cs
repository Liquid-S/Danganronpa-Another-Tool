using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Danganronpa_Another_Tool
{
    public static class BND //BND files exist only in AE.
    {
        /* I'm ignoring a lot of BND files from AE because they doesn't contain anything useful for translation purposes.
         Even if BND files share the same extension, their structure may differ,
         that's why I had to implement different approaches for each BND.  */

        public static void ExtractBND(string BNDAddress, string DestinationDir)
        {

            if (Path.GetFileNameWithoutExtension(BNDAddress).ToLower().Contains("64"))
                ExtractCommonBND64(BNDAddress, DestinationDir);
            else
                ExtractCommonBND(BNDAddress, DestinationDir);
        }

        public static void RePackBND(string DirTextToBeRepacked, string DestinationDir)
        {

            if (Path.GetFileNameWithoutExtension(DirTextToBeRepacked).ToLower().Contains("64"))
            {
                if (Path.GetFileNameWithoutExtension(DirTextToBeRepacked).ToLower().Contains("oneshot"))
                    RepackOneshotBND64(DirTextToBeRepacked, DestinationDir);
                else
                    RepackCommonBND64(DirTextToBeRepacked, DestinationDir);
            }
            else
            {
                if (Path.GetFileNameWithoutExtension(DirTextToBeRepacked).ToLower().Contains("oneshot"))
                    RepackOneshotBND(DirTextToBeRepacked, DestinationDir);
                else
                    RepackCommonBND(DirTextToBeRepacked, DestinationDir);
            }
        }


        private static void ExtractCommonBND(string BNDAddress, string DestinationDir)
        {
            List<string> Text = new List<string>();

            // Open file.BND in FileStream.
            using (FileStream BND = new FileStream(BNDAddress, FileMode.Open, FileAccess.Read))
            using (BinaryReader BinReaderBND = new BinaryReader(BND))
            {
                uint NParts = BinReaderBND.ReadUInt32(); // Number of parts that compose the BND
                uint OffsetPointersToPointers = BinReaderBND.ReadUInt32(); //This zone contains all the pointers to the text's pointers.
                BinReaderBND.ReadUInt32(); // Always zero                
                uint[] NBlocks = new uint[NParts], //Read the "Block extraction" below.
                       OffsetDataPointerZones = new uint[NParts];
                uint HeaderSize = OffsetDataPointerZones[0] = BinReaderBND.ReadUInt32();
                NBlocks[0] = BinReaderBND.ReadUInt32();

                if (NParts > 1) // Header's last part. 
                {
                    for (int i = 1; i < NParts; i++)
                    {
                        OffsetDataPointerZones[i] = BinReaderBND.ReadUInt32();
                        NBlocks[i] = BinReaderBND.ReadUInt32();
                    }
                }

                /* Blocks extraction - START
               Some files have the "Pointers To Text Zone" divided by Blocks.
               This "rule" doesn't apply to the "Pointers To Pointers zone" at the end of each file.
               These Blocks contain the pointers (and sometimes other information) of each sentence/data.
               

                FILE STRUCTURE
                ********************************************************************
                *                             HEADER                                *
                *                                                                   *
                 ********************************************************************
                *00000000000000/                                                    * Usually the first part of this zone is made by zeroes.
                *                                                                   * If there are 0x10 zeroes, then each Block is longo 0x10 bytes.
                *                     POINTERS TO TEXT ZONE                         * If there aren't any zeroes, you can find how long is each zone
                *                                                                   * by dividing the zone's size by the Number of blocks.
                *                                                                   * The Number of blocks can be found in the Header.
                *                                                                   *
                 ********************************************************************
                *                                                                   *
                *                                                                   *
                *                                                                   *
                *                        TEXT/SENTENCES                             *
                *                        AND SOMETIMES                              *
                *                        OTHER DATA LIKE                            *
                *                        FILENAMES AND                              *
                *                        MAYBE BYTECODE?                            *
                *                                                                   *
                *                                                                   *
                *                                                                   *
                 ********************************************************************
                *                                                                   *
                *                                                                   * The pointers in this zone point to the pointers in the "Pointers to Text Zone"
                *                    POINTERS TO POINTERS ZONE                      *
                *                                                                   *
                *                                                                   *
                *                                                                   *
                 ********************************************************************
                 * 
            The following code was meant to extract each "Block", however it doesn't work for every BND since
            not all the BNDs have the same structure.
            Anyway, extracting the "Blocks" one by one it's not mandatory, they can be extract as one.
            This code could be helpful to someone else, so I'm leaving it.

                for (int i = 0; i < NParts; i++)
                {
                    BND.Seek(OffsetDataPointerZones[i], SeekOrigin.Begin);

                    int BlockSize = 0;
                    ushort Letter = 0;

                    while ((Letter = BinReaderBND.ReadByte()) == 0)
                        BlockSize++;

                    BND.Seek(OffsetDataPointerZones[i], SeekOrigin.Begin);

                    if (!Directory.Exists(Path.Combine(DestinationDir, "PointerZone N_" + i.ToString("D4"))))
                        Directory.CreateDirectory(Path.Combine(DestinationDir, "PointerZone N_" + i.ToString("D4")));

                    for (int z = 0; z < NBlocks[i]; z++)
                    {
                        byte[] BodyFile = new byte[BlockSize];
                        BinReaderBND.Read(BodyFile, 0, BlockSize);

                        using (FileStream Extracted = new FileStream(Path.Combine(DestinationDir, "PointerZone N_" + i.ToString("D4"), "Part_" + z.ToString("D4") + ".bin"), FileMode.Create, FileAccess.Write))
                            Extracted.Write(BodyFile, 0, BlockSize);
                    }
                }
                //Blocks extraction - END */

                // Start reading Pointers To Pointers
                BND.Seek(OffsetPointersToPointers, SeekOrigin.Begin);

                uint NPointersToPointers = BinReaderBND.ReadUInt32();

                /* Not all the BNDs are the same, some contain just text and others text and codes/system stuff.
                 DRAT main purpose is translation, so I'm extracting what it matters for translations.
                 Besides, extracting EVERYTHING would be more time consuming, I'd have to study the files better. */
                if (Path.GetFileNameWithoutExtension(BNDAddress).ToLower().Contains("oneshot"))
                    NPointersToPointers = 120;
                else if (Path.GetFileNameWithoutExtension(BNDAddress).ToLower().Contains("item"))
                    NPointersToPointers = 76;
                else if (Path.GetFileNameWithoutExtension(BNDAddress).ToLower().Contains("kotodama"))
                    NPointersToPointers = 128;

                uint[] PointersToPointers = new uint[NPointersToPointers]; // It will contains all the pointers to the Text Pointers.                

                // Reads and saves the pointers' pointers.
                for (int i = 0; i < PointersToPointers.Length; i++)
                    PointersToPointers[i] = BinReaderBND.ReadUInt32();

                // Reads and saves the text pointers.
                uint[] PointersToText = new uint[PointersToPointers.Length]; // It will contains all the pointers to the Sentences.

                for (int i = 0; i < PointersToPointers.Length; i++)
                {
                    BND.Seek(PointersToPointers[i], SeekOrigin.Begin);
                    PointersToText[i] = BinReaderBND.ReadUInt32();
                }

                // EXTRACT sentences.
                for (int i = 0; i < PointersToPointers.Length; i++)
                {
                    if (PointersToPointers[i] == 0)
                        Text.Add("[EMPTY]");
                    else
                        Text.Add(CommonTextStuff.ReadSentence(BND, BinReaderBND, PointersToText[i]));
                }
            }

            CommonTextStuff.MakePO(Text, null, DestinationDir);

            // To repack the translated BND file I need some code from the original file.
            File.Copy(BNDAddress, Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(BNDAddress) + ".original"), true);
        }

        private static void RepackOneshotBND(string DirTextToBeRepacked, string DestinationDir)
        {
            string NewFileExtension = ".bnd",
          BytecodeAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".original"),
          PoAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".po");

            List<string> TranslatedSentences = CommonTextStuff.ExtracTextFromPo(PoAddress);

            uint[] OriginalPointersToPointers = null, OriginalPointersZone = new uint[6],
                OriginalPointersZoneSize = new uint[6] { 0xA0, 0x50, 0x78, 0x50, 0x50, 0x50 }, NewPointersZone = new uint[6];
            // There is a way to calcute each Pointer Zone size, however, put them manually inside the array is the faster solution.
            // In anycase this code is meant to work only with Oneshot.

            uint OriginalPointersToPointersZone = 0, NewPointersToPointersZone = 0;
            byte[] FirstBodyPart = null;

            // Copy "Header + Texts' Pointers" from the original file and paste it to the new one.
            using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
            using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
            {
                BYTECODE.Seek(0x04, SeekOrigin.Begin);
                OriginalPointersToPointersZone = ByteBinary.ReadUInt32();

                // Save the offset to each pointer zone
                BYTECODE.Seek(0x1C, SeekOrigin.Begin);
                for (int i = 0; i < OriginalPointersZone.Length; i++)
                    OriginalPointersZone[i] = ByteBinary.ReadUInt32();

                // Start reading Pointers To Pointers
                BYTECODE.Seek(OriginalPointersToPointersZone, SeekOrigin.Begin);

                // It will contains all the pointers to the Texts' Pointers.
                OriginalPointersToPointers = new uint[ByteBinary.ReadUInt32() - 7];

                // Reads and saves the pointers' pointers.
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                    OriginalPointersToPointers[i] = ByteBinary.ReadUInt32();

                // Reads and saves the first sentence's pointer.
                BYTECODE.Seek(OriginalPointersToPointers[0], SeekOrigin.Begin);
                uint PointerToFirstSentence = ByteBinary.ReadUInt32();

                // Save the code to "FirstBodyPart".
                BYTECODE.Seek(0x0, SeekOrigin.Begin);
                FirstBodyPart = new byte[PointerToFirstSentence];
                BYTECODE.Read(FirstBodyPart, 0, FirstBodyPart.Length);
            }

            // Start building the new file.
            using (FileStream REPACKEDFILE = new FileStream(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + NewFileExtension), FileMode.Create, FileAccess.ReadWrite))
            using (BinaryWriter BndBinaryWriter = new BinaryWriter(REPACKEDFILE), LINBinUnicode = new BinaryWriter(REPACKEDFILE, Encoding.Unicode))
            using (BinaryReader BndBinaryReader = new BinaryReader(REPACKEDFILE))
            {
                //Copy the original "Header" and "Text's Pointers Zone" to the new BND.
                REPACKEDFILE.Write(FirstBodyPart, 0, FirstBodyPart.Length);

                // "SentencesOffset" will contain the offset of each phrase.
                uint[] NewSentencesOffset = new uint[TranslatedSentences.Count];

                // Write down all the text.
                int a = 0;
                using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
                using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
                {
                    for (int i = 0; i < TranslatedSentences.Count; i++)
                    {
                        NewSentencesOffset[i] = (uint)REPACKEDFILE.Position;

                        LINBinUnicode.Write(TranslatedSentences[i].ToCharArray());

                        // Write down the null string terminator.
                        BndBinaryWriter.Write((byte)0x00);

                        if (i == 23 || i == 51 || i == 63 || i == 83 || i == 95 || i == 107 || i == TranslatedSentences.Count - 1)
                        {

                            // Padding.
                            if ((REPACKEDFILE.Position - 0x04) % 0x10 != 0)
                                while ((REPACKEDFILE.Position - 0x04) % 0x10 != 0)
                                    BndBinaryWriter.Write((byte)0x0);

                            //Copy pointer zone "a" to the new bnd.
                            if (i == 23 || i == 51 || i == 63 || i == 83 || i == 95 || i == 107)
                            {
                                NewPointersZone[a] = (uint)REPACKEDFILE.Position;

                                BYTECODE.Seek(OriginalPointersZone[a], SeekOrigin.Begin);

                                byte[] PointersZone = new byte[OriginalPointersZoneSize[a]];

                                BYTECODE.Read(PointersZone, 0, PointersZone.Length);

                                REPACKEDFILE.Write(PointersZone, 0, PointersZone.Length);

                                a++;
                            }
                        }
                        else
                        {
                            // Padding.
                            if (REPACKEDFILE.Position % 0x04 != 0)
                                while (REPACKEDFILE.Position % 0x04 != 0)
                                    BndBinaryWriter.Write((byte)0x0);
                        }
                    }

                }

                NewPointersToPointersZone = (uint)REPACKEDFILE.Position;

                // Copy the last part from the original file.
                using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
                using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
                {
                    BYTECODE.Seek(OriginalPointersToPointersZone, SeekOrigin.Begin);

                    byte[] LastBodyPart = new byte[BYTECODE.Length - BYTECODE.Position];

                    BYTECODE.Read(LastBodyPart, 0, LastBodyPart.Length);

                    REPACKEDFILE.Write(LastBodyPart, 0, LastBodyPart.Length);
                }

                // Update Pointers to Pointers. The differences between the original bnd and new start from the sentence n#24.
                REPACKEDFILE.Seek(NewPointersToPointersZone + 0x64, SeekOrigin.Begin);

                int n = 0;

                for (int i = 24; i < OriginalPointersToPointers.Length; i++)
                {
                    if (i >= 24 && i <= 51)
                        n = 0;
                    else if (i >= 52 && i <= 63)
                        n = 1;
                    else if (i >= 64 && i <= 83)
                        n = 2;
                    else if (i >= 84 && i <= 95)
                        n = 3;
                    else if (i >= 96 && i <= 107)
                        n = 4;
                    else if (i >= 108)
                        n = 5;

                    OriginalPointersToPointers[i] = (BndBinaryReader.ReadUInt32() - OriginalPointersZone[n]) + NewPointersZone[n];

                    REPACKEDFILE.Seek(REPACKEDFILE.Position - 0x04, SeekOrigin.Begin);

                    BndBinaryWriter.Write(OriginalPointersToPointers[i]);
                }

                // Update Texts' Pointers
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                {
                    REPACKEDFILE.Seek(OriginalPointersToPointers[i], SeekOrigin.Begin);
                    BndBinaryWriter.Write(NewSentencesOffset[i]);
                }

                // Update Pointers To Pointers Zone
                REPACKEDFILE.Seek(0x04, SeekOrigin.Begin);
                BndBinaryWriter.Write(NewPointersToPointersZone);

                // Update the offset to each pointer zone
                REPACKEDFILE.Seek(0x1C, SeekOrigin.Begin);
                for (int i = 0; i < OriginalPointersZone.Length; i++)
                    BndBinaryWriter.Write(NewPointersZone[i]);

            }
            // END INSERTING TEXT
        }

        private static void RepackCommonBND(string DirTextToBeRepacked, string DestinationDir)
        {
            string NewFileExtension = ".bnd",
          BytecodeAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".original"),
          PoAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".po");

            List<string> TranslatedSentences = CommonTextStuff.ExtracTextFromPo(PoAddress);

            uint[] OriginalPointersToPointers = null, NewPointersToPointers = null, OriginalPointersToData = null, OffsetDataPointerZones = null;
            uint OriginalPointersToPointersZone = 0, NewPointersToPointersZone = 0, NParts = 0, HeaderSize = 0;
            byte[] FirstBodyPart = null;

            // Copy "Header + Texts' Pointers" from the original file and paste it to the new one.
            using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
            using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
            {
                NParts = ByteBinary.ReadUInt32(); // Number of parts that compose the BND
                OffsetDataPointerZones = new uint[NParts];
                OriginalPointersToPointersZone = ByteBinary.ReadUInt32(); //This zone contains all the pointers to the text's pointers.
                ByteBinary.ReadUInt32(); // Always zero               
                HeaderSize = OffsetDataPointerZones[0] = ByteBinary.ReadUInt32();
                ByteBinary.ReadUInt32(); //NBlocks' first zone

                if (NParts > 1) // Header's last part. 
                {
                    for (int i = 1; i < NParts; i++)
                    {
                        OffsetDataPointerZones[i] = ByteBinary.ReadUInt32();
                        ByteBinary.ReadUInt32(); //NBlocks for each zone
                    }
                }

                // Start reading Pointers To Pointers
                BYTECODE.Seek(OriginalPointersToPointersZone, SeekOrigin.Begin);

                // It will contains all the pointers to the Texts' Pointers.
                OriginalPointersToPointers = new uint[ByteBinary.ReadUInt32()];
                NewPointersToPointers = new uint[OriginalPointersToPointers.Length];

                // Reads and saves the pointers' pointers.
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                    OriginalPointersToPointers[i] = NewPointersToPointers[i] = ByteBinary.ReadUInt32();

                // Reads and saves the pointers' data.
                OriginalPointersToData = new uint[OriginalPointersToPointers.Length];
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                {
                    BYTECODE.Seek(OriginalPointersToPointers[i], SeekOrigin.Begin);

                    OriginalPointersToData[i] = ByteBinary.ReadUInt32();
                }

                // Reads and saves the first sentence's pointer.
                BYTECODE.Seek(OriginalPointersToPointers[0], SeekOrigin.Begin);
                uint PointerToFirstSentence = ByteBinary.ReadUInt32();

                // Save the code to "FirstBodyPart".
                BYTECODE.Seek(0x0, SeekOrigin.Begin);
                FirstBodyPart = new byte[PointerToFirstSentence];
                BYTECODE.Read(FirstBodyPart, 0, FirstBodyPart.Length);
            }

            // Start building the new file.
            using (FileStream REPACKEDFILE = new FileStream(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + NewFileExtension), FileMode.Create, FileAccess.ReadWrite))
            using (BinaryWriter BndBinaryWriter = new BinaryWriter(REPACKEDFILE), LINBinUnicode = new BinaryWriter(REPACKEDFILE, Encoding.Unicode))
            using (BinaryReader BndBinaryReader = new BinaryReader(REPACKEDFILE))
            {
                REPACKEDFILE.Write(FirstBodyPart, 0, FirstBodyPart.Length); //Insert the original "Header + Text's Pointers Zone" to the new BND.

                // "SentencesOffset" will contain the offset of each phrase.
                uint[] NewSentencesOffset = new uint[TranslatedSentences.Count];

                // Write down all the text.                
                for (int i = 0; i < TranslatedSentences.Count; i++)
                {
                    NewSentencesOffset[i] = (uint)REPACKEDFILE.Position;

                    LINBinUnicode.Write(TranslatedSentences[i].ToCharArray());

                    if (i != TranslatedSentences.Count - 1)
                    {
                        // Write down the null string terminator.
                        BndBinaryWriter.Write((byte)0x00);

                        // Padding.
                        if (REPACKEDFILE.Position % 0x04 != 0)
                            while (REPACKEDFILE.Position % 0x04 != 0)
                                BndBinaryWriter.Write((byte)0x0);
                    }
                }

                // Padding.
                if ((REPACKEDFILE.Position - 0x04) % 0x10 != 0)
                    while ((REPACKEDFILE.Position - 0x04) % 0x10 != 0)
                        BndBinaryWriter.Write((byte)0x0);

                uint EndNewTextZone = (uint)REPACKEDFILE.Position;

                uint EndOriginalTextZone = OriginalPointersToPointersZone;

                // If NParts > 1 that means that between the last sentence and the Pointer To Pointers Zone there is other stuff that must be copied.
                if (NParts > 1 && Path.GetFileNameWithoutExtension(DirTextToBeRepacked).ToLower().Contains("kotodama"))
                    EndOriginalTextZone = OffsetDataPointerZones[2];
                else if (NParts > 1)
                    EndOriginalTextZone = OffsetDataPointerZones[1];

                // Copy the last part from the original file.
                using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
                using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
                {
                    BYTECODE.Seek(EndOriginalTextZone, SeekOrigin.Begin);

                    byte[] LastBodyPart = new byte[BYTECODE.Length - BYTECODE.Position];

                    BYTECODE.Read(LastBodyPart, 0, LastBodyPart.Length);

                    REPACKEDFILE.Write(LastBodyPart, 0, LastBodyPart.Length);
                }

                // Update Pointers To Pointers Zone
                REPACKEDFILE.Seek(0x04, SeekOrigin.Begin);
                NewPointersToPointersZone = (OriginalPointersToPointersZone - EndOriginalTextZone) + EndNewTextZone;
                BndBinaryWriter.Write(NewPointersToPointersZone);

                // Update the Pointers To Pointers zone if needed.
                if (TranslatedSentences.Count < OriginalPointersToPointers.Length)
                {
                    REPACKEDFILE.Seek(NewPointersToPointersZone + 0x04 + (TranslatedSentences.Count * 0x04), SeekOrigin.Begin);

                    for (int i = TranslatedSentences.Count; i < OriginalPointersToPointers.Length; i++)
                    {
                        NewPointersToPointers[i] = (OriginalPointersToPointers[i] - EndOriginalTextZone) + EndNewTextZone;
                        BndBinaryWriter.Write(NewPointersToPointers[i]);
                    }
                }

                // Update the Texts' Pointers
                for (int i = 0; i < NewPointersToPointers.Length; i++)
                {
                    REPACKEDFILE.Seek(NewPointersToPointers[i], SeekOrigin.Begin);

                    if (i < TranslatedSentences.Count)
                        BndBinaryWriter.Write(NewSentencesOffset[i]);
                    else
                        BndBinaryWriter.Write((OriginalPointersToData[i] - EndOriginalTextZone) + EndNewTextZone);
                }

                if (NParts > 1) // Let's update the pointers to the other pointers zones.
                {
                    int a = 1;

                    /* Kotodama text start after the pointers zone at 0x14, therefore it's always the same, no matter what.
                    We can start update the pointers from 0x1C. */
                    if (Path.GetFileNameWithoutExtension(DirTextToBeRepacked).ToLower().Contains("kotodama"))
                    {
                        REPACKEDFILE.Seek(0x1C, SeekOrigin.Begin);
                        a = 2;
                    }
                    else
                        REPACKEDFILE.Seek(0x14, SeekOrigin.Begin);


                    while (a < NParts)
                    {
                        BndBinaryWriter.Write((OffsetDataPointerZones[a] - EndOriginalTextZone) + EndNewTextZone);
                        BndBinaryReader.ReadUInt32();
                        a++;
                    }
                }

            }
            // END INSERTING TEXT
        }


        // "FileName64.bnd" can be found only in the PC version.
        
        private static void ExtractCommonBND64(string BNDAddress, string DestinationDir)
        {
            List<string> Text = new List<string>();

            // Open file.BND in FileStream.
            using (FileStream BND = new FileStream(BNDAddress, FileMode.Open, FileAccess.Read))
            using (BinaryReader BinReaderBND = new BinaryReader(BND))
            {
                ulong NParts = BinReaderBND.ReadUInt64(); // Number of parts that compose the BND
                ulong OffsetPointersToPointers = BinReaderBND.ReadUInt64(); //This zone contains all the pointers to the text's pointers.
                BinReaderBND.ReadUInt64(); // Always zero                
                ulong[] NBlocks = new ulong[NParts], //Read the "Block extraction" below.
                       OffsetDataPointerZones = new ulong[NParts];
                ulong HeaderSize = OffsetDataPointerZones[0] = BinReaderBND.ReadUInt64();
                NBlocks[0] = BinReaderBND.ReadUInt64();

                if (NParts > 1) // Header's last part. 
                {
                    for (int i = 1; i < (long)NParts; i++)
                    {
                        OffsetDataPointerZones[i] = BinReaderBND.ReadUInt64();
                        NBlocks[i] = BinReaderBND.ReadUInt64();
                    }
                }

                /* Blocks extraction - START
               Some files have the "Pointers To Text Zone" divided by Blocks.
               This "rule" doesn't apply to the "Pointers To Pointers zone" at the end of each file.
               These Blocks contain the pointers (and sometimes other information) of each sentence/data.
               

                FILE STRUCTURE
                ********************************************************************
                *                             HEADER                                *
                *                                                                   *
                 ********************************************************************
                *00000000000000/                                                    * Usually the first part of this zone is made by zeroes.
                *                                                                   * If there are 0x10 zeroes, then each Block is longo 0x10 bytes.
                *                     POINTERS TO TEXT ZONE                         * If there aren't any zeroes, you can find how long is each zone
                *                                                                   * by dividing the zone's size by the Number of blocks.
                *                                                                   * The Number of blocks can be found in the Header.
                *                                                                   *
                 ********************************************************************
                *                                                                   *
                *                                                                   *
                *                                                                   *
                *                        TEXT/SENTENCES                             *
                *                        AND SOMETIMES                              *
                *                        OTHER DATA LIKE                            *
                *                        FILENAMES AND                              *
                *                        MAYBE BYTECODE?                            *
                *                                                                   *
                *                                                                   *
                *                                                                   *
                 ********************************************************************
                *                                                                   *
                *                                                                   * The pointers in this zone point to the pointers in the "Pointers to Text Zone"
                *                    POINTERS TO POINTERS ZONE                      *
                *                                                                   *
                *                                                                   *
                *                                                                   *
                 ********************************************************************
                 * 
            The following code was meant to extract each "Block", however it doesn't work for every BND since
            not all the BNDs have the same structure.
            Anyway, extracting the "Blocks" one by one it's not mandatory, they can be extract as one.
            This code could be helpful to someone else, so I'm leaving it.

                for (int i = 0; i < NParts; i++)
                {
                    BND.Seek(OffsetDataPointerZones[i], SeekOrigin.Begin);

                    int BlockSize = 0;
                    ushort Letter = 0;

                    while ((Letter = BinReaderBND.ReadByte()) == 0)
                        BlockSize++;

                    BND.Seek(OffsetDataPointerZones[i], SeekOrigin.Begin);

                    if (!Directory.Exists(Path.Combine(DestinationDir, "PointerZone N_" + i.ToString("D4"))))
                        Directory.CreateDirectory(Path.Combine(DestinationDir, "PointerZone N_" + i.ToString("D4")));

                    for (int z = 0; z < NBlocks[i]; z++)
                    {
                        byte[] BodyFile = new byte[BlockSize];
                        BinReaderBND.Read(BodyFile, 0, BlockSize);

                        using (FileStream Extracted = new FileStream(Path.Combine(DestinationDir, "PointerZone N_" + i.ToString("D4"), "Part_" + z.ToString("D4") + ".bin"), FileMode.Create, FileAccess.Write))
                            Extracted.Write(BodyFile, 0, BlockSize);
                    }
                }
                //Blocks extraction - END */

                // Start reading Pointers To Pointers
                BND.Seek((long)OffsetPointersToPointers, SeekOrigin.Begin);

                ulong NPointersToPointers = BinReaderBND.ReadUInt64();

                /* Not all the BNDs are the same, some contain just text and others text and codes/system stuff.
                 DRAT main purpose is translation, so I'm extracting what it matters for translations.
                 Besides, extracting EVERYTHING would be more time consuming, I'd have to study the files better. */
                if (Path.GetFileNameWithoutExtension(BNDAddress).ToLower().Contains("oneshot"))
                    NPointersToPointers = 120;
                else if (Path.GetFileNameWithoutExtension(BNDAddress).ToLower().Contains("item"))
                    NPointersToPointers = 76;
                else if (Path.GetFileNameWithoutExtension(BNDAddress).ToLower().Contains("kotodama"))
                    NPointersToPointers = 128;

                ulong[] PointersToPointers = new ulong[NPointersToPointers]; // It will contains all the pointers to the Text Pointers.                

                // Reads and saves the pointers' pointers.
                for (int i = 0; i < PointersToPointers.Length; i++)
                    PointersToPointers[i] = BinReaderBND.ReadUInt64();

                // Reads and saves the text pointers.
                ulong[] PointersToText = new ulong[PointersToPointers.Length]; // It will contains all the pointers to the Sentences.

                for (int i = 0; i < PointersToPointers.Length; i++)
                {
                    BND.Seek((long)PointersToPointers[i], SeekOrigin.Begin);
                    PointersToText[i] = BinReaderBND.ReadUInt64();
                }

                // EXTRACT sentences.
                for (int i = 0; i < PointersToPointers.Length; i++)
                {
                    if (PointersToPointers[i] == 0)
                        Text.Add("[EMPTY]");
                    else
                        Text.Add(CommonTextStuff.ReadSentence(BND, BinReaderBND, (long)PointersToText[i]));
                }
            }

            CommonTextStuff.MakePO(Text, null, DestinationDir);

            // To repack the translated BND file I need some code from the original file.
            File.Copy(BNDAddress, Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(BNDAddress) + ".original"), true);
        }

        private static void RepackOneshotBND64(string DirTextToBeRepacked, string DestinationDir)
        {
            string NewFileExtension = ".bnd",
          BytecodeAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".original"),
          PoAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".po");

            List<string> TranslatedSentences = CommonTextStuff.ExtracTextFromPo(PoAddress);

            ulong[] OriginalPointersToPointers = null, OriginalPointersZone = new ulong[6],
                OriginalPointersZoneSize = new ulong[6] { 0x140, 0xA0, 0xF0, 0xA0, 0xA0, 0xA0 }, NewPointersZone = new ulong[6];
            // There is a way to calcute each Pointer Zone size, however, put them manually inside the array is faster solution.
            // In anycase this code is meant to work only with Oneshot.

            ulong OriginalPointersToPointersZone = 0, NewPointersToPointersZone = 0;
            byte[] FirstBodyPart = null;

            // Copy "Header + Texts' Pointers" from the original file and paste it to the new one.
            using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
            using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
            {
                BYTECODE.Seek(0x08, SeekOrigin.Begin);
                OriginalPointersToPointersZone = ByteBinary.ReadUInt64();

                // Save the offset to each pointer zone
                BYTECODE.Seek(0x38, SeekOrigin.Begin);
                for (int i = 0; i < OriginalPointersZone.Length; i++)
                    OriginalPointersZone[i] = ByteBinary.ReadUInt64();

                // Start reading Pointers To Pointers
                BYTECODE.Seek((long)OriginalPointersToPointersZone, SeekOrigin.Begin);

                // It will contains all the pointers to the Texts' Pointers.
                OriginalPointersToPointers = new ulong[ByteBinary.ReadUInt64() - 7];

                // Reads and saves the pointers' pointers.
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                    OriginalPointersToPointers[i] = ByteBinary.ReadUInt64();

                // Reads and saves the first sentence's pointer.
                BYTECODE.Seek((long)OriginalPointersToPointers[0], SeekOrigin.Begin);
                ulong PointerToFirstSentence = ByteBinary.ReadUInt64();

                // Save the code to "FirstBodyPart".
                BYTECODE.Seek(0x0, SeekOrigin.Begin);
                FirstBodyPart = new byte[PointerToFirstSentence];
                BYTECODE.Read(FirstBodyPart, 0, FirstBodyPart.Length);
            }

            // Start building the new file.
            using (FileStream REPACKEDFILE = new FileStream(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + NewFileExtension), FileMode.Create, FileAccess.ReadWrite))
            using (BinaryWriter BndBinaryWriter = new BinaryWriter(REPACKEDFILE), LINBinUnicode = new BinaryWriter(REPACKEDFILE, Encoding.Unicode))
            using (BinaryReader BndBinaryReader = new BinaryReader(REPACKEDFILE))
            {
                //Insert the original "Header + Text's Pointers Zone" to the new BND.
                REPACKEDFILE.Write(FirstBodyPart, 0, FirstBodyPart.Length);

                // "SentencesOffset" will contain the offset of each phrase.
                ulong[] NewSentencesOffset = new ulong[TranslatedSentences.Count];

                // Write down all the text.
                int a = 0;
                using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
                using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
                {
                    for (int i = 0; i < TranslatedSentences.Count; i++)
                    {
                        NewSentencesOffset[i] = (ulong)REPACKEDFILE.Position;

                        LINBinUnicode.Write(TranslatedSentences[i].ToCharArray());

                        // Write down the null string terminator.
                        BndBinaryWriter.Write((byte)0x00);

                        if (i == 23 || i == 51 || i == 63 || i == 83 || i == 95 || i == 107 || i == TranslatedSentences.Count - 1)
                        {

                            // Padding.
                            if ((REPACKEDFILE.Position - 0x08) % 0x10 != 0)
                                while ((REPACKEDFILE.Position - 0x08) % 0x10 != 0)
                                    BndBinaryWriter.Write((byte)0x0);

                            //Copy pointer zone "a" to the new bnd.
                            if (i == 23 || i == 51 || i == 63 || i == 83 || i == 95 || i == 107)
                            {
                                NewPointersZone[a] = (ulong)REPACKEDFILE.Position;

                                BYTECODE.Seek((long)OriginalPointersZone[a], SeekOrigin.Begin);

                                byte[] PointersZone = new byte[OriginalPointersZoneSize[a]];

                                BYTECODE.Read(PointersZone, 0, PointersZone.Length);

                                REPACKEDFILE.Write(PointersZone, 0, PointersZone.Length);

                                a++;
                            }
                        }
                        else
                        {
                            // Padding.
                            if (REPACKEDFILE.Position % 0x08 != 0)
                                while (REPACKEDFILE.Position % 0x08 != 0)
                                    BndBinaryWriter.Write((byte)0x0);
                        }
                    }

                }

                NewPointersToPointersZone = (ulong)REPACKEDFILE.Position;

                // Copy the last part from the original file.
                using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
                using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
                {
                    BYTECODE.Seek((long)OriginalPointersToPointersZone, SeekOrigin.Begin);

                    byte[] LastBodyPart = new byte[BYTECODE.Length - BYTECODE.Position];

                    BYTECODE.Read(LastBodyPart, 0, LastBodyPart.Length);

                    REPACKEDFILE.Write(LastBodyPart, 0, LastBodyPart.Length);
                }

                // Update Pointers to Pointers. The differences between the original bnd and new start from the sentence n#24.
                REPACKEDFILE.Seek((long)NewPointersToPointersZone + 0xC8, SeekOrigin.Begin);

                int n = 0;

                for (int i = 24; i < OriginalPointersToPointers.Length; i++)
                {
                    if (i >= 24 && i <= 51)
                        n = 0;
                    else if (i >= 52 && i <= 63)
                        n = 1;
                    else if (i >= 64 && i <= 83)
                        n = 2;
                    else if (i >= 84 && i <= 95)
                        n = 3;
                    else if (i >= 96 && i <= 107)
                        n = 4;
                    else if (i >= 108)
                        n = 5;

                    OriginalPointersToPointers[i] = (BndBinaryReader.ReadUInt64() - OriginalPointersZone[n]) + NewPointersZone[n];

                    REPACKEDFILE.Seek(REPACKEDFILE.Position - 0x08, SeekOrigin.Begin);

                    BndBinaryWriter.Write(OriginalPointersToPointers[i]);
                }

                // Update Texts' Pointers
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                {
                    REPACKEDFILE.Seek((long)OriginalPointersToPointers[i], SeekOrigin.Begin);
                    BndBinaryWriter.Write(NewSentencesOffset[i]);
                }

                // Update Pointers To Pointers Zone
                REPACKEDFILE.Seek(0x08, SeekOrigin.Begin);
                BndBinaryWriter.Write(NewPointersToPointersZone);

                // Update the offset to each pointer zone
                REPACKEDFILE.Seek(0x38, SeekOrigin.Begin);
                for (int i = 0; i < OriginalPointersZone.Length; i++)
                    BndBinaryWriter.Write(NewPointersZone[i]);

            }
            // END INSERTING TEXT
        }

        private static void RepackCommonBND64(string DirTextToBeRepacked, string DestinationDir)
        {
            string NewFileExtension = ".bnd",
          BytecodeAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".original"),
          PoAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".po");

            List<string> TranslatedSentences = CommonTextStuff.ExtracTextFromPo(PoAddress);

            ulong[] OriginalPointersToPointers = null, NewPointersToPointers = null, OriginalPointersToData = null, OffsetDataPointerZones = null;
            ulong OriginalPointersToPointersZone = 0, NewPointersToPointersZone = 0, NParts = 0, HeaderSize = 0;
            byte[] FirstBodyPart = null;

            // Copy "Header + Texts' Pointers" from the original file and paste it to the new one.
            using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
            using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
            {
                NParts = ByteBinary.ReadUInt64(); // Number of parts that compose the BND
                OffsetDataPointerZones = new ulong[NParts];
                OriginalPointersToPointersZone = ByteBinary.ReadUInt64(); //This zone contains all the pointers to the text's pointers.
                ByteBinary.ReadUInt64(); // Always zero               
                HeaderSize = OffsetDataPointerZones[0] = ByteBinary.ReadUInt64();
                ByteBinary.ReadUInt64(); //NBlocks' first zone

                if (NParts > 1) // Header's last part. 
                {
                    for (int i = 1; i < (long)NParts; i++)
                    {
                        OffsetDataPointerZones[i] = ByteBinary.ReadUInt64();
                        ByteBinary.ReadUInt64(); //NBlocks for each zone
                    }
                }

                // Start reading Pointers To Pointers
                BYTECODE.Seek((long)OriginalPointersToPointersZone, SeekOrigin.Begin);

                // It will contains all the pointers to the Texts' Pointers.
                OriginalPointersToPointers = new ulong[ByteBinary.ReadUInt64()];
                NewPointersToPointers = new ulong[OriginalPointersToPointers.Length];

                // Reads and saves the pointers' pointers.
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                    OriginalPointersToPointers[i] = NewPointersToPointers[i] = ByteBinary.ReadUInt64();

                // Reads and saves the pointers' data.
                OriginalPointersToData = new ulong[OriginalPointersToPointers.Length];
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                {
                    BYTECODE.Seek((long)OriginalPointersToPointers[i], SeekOrigin.Begin);

                    OriginalPointersToData[i] = ByteBinary.ReadUInt64();
                }

                // Reads and saves the first sentence's pointer.
                BYTECODE.Seek((long)OriginalPointersToPointers[0], SeekOrigin.Begin);
                ulong PointerToFirstSentence = ByteBinary.ReadUInt64();

                // Save the code to "FirstBodyPart".
                BYTECODE.Seek(0x0, SeekOrigin.Begin);
                FirstBodyPart = new byte[PointerToFirstSentence];
                BYTECODE.Read(FirstBodyPart, 0, FirstBodyPart.Length);
            }

            // Start building the new file.
            using (FileStream REPACKEDFILE = new FileStream(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + NewFileExtension), FileMode.Create, FileAccess.ReadWrite))
            using (BinaryWriter BndBinaryWriter = new BinaryWriter(REPACKEDFILE), LINBinUnicode = new BinaryWriter(REPACKEDFILE, Encoding.Unicode))
            using (BinaryReader BndBinaryReader = new BinaryReader(REPACKEDFILE))
            {
                REPACKEDFILE.Write(FirstBodyPart, 0, FirstBodyPart.Length); //Insert the original "Header + Text's Pointers Zone" to the new BND.

                // "SentencesOffset" will contain the offset of each phrase.
                ulong[] NewSentencesOffset = new ulong[TranslatedSentences.Count];

                // Write down all the text.                
                for (int i = 0; i < TranslatedSentences.Count; i++)
                {
                    NewSentencesOffset[i] = (ulong)REPACKEDFILE.Position;

                    LINBinUnicode.Write(TranslatedSentences[i].ToCharArray());

                    // Write down the null string terminator.
                    BndBinaryWriter.Write((byte)0x00);

                    if (i != TranslatedSentences.Count - 1)
                    {
                        // Padding.
                        if (REPACKEDFILE.Position % 0x08 != 0)
                            while (REPACKEDFILE.Position % 0x08 != 0)
                                BndBinaryWriter.Write((byte)0x0);
                    }
                }

                // Padding.
                if ((REPACKEDFILE.Position - 0x08) % 0x10 != 0)
                    while ((REPACKEDFILE.Position - 0x08) % 0x10 != 0)
                        BndBinaryWriter.Write((byte)0x0);

                ulong EndNewTextZone = (ulong)REPACKEDFILE.Position;

                ulong EndOriginalTextZone = OriginalPointersToPointersZone;

                // If NParts > 1 that means that between the last sentence and the Pointer To Pointers Zone there is other stuff that must be copied.
                if (NParts > 1 && Path.GetFileNameWithoutExtension(DirTextToBeRepacked).ToLower().Contains("kotodama"))
                    EndOriginalTextZone = OffsetDataPointerZones[2];
                else if (NParts > 1)
                    EndOriginalTextZone = OffsetDataPointerZones[1];

                // Copy the last part from the original file.
                using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
                using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
                {
                    BYTECODE.Seek((long)EndOriginalTextZone, SeekOrigin.Begin);

                    byte[] LastBodyPart = new byte[BYTECODE.Length - BYTECODE.Position];

                    BYTECODE.Read(LastBodyPart, 0, LastBodyPart.Length);

                    REPACKEDFILE.Write(LastBodyPart, 0, LastBodyPart.Length);
                }

                // Update Pointers To Pointers Zone
                REPACKEDFILE.Seek(0x08, SeekOrigin.Begin);
                NewPointersToPointersZone = (OriginalPointersToPointersZone - EndOriginalTextZone) + EndNewTextZone;
                BndBinaryWriter.Write(NewPointersToPointersZone);

                // Update the Pointers To Pointers zone if needed.
                if (TranslatedSentences.Count < OriginalPointersToPointers.Length)
                {
                    REPACKEDFILE.Seek((long)NewPointersToPointersZone + 0x08 + (TranslatedSentences.Count * 0x08), SeekOrigin.Begin);

                    for (int i = TranslatedSentences.Count; i < OriginalPointersToPointers.Length; i++)
                    {
                        NewPointersToPointers[i] = (OriginalPointersToPointers[i] - EndOriginalTextZone) + EndNewTextZone;
                        BndBinaryWriter.Write(NewPointersToPointers[i]);
                    }
                }

                // Update the Texts' Pointers
                for (int i = 0; i < NewPointersToPointers.Length; i++)
                {
                    REPACKEDFILE.Seek((long)NewPointersToPointers[i], SeekOrigin.Begin);

                    if (i < TranslatedSentences.Count)
                        BndBinaryWriter.Write(NewSentencesOffset[i]);
                    else
                        BndBinaryWriter.Write((OriginalPointersToData[i] - EndOriginalTextZone) + EndNewTextZone);
                }

                if (NParts > 1) // Let's update the pointers to the other pointers zones.
                {
                    int a = 1;

                    /* Kotodama text start after the pointers zone at 0x14, therefore it's always the same, no matter what.
                    We can start update the pointers from 0x1C. */
                    if (Path.GetFileNameWithoutExtension(DirTextToBeRepacked).ToLower().Contains("kotodama"))
                    {
                        REPACKEDFILE.Seek(0x38, SeekOrigin.Begin);
                        a = 2;
                    }
                    else
                        REPACKEDFILE.Seek(0x28, SeekOrigin.Begin);


                    while (a < (long)NParts)
                    {
                        BndBinaryWriter.Write((OffsetDataPointerZones[a] - EndOriginalTextZone) + EndNewTextZone);
                        BndBinaryReader.ReadUInt64();
                        a++;
                    }
                }

            }
            // END INSERTING TEXT
        }

        /* RepackCommonBND's old version. Unused because IT'S A MESS!
        private static void RepackSimpleBND(string DirTextToBeRepacked, string DestinationDir)
        {
            string NewFileExtension = ".bnd",
          BytecodeAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".original"),
          PoAddress = Path.Combine(DirTextToBeRepacked, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + ".po");

            List<string> TranslatedSentences = CommonTextStuff.ExtracTextFromPo(PoAddress);

            uint[] OriginalPointersToPointers = null;
            uint OriginalPointersToPointersZone = 0, NewPointersToPointersZone = 0;
            byte[] FirstBodyPart = null;

            // Copy "Header + Texts' Pointers" from the original file and paste it to the new one.
            using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
            using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
            {
                BYTECODE.Seek(0x04, SeekOrigin.Begin);

                OriginalPointersToPointersZone = ByteBinary.ReadUInt32();

                // Start reading Pointers To Pointers
                BYTECODE.Seek(OriginalPointersToPointersZone, SeekOrigin.Begin);

                // It will contains all the pointers to the Texts' Pointers.
                OriginalPointersToPointers = new uint[ByteBinary.ReadUInt32()];

                // Reads and saves the pointers' pointers.
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                    OriginalPointersToPointers[i] = ByteBinary.ReadUInt32();

                // Reads and saves the first sentence's pointer.
                BYTECODE.Seek(OriginalPointersToPointers[0], SeekOrigin.Begin);
                uint PointerToFirstSentence = ByteBinary.ReadUInt32();

                // Save the code to "FirstBodyPart".
                BYTECODE.Seek(0x0, SeekOrigin.Begin);
                FirstBodyPart = new byte[PointerToFirstSentence];
                BYTECODE.Read(FirstBodyPart, 0, FirstBodyPart.Length);
            }

            // Start building the new file.
            using (FileStream REPACKEDFILE = new FileStream(Path.Combine(DestinationDir, Path.GetFileNameWithoutExtension(DirTextToBeRepacked) + NewFileExtension), FileMode.Create, FileAccess.ReadWrite))
            using (BinaryWriter BndBinaryWriter = new BinaryWriter(REPACKEDFILE), LINBinUnicode = new BinaryWriter(REPACKEDFILE, Encoding.Unicode))
            using (BinaryReader BndBinaryReader = new BinaryReader(REPACKEDFILE))
            {
                REPACKEDFILE.Write(FirstBodyPart, 0, FirstBodyPart.Length); //Insert the original "Header + Text's Pointers Zone" to the new BND.

                // "SentencesOffset" will contain the offset of each phrase.
                uint[] NewSentencesOffset = new uint[TranslatedSentences.Count];

                // Write down all the text.                
                for (int i = 0; i < TranslatedSentences.Count; i++)
                {
                    NewSentencesOffset[i] = (uint)REPACKEDFILE.Position;

                    LINBinUnicode.Write(TranslatedSentences[i].ToCharArray());

                    if (i != TranslatedSentences.Count - 1)
                    {
                        // Write down the null string terminator.
                        BndBinaryWriter.Write((byte)0x00);

                        // Padding.
                        if (REPACKEDFILE.Position % 0x04 != 0)
                            while (REPACKEDFILE.Position % 0x04 != 0)
                                BndBinaryWriter.Write((byte)0x0);
                    }
                }

                // Padding.
                if (REPACKEDFILE.Position % 0x10 != 0)
                    while (REPACKEDFILE.Position % 0x10 != 0)
                        BndBinaryWriter.Write((byte)0x0);

                BndBinaryWriter.Write((uint)0x00);

                NewPointersToPointersZone = (uint)REPACKEDFILE.Position;

                // Copy the last part from the original file.
                using (FileStream BYTECODE = new FileStream(BytecodeAddress, FileMode.Open, FileAccess.Read))
                using (BinaryReader ByteBinary = new BinaryReader(BYTECODE))
                {
                    BYTECODE.Seek(OriginalPointersToPointersZone, SeekOrigin.Begin);

                    byte[] LastBodyPart = new byte[BYTECODE.Length - BYTECODE.Position];

                    BYTECODE.Read(LastBodyPart, 0, LastBodyPart.Length); // Insert the bytecode inside "LastBodyPart".

                    REPACKEDFILE.Write(LastBodyPart, 0, LastBodyPart.Length);
                }

                // Update the Texts' Pointers
                for (int i = 0; i < OriginalPointersToPointers.Length; i++)
                {
                    REPACKEDFILE.Seek(OriginalPointersToPointers[i], SeekOrigin.Begin);
                    BndBinaryWriter.Write(NewSentencesOffset[i]);
                }

                // Update Pointers To Pointers Zone
                REPACKEDFILE.Seek(0x04, SeekOrigin.Begin);
                BndBinaryWriter.Write(NewPointersToPointersZone);

            }
            // END INSERTING TEXT
        }
        */
    }
}