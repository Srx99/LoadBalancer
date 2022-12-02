using Common;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enumeracija;

namespace DataBaseCRUD
{
    [ExcludeFromCodeCoverage]
    public class DataBaseCRUDComponent : IDataBase
    {

        //string fileName = "database.sqlite3";

        public SQLiteConnection connection { get; set; }



        public DataBaseCRUDComponent()
        {
            connection = new SQLiteConnection("Data Source=dataSet1.sqlite3");
            if (!File.Exists("dataSet1.sqlite3"))
            {
                SQLiteConnection.CreateFile("dataSet1.sqlite3");
            }

            connection = new SQLiteConnection("Data Source=dataSet2.sqlite3");
            if (!File.Exists("dataSet2.sqlite3"))
            {
                SQLiteConnection.CreateFile("dataSet2.sqlite3");
            }

            connection = new SQLiteConnection("Data Source=dataSet3.sqlite3");
            if (!File.Exists("dataSet3.sqlite3"))
            {
                SQLiteConnection.CreateFile("dataSet3.sqlite3");
            }

            connection = new SQLiteConnection("Data Source=dataSet4.sqlite3");
            if (!File.Exists("dataSet4.sqlite3"))
            {
                SQLiteConnection.CreateFile("dataSet4.sqlite3");
            }
        }



        private void OpenConnection()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            
        }



        private void CloseConnection()
        {
            if(connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }



        public List<Item> CitanjeIzBaze(int dataSet, int id)
        {
            if (dataSet < 0 || dataSet > 4)
            {
                throw new ArgumentOutOfRangeException("DataSet moze biti od 1 do 4");
            }

            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException("ID mora biti veci od nule");
            }




            string tabela = "";

            if (dataSet == 1)
            {
                connection = new SQLiteConnection("Data Source=dataSet1.sqlite3");
                tabela = "dataSet1";
            }
            else if (dataSet == 2)
            {
                connection = new SQLiteConnection("Data Source=dataSet2.sqlite3");
                tabela = "dataSet2";
            }
            else if (dataSet == 3)
            {
                connection = new SQLiteConnection("Data Source=dataSet3.sqlite3");
                tabela = "dataSet3";
            }
            else if (dataSet == 4)
            {
                connection = new SQLiteConnection("Data Source=dataSet4.sqlite3");
                tabela = "dataSet4";
            }




            string query = $"SELECT * FROM {tabela}";
            List<Item> temp = new List<Item>();

            SQLiteCommand command = new SQLiteCommand(query, connection);
            OpenConnection();
            SQLiteDataReader read = command.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    if (read.GetInt32(0) == id && read.GetInt32(1) == dataSet)
                    {
                        CodeEnum codeEnum;
                        Enum.TryParse(read.GetString(2), out codeEnum);
                        temp.Add(new Item(codeEnum, read.GetDouble(3)));
                    }
                }
            }

            CloseConnection();


            return temp;
        }






        public string IstorijaCoda(CodeEnum code)
        {
            try
            {
                string tabela = "";
                if (code == CodeEnum.CODE_ANALOG)
                {
                    connection = new SQLiteConnection("Data Source=dataSet1.sqlite3");
                    tabela = "dataSet1";
                }
                else if (code == CodeEnum.CODE_DIGITAL)
                {
                    connection = new SQLiteConnection("Data Source=dataSet1.sqlite3");
                    tabela = "dataSet1";
                }
                else if (code == CodeEnum.CODE_CUSTOM)
                {
                    connection = new SQLiteConnection("Data Source=dataSet2.sqlite3");
                    tabela = "dataSet2";
                }
                else if (code == CodeEnum.CODE_LIMITSET)
                {
                    connection = new SQLiteConnection("Data Source=dataSet2.sqlite3");
                    tabela = "dataSet2";
                }
                else if (code == CodeEnum.CODE_SINGLENODE)
                {
                    connection = new SQLiteConnection("Data Source=dataSet3.sqlite3");
                    tabela = "dataSet3";
                }
                else if (code == CodeEnum.CODE_MULTIPLENODE)
                {
                    connection = new SQLiteConnection("Data Source=dataSet3.sqlite3");
                    tabela = "dataSet3";
                }
                else if (code == CodeEnum.CODE_CONSUMER)
                {
                    connection = new SQLiteConnection("Data Source=dataSet4.sqlite3");
                    tabela = "dataSet4";
                }
                else
                {
                    connection = new SQLiteConnection("Data Source=dataSet4.sqlite3");
                    tabela = "dataSet4";
                }

                string temp = "";
                string query = $"SELECT * FROM {tabela}";

                SQLiteCommand command = new SQLiteCommand(query, connection);
                OpenConnection();
                SQLiteDataReader read = command.ExecuteReader();
                if (read.HasRows)
                {
                    while (read.Read())
                    {
                        if (read.GetString(2) == code.ToString())
                        {
                            temp += $"Code: {read.GetString(2)} - Value: {read.GetDouble(3)} - Date: {read.GetString(4)}\n";
                        }
                    }
                }

                CloseConnection();

                return temp;
            }
            catch
            {
                throw new ArgumentNullException("Code ne sme biti null vrednosti");
            }
        }







        public void UpisUBazuPodataka(List<Item> item, int id, int dataSet)
        {
            try
            {
                if (dataSet < 0 || dataSet > 4)
                {
                    throw new ArgumentOutOfRangeException("DataSet moze biti od 1 do 4");
                }

                if (id <= 0)
                {
                    throw new ArgumentOutOfRangeException("ID mora biti veci od nule");
                }


                string tabela = "";

                if (dataSet == 1)
                {
                    connection = new SQLiteConnection("Data Source=dataSet1.sqlite3");
                    tabela = "dataSet1";
                }
                else if (dataSet == 2)
                {
                    connection = new SQLiteConnection("Data Source=dataSet2.sqlite3");
                    tabela = "dataSet2";
                }
                else if (dataSet == 3)
                {
                    connection = new SQLiteConnection("Data Source=dataSet3.sqlite3");
                    tabela = "dataSet3";
                }
                else if (dataSet == 4)
                {
                    connection = new SQLiteConnection("Data Source=dataSet4.sqlite3");
                    tabela = "dataSet4";
                }



                string query = $"INSERT INTO {tabela}('id', 'dataSet', 'code', 'value', 'date') " +
                                "values (@id, @dataSet, @code, @value, @date)";

                SQLiteCommand command = new SQLiteCommand(query, connection);
                OpenConnection();
                foreach (Item i in item)
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@dataSet", dataSet);
                    command.Parameters.AddWithValue("@code", i.Code.ToString());
                    command.Parameters.AddWithValue("@value", i.Value);
                    command.Parameters.AddWithValue("@date", DateTime.Now.ToString());
                    command.ExecuteNonQuery();
                }
                CloseConnection();
            }
            catch
            {
                throw new ArgumentNullException("Parametri ne smeju biti null");
            }
        }
    }
}
