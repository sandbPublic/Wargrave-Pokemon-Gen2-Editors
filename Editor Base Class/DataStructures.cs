using System.Collections.Generic;
using System.Linq; // Enumerable
using System; // Convert

namespace Editor_Base_Class {

    /// <summary>
    /// two bytes and ROM bank;
    /// read and save in order X, Y;
    /// implicitly converts to/from int;
    /// immutable;
    /// </summary>
    public class gbcPtr {
        public readonly byte X;
        // first in order
        public readonly byte Y;
        public readonly int ROMbank;

        public gbcPtr(byte Xb, byte Yb, int positon) {
            ROMbank = (int)(positon & ~0x3FFF);
            // banks start each 0x4000 bytes so 0x3FFF bits should be zeroed
            X = Xb;
            Y = Yb;
        }

        public gbcPtr(int absolutePtr) {
            int pointerGBC = absolutePtr & 0x3FFF; // truncation to bank
            // using % doesn't work for "negative" numbers?
            X = (byte)pointerGBC; // truncate for lower byte
            Y = (byte)(pointerGBC / 0x100); // higher byte
            // Yp should already be in range 00 ~ 3F
            Y += 0x40; // always in range 0x4000-0x7FFF
            ROMbank = absolutePtr - pointerGBC;
        }

        // absolute ptr
        public static implicit operator int(gbcPtr ptr) {
            return ptr.ROMbank + (ptr.Y % 0x40) * 0x100 + ptr.X;
        }

        public static implicit operator gbcPtr(int absolutePtr) {
            return new gbcPtr(absolutePtr);
        }
    }

    public interface IData {
        int length();
        //write to file
        void readFromFile(ROM_FileStream ROM_File, string pushFrontWith = "",
            int maxEndPtr = 0, bool lastClass = false); // for DBTrainerList
        void writeToFile(ROM_FileStream ROM_File);
    }

    /// <summary>
    /// a string that implements IData such that 
    /// it can parametrize a DataBlock
    /// implicitly converts to and from string
    /// </summary>
    public class DBString : IData{
        public string me;
        public DBString(string s) {me = s;}
        
        public static implicit operator string(DBString dbs) {
            if (dbs == null) return null;
            return dbs.me;
        }
        public static implicit operator DBString(string s) {return new DBString(s);}

        public int length() { return me.Length + 1; } // +1 for null terminator
        public void readFromFile(ROM_FileStream ROM_File, string pushFrontWith = "",
            int maxEndPtr = 0, bool lastClass = false) {

            me = pushFrontWith + ROM_File.pkmnReadString();
        }
        public void writeToFile(ROM_FileStream ROM_File) {
            ROM_File.pkmnWriteString(me);
        }
    }

    public class EvoData {
        public byte method; // eg level up, trade
        public byte param; // eg level, item ID
        public byte DVparam; // for tyrogue
        public byte species;

        public bool tyrogue() {
            return method == 5;
        }
    }

    // level up move
    public class LearnData {
        public byte level;
        public byte move;
    }

    public class EvoAndLearnset : IData {
        public List<EvoData> evoList;
        public List<LearnData> learnList;

        public EvoAndLearnset() {
            evoList = new List<EvoData>();
            learnList = new List<LearnData>();
        }

        public int length() {
            int ret = learnList.Count * 2 + 1; // learnData terminator
            foreach (EvoData eD in evoList) {
                ret += (eD.tyrogue() ? 4 : 3);
            }
            return ret + 1; // evoData terminator
        }

        public void readFromFile(ROM_FileStream ROM_File, string pushFrontWith = "",
            int maxEndPtr = 0, bool lastClass = false) {

            byte currByte = (byte)ROM_File.ReadByte();
            while (currByte != 0) {
                EvoData eD = new EvoData();

                eD.method = currByte;
                eD.param = (byte)ROM_File.ReadByte();
                if (eD.tyrogue()) eD.DVparam = (byte)ROM_File.ReadByte();
                eD.species = (byte)ROM_File.ReadByte();
                currByte = (byte)ROM_File.ReadByte();

                evoList.Add(eD);
            }

            currByte = (byte)ROM_File.ReadByte();
            while (currByte != 0) {
                LearnData lD = new LearnData();

                lD.level = currByte;
                lD.move = (byte)ROM_File.ReadByte();
                currByte = (byte)ROM_File.ReadByte();

                learnList.Add(lD);
            }
        }

        public void writeToFile(ROM_FileStream ROM_File) {
            foreach (EvoData eD in evoList) {
                ROM_File.WriteByte(eD.method);
                ROM_File.WriteByte(eD.param);
                if (eD.tyrogue()) ROM_File.WriteByte(eD.DVparam);
                ROM_File.WriteByte(eD.species);
            }
            ROM_File.WriteByte(0);
            foreach (LearnData lD in learnList) {
                ROM_File.WriteByte(lD.level);
                ROM_File.WriteByte(lD.move);
            }
            ROM_File.WriteByte(0);
        }
    }

    // a pokemon in a team
    public class TeamMember {
        public byte level;
        public byte species;
        public byte item;
        public byte[] moves = new byte[4];
    }

    public class Trainer {
        public string name;
        // first byte: 00 normal 01 moves 02 item 03 moves and item
        public bool hasItems;
        public bool hasMoves;

        public List<TeamMember> team = new List<TeamMember>();

        public int bytesUsed() {
            return name.Length + 1 // name terminator
                + 1 // category 
                + team.Count * (2 + (hasMoves ? 4 : 0) + (hasItems ? 1 : 0))
                + 1; // team terminator
        }

        public bool valid() {
            return team.Count <= 6 && name != ROM_FileStream.INVALID_STRING;
        }
    };

    public class DBTrainerList : IData {
        public List<Trainer> LT;

        public DBTrainerList() {
            LT = new List<Trainer>();
        }

        public int length() {
            int ret = 0;
            foreach (Trainer t in LT) ret += t.bytesUsed();
            return ret;
        }

        public void readFromFile(ROM_FileStream ROM_File, string pushFrontWith = "",
            int maxEndPtr = 0, bool lastClass = false) {

            if (!lastClass) {
                while (ROM_File.Position < maxEndPtr) {
                    Trainer Tr = ROM_File.readTrainer();
                    if (Tr.valid() && ROM_File.Position <= maxEndPtr) {
                        LT.Add(Tr);
                    }
                }
            } else { // last class, end when first byte of expected name is 0
                while (ROM_File.ReadByte() != 0) {
                    ROM_File.Position--;
                    Trainer Tr = ROM_File.readTrainer();
                    if (Tr.valid() && ROM_File.Position <= maxEndPtr) {
                        LT.Add(Tr);
                    }
                }
            }
        }

        public void writeToFile(ROM_FileStream ROM_File) {
            foreach (Trainer Tr in LT) {
                ROM_File.pkmnWriteString(Tr.name);

                byte category = 0;
                if (Tr.hasItems) category += 2;
                if (Tr.hasMoves) category++;
                ROM_File.WriteByte(category);

                foreach (TeamMember TM in Tr.team) {
                    ROM_File.WriteByte(TM.level);
                    ROM_File.WriteByte(TM.species);
                    if (Tr.hasItems) ROM_File.WriteByte(TM.item);
                    if (Tr.hasMoves) {
                        ROM_File.WriteByte(TM.moves[0]);
                        ROM_File.WriteByte(TM.moves[1]);
                        ROM_File.WriteByte(TM.moves[2]);
                        ROM_File.WriteByte(TM.moves[3]);
                    }
                }
                ROM_File.WriteByte(0xFF);
            }
        }
    }

    // todo animationInstruction

    /// <summary>
    /// Animation instruction for battles
    /// </summary>
    public class AnimeInstr {
        /*
        XX wait, XX < D0?
        D0 obj  (4)
        D1 1gFx (1)
        D2 2gFx (2)
        D3 3gFx (3)
        D6 inc  (1)
        D7 set  (2)
        D8 +bgx (1)
        D9 nFeet
        DB playerHeadobj
        DB cPBal
        DC trans
        DF resetobp0
        E0 sfx  (2)
        E1 cry  (1)
        E5 clrOb
        E6 beat^
        E8 upAct
        EE jmp& (1,*)
        EF jTil (*)
        F0 bgfx (4)
        F2 obp0 (1)
        F4 clrSp
        FC jump (*)
        FD loop (1,*)
        F8 jpIf (1,*)
        F9 var= (1)
        FB jpVr (1,*)
        FE call (*)
        FF RETURN
         * */
        public byte opCode;
        public List<byte> parameters;
        public gbcPtr ptr;

        public AnimeInstr() {
            parameters = new List<byte>();
        }

        public int size() {
            return 1 + parameters.Count + (ptr == null ? 0 : 2);
        }

        public int expectedParameters() {
            switch (opCode) {
                case 0xD1: // 1gfx
                case 0xD6: // inc
                case 0xD8: // +bgx
                case 0xE1: // cry
                case 0xEE: // jmp&
                case 0xF2: // +obp0
                case 0xF8: // jpIf
                case 0xF9: // var=
                case 0xFB: // jpVr
                case 0xFD: // loop
                    return 1; 

                case 0xD2: // 2gfx
                case 0xD7: // set
                case 0xE0: // sfx
                    return 2; 

                case 0xD3: // 3gfx
                    return 3;

                case 0xD0: // obj
                case 0xF0: // bgfx
                    return 4; 

                default:
                    return 0;
            }
        }

        public bool expectedPtr() {
            return (opCode == 0xEE // jmp&
                || opCode == 0xEF // jTil
                || opCode == 0xF8 // jpIf
                || opCode == 0xFB // jump var 
                || opCode == 0xFC // jump
                || opCode == 0xFD // loop
                || opCode == 0xFE // call
                ); 
        }

        public string byteString() {
            string ret = opCode.ToString("X2") + " ";
            foreach (byte b in parameters) {
                ret += b.ToString("X2") + " ";
            }
            if (ptr != null) {
                ret += ptr.X.ToString("X2") + " ";
                ret += ptr.Y.ToString("X2") + " ";
            }
            return ret.Trim();
        }

        public string codeString() {
            string ret = "";
            switch (opCode) {
                case 0xD0: ret += "obj  "; break;
                case 0xD1: ret += "1gFx "; break;
                case 0xD2: ret += "2gFx "; break;
                case 0xD3: ret += "3gFx "; break;
                case 0xD6: ret += "obj+ "; break;
                case 0xD7: ret += "obj= "; break;
                case 0xD8: ret += "+bgx "; break;
                case 0xD9: ret += "enemyFeetObj"; break;
                case 0xDA: ret += "playerHeadObj"; break;
                case 0xDB: ret += "checkPkBall"; break;
                case 0xDF: ret += "reset_obp0"; break;
                case 0xDC: ret += "transform"; break;
                case 0xE0: ret += "sFx  "; break;
                case 0xE1: ret += "cry  "; break;
                case 0xE5: ret += "clearObj"; break;
                case 0xE6: ret += "beatUp"; break;
                case 0xE8: ret += "updateAct"; break;
                case 0xEE: ret += "jmp& "; break;
                case 0xEF: ret += "jTil "; break;
                case 0xF0: ret += "bgFx "; break;
                case 0xF2: ret += "obp0 "; break;
                case 0xF4: ret += "clearSprites"; break;
                case 0xF8: ret += "jpIf "; break;
                case 0xF9: ret += "var= "; break;
                case 0xFB: ret += "jpVr "; break;
                case 0xFC: ret += "jump "; break;
                case 0xFD: ret += "loop "; break;
                case 0xFE: ret += "call "; break;
                case 0xFF: ret += "RETURN"; break;
                default: ret += (opCode < 0xD0 ? "wait " : "???? ") 
                    + opCode.ToString("X2"); break; 
            }
            foreach (byte b in parameters) {
                ret += b.ToString("X2") + " ";
            }
            if (ptr != null) {
                ret += (ptr % 0x10000).ToString("X");
            }
            return ret.Trim();
        }
    }

    public class AnimationCode : IData {
        public List<AnimeInstr> me;
        public List<AnimationCode> jumps;
        public long startAddr;

        public AnimationCode() {
            me = new List<AnimeInstr>();
        }

        public int length() {
            int ret = 0;
            foreach (AnimeInstr AI in me) ret += AI.size();
            return ret;
        }

        public void readParams(ref AnimeInstr AI, ROM_FileStream ROM_File) {
            for (int byte_i = 0; byte_i < AI.expectedParameters(); byte_i++) {
                AI.parameters.Add((byte)ROM_File.ReadByte());
            }
        }

        public void readFromFile(ROM_FileStream ROM_File, string pushFrontWith = "",
            int maxEndPtr = 0, bool lastClass = false) {

            readFromFileRecursive(ROM_File, new List<long>());
        }

        public void readFromFileRecursive(ROM_FileStream ROM_File, List<long> triedAddresses) {
            startAddr = ROM_File.Position;
            triedAddresses.Add(startAddr);

            byte currByte = 0;
            while (currByte != 0xFF) {
                AnimeInstr aI = new AnimeInstr();
                currByte = (byte)ROM_File.ReadByte();
                aI.opCode = currByte;
                readParams(ref aI, ROM_File);
                if (aI.expectedPtr()) aI.ptr = ROM_File.readGBCPtr();
                me.Add(aI);
            }

            // recursively fill out everything jumped to
            jumps = new List<AnimationCode>();
            foreach (AnimeInstr aI in me) {
                if (aI.ptr != null) {
                    // not internal loop
                    if (aI.ptr < startAddr || aI.ptr >= startAddr + length()) { 
                        // not already tried
                        bool tried = false;
                        foreach (long addr in triedAddresses) {
                            if (addr == aI.ptr) tried = true;
                        }

                        if (!tried) {
                            ROM_File.Position = aI.ptr;
                            List<long> triedAddresses2 = new List<long>();
                            foreach (long l in triedAddresses) {
                                triedAddresses2.Add(l);
                            }

                            AnimationCode jump = new AnimationCode();
                            jump.readFromFileRecursive(ROM_File, triedAddresses2);
                            jumps.Add(jump);
                        }
                    }
                }
            }
        }

        public void writeToFile(ROM_FileStream ROM_File) {
            foreach (AnimeInstr aI in me) {
                ROM_File.WriteByte(aI.opCode);
                foreach (byte b in aI.parameters) {
                    ROM_File.WriteByte(b);
                }
                if (aI.expectedPtr()) {
                    ROM_File.writeLocalGBCPtr(aI.ptr);
                }
            }
        }
    }

    public class AreaWildData {
          // data stored as such in rom at pointed locations:
 // five header bytes: Bank NUm m,d,n freq
 // 2*7*3=42 data bytes, level+species 2, seven slots 14, three times 42

        public static int MORN = 0, DAY = 1, NIGHT = 2;

        public bool water;
        public IEnumerable<int> timeRange() {
            return Enumerable.Range(0, times());
        }
        public int times() {
            return water ? 1 : 3;
        }
        public IEnumerable<int> slotRange() {
            return Enumerable.Range(0, slots());
        }
        public int slots() {
            return water ? 3 : 7;
        }

        public int duration(int slot_i) {
            if (water) return 12;
            if (slot_i == MORN) return 3; // 4 to 10
            if (slot_i == DAY) return 4; // 10 to 18
            return 5; // 18 to 4
        }

        public byte mapBank; // bank and num don't match exactly with G2Map v0.2?
        public byte mapNum; // off by one?
        public byte[] freq;
        public byte[,] levels;
        public byte[,] species;

        public AreaWildData(bool wtr = false) {
            water = wtr;

            freq = new byte[times()];
            levels = new byte[times(), slots()];
            species = new byte[times(), slots()];
        }
    }

    // class for data blocks in ROM that store variable # of bytes
    // may contain pointer table
    // if so, may contain discontiguous data entries
    public class DataBlock<T> where T : IData{
        private readonly int startOffset;
        public readonly int endOffset;

        public readonly int start_i; // index from 1 generally, since 00 is special
        public readonly int end_i; // end at 0xFB, etc.
        private readonly bool hasPtrs;

        public T[] data;
        public gbcPtr[] ptrs;

        /// <summary>
        /// if data at index is not contiguous with prior data/ptr table, return true
        /// </summary>
        public bool[] discontigAt;

        public DataBlock(int offStart, int offEnd, bool ptrTable = false, 
            int end = 0xFF, int start = 1) {

            startOffset = offStart;
            endOffset = offEnd;
            start_i = start;
            end_i = end;
            hasPtrs = ptrTable;

            data = new T[end_i + 1];

            for (int data_i = start_i; data_i <= end_i; data_i++) {
                constructDataAt(data_i);// initialize here so data will never be null after creation
            }

            if (hasPtrs) {
                ptrs = new gbcPtr[end_i + 2];
                ptrs[end_i + 1] = endOffset;
                discontigAt = new bool[end_i + 2]; // last is if final T is "contig" with end
            }
        }

        public IEnumerable<int> range() {
            return Enumerable.Range(start_i, end_i - start_i + 1);
        }

        public void constructDataAt(int index){
            if (data is DBString[]) {
                data[index] = (T)Convert.ChangeType(new DBString(""), typeof(T));
            } else if (data is EvoAndLearnset[]) {
                data[index] = (T)Convert.ChangeType(new EvoAndLearnset(), typeof(T));
            } else if (data is DBTrainerList[]) {
                data[index] = (T)Convert.ChangeType(new DBTrainerList(), typeof(T));
            } else if (data is AnimationCode[]) {
                data[index] = (T)Convert.ChangeType(new AnimationCode(), typeof(T));
            } 
        }

        public int endPtr(int index) {
            if (index == start_i - 1) 
                return startOffset + 2 * (end_i - start_i + 1); // end of table

            return ptrs[index] + data[index].length();
        }

        /// <summary>
        /// return ptr[index+1] if contiguous, or search if not
        /// will return pointer with is STRICTLY greater
        /// relevant for e.g. PKMON PROF. and ELITE FOUR (WILL)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int maxEndPtr(int index) {
            if (hasPtrs) {
                if (!discontigAt[index + 1]) {
                    return ptrs[index + 1];
                }
                // search for min which is larger than ptrs[index]
                // <= to allow for empty structures? e.g. empty trainer list
                int end = endOffset;
                for (int ptr_i = start_i; ptr_i <= end_i; ptr_i++) {
                    if (ptrs[index] < ptrs[ptr_i] && ptrs[ptr_i] < end) {
                        end = ptrs[ptr_i];
                    }
                }
                return end;
            }
            return 0;
        }

        /// <summary>
        /// only valid once made contiguous
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int bytesFreeAt(int index) {
            if (hasPtrs) {
                while (index <= end_i && !discontigAt[index + 1]) {
                    index++; // find next discontiguity
                }
                if (index > end_i) index = end_i; // no discontiguities, except perhaps "end"
                return maxEndPtr(index) - endPtr(index);
            }

            int ret = endOffset - startOffset;
            for (int data_i = start_i; data_i <= end_i; data_i++) {
                ret -= data[data_i].length();
            }
            return ret;
        }

        /// <summary>
        /// returns -1 if bytes OK
        /// </summary>
        /// <returns></returns>
        public int bytesOverlapAt() {
            for (int data_i = start_i; data_i <= end_i; data_i++) {
                if (endPtr(data_i) > maxEndPtr(data_i)) {
                    return data_i;
                }
            }
            return -1;
        }

        public int relativePtr(int index) {
            return ptrs[index] - startOffset;
        }

        public void setRelativePtr(int index, int value) {
            ptrs[index] = startOffset + value;
        }

        public void updatePtrs(int changed_i) {
            if (hasPtrs) {
                int deltaBytes = data[changed_i].length() - // current length
                    (maxEndPtr(changed_i) - ptrs[changed_i]); // old length

                while (changed_i < end_i && !discontigAt[changed_i + 1]) {
                    changed_i++;
                    ptrs[changed_i] = ptrs[changed_i] + deltaBytes;
                }
            }
        }

        /// <summary>
        /// DOESN'T change discontigAt values
        /// just repoints
        /// </summary>
        public void makeContiguous() {
            if (hasPtrs) {
                for (int ptr_i = start_i; ptr_i <= end_i; ptr_i++) {
                    if (!discontigAt[ptr_i]) {
                        ptrs[ptr_i] = endPtr(ptr_i - 1); // start_i - 1 returns table end
                    }
                }
            }
        }

        /// <summary>
        /// removes things like Pkmon prof class that 
        /// share a pointer with the next data
        /// </summary>
        public void purgeDupes() {
            for (int dupe_i = start_i; dupe_i < end_i; dupe_i++) {
                if (discontigAt[dupe_i + 1]
                    && (int) ptrs[dupe_i] == ptrs[dupe_i + 1]) {

                    constructDataAt(dupe_i);
                    discontigAt[dupe_i + 1] = false;
                    updatePtrs(dupe_i);
                }
            }
        }

        /// <summary>
        /// jump to offset first and check if not null with jumpToIfLoading()
        /// automatically marks discontigAt
        /// automatically initializes data
        /// </summary>
        public void readFromFile(ROM_FileStream ROM_File, bool pushFrontWithHex = false) {
            if (hasPtrs) {
                foreach (int ptr_i in range()) {
                    ptrs[ptr_i] = ROM_File.readGBCPtr();
                }
                foreach (int data_i in range()) {
                    ROM_File.Position = ptrs[data_i];
                    constructDataAt(data_i);

                    discontigAt[data_i+1] = true; // must assume this to get correct maxEndPtr here
                    data[data_i].readFromFile(ROM_File, 
                        (pushFrontWithHex ? data_i.ToString("X2") + "-" : ""),
                        maxEndPtr(data_i), //special code for TrainerLists due to irregular termination
                        maxEndPtr(data_i) == endOffset);

                    discontigAt[data_i] = (ptrs[data_i] != endPtr(data_i - 1));
                }
                discontigAt[end_i+1] = (maxEndPtr(end_i) != endOffset);

            } else {
                foreach (int data_i in range()) {
                    constructDataAt(data_i);
                    data[data_i].readFromFile(ROM_File, 
                        (pushFrontWithHex ? data_i.ToString("X2") + "-" : ""));
                }
            }
        }

        /// <summary>
        /// jump to offset first and check if not null with jumpToIfSaving()
        /// </summary>
        public void writeToFile(ROM_FileStream ROM_File) {
            if (hasPtrs) {
                makeContiguous();
                foreach (int ptr_i in range()) {
                    ROM_File.writeLocalGBCPtr(ptrs[ptr_i]);
                }
            }

            foreach (int data_i in range()) {
                if (hasPtrs) ROM_File.Position = ptrs[data_i];
                data[data_i].writeToFile(ROM_File);    
            }

            if (!hasPtrs || !discontigAt[end_i + 1]) {
                while (ROM_File.Position < endOffset) {
                    ROM_File.WriteByte(0); // "free" data
                }
            }
        }
    }

    partial class Gen2Editor {
        #region ITEM
        protected const int 
            COST1_I = 0, // lower byte eg 100
            COST2_I = 1, // upper byte eg 9800
            HELD_ITEM_ID_I = 2, // For held items only
            PARAM_I = 3, // Heal amount, probability of effect in hex, etc
            FLAG_I = 4, // Key items and TMs = C0; Bike, Rods, and Itemfinder = 80; all others 40       
            POCKET_I = 5,
            USE_RESTRICTION_I = 6;

        protected readonly string[] POCKETS = { "Item", "Key", "Ball", "TM" };
        protected byte[,] items;
        protected int[] itemASM;
        //protected const int LAST_NON_TM_ITEM = 0xBD;
        // TMs are special in a few ways: they don't have item descriptions
        // but instead use the corresponding move desc. They also don't have
        // their asm pointers in the same table

        protected DataBlock<DBString> itemNames;
        protected DataBlock<DBString> itemDescs;
        #endregion
        #region MOVE
        public DataBlock<DBString> moveNames; // public for TM form
        protected DataBlock<DBString> moveDescs;

        protected const int ANIMATION_I = 0, EFFECT_I = 1, POWER_I = 2, 
            TYPE_I = 3, ACCURACY_I = 4, PP_I = 5, EFFECT_CHANCE_I = 6;
        protected byte[,] moves;

        protected bool[] moveIsCrit;
        protected int critBytesUsed() {
            int ret = 1; // terminator
            for (int crit_i = 1; crit_i <= offset[NUM_OF_MOVES_I]; crit_i++) {
                if (moveIsCrit[crit_i]) ret++;
            }
            return ret;
        }
        protected int critBytesAvailable() {
            return offset[NEW_CRIT_LIST_END_I] - offset[NEW_CRIT_LIST_I];
        }
        protected bool critBytesOK() {
            return !loadOffset[CRIT_LIST_PTR_I] || (critBytesUsed() <= critBytesAvailable());
        }
        // repoint crits when saving, default location is 
        // bordered with unknown data on both sides

        // type names
        protected DataBlock<DBString> typeNames; // 17 + ??? + bird + 9 NORMAL type
        // table of 28 ptrs, index of ptr corresponds to byte game uses to represent type
        #endregion
        #region MOVESET
        protected string[] pkmnNames; //10 bytes per name

        // tm hm tutor and egg moves not included here
        protected DataBlock<EvoAndLearnset> movesets;

        public byte[] TMCodes = new byte[50]; // public for TM form
        public byte[] HMCodes = new byte[8];
        public bool[,] TMSets = new bool[256, 64];
        #endregion
        #region TRAINER
        protected DataBlock<DBString> trClassNames;

        //protected DataBlock<List<Trainer>> trainerLists;
        protected DataBlock<DBTrainerList> trainerLists;

        protected List<byte> trClassDVs;
        protected List<byte> trClassItems;
        protected List<byte> trClassRewards;
        #endregion
        protected DataBlock<AnimationCode> animations;
        #region WILD
        protected List<AreaWildData> johtoLand;
        protected List<AreaWildData> johtoWater;
        protected List<AreaWildData> kantoLand;
        protected List<AreaWildData> kantoWater;
        protected List<AreaWildData> swarm;

        //area name data
        //@1CAA43 -> 436A ptr @1CA8C9
        //tables starts @1CA8C5?
        // for some reason only every other pointer is actually an area name....
        protected DataBlock<DBString> areaNames; 
        #endregion
    }
}