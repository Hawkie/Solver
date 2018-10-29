using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SolverLib.Core;
using SolverLib.Space;

namespace SolverLib.Reader
{

    public class PuzzleReader 
    {        

        public IList<int> Read(string filename)
        {
            StreamReader streamReader = null;
            IList<int> list = new List<int>();
            try
            {
                streamReader = new StreamReader(filename);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw (e);
            }
            if (streamReader != null)
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    char[] deliminters = {',', ' '};
                    string[] stringValues = line.Split(deliminters, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string s in stringValues)
                    {
                        int v = Int32.Parse(s);
                        list.Add(v);
                    }
                }
            }
            return list;
        }

        public void ConvertToInitialValues(IList<int> values, ISpace<int> space)
        {
            
            int updated = 0;
            for (int i = 0; i < values.Count(); i++)
            {
                if (values[i] > 0)
                {
                    Possible possible = new Possible() { values[i] };
                    space.Add(i + 1, possible);
                    updated++;
                }
            }

        }
    }
}
