using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProyectoGenerico.Services
{
    public class MotivoService
    {

        public static Dictionary<string,string> Servicio()
        {
            Dictionary<string, string> motivos = new Dictionary<string, string>();
            
            motivos.Add("02", "Titular Desconocido");
            motivos.Add("03", "Se Mudó");
            motivos.Add("04", "No se ubica Domicilio");
            motivos.Add("05", "No responde al Llamado");
            motivos.Add("08", "Fallecido");
            motivos.Add("09", "Domicilio Incompleto");
            motivos.Add("10", "Rechazado");
            motivos.Add("11", "De Viaje");
            motivos.Add("13", "Persona No Habilitada");
            motivos.Add("14", "Siniestrado");
            motivos.Add("15", "Reencaminada");
            motivos.Add("16", "Solicitada por Cliente");
            motivos.Add("17", "Domicilio Inexistente");
            motivos.Add("18", "Zona Peligrosa");
            motivos.Add("19", "No tiene Pago");
            motivos.Add("20", "Requisitos Incompletos");
            motivos.Add("21", "No responde Llamado Reiteradas Visitas");
            motivos.Add("22", "Difiere Entrega");
            motivos.Add("23", "Intransitab o Probl Clima");
            motivos.Add("24", "Entrega Parcial");
            motivos.Add("25", "Envío No Solicitado");
            motivos.Add("26", "Demorado x Corte Ruta");
            motivos.Add("27", "Rechaza por No Dev Previa");
            motivos.Add("Y1", "No Retirado");
            motivos.Add("Y2", "Retraso fza mayor");
            motivos.Add("Y3", "Retiro Cancelado");
            motivos.Add("Y4", "Retiro Fallido");
            motivos.Add("Y5", "Siniestrado–Por Rotura");
            motivos.Add("Y6", "Siniestrado–Por Robo");
            motivos.Add("Y7", "Siniestrado–Por Extravio");
            motivos.Add("Z1", "Retirado");
            motivos.Add("Z2", "Retiro No Efectuado por Ocasa");
            motivos.Add("Z3", "Pedido Anulado");
            motivos.Add("Z4", "Entregado");
            motivos.Add("Z5", "Facturado en abono");
            motivos.Add("Z7", "Derivado a Carga General");
            motivos.Add("Z8", "No Entrega de producto");
            motivos.Add("Z9", "No Entrega");
            motivos.Add("CCO", "Confiscado por aduana");
            motivos.Add("CTO", "Discrepancias aduaneras");
            motivos.Add("HCO", "Retenido a solicitud del cliente");
            motivos.Add("HDA", "En espera aprobación de destino");
            motivos.Add("HDD", "Retenido por discrepancia en documento");
            motivos.Add("HIC", "Retenido en aduana");
            motivos.Add("HPC", "Falta poder del dest..para gestión aduane");
            motivos.Add("HPG", "En espera por reembalaje");
            motivos.Add("HSP", "En gestión de permisos especiales");
            motivos.Add("MCO", "Pierde horario de corte de embarque");
            motivos.Add("MDO", "Falta documentación");
            motivos.Add("SDA", "Envío declarado en abandono por aduana");
            motivos.Add("SHW", "Retenido en guarda transitoria");

            return motivos;
        }
    }
}
