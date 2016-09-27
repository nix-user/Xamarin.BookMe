using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data
{
    public class ProfileRepository : BaseRepository<ProfileModel>, IProfileRepository
    {
        public ProfileRepository(IHttpService httpService) : base(httpService)
        {
        }

        public async Task<BaseOperationResult> Put(ProfileModel profile)
        {
            return await HttpService.Put(RestURl.ProfileUrl, profile);
        }

        public async Task<BaseOperationResult<ProfileModel>> Get()
        {
            return await this.HttpService.Get<ProfileModel>(RestURl.ProfileUrl);
        }

        public override Task<BaseOperationResult<IEnumerable<ProfileModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<BaseOperationResult<ProfileModel>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override Task<BaseOperationResult> Add(ProfileModel reservation)
        {
            throw new NotImplementedException();
        }

        public override Task<BaseOperationResult> Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}