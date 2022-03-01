namespace VacationPortal.Web.Areas.Identity.Models
{
    public record LoginCredential
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
