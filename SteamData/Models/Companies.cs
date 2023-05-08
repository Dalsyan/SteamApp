﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace SteamData.Models
{
    public partial class Companies
    {
        public Companies()
        {
            Devs = new HashSet<Devs>();
            Games = new HashSet<Games>();
            Servers = new HashSet<Servers>();
            CountriesCountry = new HashSet<Countries>();
        }

        public int CompanyId { get; set; }
        public string CompanyName { get; set; }

        public virtual ICollection<Devs> Devs { get; set; }
        public virtual ICollection<Games> Games { get; set; }
        public virtual ICollection<Servers> Servers { get; set; }

        public virtual ICollection<Countries> CountriesCountry { get; set; }
    }
}