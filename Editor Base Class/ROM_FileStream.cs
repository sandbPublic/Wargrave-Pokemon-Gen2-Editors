using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; // open save read write files

namespace Editor_Base_Class
{
    public class ROM_FileStream : FileStream
    {
        public ROM_FileStream(string path, FileMode mode) : base(path, mode)
        {
        }

        public byte[] ReadBytes(int offset, int lengthToRead)
        {
            byte[] data = new byte[lengthToRead];

            if ((offset + lengthToRead < Length) && (offset >= 0))
            {
                Position = offset;
                Read(data, 0, lengthToRead);
            }

            return data;
        }

        public void WriteBytes(byte[] data, int offset)
        {
            if ((offset + data.Length < Length) && offset >= 0)
            {
                Position = offset;
                Write(data, 0, data.Length);
            }
        }

        // TODO as dictionary?
        public static char PkmnByteToChar(byte b)
        {
            switch (b)
            {
                case 0x1F: return ' '; // area names
                case 0x4A: return '^'; // "pkmn" for pkmn trainer?
                case 0x4E: return '|';
                case 0x50: return '~';
                case 0x54: return '{'; // PK also E1?
                case 0xE2: return '}'; // MN
                case 0xE6: return '?';
                case 0x7F: return ' ';
                case 0x80: return 'A';
                case 0x81: return 'B';
                case 0x82: return 'C';
                case 0x83: return 'D';
                case 0x84: return 'E';
                case 0x85: return 'F';
                case 0x86: return 'G';
                case 0x87: return 'H';
                case 0x88: return 'I';
                case 0x89: return 'J';
                case 0x8A: return 'K';
                case 0x8B: return 'L';
                case 0x8C: return 'M';
                case 0x8D: return 'N';
                case 0x8E: return 'O';
                case 0x8F: return 'P';
                case 0x90: return 'Q';
                case 0x91: return 'R';
                case 0x92: return 'S';
                case 0x93: return 'T';
                case 0x94: return 'U';
                case 0x95: return 'V';
                case 0x96: return 'W';
                case 0x97: return 'X';
                case 0x98: return 'Y';
                case 0x99: return 'Z';
                case 0x9A: return '(';
                case 0x9B: return ')';
                case 0xA0: return 'a';
                case 0xA1: return 'b';
                case 0xA2: return 'c';
                case 0xA3: return 'd';
                case 0xA4: return 'e';
                case 0xA5: return 'f';
                case 0xA6: return 'g';
                case 0xA7: return 'h';
                case 0xA8: return 'i';
                case 0xA9: return 'j';
                case 0xAA: return 'k';
                case 0xAB: return 'l';
                case 0xAC: return 'm';
                case 0xAD: return 'n';
                case 0xAE: return 'o';
                case 0xAF: return 'p';
                case 0xB0: return 'q';
                case 0xB1: return 'r';
                case 0xB2: return 's';
                case 0xB3: return 't';
                case 0xB4: return 'u';
                case 0xB5: return 'v';
                case 0xB6: return 'w';
                case 0xB7: return 'x';
                case 0xB8: return 'y';
                case 0xB9: return 'z';
                case 0xD4: return '`';
                case 0xE0: return '\'';
                case 0xE3: return '-';
                case 0xE8: return '.';
                case 0xE9: return '&';
                case 0xF3: return '/';
                case 0xF4: return ',';
                case 0xF6: return '0';
                case 0xF7: return '1';
                case 0xF8: return '2';
                case 0xF9: return '3';
                case 0xFA: return '4';
                case 0xFB: return '5';
                case 0xFC: return '6';
                case 0xFD: return '7';
                case 0xFE: return '8';
                case 0xFF: return '9';
            }
            return (char)b;
        }

        public static byte PkmnCharToByte(char c)
        {
            switch (c)
            {
                case '^': return 0x4A;
                case '|': return 0x4E;
                case '~': return 0x50;
                case '{': return 0x54; // PK also E1?
                case '}': return 0xE2; // MN
                case '?': return 0xE6;
                case ' ': return 0x7F;
                case 'A': return 0x80;
                case 'B': return 0x81;
                case 'C': return 0x82;
                case 'D': return 0x83;
                case 'E': return 0x84;
                case 'F': return 0x85;
                case 'G': return 0x86;
                case 'H': return 0x87;
                case 'I': return 0x88;
                case 'J': return 0x89;
                case 'K': return 0x8A;
                case 'L': return 0x8B;
                case 'M': return 0x8C;
                case 'N': return 0x8D;
                case 'O': return 0x8E;
                case 'P': return 0x8F;
                case 'Q': return 0x90;
                case 'R': return 0x91;
                case 'S': return 0x92;
                case 'T': return 0x93;
                case 'U': return 0x94;
                case 'V': return 0x95;
                case 'W': return 0x96;
                case 'X': return 0x97;
                case 'Y': return 0x98;
                case 'Z': return 0x99;
                case '(': return 0x9A;
                case ')': return 0x9B;
                case 'a': return 0xA0;
                case 'b': return 0xA1;
                case 'c': return 0xA2;
                case 'd': return 0xA3;
                case 'e': return 0xA4;
                case 'f': return 0xA5;
                case 'g': return 0xA6;
                case 'h': return 0xA7;
                case 'i': return 0xA8;
                case 'j': return 0xA9;
                case 'k': return 0xAA;
                case 'l': return 0xAB;
                case 'm': return 0xAC;
                case 'n': return 0xAD;
                case 'o': return 0xAE;
                case 'p': return 0xAF;
                case 'q': return 0xB0;
                case 'r': return 0xB1;
                case 's': return 0xB2;
                case 't': return 0xB3;
                case 'u': return 0xB4;
                case 'v': return 0xB5;
                case 'w': return 0xB6;
                case 'x': return 0xB7;
                case 'y': return 0xB8;
                case 'z': return 0xB9;
                case '`': return 0xD4;
                case '\'': return 0xE0;
                case '-': return 0xE3;
                case '.': return 0xE8;
                case '&': return 0xE9;
                case '/': return 0xF3;
                case ',': return 0xF4;
                case '0': return 0xF6;
                case '1': return 0xF7;
                case '2': return 0xF8;
                case '3': return 0xF9;
                case '4': return 0xFA;
                case '5': return 0xFB;
                case '6': return 0xFC;
                case '7': return 0xFD;
                case '8': return 0xFE;
                case '9': return 0xFF;
            }
            return (byte)c;
        }

        // caused by loading a bad 00 byte
        public static readonly string INVALID_STRING = "INVALID_STRING";
        /// <summary>
        /// reads until '~' or maxLength chars
        /// public for DataBlock usage
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public string PkmnReadString(int maxLength = 256)
        {
            string s = "";

            for (int bytesRead = 0; bytesRead < maxLength; bytesRead++)
            {
                byte b = (byte)ReadByte();

                if (b == 0) return INVALID_STRING;

                if (b == 0x50) return s;

                s += PkmnByteToChar(b);
            }

            return s;
        }

        public void PkmnWriteString(string s)
        {
            foreach (char c in s) WriteByte(PkmnCharToByte(c));
            WriteByte(0x50);
        }

        public static bool[] TMBoolsFromBytes(byte[] bytes)
        {
            bool[] ret = new bool[64];
            if (bytes.Length == 8)
            {
                for (int byte_i = 0; byte_i < 8; byte_i++)
                {
                    System.Diagnostics.Debug.Print(bytes[byte_i].ToString("X2"));
                    for (int bit_i = 0; bit_i < 8; bit_i++)
                    {
                        // extract single bit
                        ret[byte_i * 8 + bit_i] = ((bytes[byte_i] & (0x1 << bit_i)) != 0);
                    }
                }
            }

            return ret;
        }

        public static byte[] TMBytesFromBools(bool[] bools)
        {
            byte[] ret = new byte[8];
            if (bools.Length == 64)
            {
                for (int byte_i = 0; byte_i < 8; byte_i++)
                {
                    ret[byte_i] = 0;
                    for (int bit_i = 0; bit_i < 8; bit_i++)
                    {
                        // assemble byte from 8 bools
                        if (bools[byte_i * 8 + bit_i])
                            ret[byte_i] |= (byte)(0x1 << bit_i);
                    }
                }
            }
            return ret;
        }

        public GbcPtr ReadGBCPtr()
        {
            int p = (int)Position; // don't want p+2 after reads
            byte Xp = (byte)ReadByte();
            byte Yp = (byte)ReadByte();

            return new GbcPtr(Xp, Yp, p); //swap bytes
        }

        /// <summary>
        /// writes at current ROM_File position, advancing two bytes
        /// </summary>
        /// <param name="absolutePtr"></param>
        public void WriteLocalGBCPtr(GbcPtr ptr)
        {
            WriteByte(ptr.X);
            WriteByte(ptr.Y);
        }

        // put ROM_File in correct position first
        public Trainer ReadTrainer()
        {
            Trainer t = new Trainer();
            t.name = PkmnReadString();
            int category = ReadByte();
            t.hasItems = (category >= 2);
            t.hasMoves = (category == 1 || category == 3);

            // read pokemon in team
            t.team = new List<TeamMember>();
            
            for (int pkmn_i = 0; pkmn_i < 6; pkmn_i++)
            {
                byte b = (byte)ReadByte();

                if (b == 0xFF) break;

                TeamMember TM = new TeamMember
                {
                    level = b,
                    species = (byte)ReadByte()
                };
                
                if (t.hasItems) TM.item = (byte)ReadByte();
                
                if (t.hasMoves)
                {
                    TM.moves[0] = (byte)ReadByte();
                    TM.moves[1] = (byte)ReadByte();
                    TM.moves[2] = (byte)ReadByte();
                    TM.moves[3] = (byte)ReadByte();
                }
                t.team.Add(TM);
            }
            return t;
        }

        public AreaWildData ReadWildArea(bool water = false)
        {
            AreaWildData ret = new AreaWildData
            {
                water = water,
                mapBank = (byte)ReadByte(),
                mapNum = (byte)ReadByte()
            };
            for (int time_i = 0; time_i < ret.Times(); time_i++)
            {
                ret.freq[time_i] = (byte)ReadByte();
            }
            for (int time_i = 0; time_i < ret.Times(); time_i++)
            {
                for (int slot_j = 0; slot_j < ret.Slots(); slot_j++)
                {
                    ret.levels[time_i, slot_j] = (byte)ReadByte();
                    ret.species[time_i, slot_j] = (byte)ReadByte();
                }
            }
            return ret;
        }

        public void WriteWildAreaList(List<AreaWildData> lAWD)
        {
            foreach (AreaWildData awd in lAWD)
            {
                WriteByte(awd.mapBank);
                WriteByte(awd.mapNum);
                WriteByte(awd.freq[0]);
                if (!awd.water)
                {
                    WriteByte(awd.freq[1]);
                    WriteByte(awd.freq[2]);
                }

                for (int time_i = 0; time_i < awd.Times(); time_i++)
                {
                    for (int slot_j = 0; slot_j < awd.Slots(); slot_j++)
                    {
                        WriteByte(awd.levels[time_i, slot_j]);
                        WriteByte(awd.species[time_i, slot_j]);
                    }
                }
            }
            WriteByte(0xFF);
        }
    }
}
