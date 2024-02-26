using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace AppointmentSystem.Models
{
    public class DBmanager
    {
        private readonly string ConnStr = "Data Source=USER;Initial Catalog=mmoo;User ID=sa ;pwd=snowwhite504";

        public List<Reservation> GetReservations()
        {
            List<Reservation> reservations = new List<Reservation>();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reserve");
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Reservation reservation = new Reservation {
                        ID = reader.GetInt32(reader.GetOrdinal("Id")),
                        NAME = reader.GetString(reader.GetOrdinal("Name")),
                        PHONE = reader.GetString(reader.GetOrdinal("Phone")),
                        DATE = reader.GetDateTime(reader.GetOrdinal("Date")),

                    };

                    Console.WriteLine(reservation.DATE.GetType()); // 檢查型別
                    reservations.Add(reservation);
                }
            }
            else
            {
                Console.WriteLine("資料庫為空！");
            }
            sqlConnection.Close();
            return reservations;
        }

        public void NewReservation(Reservation reservation)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand(
                @"INSERT INTO Reserve(Id, Name, Phone, Date)
                VALUES (@Id,@Name, @Phone,@Date)");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@Id", reservation.ID));
            // createReservation.cshtml裡的name要和models/reservation.cs中的變數名稱一樣
            sqlCommand.Parameters.Add(new SqlParameter("@Name", reservation.NAME));
            sqlCommand.Parameters.Add(new SqlParameter("@Phone", reservation.PHONE));
            sqlCommand.Parameters.Add(new SqlParameter("@Date", reservation.DATE));
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public Reservation GetReservationById(int id)
        {
            Reservation reservation = new Reservation();
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Reserve WHERE Id = @Id");
            sqlCommand.Connection = sqlConnection;
            sqlCommand.Parameters.Add(new SqlParameter("@Id", id));
;            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    reservation = new Reservation
                    {
                        ID = reader.GetInt32(reader.GetOrdinal("Id")),
                        NAME = reader.GetString(reader.GetOrdinal("Name")),
                        PHONE = reader.GetString(reader.GetOrdinal("Phone")),
                        DATE = reader.GetDateTime(reader.GetOrdinal("Date")),

                    };

                }
            }
            else
            {
                reservation.NAME = "未找到該筆資料";
            }
            sqlConnection.Close();
            return reservation;
        }
        public void UpdateReservation(Reservation reservation)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnStr)) 
            {
                sqlConnection.Open(); // Open the connection here

                using (SqlCommand sqlCommand = new SqlCommand(
                    @"UPDATE Reserve SET Name=@Name, Phone=@Phone, Date=@Date WHERE Id = @Id", sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("@Name", reservation.NAME));
                    sqlCommand.Parameters.Add(new SqlParameter("@Phone", reservation.PHONE));
                    sqlCommand.Parameters.Add(new SqlParameter("@Date", reservation.DATE));
                    sqlCommand.Parameters.Add(new SqlParameter("@Id", reservation.ID));

                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                } // SqlCommand and SqlConnection will be properly disposed here
            } // SqlConnection will be closed here
        }
        public void DeleteReservationByID(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnStr);
            SqlCommand sqlcommand = new SqlCommand("DELETE FROM Reserve WHERE Id=@Id");
            sqlcommand.Connection = sqlConnection;
            sqlcommand.Parameters.Add(new SqlParameter("@Id", id));
            sqlConnection.Open();
            sqlcommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

    }
}