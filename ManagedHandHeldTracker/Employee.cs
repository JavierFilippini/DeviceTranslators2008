﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ManagedHandHeldTracker
{
    public class Employee
    {
        public const int NODEFINIDO = -1;               // Id de usuario no definido

        public int Id;
        public string Nombre;
        public string Apellido;
        public bool Sexo;       // TRUE: Masculino, FALSE: Femenino
        public string EMail;
        public string Direccion;
        public string Nacionalidad;
        public string Ciudad;
        public string Departamento;
        public DateTime FechaNacimiento;
        public string Telefono;
        public string Celular;
        public string TipoDocumento;
        public string NumeroDocumento;
        public DateTime FechaExpedicionDocumento;
        public DateTime FechaVencimientoDocumento;
        public DateTime FechaVencimientoCarnetSalud;
        public DateTime FechaIngresoBPS;
        public string Empresa;
        public int idImagenEmpleado;
        public int imageVersion;
        public DateTime ultimaActualizacion;

        public int VersionEmpleado;
        public int PersonID;                // El empID de LENEL

        public byte[] imageDataBytes;      // byte array correspondiente a la imagen del empleado

        public Employee()
        {
            imageDataBytes = null;
            FechaNacimiento = new DateTime(2012, 1, 1);
            FechaVencimientoCarnetSalud = new DateTime(2012, 1, 1);
            FechaExpedicionDocumento = new DateTime(2012, 1, 1);
            FechaVencimientoDocumento = new DateTime(2012, 1, 1);
            //VencimientoLibretaConducir = new DateTime(2012, 1, 1);
            FechaIngresoBPS = new DateTime(2012, 1, 1);
            //VigenciaBSE = new DateTime(2012, 1, 1);
        }

        // Constructor de copia
        public Employee(Employee original)
        {
            Id = original.Id;
            Nombre = original.Nombre;

            Apellido = original.Apellido;
            Sexo = original.Sexo;
            EMail = original.EMail;
            Direccion = original.Direccion;
            Nacionalidad = original.Nacionalidad;
            Ciudad = original.Ciudad;
            Departamento = original.Departamento;
            FechaNacimiento = original.FechaNacimiento;
            Telefono = original.Telefono;
            Celular = original.Celular;
            TipoDocumento = original.TipoDocumento;
            NumeroDocumento = original.NumeroDocumento;
            FechaExpedicionDocumento = original.FechaExpedicionDocumento;
            FechaVencimientoDocumento = original.FechaVencimientoDocumento;
            FechaVencimientoCarnetSalud = original.FechaVencimientoCarnetSalud;
            FechaIngresoBPS = original.FechaIngresoBPS;
            Empresa = original.Empresa;
            idImagenEmpleado = original.idImagenEmpleado;
            imageVersion = original.imageVersion;
            ultimaActualizacion = original.ultimaActualizacion;
            VersionEmpleado = original.VersionEmpleado;
            PersonID = original.PersonID;                              // El empID de LENEL

            if (original.imageDataBytes != null)
            {
                imageDataBytes = new byte[original.imageDataBytes.Length];
                Array.Copy(original.imageDataBytes, imageDataBytes, original.imageDataBytes.Length); // byte array correspondiente a la imagen del empleado
            }
            else
                imageDataBytes = null;
        }


        public void attachImage(byte[] v_imagen)
        {
            imageDataBytes = v_imagen;
        }

        public bool hasImage()
        {
            return imageDataBytes != null;
        }
        public byte[] getImage()
        {
            return imageDataBytes;
        }





    }
}
