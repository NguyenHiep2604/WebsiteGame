//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebGame.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class List_Game
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string LinkIOS { get; set; }
        public string LinkAndroid { get; set; }
        public string Caption { get; set; }
        public bool Featured_Games { get; set; }
    }
}