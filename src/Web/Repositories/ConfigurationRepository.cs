namespace DailyParser.Web.Repositories;

public class ConfigurationRepository : IConfigurationRepository
{
    private IConfiguration Configuration { get; }

    public ConfigurationRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string GetSetting(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException("Must provide a key");
        }

        var settingToReturn = Environment.GetEnvironmentVariable(key);
            
        if (string.IsNullOrWhiteSpace(settingToReturn))
        {
            settingToReturn = Configuration.GetValue<string>(key.Replace("_", ":"));
        }

        if (string.IsNullOrWhiteSpace(settingToReturn))
        {
            throw new ArgumentException("Key not found in setting/environmental variable");
        }
        
        return settingToReturn;            
    }
}
