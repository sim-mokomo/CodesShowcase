using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Google.Protobuf;

namespace MokomoGames.Network
{
    public class ApiRequestRunner
    {
        private PlayFabApiCommunicator _communicator;

        public enum ApiName
        {
            HelloWorld,
            AddProduct,
            LoadInventory,
            DeleteTemplates,
            LoadTemplates,
            RegisterTemplates,
            Max
        }

        private readonly Dictionary<ApiName, string> _apiTable = new Dictionary<ApiName, string>
        {
            { ApiName.HelloWorld, "HelloWorld" },
            { ApiName.AddProduct, "AddProduct" },
            { ApiName.LoadInventory , "LoadInventory" },
            { ApiName.DeleteTemplates , "DeleteTemplates" },
            { ApiName.LoadTemplates , "LoadTemplates" },
            { ApiName.RegisterTemplates , "RegisterTemplates" },
        };

        public ApiRequestRunner()
        {
            _communicator = new PlayFabApiCommunicator();
        }

        public async UniTask<TResponse> Request<TRequest, TResponse>
            (ApiName apiName, TRequest request) 
            where TRequest : IMessage<TRequest> 
            where TResponse : IMessage<TResponse>, new()
        {
            if (_apiTable.TryGetValue(apiName, out var apiNameStr))
            {
                return await _communicator.Communicate<TRequest, TResponse>(apiNameStr, request);
            }
            return new TResponse();
        }
    }
}