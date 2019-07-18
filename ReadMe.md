# Brigandine Grand Edition Data Editor <sub>For Playstation</sub>
## Memory Accessor Module
This module encapsulates all the functionality for reading and writing (not finished yet) to the SLPS_026.61 and SLPS_026.62 file. The SLPS_062
files can be found on the original Brigandine GE disks or on images of
the disks. See Also: [About SLPS_026](#About-LPS_026). The english translation patch from [Brigandine Grand Edition English Translation](https://www.swordofmoonlight.com/bbs/index.php?topic=869.0) was used but it shouldn't matter if you use the patch or not.

#### MemoryAddresses Class
This is a static class that contains static classes that have the same name as the structs in
the DataType folder. The addresses I used are originally came from this forum post [Brigandine GE : Hex Editor](https://www.swordofmoonlight.com/bbs/index.php?topic=908.0). The addresses in the post are for a bin disk image of Brigandine GE, while the addresses found in this class are for the SLPS_026 files and not bin disk images.
This is done because in the bin disk image some of the data
arrays for the structs are fragmented. Instead if the file containing
the data is extracted before using it then the arrays line up perfectly
to make reading and writing easier.

#### Data Types
Currently there are 9 supported data types and 2 that are a work in
progress. All of the initial struct information came from the forum post
[Brigandine GE : Hex Editor](https://www.swordofmoonlight.com/bbs/index.php?topic=908.0).
Each data type has a property and backing field for easy access in the
MemoryAccessor class. A MemoryAccessor class object can be created by
using the CreateAccessor factory method which takes an SLPS_026 file as
a MemoryMappedFile. I plan to add more CreateAccessor factory methods
like one that uses string or File instead of a MemoryMappedFile.

1. **AttackData**: Contains data for both the primary and secondary attacks.
2. **CastleData**: Contains the data for a castle.
3. **ItemData**: Contains the data for an item.
4. **DefaultKnightData**: Contains the starting data for a knight.
5. **ClassData**: Contains the data for a class. Monster classes share the same data.
6. **SpellData**: Contains the data for a spell.
7. **SpecialAttackData**: Contains the data for a special attack like a breath attack.
8. **SkillData**: Contains the data (name and description) for a skill.
9. **MonsterInSummonData**: Contains the summon data for a monster.
<br>● I believe this struct might be used for holding monster data for the castle summon GUI. It could possibly be used with CastleData's MonstersThatCanBeSummoned byte field to show 
the monster data for summoning. MonstersThatCanBeSummoned might really be an an enum that is used as an index into this data array.
###### Work In Progress:
These are all in #if WORK_IN_PROGRESS so they are not used by default
but if you uncomment #Define WORK_IN_PROGRESS to add these parts.
1. **StatGrowthData**: Contains all the stat growth data for all the
   knights. I haven't had a chance to go through the bin and the game to
   confirm the struct.
2. **MonsterData**: Possibly contains data on monsters but not sure if the
   SLPS_026 files even have this struct.
   
### About SLPS_026
The SLPS files are a type of Playstation executable which contains most
of the data for things like names, stats, monsters, etc... The files can
be found on the Brigandine GE disks, disk 1 has SLPS_026.61 and disk 2
has SLPS_026.62. While they are both different files they do have areas
where they are exactly the same and that is the data we are interested
in.

###### Missing Embbeded Resource In Test Project
If you try to run the tests from the Test Project they will fail because
an embedded resource called SLPS_026 which is just the file above
without an extension is missing from the project. If you put the file in
the project directory and make it an embedded resource the tests should
all run successfully. I am going to create a proper test bin file to use
at a later time.