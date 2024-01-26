using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
using FIAP.TechChalenge.InvestNetHub.Domain.Validation;

namespace Athenas.Pharma.Stock.Domain.Entity.PersonDomain;
public class User : AggregateRoot 
{
    public User(
        string name,
        string email,
        string phone,
        string cpf,
        DateTime dateOfBirth,
        string password, 
        string? rg = null,
        bool isActive = true
    ) : base() 
    {
        Name = name;
        Email = email;
        Phone = phone;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
        CPF = cpf;
        RG = rg;
        DateOfBirth = dateOfBirth;
        Password = password;

        Validate(); 
    }

    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CPF { get; private set; }
    public string? RG { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Password { get; private set; }

    public void Update(string name, string email, string phone, string cpf, DateTime dateOfBirth, string password, string? rg = null)
    {
        Name = name;
        Email = email;
        Phone = phone;
        CPF = cpf;
        RG = rg;
        DateOfBirth = dateOfBirth;
        Password = password; // Atualizando senha
        Validate(); // Validação após atualização
    }

    // Método de validação que inclui todas as validações de Person e User
    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
        if (Name.Length < 3)
            throw new EntityValidationException($"{nameof(Name)} should be greater than 2 characters");
        if (string.IsNullOrWhiteSpace(Email))
            throw new EntityValidationException($"{nameof(Email)} should not be empty or null");
        if (!ValidationHelper.IsValidEmail(Email))
            throw new EntityValidationException($"{nameof(Email)} is not in a valid format");
        if (string.IsNullOrWhiteSpace(Phone))
            throw new EntityValidationException($"{nameof(Phone)} should not be empty or null");
        if (!ValidationHelper.IsValidPhone(Phone))
            throw new EntityValidationException($"{nameof(Phone)} is not in a valid format");
        if (string.IsNullOrWhiteSpace(CPF))
            throw new EntityValidationException($"{nameof(CPF)} should not be empty or null");
        if (!ValidationHelper.IsCpfValid(CPF))
            throw new EntityValidationException($"{nameof(CPF)} is not valid");
        if (string.IsNullOrWhiteSpace(Password))
            throw new EntityValidationException($"{nameof(Password)} should not be empty or null");
        // Considerar adicionar validações de complexidade da senha
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }

    public void Deactivate()
    {
        IsActive = false;
        Validate();
    }
}
