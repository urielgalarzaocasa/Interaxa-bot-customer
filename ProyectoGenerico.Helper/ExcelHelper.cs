using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace ProyectoGenerico.Helper
{
    public static class ExcelHelper
    {
        private static string GetConnectionString(string dataSource)
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = dataSource;

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }

        public static void DataTabletoExcel(this DataTable dataTable, string filePath, bool overwiteFile = true)
        {
            if (System.IO.File.Exists(filePath) && overwiteFile)
            {
                System.IO.File.Delete(filePath);
            }

            var conn = GetConnectionString(filePath);
            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand())
                {
                    command.Connection = connection;

                    List<string> columnNames = new List<string>();
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        columnNames.Add(dataColumn.ColumnName);
                    }

                    string tableName = !string.IsNullOrWhiteSpace(dataTable.TableName) ? dataTable.TableName : Guid.NewGuid().ToString();
                    command.CommandText = $"CREATE TABLE [{tableName}] ({string.Join(",", columnNames.Select(c => $"[{c}] VARCHAR").ToArray())});";
                    command.ExecuteNonQuery();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        List<string> rowValues = new List<string>();
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            rowValues.Add((row[column] != null && row[column] != DBNull.Value) ? row[column].ToString() : string.Empty);
                        }
                        command.CommandText = $"INSERT INTO [{tableName}]({string.Join(",", columnNames.Select(c => $"[{c}]"))}) VALUES ({string.Join(",", rowValues.Select(r => $"'{r.Replace("'", "''")}'").ToArray())});";
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }

        public static void DataSetToExcel(this DataSet dataSet, string filePath, bool overwiteFile = true)
        {
            try
            {
                if (System.IO.File.Exists(filePath) && overwiteFile)
                {
                    System.IO.File.Delete(filePath);
                }

                foreach (DataTable dataTable in dataSet.Tables)
                {
                    try
                    {
                        DataTabletoExcel(dataTable, filePath, false);
                    }
                    catch (Exception exTable)
                    {
                        LogHelper.GetInstance().PrintError(exTable);
                        throw exTable;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError(ex);
                throw ex;
            }
        }
    }
}