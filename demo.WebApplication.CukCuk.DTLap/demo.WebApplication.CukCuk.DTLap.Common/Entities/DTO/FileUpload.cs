﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.Common.Entities.DTO
{
    public class FileUpload
    {
        public IFormFile? ImageFile { get; set; }
    }
}
