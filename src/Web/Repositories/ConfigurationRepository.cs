namespace Web.Repositories;

public class ConfigurationRepository : IConfigurationRepository
{
    private IConfiguration Configuration { get; }

    public ConfigurationRepository(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public string Get(string key)
    {
        var value = Configuration.GetValue<string>(key);

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new NullReferenceException();
        }

        return value;
    }
}