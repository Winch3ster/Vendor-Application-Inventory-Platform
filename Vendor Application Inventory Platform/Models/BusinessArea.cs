﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vendor_Application_Inventory_Platform.Models
{
    public class BusinessArea
    {
        public int BusinessAreaID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$",
         ErrorMessage = "Characters are not allowed.")]
        [DisplayName("Buiness area")]
        public string Description { get; set; }

        //Navigations Properties
        public List<Software_Area> Software_Areas { get; set; }


    }
}
