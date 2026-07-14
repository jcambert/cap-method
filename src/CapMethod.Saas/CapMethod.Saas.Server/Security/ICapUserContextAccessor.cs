namespace CapMethod.Saas.Server.Security;

public interface ICapUserContextAccessor
{
    CapUserContext GetRequiredContext();
}
