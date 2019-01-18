﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Video_Rental_Shop.Models
{
    public class MovieGenre
    {
        public byte Id { get; set; }
        public string Name { get; set; }
        public IList<Movie> Movies { get; set; }
    }
}