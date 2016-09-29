using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using BookMeMobile.Resources;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookMeMobile.BL.Concrete
{
    internal class ProfileService : IProfileService
    {
        private IProfileRepository profileRepository;

        public ProfileService(IProfileRepository repository)
        {
            this.profileRepository = repository;
        }

        public async Task<BaseOperationResult<ProfileModel>> GetUserData()
        {
            var result = await this.profileRepository.Get();
            return result;
        }

        public async Task<BaseOperationResult> SaveProfileModel(ProfileModel model)
        {
            var result = await this.profileRepository.Put(model);
            return result;
        }
    }
}