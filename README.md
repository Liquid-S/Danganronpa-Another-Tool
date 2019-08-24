# Danganronpa: Another Tool (aka DRAT)
## Version: 1.0 (Release: 09th April 2017)
### Romhacking tool to mod and translate Danganronpa games for PC and PSVITA.

If you're planning on translating DR1 or DR2, please consider there are
projects already in process of working on them, which you can also join. 

## List of DR1 (PC) fan-made translations:
- Italian: http://alliceteam.altervista.org (by All-Ice Team)
- Spanish: https://transcene.net (by TranScene)
- French: https://equipemirai.com (by Mirai Team)
- Russian: http://anivisual.net/board/1-1-0-463
- Portuguese: http://zetsuboutranslations.me (by Zetsubou Translations)

## List of DR2 (PC) fan-made translations:
- Italian: http://alliceteam.altervista.org (by All-Ice Team)
- Spanish: https://transcene.net (by TranScene)

-------------------------------------------------------------------------------------

# 1 — Game titles compatible with the DRAT
By using this program, you will be able to extract and repack almost every file from:
- Danganronpa: Trigger Happy Havoc (PSP DEMO - PSP - PSVITA - PC)
- Danganronpa 2: Goodbye Despair (PSVITA - PC)
- Danganronpa Another Episode: Ultra Despair Girls (PSVITA - PC)

-------------------------------------------------------------------------------------

# 2 — Requirements
- .NET Framework 4: https://www.microsoft.com/en-US/download/details.aspx?id=17718
- The folder "Ext" has to be next to the tool so that it can work properly.

-------------------------------------------------------------------------------------

# 3 — Usage
The program should be intuitive to use, however ".pak" files maybe aren't so easy to
understand, therefore I'll try to give you a quickly explanation about them. 

## "EXTRACT .PAK" and "REPACK .PAK"
There are different ".pak" files, some store textures, while others store texts.
They are a generic archive container, that's why you need to choose correctly the
right type of ".pak" you are going to work on.

- EXTRACT/REPACK ".PAK" TYPE 1: It works for files like "00_System.pak", "26_Menu.pak",
  "mtb_s03.pak", "49_Novel.pak", "mtb2_s06.pak", etc.
  These type of files ".pak" store only text.

- EXTRACT/REPACK ".PAK" TYPE 2: It works for files like "script_pak_e02.pak",
  "script_pak_novel.pak", "script_pak_e07.pak", "novel_ch01.pak", "file_book.pak", etc.
  These type of files ".pak" store ".LIN" files.

- EXTRACT/REPACK ".PAK" TYPE 3: It works for files like "bin_special_font_l.pak",
  "bin_pb_font_l.pak", "bin_sv_font_l.pak", etc.
  These type of files ".pak" recursively store other ".pak" files.
		
- EXTRACT/REPACK TEXTURE ".PAK" W/O CONVERT: It works for all the ".pak" files
  that use to store textures. Textures will be extracted and repacked as they are.

- EXTRACT/REPACK TEXTURE ".PAK" TO PNG: It works for all the ".pak" files that used
  to store textures. Textures will be extracted as ".png" and will automatically
  encoded them back into the right format while repacked back.

## N.B.  
- Use some good text editor like Notepad++ to edit the ".xml" files.
  DON'T USE WINDOWS' BLOCK NOTES otherwise you will not able to repack back
  the texts correctly! https://notepad-plus-plus.org
  
- The only tool which you can use to edit the fonts for DR1 and DR2 is the SDSE1:
  https://bitbucket.org/blackdragonhunt/the-super-duper-script-editor

- At the moment the DRAT can't be used to edit anagrams or ".cpk" files.

- For specific "reasons" you need to find "psp2gxt.exe" by your own
  and put it inside the "Ext" folder.
  
-------------------------------------------------------------------------------------

# 4 — Changelog
## Version: 1.0 (Release: 09 April 2017)
- The tool has been entirely remade from scratch.
- Everything has been improved
- New features has been added.

## Version: 0.9 (Release: 16th May 2016)
- First public release.

-------------------------------------------------------------------------------------

# 5 — Acknowledgements 
- "convert.exe" is used to convert textures from ".TGA" to ".PNG" and viceversa.
  Thanks to: http://www.imagemagick.org/script/convert.php

- "ScarletTestApp.exe" is used to convert textures from ".GXT" and ".BTX" to ".PNG".
  Thanks to: https://github.com/xdanieldzd/Scarlet
  
- "GIM2PNG.exe" is used to convert textures from ".GIM" to ".PNG".
  Thanks to: https://junk2ool.net/tools/psx/start
  
- "psp2gxt.exe" is used to convert textures from ".TGA" to ".GXT".
