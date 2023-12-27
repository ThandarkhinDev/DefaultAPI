using System;
using System.Collections.Generic;
using MFI; 

namespace MFI.Repository
{
    public interface ISampleRepository : IRepositoryBase<Sample>
    {
        Sample GetSampleById(int id);
    }
}
