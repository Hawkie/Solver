using System.Collections.Generic;

namespace SolverLib.Logic
{
    public interface ILogicOperation : IList<ILogicOperation>
    {
        string Name { get; set; }

        string Result { get; set; }

        bool Nest { get; set; }
    }
}