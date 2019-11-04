# Brigandine GE Data Editor Library <br> [![Build](http://144.91.79.225:8111/app/rest/builds/buildType:(id:BrigandineGeDataEditor_ReleaseBuild)/statusIcon)](http://144.91.79.225:8111/viewType.html?buildTypeId=BrigandineGeDataEditor_ReleaseBuild&guest=1)[![semantic-release](https://img.shields.io/badge/%20%20%F0%9F%93%A6%F0%9F%9A%80-semantic--release-e10079.svg)](https://github.com/semantic-release/semantic-release)
I built this library to make viewing and/or editing things like names,
descriptions, and stats in Brigandine Grand Edition easier. The
MemoryAccessor class encapsulates the functionality for reading and
writing to the SLPS_026.61/62 file. The SLPS_026 files are on the
original BGE disks or images of the disks, for more information see:
[About SLPS_026.61/62](#About-SLPS_026.61/62). Included in the library
is a default data resource that contains a slightly modified version of
the english translation that can be found here
[Brigandine Grand Edition English Translation](https://www.swordofmoonlight.com/bbs/index.php?topic=869.0),
more information can be found in the [Default Data](#Default-Data)
section.

### About Brigandine
Brigandine Grand Edition or BGE for short is a tactical turn based rpg.
The game originally released in Japan and North America in 1998 as
Brigandine: The Legend of Forsena. In 2000 a new version called
Brigandine Grand Edition released but only in Japan and it was never
officially translated. After more than a decade **HwitVlf** at the
[Sword of Moonlight forums](https://www.swordofmoonlight.com) released
his own English translation that is located on the Sword of Moonlight
forums
[here](https://www.swordofmoonlight.com/bbs/index.php?topic=869.0).

### MemoryAddresses Class
This is a static class that contains the starting address, struct size,
and length of the array, for each the data types. The initial addresses
information came from the forum post
[Brigandine GE: Hex Editor](https://www.swordofmoonlight.com/bbs/index.php?topic=908.0)
on the [Sword of Moonlight forums](https://www.swordofmoonlight.com).
The addresses in the post are for a cue/bin disk image of BGE, while the
addresses used in the library are for the extracted SLPS_026 files in
the cue/bin. The data arrays for the data type structs are fragmented
when in an archive like cue/bin disk image. Instead if the file
containing the data is extracted before using it the arrays line up to
make reading and writing much easier. If possible, I would like to use
the whole archive instead of having the extract the SLPS_026 file.

### Default Data
This file contains all the data and text from the
[Brigandine Grand Edition English Translation](https://www.swordofmoonlight.com/bbs/index.php?topic=869.0)
patch. There are some minor text differences in the default data that
brings the naming closer to the original NA release of Brigandine, and
uses the unmodified stats from the JP release of BGE. The default data
resource contains only the text and stat data, it ***does not*** contain
all the data needed to run the game.

### Data Types
Currently there are 8 supported data types and 3 that are a work in
progress, some of the data types do have nested types as well. All the
initial struct information came from the forum post
[Brigandine GE: Hex Editor](https://www.swordofmoonlight.com/bbs/index.php?topic=908)
and
[forsena.org from May 2014](https://web.archive.org/web/20140517111817/http://forsena.org/).
Each data type has an array in the MemoryAccessor class, which has a
CreateAccessor factory methods which takes an SLPS_026 file as a
MemoryMappedFile or string. I do plan to add more CreateAccessor factory
methods.
1. **AttackData**: Contains data for both the primary and secondary attacks.
2. **CastleData**: Contains the data for a castle.
3. **ItemData**: Contains the data for an item.
4. **DefaultKnightData**: Contains the starting data for a knight.
5. **ClassData**: Contains the data for a class. Monster classes share the same data.
6. **SpellData**: Contains the data for a spell.
7. **SpecialAttackData**: Contains the data for a special attack like a breath attack.
8. **SkillData**: Contains the data (name and description) for a skill.

### Work In Progress:
These are all in #if WORK_IN_PROGRESS and not used by default but if you
uncomment #Define WORK_IN_PROGRESS to add these parts.
1. **StatGrowthData**: Contains all the stat growth data for all the
   knights. I have not had a chance to go through the bin and the game
   to confirm the struct.
2. **MonsterData**: Possibly contains data on monsters but not sure if
   the SLPS_026 files even have this struct.
3. **MonsterInSummonData**: Contains the summon data for a monster.
   Currently is not working correctly.
   -  I believe this struct might be used for holding monster data for
      the castle summon GUI. It could possibly be used with CastleData's
      MonstersThatCanBeSummoned byte field to show the monster data for
      summoning. MonstersThatCanBeSummoned might really be an enum that
      is used as an index into this data array.
      
### About SLPS_026.61/62
The SLPS files are a type of PlayStation executable which contains most
of the data for things like names, stats, monsters, etc... The files are
on the Brigandine GE disks, disk 1 has SLPS_026.61 and disk 2 has
SLPS_026.62. While they are both different files, they do have areas
where they are the same and that is the data, we are interested in.

### Dependencies
The only dependency this library has is .Net Standard 2.0, this is so
anyone can use this library to build GUI tools for editing the SLPS
files. A dependency on System.Memory may be added in the future so
Memory<T> and Span<T> can be used.