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
            this.profileRepository = new ProfileRepository(new HttpService(new CustomDependencyService(), new HttpClientHandler()));
        }

        public async Task<BaseOperationResult<ProfileModel>> GetUserData()
        {
            int someId = 0;
            var result = await this.profileRepository.Get();
            await this.Save(result.Result, result.Status);
            return result;
        }

        public async Task<BaseOperationResult> SaveProfileModel(ProfileModel model)
        {
            var result = await this.profileRepository.Put(model);
            await this.Save(model, result.Status);
            return result;
        }

        private async Task Save(ProfileModel model, StatusCode status)
        {
            if (status == StatusCode.Ok)
            {
                var json = JsonConvert.SerializeObject(model);
                await DependencyService.Get<IFileWorker>().SaveTextAsync(FileResources.ProfileData, json);
            }
        }

        public async Task<ProfileModel> GetProfileFromFile()
        {
            var jsonModel = await DependencyService.Get<IFileWorker>().LoadTextAsync(FileResources.ProfileData);
            if (jsonModel != null)
            {
                return JsonConvert.DeserializeObject<ProfileModel>(jsonModel);
            }
            else
            {
                return null;
            }
        }
    }
}