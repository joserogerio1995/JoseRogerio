using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoseRogerio
{
    public class Database
    {
        private static SQLiteConnection sqliteConnection;
        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=c:\\dados\\ACME.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public static void Criar()
        {
            if (!File.Exists(@"c:\dados\ACME.sqlite")) { 
                try
            {
                SQLiteConnection.CreateFile(@"c:\dados\ACME.sqlite");
            }
            catch
            {
                throw;
            }
            }
        }

        public static void CriarTabela()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS TB_VOO (id_voo int, data_voo DateTime, custo numeric(10,2), distancia int, captura varchar(1), nivel_dor int)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static DataTable GetVoos()
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TB_VOO";
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetVoosId(string id)
        {
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TB_VOO Where Id=" + id;
                    da = new SQLiteDataAdapter(cmd.CommandText, DbConnection());
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static void Adicionar(Voo voos)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO TB_VOO(Id_voo, data_voo, custo, distancia, captura, nivel_dor) values (@Id_voo," +
                        " @data_voo, @custo, @distancia, @captura, @nivel_dor)";
                    cmd.Parameters.AddWithValue("@id_voo", voos.IdVoo);
                    cmd.Parameters.AddWithValue("@data_voo", voos.DataVoo);
                    cmd.Parameters.AddWithValue("@custo", voos.Custo);
                    cmd.Parameters.AddWithValue("@distancia", voos.Distancia);
                    cmd.Parameters.AddWithValue("@captura", voos.Captura);
                    cmd.Parameters.AddWithValue("@nivel_dor", voos.NivelDor);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update(Voo voos)
        {

            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    if (voos.IdVoo != null)
                    {
                        cmd.CommandText = "UPDATE TB_VOO SET data_voo=@data_voo, custo=@custo,distancia=@distancia, captura=@captura,nivel_dor=@nivel_dor WHERE (Id_voo=@Id_voo)";
                        cmd.Parameters.AddWithValue("@Id_voo", voos.IdVoo);
                        cmd.Parameters.AddWithValue("@data_voo", voos.DataVoo);
                        cmd.Parameters.AddWithValue("@custo", voos.Custo);
                        cmd.Parameters.AddWithValue("@distancia", voos.Distancia);
                        cmd.Parameters.AddWithValue("@captura", voos.Captura);
                        cmd.Parameters.AddWithValue("@nivel_dor", voos.NivelDor);
                        cmd.ExecuteNonQuery();
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Delete(int Id)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM TB_VOO Where id_voo = "+Id;
                    cmd.Parameters.AddWithValue("@id_voo", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
