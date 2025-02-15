﻿using LibraryMgtApp.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Dto
{
    public class UpdateBookDto
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public decimal Cost { get; set; }
        public DateTime PublichYear { get; set; }
        public Guid AuthorId { get; set; }
        public StatusModes StatusMode { get; set; }
    }
}
