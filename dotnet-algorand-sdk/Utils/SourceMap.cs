using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Algorand.Utils
{
    public  class SourceMap
    {
        public int version;
        public string file;
        public string[] sources;
        public string[] names;
        public string mappings;

        public Dictionary<int, int> pcToLine;
        public Dictionary<int, List<int>> lineToPc;

        public SourceMap(JObject sourceMap)
        {
            int version = (int)sourceMap["version"];
            if (version != 3)
            {
                throw new ArgumentException("Only source map version 3 is supported");
            }
            this.version = version;

            this.file = (string)sourceMap["file"];
            this.mappings = (string)sourceMap["mappings"];

            this.lineToPc = new Dictionary<int, List<int>>();
            this.pcToLine = new Dictionary<int, int>();

            int lastLine = 0;
            string[] vlqs = this.mappings.Split(';');
            for (int i = 0; i < vlqs.Length; i++)
            {
                List<int> vals = VLQDecoder.DecodeSourceMapLine(vlqs[i]);

                // If the vals length >= 3 the lineDelta
                if (vals.Count >= 3)
                {
                    lastLine = lastLine + vals[2];
                }

                if (!this.lineToPc.ContainsKey(lastLine))
                {
                    this.lineToPc[lastLine] = new List<int>();
                }

                List<int> currList = this.lineToPc[lastLine];
                currList.Add(i);
                this.lineToPc[lastLine] = currList;

                this.pcToLine[i] = lastLine;
            }
        }

       /**
       * Returns the Integer line number for the passed PC or null if not found
       * @param  pc   the pc (program counter) of the assembled file
       * @return      the line number or null if not found 
       */
        public int GetLineForPc(int pc)
        {
            return this.pcToLine[pc];
        }

        /**
        * Returns the List of PCs for the passed line number 
        * @param  line the line number of the source file
        * @return      the list of PCs that line generated or empty array if not found 
        */
        public List<int> GetPcsForLine(int line)
        {
            if (!this.pcToLine.ContainsKey(line))
            {
                return new List<int>();
            }
            return this.lineToPc[line];
        }

        public static class VLQDecoder
        {
            private static readonly string b64table = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
            private const int vlqShiftSize = 5;
            private const int vlqFlag = 1 << vlqShiftSize;
            private const int vlqMask = vlqFlag - 1;

            public static List<int> DecodeSourceMapLine(string vlq)
            {
                List<int> results = new List<int>();
                int value = 0;
                int shift = 0;

                for (int i = 0; i < vlq.Length; i++)
                {
                    int digit = b64table.IndexOf(vlq[i]);

                    value |= (digit & vlqMask) << shift;

                    if ((digit & vlqFlag) > 0)
                    {
                        shift += vlqShiftSize;
                        continue;
                    }

                    if ((value & 1) > 0)
                    {
                        value = (value >> 1) * -1;
                    }
                    else
                    {
                        value = value >> 1;
                    }

                    results.Add(value);

                    // Reset
                    value = 0;
                    shift = 0;
                }

                return results;
            }
        }
    }
}
