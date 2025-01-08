using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGenerico.Services
{
    public class EstadoService
    {

        public static Dictionary<string,string> Servicio()
        {
            Dictionary<string, string> estados = new Dictionary<string, string>();
            
            estados.Add("ADM", "ADMISIÓN PROVISORIA");
            estados.Add("AWA", "AGUARDANDO AUTORIZACION");
            estados.Add("CCP", "EN PROCESO DE ADUANA");
            estados.Add("CID", "CONTENEDOR EN DESTINO");
            estados.Add("CLI", "APROBADA POR CLIENTE");
            estados.Add("CLO", "CIERRE OPERATIVO");
            estados.Add("DAG", "DOCS RECIBIDOS CON APROB.GUBERNAMENTAL");
            estados.Add("DRO", "DOCS RECIBIDOS EN OCASA");
            estados.Add("DRS", "DOCS RECIBIDOS DEL SITE");
            estados.Add("DSD", "DOCS ENVIADOS A DESTINO");
            estados.Add("DSG", "DOCS ENVIADOS PARA APROB.GUBERNAMENTAL");
            estados.Add("DSS", "DOCS ENVIADOS AL SITE");
            estados.Add("END", "FINALIZADA");
            estados.Add("HDC", "PENDIENTE DE DISTRIBUCION EN CONTENEDOR");
            estados.Add("HF2", "EN SUCURSAL AG");
            estados.Add("HFD", "PENDIENTE DE DISTRIBUCION");
            estados.Add("HSI", "RENDICION INTERNA PENDIENTE DE DESPACHO");
            estados.Add("HSP", "CARTA DE RENDICION PENDIENTE DESPACHO");
            estados.Add("ISD", "RENDICION INTERNA DESPACHADA");
            estados.Add("ITW", "EN GUARDA TRANSITORIA");
            estados.Add("KSO", "KITS ENVIADOS A OCASA");
            estados.Add("MPD", "CARTA DE RENDICION EN DISTRIBUCION");
            estados.Add("MPS", "CARTA DE RENDICION AL CLIENTE GENERADA");
            estados.Add("ODP", "EN DISTRIBUCION");
            estados.Add("PO1", "POD DIGITAL");
            estados.Add("POD", "POD");
            estados.Add("PUC", "PEDIDO CLIENTE");
            estados.Add("PUP", "PEDIDO RECOLECTADO");
            estados.Add("PUR", "PEDIDO ASIGNADO(EN CURSO)");
            estados.Add("PUS", "PEDIDO REGISTRADO(PICKUP)");
            estados.Add("RAO", "PEDIDO RECIBIDO EN OCASA");
            estados.Add("RFL", "CARGA DE REFERENCIAS");
            estados.Add("SCC", "ENVIO LIBERADO");
            estados.Add("ST2", "ARRIBADO");
            estados.Add("STD", "DESPACHADO");
            estados.Add("STK", "EN STOCK");
            estados.Add("VLT", "EN BOVEDA");
            estados.Add("WFD", "PENDIENTE DE DESTRUCCION");
            estados.Add("WFN", "PEND. DE CARGA DE NOVEDADES");
            estados.Add("WFS", "PENDIENTE DE DESPACHO");
            estados.Add("WPD", "PENDIENTE DE RENDICION");
            estados.Add("WRN", "RENDICION PEND.DE CARGA DE NOVEDADES");


            return estados;
        }
    }
}
