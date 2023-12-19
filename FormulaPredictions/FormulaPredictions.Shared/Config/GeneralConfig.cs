namespace FormulaPredictions.Shared.Config;

public class GeneralConfig
{
    public const string SectionName = "General";

    public int Year { get; set; }
    public string DataRoot { get; set; } = string.Empty;

    public string DataBasePath => string.Format(DataRoot, Year);
}
