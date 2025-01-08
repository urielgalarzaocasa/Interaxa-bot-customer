using ProyectoGenerico.Entities.ViewModel;
using System;
using System.Data.SqlClient;
using System.Security.Claims;

namespace ProyectoGenerico.Entities
{
    public class CabeceraPostal
    {
        public string clave { get; set;  }
        public string NroSeguimiento { get; set;  }
        public string Remitente { get; set;  }
        public string Destinatario { get; set;  }
        public string Domicilio_Destino { get; set;  }
        public string Domicilio_GPS_Destino { get; set;  }
        public string Domicilio_Gps_Origen { get; set;  }
        public string Patente { get; set;  }
        public string Estado { get; set;  }
        public string Solicitante { get; set;  }
        public string Desc_Corta { get; set;  }
        public string TipoTracking { get; set;  }
        public string TipoServicio { get; set;  }
        public string Recibe { get; set;  }

    }
}