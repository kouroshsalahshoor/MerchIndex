﻿using System.ComponentModel.DataAnnotations;

namespace MerchIndex.Auto.Client.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Fyll i {0}")]
        public string Name { get; set; } = default!;
    }
}
