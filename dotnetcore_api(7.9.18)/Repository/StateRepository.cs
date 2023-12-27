using System.Collections.Generic;
using System.Linq;
using System;
using Repository;

namespace MFI.Repository
{
    public class StateRepository: RepositoryBase<State>, IStateRepository
    {
        public StateRepository(AppDb repositoryContext):base(repositoryContext)
        {
           
        }
        public IEnumerable<State> GetAllActiveState() {
            return FindByCondition(x => x.isactive == true);
        }
      
    }
}
