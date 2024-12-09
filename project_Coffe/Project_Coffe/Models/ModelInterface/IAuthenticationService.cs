namespace Project_Coffe.Models.ModelInterface
{
    public interface IAuthenticationService
    {
        public string GenerateToken(int userId, string role);
    }
}
