﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UlalaAPI.Models
{
    //STATUT : OK
    public class ZoneModel
    {
        public int Id { get; set; }
        public string ContinentFR { get; set; }
        public string ContinentEN { get; set; }
        public string ZoneFR { get; set; }
        public string ZoneEN { get; set; }
        public int NbZones { get; set; }
        public int Actif { get; set; }
    }
}