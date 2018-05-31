using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public abstract class BaseRepository<T> where T : class
    {
        private static SqlConnection _connection;
        public BaseRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public virtual T PopulateRecord(SqlDataReader reader)
        {
            return null;
        }

        protected IEnumerable<T> GetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _connection;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                        list.Add(PopulateRecord(reader));
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }

        protected bool HasData(SqlCommand command)
        {
            int output = 0;
            command.Connection = _connection;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        output = Convert.ToInt32(reader[0]);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return output != 0;
        }

        protected bool UpdateData(SqlCommand command)
        {
            int numberOfRecords = 0;
            command.Connection = _connection;
            _connection.Open();
            try
            {
                try
                {
                    numberOfRecords = command.ExecuteNonQuery();
                }
                finally
                {
                    // Always call Close when done reading.
                }
            }
            finally
            {
                command.Dispose();
                _connection.Close();
            }
            return numberOfRecords != 0;
        }

        protected T GetRecord(SqlCommand command)
        {
            T record = null;
            command.Connection = _connection;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        record = PopulateRecord(reader);
                        break;
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return record;
        }

        protected IEnumerable<T> ExecuteStoredProc(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _connection;
            command.CommandType = CommandType.StoredProcedure;
            _connection.Open();
            try
            {
                var reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        var record = PopulateRecord(reader);
                        if (record != null) list.Add(record);
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }
    }
}
