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
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"HistorialAdd '{historial.Digito}', '{historial.SuperDigito}', {historial.Usuario.IdUsuario} ");

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
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"HitorialDelete {idHistorial}");

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
            //Imprimir todas las sumas
            //int resultado = suma;
            //while(resultado != 0)
            //{
            //    Console.WriteLine("La suma de los super digitos es {0} ", resultado);
            //    break;
            //}

            return CalcularSuperDigito(suma);
        }

        //public static ML.Result HistorialGetByIdUsuario(int? idUsuario)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.CescalonaSicossContext contex = new DL.CescalonaSicossContext())
        //        {
        //            var RowsAfected = contex.Historials.FromSqlRaw($"HistorialGetByIdUsuario {idUsuario}").AsEnumerable().FirstOrDefault();

        //            result.Object = new object();

        //            if (RowsAfected != null)
        //            {
        //                ML.Historial cine = new ML.Historial();
        //                cine.IdHistoial = RowsAfected.IdHistorial;
        //                cine.Numero = RowsAfected.Numero.Value;
        //                cine.Resultado = RowsAfected.Resultado.Value;
        //                cine.FechaHora = RowsAfected.FechaHora.ToString();
        //               result.Objects.Add(cine);

        //                result.Object = cine; //boxing

        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "Ocurrió un error al obtener el registros en la tabla Cine";
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //    }
        //    return result;
        //}
        //public static int CalcularSuperDigito(string Numero)
        //{
        //    int Resultado = 0;
        //    try
        //    {
        //        //AGREGAR LOS DIGITOS DEL NUMERO A UN ARREGLO
        //        String[] ArregloChar = NumeroToArrayString(Numero);

        //        //CASTEAR DE STRING A INT ARRAY
        //        int[] NumerosConvertInt = ArregloStringToInt(ArregloChar);
        //        int Suma = NumerosConvertInt.Sum();

        //        while (Suma > 9)
        //        {
        //            String ConvertirSuma = Suma.ToString();
        //            String[] NewArregloChar = NumeroToArrayString(ConvertirSuma);
        //            int[] NewNumerosConvertInt = ArregloStringToInt(NewArregloChar);
        //            Suma = NewNumerosConvertInt.Sum();

        //        }
        //        Resultado = Suma;
        //        Console.WriteLine("El super digito del numero " + Numero + " Es: " + Resultado);

        //    }
        //    catch (Exception)
        //    {

        //    }
        //    return Resultado;

        //}

        //public static string[] NumeroToArrayString(string Numero)
        //{
        //    int TamCadena = Numero.Length;
        //    string[] ArregloChar = new string[TamCadena];

        //    for (int i = 0; i <= TamCadena - 1; i++)
        //    {
        //        string Num = Numero.Substring(i, 1);
        //        ArregloChar[i] = Num;

        //    }
        //    return ArregloChar;
        //}
        //public static int[] ArregloStringToInt(string[] NumerosString)
        //{
        //    int[] numeros = new int[NumerosString.Length];
        //    numeros = NumerosString.Select(x => Convert.ToInt32(x)).ToArray();
        //    return numeros;
        //}
    }
}
