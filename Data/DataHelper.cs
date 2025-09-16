using Microsoft.Data.SqlClient;
using System.Data;

namespace EFWebAPI.Data
{
    public class DataHelper
    {
        public static DataHelper _instance;
        public SqlConnection _cnn;

        private DataHelper()
        {
            _cnn = new SqlConnection(Properties.Resources.CadenaConexion);
        }

        public static DataHelper GetInstance()
        {
            if(_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }

        public SqlConnection GetConnection()
        {
            return _cnn;
        }

        public DataTable ConsultaSQL(string sp, List<Parameters> values)
        {
            DataTable dt = new DataTable();

            _cnn.Open();
            SqlCommand cmd = new SqlCommand(sp, _cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            if(values != null)
            {
                foreach(Parameters p in values)
                {
                    cmd.Parameters.AddWithValue(p.Clave, p.Valor);
                }
            }
            dt.Load(cmd.ExecuteReader());
            _cnn.Close();
            return dt;
        }

        public int ConsultaEscalarSQL(string sp, string output)
        {
            _cnn.Open();
            SqlCommand cmd = new SqlCommand(sp, _cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter pOut = new SqlParameter();         // Crea un parámetro de salida SQL
            pOut.ParameterName = output;                    // Se asigna el nombre del parámetro de salida
            pOut.DbType = DbType.Int32;                     // Define el tipo de dato del parámetro de salida
            pOut.Direction = ParameterDirection.Output;     // Indica que es un parámetro de salida
            cmd.Parameters.Add(pOut);                       // Agrega el parámetro al comando SQL
            cmd.ExecuteNonQuery();                          // Ejecuta el comando SQL sin devolver filas
            _cnn.Close();

            return (int)pOut.Value;                         // Devuelve el valor del parámetro de salida
        }

        public int EjecutarSQL(string sp, List<Parameters> values)
        {
            int filasAfectadas = 0;
            SqlTransaction t = null;    // Objeto de transacción, inicialmente nulo

            try
            {
                SqlCommand cmd = new SqlCommand();
                _cnn.Open();
                t = _cnn.BeginTransaction();                        // Inicia una transacción
                cmd.Connection = _cnn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                cmd.Transaction = t;                                // Asocia el comando con la transacción

                if(values != null)                                  // Si hay parámetros de entrada...
                {
                    foreach(Parameters p in values)                 // ...recorre cada parámetro
                    {
                        cmd.Parameters.AddWithValue(p.Clave, p.Valor);  // Agrega el parámetro y su valor al comando
                    }
                }

                filasAfectadas = cmd.ExecuteNonQuery();             // Ejecuta el comando
                t.Commit();                                         // Confirma la transacción
            }
            catch (Exception)
            {
                if(t != null)       // Si la transacción se inició...
                {
                    t.Rollback();   // ...revierte la transacción en caso de error
                }
            }
            finally
            {
                if(_cnn != null && _cnn.State == ConnectionState.Open)  // Si la conexión existe y está abierta...
                {
                    _cnn.Close();                                       // ...la cierra
                }
            }
            return filasAfectadas;  // Devuelve el nro de filas afectadas
        }

        public SqlConnection ObtenerConexion()
        {
            return this._cnn;
        }
    }
}
