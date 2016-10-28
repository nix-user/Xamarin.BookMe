using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.Abstract
{
    public interface IProfileRepository : IRepository<ProfileModel>
    {
        Task<BaseOperationResult> Put(ProfileModel reservation);

        Task<BaseOperationResult<ProfileModel>> Get();
    }
}