//------------------------------------------------------------------------------
namespace AgendaWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class usuarios
    {
        public int ID { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string senha { get; set; }
    }
}
