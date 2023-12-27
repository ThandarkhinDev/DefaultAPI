using System;
using System.Collections.Generic;
using MFI; 

namespace MFI.Repository
{
    public interface ISampleRepository
    {
        IEnumerable<Sample> GetAllSample();
        Sample GetSampleById(int id);
        void AddSample(Sample sample);
        void UpdateSample(Sample sample);
        void DeleteSample(Sample sample);
    }
}
