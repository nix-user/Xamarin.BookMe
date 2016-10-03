using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.BL.Abstract
{
    public interface IProfileService
    {
        Task<BaseOperationResult<ProfileModel>> GetUserData();

        Task<BaseOperationResult> SaveProfileModel(ProfileModel model);
    }
}