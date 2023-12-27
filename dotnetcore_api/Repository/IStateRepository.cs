//using Entities.ExtendedModels;
using MFI;
using System;
using System.Collections.Generic;

namespace MFI.Repository
{
    public interface IStateRepository
    {
       IEnumerable<State> GetAllActiveState();

    }
       
}
