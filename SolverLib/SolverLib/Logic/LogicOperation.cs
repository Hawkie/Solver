using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public class LogicOperation : List<ILogicOperation>, ILogicOperation
    {
        public LogicOperation()
        {}

        public LogicOperation(string operation)
        {
            this.Name = operation;
        }

        public LogicOperation(string operation, bool nest)
        {
            this.Name = operation;
            this.Nest = nest;
        }

        public string Name { get; set; }

        public bool Nest { get; set; }

        public string Result { get; set; }

        public override string ToString()
        {
            return MyString(this, 0);
        }

        private static string MyString(ILogicOperation operation, int depth)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(operation.Name);
            
            if (operation.Count > 0)
            {
                if (operation.Nest)
                {
                    sb.AppendLine();
                    sb.Append(Indent(depth));
                }
                sb.Append("(");
                if (operation.Nest)
                {
                    sb.AppendLine();
                    sb.Append(Indent(depth + 1));
                }

                sb.Append(MyString(operation[0], depth + 1));

                foreach (ILogicOperation nextOperation in operation.Skip(1))
                {
                    sb.Append(",");
                    if (operation.Nest)
                    {
                        sb.AppendLine();
                        sb.Append(Indent(depth + 1));
                    }

                    sb.Append(MyString(nextOperation, depth + 1));
                }
                if (operation.Nest)
                {
                    sb.AppendLine();
                    sb.Append(Indent(depth));
                }
                sb.Append(")");
            }

            sb.Append("[" + operation.Result + "]");

            return sb.ToString();
        }

        private static string Indent(int depth)
        {
            string s = string.Empty;
            return s.PadRight(depth * 4);
        }
    }
}
