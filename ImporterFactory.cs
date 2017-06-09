public interface IImporterFactory
{
	IImporter Create(string name);
}

public class ImporterFactory : Dictionary<string, Func<IImporter>>, IImporterFactory
{
	public IImporter Create(string name)
	{
		Func<IImporter> importer;
		if (!TryGetValue(name, out importer))
		{
			throw new ImportException(ValidationMessages.FileStructureValidation);
		}

		return importer();
	}
}

//Container
container.RegisterSingleton<IImporterFactory>(new ImporterFactory
{
	{ CountriesWorksheet.Name, container.GetInstance<CountryImporter> },
	{ CurrenciesWorksheet.Name, container.GetInstance<CurrencyImporter> }
});