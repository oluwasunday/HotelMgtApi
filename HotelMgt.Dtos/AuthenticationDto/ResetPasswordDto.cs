using System.ComponentModel.DataAnnotations;

namespace HotelMgt.Dtos.AuthenticationDto
{
    public class ResetPasswordDto
    {
        public string Token { get; set; }

        public string Email { get; set; }

        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "New Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; }
    }
}
