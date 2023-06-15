using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Historial
    {
        public static ML.Result Add(ML.Historial historial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CescalonaSicossContext contex = new DL.CescalonaSicossContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"HistorialAdd '{historial.Digito}', '{historial.SuperDigito}','{historial.Fecha}', {historial.Usuario.IdUsuario} ");

                    if (RowsAfected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al ingresar al historial";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static ML.Result Delete(ML.Historial idHistorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CescalonaSicossContext contex = new DL.CescalonaSicossContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"HitorialDelete '{idHistorial.IdHistorial}'");

                    if (RowsAfected > 0)
                    {
                        result.Correct = true; ;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al Elimar el cine";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
        public static int CalcularSuperDigito(int Digito)
        {
            if (Digito < 10)//Checamos si el numero es menor a 10
            {
                return Digito;
            }

            int suma = 0;
            while (Digito > 0)//Sumar cada uno de los digitos
            {
                suma += Digito % 10;//Obtenemos el residuo y lo sumamos
                Digito /= 10;//obtenemos la reduccion del numero
            }
           
            return CalcularSuperDigito(suma);
        }
     
    }
}
