using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace PruebaOracle
{
    public class Class1
    {
        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();


            try   
            {
                using (OracleConnection context = new OracleConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriaAdd";

                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        OracleParameter[] collection = new OracleParameter[5];

                        collection[0] = new OracleParameter("Nombre", OracleDbType.NVarchar2);
                        collection[0].Value = materia.Nombre;

                        collection[1] = new OracleParameter("Creditos", OracleDbType.Int32);
                        collection[1].Value = materia.Creditos;

                        collection[2] = new OracleParameter("Costo", OracleDbType.Decimal);
                        collection[2].Value = materia.Costo;

                        collection[3] = new OracleParameter("IdSemestre", OracleDbType.Int16);
                        collection[3].Value = 1;

                        collection[4] = new OracleParameter("Imagen", OracleDbType.Array);
                        collection[4].Value = materia.Imagen;


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        cmd.Connection.Close();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "La materia no se pudo insertar correctamente.";
                        }
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

    }
}
