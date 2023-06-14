using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL
{
    public  class Usuario
    {
        public static ML.Result Add(ML.Usuario cine)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CescalonaSicossContext contex = new DL.CescalonaSicossContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"UsuarioAdd '{cine.UserName}', '{ cine.Password}' ");

                    if (RowsAfected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al ingresar el cine";
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
        public static ML.Result GetByUserName(string userName)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CescalonaSicossContext context = new DL.CescalonaSicossContext())
                {
                    var objUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{userName}' ").AsEnumerable().FirstOrDefault();

                    //result.Objects = new List<object>();

                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.UserName = objUsuario.UserName;
                        usuario.Password = objUsuario.Password;



                        result.Object = usuario; //boxing


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = true;
                        result.ErrorMessage = "Ocurrio un erro al obtener los regristro en la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = !false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        public static ML.Result HistorialGetByIdUsuario(int? idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CescalonaSicossContext contex = new DL.CescalonaSicossContext())
                {
                    var query = contex.Historials.FromSqlRaw($"HistorialGetByIdUsuario {idUsuario}");

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var RowsAfected in query)
                        {
                            ML.Historial historial = new ML.Historial();

                            ML.Historial cine = new ML.Historial();
                            cine.IdHistorial = RowsAfected.IdHistorial;
                            cine.Digito = RowsAfected.Numero.Value;
                            cine.SuperDigito = RowsAfected.Resultado.Value;
                            cine.Fecha = RowsAfected.FechaHora.ToString();
                            cine.Usuario = new ML.Usuario();
                            cine.Usuario.IdUsuario = RowsAfected.IdUsuario.Value;
                            result.Objects.Add(cine);

                            result.Objects.Add(historial);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
