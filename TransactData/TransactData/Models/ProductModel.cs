using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactData.DataAccess;

namespace TransactData.Models
{
    public class ProductModel:ConnectionSql
    {
        private int id;
        private string category;
        private string mark;
        private string description;
        private float cost;

        public int _Id { get => id; set => id = value; }
        public string _Category { get => category; set => category = value; }
        public string _Mark { get => mark; set => mark = value; }
        public string _Description { get => description; set => description = value; }
        public float _Cost { get => cost; set => cost = value; }

        public void InsertBigData(IEnumerable<ProductModel> products)
        {
            var table = new DataTable();
            table.Columns.Add("id", typeof(int));
            table.Columns.Add("category", typeof(string));
            table.Columns.Add("mark", typeof(string));
            table.Columns.Add("description", typeof(string));
            table.Columns.Add("cost", typeof(float));

            foreach (var item in products)
            {
                table.Rows.Add(new object[] 
                {
                    item._Id,
                    item._Category,
                    item._Mark,
                    item._Description,
                    item._Cost
                });
            }
            using (var conn = GetConnection())
            {
                conn.Open();
                using(SqlTransaction transaction = conn.BeginTransaction())
                {
                    using(SqlBulkCopy sqlBulk = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, transaction))
                    {
                        try
                        {
                            sqlBulk.DestinationTableName = "PRODUCTS";
                            sqlBulk.WriteToServer(table);
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            conn.Close();
                        }
                    }
                }
            }
        }
    }
}
