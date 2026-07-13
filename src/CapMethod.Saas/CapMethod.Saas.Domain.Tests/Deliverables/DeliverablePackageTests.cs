using CapMethod.Saas.Domain.Deliverables;

namespace CapMethod.Saas.Domain.Tests.Deliverables;

public sealed class DeliverablePackageTests
{
    [Fact]
    public void Draft_package_should_not_be_deliverable()
    {
        DeliverablePackage package = DeliverablePackage.CreateDraft(
            Guid.NewGuid(),
            Guid.NewGuid(),
            containsAiDraft: false);

        Assert.False(package.CanBeDeliveredToBeneficiary);
    }

    [Fact]
    public void Validated_package_without_ai_draft_should_be_deliverable()
    {
        DeliverablePackage package = DeliverablePackage.CreateDraft(
            Guid.NewGuid(),
            Guid.NewGuid(),
            containsAiDraft: false);

        package.ValidateForDelivery();

        Assert.True(package.CanBeDeliveredToBeneficiary);
    }

    [Fact]
    public void Package_containing_ai_draft_should_not_be_validated_for_delivery()
    {
        DeliverablePackage package = DeliverablePackage.CreateDraft(
            Guid.NewGuid(),
            Guid.NewGuid(),
            containsAiDraft: true);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(package.ValidateForDelivery);

        Assert.Equal("AI drafts cannot be delivered to beneficiaries.", exception.Message);
        Assert.False(package.CanBeDeliveredToBeneficiary);
    }
}
