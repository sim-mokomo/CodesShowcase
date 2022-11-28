namespace MokomoGames.Editor.DefineSymbol
{
    public class DefineSymbolService
    {
        public DefineSymbolSettingList LoadDefineSymbol()
        {
            var defineSymbolSettingList = new DefineSymbolSettingList();
            foreach (var platform in DefineSymbolConfig.SupportPlatformList)
            {
                UnityDefineSymbolRepository.LoadSymbols(platform).ForEach(symbolName =>
                {
                    defineSymbolSettingList.AddSymbol(platform, symbolName);
                });
            }

            return defineSymbolSettingList;
        }
    }
}