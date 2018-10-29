using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolverLib.Logic
{
    public class LogicResult : ILogicResult
    {
        private object myValue;

        private int type = 0;

        private LogicResult()
        {
            myValue = null;
        }

        public LogicResult(object value)
        {
            this.myValue = value;
        }

        public LogicResult(int value)
        {
            this.myValue = value;
        }

        public object Value
        {
            get
            {
                return myValue;
            }
            set
            {
                myValue = value;
            }
        }

        public int Type
        {
            get
            {
                return type;
            }
        }

        public int ToInt32
        {
            get
            {
                return Convert.ToInt32(myValue);
            }

            set
            {
                myValue = value;
            }
        }

        public double PassRating 
        { 
            get 
            {
                double result = this.ToInt32;
                if (this.type == 0)
                {
                    result = Math.Pow(result, 2);
                    this.type = 1;
                }
                return result;
            }
        }

        public int CompareTo(ILogicResult other)
        {
            return this.ToInt32.CompareTo(other.ToInt32);
        }

        public ILogicResult Subtract(ILogicResult other)
        {
            LogicResult result = new LogicResult(this.ToInt32 - other.ToInt32);
            return result;
        }

        public ILogicResult Add(ILogicResult other)
        {
            LogicResult result = new LogicResult(this.ToInt32 + other.ToInt32);
            return result;
        }

        public ILogicResult Combine(ILogicResult other)
        {
            LogicResult result = new LogicResult(this.PassRating + other.PassRating);
            this.type = 1;
            return result;
        }

        public ILogicResult Or(ILogicResult other)
        {
            LogicResult result = new LogicResult(this.ToInt32 | other.ToInt32);
            return result;
        }

        public ILogicResult And(ILogicResult other)
        {
            LogicResult result = new LogicResult(this.ToInt32 & other.ToInt32);
            return result;
        }

        public bool NotZero
        {
            get
            {
                if (this.ToInt32 > 0
                    || this.ToInt32 < 0)
                {
                    return true;
                }
                return false;
            }
        }

        public static int MaxValue
        {
            get
            {
                return 100;
            }
        }

        public static int MinValue
        {
            get
            {
                return -100;
            }
        }

        public override string ToString()
        {
            return this.ToInt32.ToString();
        }
    }
}
