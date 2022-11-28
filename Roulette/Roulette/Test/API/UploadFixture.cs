using System;
using System.Collections.Generic;

namespace Roulette.Test.API
{
    [Serializable]
    public class UploadFixtureRequest
    {
        public List<string> keys = new();
        public List<string> values = new();
    }

    [Serializable]
    public class UploadFixtureResponse
    {
    }
}