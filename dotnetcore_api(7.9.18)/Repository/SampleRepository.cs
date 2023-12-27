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
                
        public Sample GetSampleById(int id) {
            return RepositoryContext.Sample.Find(id); //FindByCondition(x => x.ID.Equals(id)).FirstOrDefault();
        }
        
    }
}
