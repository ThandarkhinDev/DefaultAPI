using System;
using System.Collections.Generic;
using System.Linq;
using MFI;
using Repository;

namespace MFI.Repository
{
    public class SampleRepository: RepositoryBase<Sample>, ISampleRepository
    {
        public SampleRepository(AppDb repositoryContext)
            :base(repositoryContext)
        {
        }

        public IEnumerable<Sample> GetAllSample()
        {
            return FindAll();
        }

        public Sample GetSampleById(int id) {
            return RepositoryContext.Sample.Find(id); //FindByCondition(x => x.ID.Equals(id)).FirstOrDefault();
        }

        public void AddSample(Sample sample)
        {
            Create(sample);
            Save();
        }

        public void UpdateSample(Sample sample)
        {
           Update(sample);
           Save();
        }

        public void DeleteSample(Sample sample)
        {
            throw new NotImplementedException();
        }
    }
}
