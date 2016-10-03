using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;

namespace BookMeMobile.Data.FakeRepository
{
    public class FakeProfileRepository : BaseRepository<ProfileModel>, IProfileRepository
    {
        public FakeProfileRepository(IHttpService httpService) : base(httpService)
        {
        }

        public override Task<BaseOperationResult> Add(ProfileModel reservation)
        {
            throw new NotImplementedException();
        }

        public Task<BaseOperationResult<ProfileModel>> Get()
        {
            return Task.FromResult(FakeData.ProfileModel());
        }

        public override Task<BaseOperationResult<IEnumerable<ProfileModel>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public override Task<BaseOperationResult<ProfileModel>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseOperationResult> Put(ProfileModel reservation)
        {
            return Task.FromResult(FakeData.SuccessResult());
        }

        public override Task<BaseOperationResult> Remove(int id)
        {
            throw new NotImplementedException();
        }

        Task<BaseOperationResult<IEnumerable<ProfileModel>>> IRepository<ProfileModel>.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<BaseOperationResult<ProfileModel>> IRepository<ProfileModel>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}