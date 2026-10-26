using System.ComponentModel.DataAnnotations;

namespace LAB2.Models
{
    public class Student
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Họ tên bắt buộc phải nhập")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải từ 4 đến 100 ký tự")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@gmail\.com$", ErrorMessage = "Email phải có định dạng hợp lệ và có đuôi gmail.com")]
        public string? Email { get; set; } 

        [Required(ErrorMessage = "Mật khẩu bắt buộc phải được nhập")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Mật khẩu từ 8 ký tự trở lên, có ký tự viết hoa, viết thường, chữ số và ký tự đặc biệt")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngành học")]
        public Branch? Branch { get; set; } 

        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public Gender? Gender { get; set; } 

        public bool IsRegular { get; set; } 

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Địa chỉ bắt buộc phải nhập")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Ngày sinh bắt buộc phải nhập")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày sinh")]
        public DateTime DateOfBorth { get; set; } 

        [Required(ErrorMessage = "Điểm số bắt buộc phải nhập")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm số phải là kiểu số thực và nằm trong khoảng từ 0.0 đến 10.0")]
        public double? Score { get; set; } 

        public string? AvatarUrl { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile? AvatarFile { get; set; }
    }
}