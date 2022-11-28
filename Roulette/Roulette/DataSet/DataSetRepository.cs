using Cysharp.Threading.Tasks;
using MokomoGames.Network;
using Protobuf;
using UnityEngine;

namespace Roulette.DataSet
{
    public class DataSetRepository
    {
        private readonly ApiRequestProcess _apiRequestProcess;
        public DataSetRepository()
        {
            _apiRequestProcess = Object.FindObjectOfType<ApiRequestProcess>();
        }

        public UniTask<RegisterTemplateListResponse> RegisterListAsync(RegisterTemplateListRequest request)
        {
            return _apiRequestProcess.Request<RegisterTemplateListRequest, RegisterTemplateListResponse>(
                    ApiRequestRunner.ApiName.RegisterTemplates, request);
        }

        public UniTask<DeleteTempalteListResponse> DeleteTemplateListAsync(DeleteTempalteListRequest request)
        {
            return _apiRequestProcess.Request<DeleteTempalteListRequest, DeleteTempalteListResponse>(
                    ApiRequestRunner.ApiName.DeleteTemplates, request);
        }

        public UniTask<LoadTemplateListResponse> LoadAsync(LoadTemplateListRequest request)
        {
            return _apiRequestProcess.Request<LoadTemplateListRequest, LoadTemplateListResponse>(
                    ApiRequestRunner.ApiName.LoadTemplates, request);
        }
    }
}