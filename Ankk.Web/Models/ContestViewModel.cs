namespace Ankk.Web.Models
{
    using Ankk.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;

    public class ContestViewModel
    {
        public static Expression<Func<Contest, ContestViewModel>> ViewModel
        {
            get
            {
                return contest => new ContestViewModel
                {
                    Id = contest.Id,
                    Name = contest.Name,
                    IsVisible = contest.IsVisible
                };
            }
        }

       
        [Display(Name = "№")]
        [DefaultValue(null)]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        
        [Display(Name = "Име")]        
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Позволената дължина е между 6 и 100 символа")]
        [UIHint("SingleLineText")]
        public string Name { get; set; }

        [Required]
        public bool IsVisible { get; set; }
    }
}