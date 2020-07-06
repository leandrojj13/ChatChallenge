﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Services.Models
{
    public class StookRequestReponse
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public int Volume { get; set; }
    }
}
