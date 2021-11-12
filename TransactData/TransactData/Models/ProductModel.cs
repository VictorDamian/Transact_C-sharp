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
        private int _id;
        private string _category;
        private string _mark;
        private string _description;
        private float _cost;

        public int Id { get => _id; set => _id = value; }
        public string Category { get => _category; set => _category = value; }
        public string Mark { get => _mark; set => _mark = value; }
        public string Description { get => _description; set => _description = value; }
        public float Cost { get => _cost; set => _cost = value; }

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
                    item._id,
                    item._category,
                    item._mark,
                    item._description,
                    item._cost
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
