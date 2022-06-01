﻿using System.ComponentModel.DataAnnotations;

namespace CarStore.DTO.Customer.Request
{
    public class NewCustomer
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UserId { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Max string length is 255")]
        [MinLength(1, ErrorMessage = "Min string length is 1")]
        public string AddressName { get; set; }


        public int ZipCode { get; set; }


        public string CityName { get; set; }
    }
}
