﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class CartMaster
    {
        [Key]
        public int CardMasterId { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<CartDetail> CartDetails { get; set; }
    }
}
