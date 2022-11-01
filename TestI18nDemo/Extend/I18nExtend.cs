using BlazorComponent.I18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestI18nDemo.Extend
{
    public static class I18nExtend
    {
        public static async Task<IBlazorComponentBuilder> AddI18nForMauiBlazor(this IBlazorComponentBuilder builder, string localesDirectoryApi)
        {
            string supportedCulturesApi = Path.Combine(localesDirectoryApi, "supportedCultures.json");
            bool existsCultures = await FileSystem.AppPackageFileExistsAsync(supportedCulturesApi);
            if (!existsCultures)
            {
                throw new Exception("Can't find path：" + supportedCulturesApi);
            }

            using Stream streamCultures = await FileSystem.OpenAppPackageFileAsync(supportedCulturesApi);
            using StreamReader readerCultures = new StreamReader(streamCultures);
            string contents = readerCultures.ReadToEnd();
            string[] cultures = JsonSerializer.Deserialize<string[]>(contents) ?? throw new Exception("Failed to read supportedCultures json file data!"); ;
            List<(string culture, Dictionary<string, string>)> locales = new List<(string, Dictionary<string, string>)>();
            string[] array = cultures;
            foreach (string culture in array)
            {
                string cultureApi = Path.Combine(localesDirectoryApi, culture + ".json");
                bool existsCulture = await FileSystem.AppPackageFileExistsAsync(cultureApi);
                if (!existsCulture)
                {
                    throw new Exception("Can't find path：" + cultureApi);
                }

                await using Stream stream = await FileSystem.OpenAppPackageFileAsync(cultureApi);
                using StreamReader reader = new StreamReader(stream);
                Dictionary<string, string> map = I18nReader.Read(reader.ReadToEnd());
                locales.Add((culture, map));
            }

            (string culture, Dictionary<string, string>)[] localesArray = locales.ToArray();
            I18nServiceCollectionExtensions.AddI18n(builder, localesArray);
            return builder;
        }
    }
}
