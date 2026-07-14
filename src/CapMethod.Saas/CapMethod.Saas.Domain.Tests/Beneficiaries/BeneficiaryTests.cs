using CapMethod.Saas.Domain.Beneficiaries;

namespace CapMethod.Saas.Domain.Tests.Beneficiaries;

public sealed class BeneficiaryTests
{
    [Fact]
    public void Create_should_initialize_required_fields()
    {
        Guid tenantId = Guid.NewGuid();

        Beneficiary beneficiary = Beneficiary.Create(tenantId, " Ada ", " Lovelace ", " ada@example.test ");

        Assert.NotEqual(Guid.Empty, beneficiary.Id);
        Assert.Equal(tenantId, beneficiary.TenantId);
        Assert.Equal("Ada", beneficiary.FirstName);
        Assert.Equal("Lovelace", beneficiary.LastName);
        Assert.Equal("ada@example.test", beneficiary.Email);
    }

    [Fact]
    public void Create_should_normalize_empty_email_to_null()
    {
        Beneficiary beneficiary = Beneficiary.Create(Guid.NewGuid(), "Ada", "Lovelace", " ");

        Assert.Null(beneficiary.Email);
    }

    [Fact]
    public void Create_should_reject_empty_tenant_id()
    {
        Assert.Throws<ArgumentException>(() => Beneficiary.Create(Guid.Empty, "Ada", "Lovelace", null));
    }

    [Fact]
    public void Create_should_reject_empty_first_name()
    {
        Assert.Throws<ArgumentException>(() => Beneficiary.Create(Guid.NewGuid(), " ", "Lovelace", null));
    }

    [Fact]
    public void Create_should_reject_empty_last_name()
    {
        Assert.Throws<ArgumentException>(() => Beneficiary.Create(Guid.NewGuid(), "Ada", " ", null));
    }
}
