using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.WebUI.Models
{
    public class AdminProductEditModel
    {
        [Display(Name = "Mã sách")]
        [Required(ErrorMessage ="Vui lòng nhập mã sách")]
        public int ProductId { get; set; }
        [Display(Name = "Tên sách")]
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string ProductName { get; set; }
        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "Vui lòng nhập đơn giá")]
        [Range(0.1, 100000, ErrorMessage = "Đơn giá trong khoảng từ 0.1 đến 100000")]
        public decimal Price { get; set; }
        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Vui lòng nhập số lượng")]
        [Range(1, 10000, ErrorMessage = "Số lượng từ 1 đến 10000")]
        public int Amount { get; set; }
        [Display(Name = "Doanh mục")]
        [Required(ErrorMessage = "Vui lòng nhập danh mục")]
        public int CategoryId { get; set; }
        [Display(Name = "Hình ảnh sách")]
        public string Avatar { get; set; }
        [Display(Name = "Mô tả")]
        [AllowHtml]
        public string Description { get; set; }
        
    }
}