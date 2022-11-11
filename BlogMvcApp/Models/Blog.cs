﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMvcApp.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public string Photo { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Approval { get; set; }
        public bool AddedHomePage { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}