namespace Parking.Infrastructure.CrossCutting.DTOs
{
    public class ValidationDto
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}
