using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Google.Protobuf;
using PlayFab;
using PlayFab.CloudScriptModels;
using UnityEngine;

namespace MokomoGames.Network
{
    public class PlayFabApiCommunicator
    {
        public UniTask<TResponse> Communicate<TRequest, TResponse>(string apiName, TRequest request) 
            where TRequest : IMessage<TRequest>
            where TResponse : IMessage<TResponse>, new()
        {
            Debug.Log($"{typeof(TRequest)}: \n{JsonFormatter.Default.Format(request)}");
            var source = new UniTaskCompletionSource<TResponse>();
            PlayFabCloudScriptAPI.ExecuteFunction(new ExecuteFunctionRequest()
                {
                    Entity = new EntityKey
                    {
                        Id = PlayFabSettings.staticPlayer.EntityId,
                        Type = PlayFabSettings.staticPlayer.EntityType
                    },
                    FunctionName = apiName,
                    FunctionParameter = new Dictionary<string, string>()
                    {
                        { "request", JsonFormatter.Default.Format(request) }
                    },
                    GeneratePlayStreamEvent = true
                },
                result =>
                {
                    if (result.FunctionResult == null)
                    {
                        Debug.Log("Nothing Response");
                        return;
                    }

                    var json = result.FunctionResult.ToString();
                    Debug.Log($"{typeof(TResponse)}: \n {json}");
                    var parser = new MessageParser<TResponse>(() => new TResponse());
                    var response = parser.ParseJson(json);
                    source.TrySetResult(response);
                },
                GenerateErrorReport
            );
            return source.Task;
        }
    
        private static void GenerateErrorReport(PlayFabError error)
        {
            Debug.Log($"Opps Something went wrong: {error.GenerateErrorReport()}");
            Debug.Log(error.HttpCode.ToString());
            Debug.Log(error.HttpStatus);
            if (error.ErrorDetails == null)
                return;
            foreach (var detail in error.ErrorDetails) UnityEngine.Debug.Log($"{detail.Key}/{detail.Value}");
        }   
    }
}
