﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace WServices
{
    /// <summary>
    /// Descripción breve de ServicioEstudiantil
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]

    
    public class ServicioEstudiantil : System.Web.Services.WebService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;

        [WebMethod]
        public XmlDocument CreateUsuario(string Nombre, string Apellido, string telefono)
        {
            using (SqlConnection connection= new SqlConnection(connectionString))
            {
                string query = "INSERT INTO estudiante (nombre,apellido, telefono) VALUES (@nombre, @apellido,@telefono)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", Nombre);
                command.Parameters.AddWithValue("@apellido", Apellido);
                command.Parameters.AddWithValue("@telefono", telefono);

                connection.Open();
                command.ExecuteNonQuery();
            }

            XmlDocument xmlResponse = new XmlDocument();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Datos registrado correctamente";
            rootElement.AppendChild(responseElement);

            return xmlResponse;

    
        }
        [WebMethod]
        public DataSet GetUsuarios()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM estudiante";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();

                connection.Open();
                adapter.Fill(dataSet, "estudiante");

                return dataSet;
            }
        }

        [WebMethod]
        public XmlDocument UpdateUsuario(int id, string nuevoNombre, string nuevoApellido, int nuevoTelefono)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE estudiante SET nombre = @nuevoNombre, apellido = @nuevoApellido, telefono = @nuevoTelefono WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nuevoNombre", nuevoNombre);
                command.Parameters.AddWithValue("@nuevoApellido", nuevoApellido);
                command.Parameters.AddWithValue("@nuevoTelefono", nuevoTelefono);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
            XmlDocument xmlResponse = new XmlDocument();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Se ha realizado una modificacion exitosa.";
            rootElement.AppendChild(responseElement);

            return xmlResponse;
        }

        [WebMethod]
        public XmlDocument DeleteUsuario(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM estudiante WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
             
            }

            XmlDocument xmlResponse = new XmlDocument();

            XmlElement rootElement = xmlResponse.CreateElement("Response");
            xmlResponse.AppendChild(rootElement);

            XmlElement responseElement = xmlResponse.CreateElement("Message");
            responseElement.InnerText = "Registro Eliminado correctamente";
            rootElement.AppendChild(responseElement);

            return xmlResponse;
        }
        
    }
    
}
