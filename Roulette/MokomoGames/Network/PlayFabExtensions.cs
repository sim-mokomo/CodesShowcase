using System;
using Cysharp.Threading.Tasks;
using PlayFab;
using PlayFab.ClientModels;

namespace MokomoGames.Network
{
    public class PlayFabExtensions
    {
        public class PlayFabErrorException : Exception
        {
            public PlayFabError PlayFabError;
        }
        
        public static UniTask<GetUserDataResult> GetUserDataAsync(GetUserDataRequest request)
        {
            var source = new UniTaskCompletionSource<GetUserDataResult>();

            PlayFabClientAPI.GetUserData(
                request,
                result =>
                {
                    source.TrySetResult(result);
                },
                error => throw new PlayFabErrorException
                {
                    PlayFabError = error
                }
            );
            return source.Task;
        }

        public static UniTask<UpdateUserDataResult> UpdateUserDataAsync(UpdateUserDataRequest request)
        {
            var source = new UniTaskCompletionSource<UpdateUserDataResult>();
            PlayFabClientAPI.UpdateUserData(
                request,
                result =>
                {
                    source.TrySetResult(result);
                },
                error => throw new PlayFabErrorException
                {
                    PlayFabError = error
                }
            );
            return source.Task;
        }
    }
}