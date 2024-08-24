namespace FIAP.TechChalenge.InvestNetHub.Api.ApiModels.Portfolio;

public class CreatePortfolioApiInput
{
    public CreatePortfolioApiInput(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
}
