using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.WebUI.Models
{
    public class CartCheckoutViewModel
    {
        [Display(Name ="Họ và tên")]
        [Required(ErrorMessage ="Họ tên không được để trống")]
        public string FullName { get; set; }
        [Display(Name ="Địa chỉ nhận hàng")]
        [Required(ErrorMessage ="Vui lòng nhập địa chỉ nhận hàng")]
        [StringLength(1000,ErrorMessage ="Địa chỉ không được vượt quá 1000 kí tự")]
        public string Address { get; set; }
        [EmailAddress(ErrorMessage ="Email không hợp lệ")]
        [Required(ErrorMessage ="Vui lòng nhập Email")]
        public string Email { get; set; }
        [Display(Name ="Số điện thoại")]
        [StringLength(20,MinimumLength =9, ErrorMessage ="Số điện thoại không hợp lệ")]
        [Required(ErrorMessage ="Vui lòng nhập số điện thoại")]
        public string Phone { get; set; }
        [Display(Name ="Phương thức thanh toán")]
        [Required]
        public int PaymentMethod { get; set; }
        [Display(Name ="Ghi chú")]
        [StringLength(maximumLength:20000, ErrorMessage ="vui lòng nhập ít hơn 20000")]
        public string Note { get; set; }
        [Display(Name ="Ship hàng tận nhà?")]
        public bool UserShipper { get; set; }
    }
}