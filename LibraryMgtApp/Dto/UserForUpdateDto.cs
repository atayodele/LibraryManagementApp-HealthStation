﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryMgtApp.Dto
{
    public class UserForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string NIN { get; set; }
    }
}
