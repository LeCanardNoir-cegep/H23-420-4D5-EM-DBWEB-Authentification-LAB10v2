using System;
using System.Collections.Generic;

namespace _4204D5_labo10.Models
{
    public partial class Changelog
    {
        public int Id { get; set; }
        public byte? Type { get; set; }
        public string? Version { get; set; }
        public string Description { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Checksum { get; set; }
        public string InstalledBy { get; set; } = null!;
        public DateTime InstalledOn { get; set; }
        public bool Success { get; set; }
    }
}
