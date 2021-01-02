using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen Adınızı Giriniz.")]
        [Display(Name = "İsim")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen Soyadınızı Giriniz.")]
        [Display(Name = "Soyisim")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Email adresi gereklidir.")]
        [Display(Name = "Email Adresiniz.")]
        [EmailAddress(ErrorMessage = "Emailiniz yanlış formatta.")]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre giriniz.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string ProfileImage { get; set; }
        public string BgImage { get; set; }
        public bool button { get; set; }
        public string UserName { get; set; }
    }
}
