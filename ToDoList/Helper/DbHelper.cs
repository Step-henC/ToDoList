using System;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace ToDoList.Helper
{
    public class DbHelper
    {
       
            public static MySqlConnection GetConnection()
            {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "password";
            conn_string.Database = "ToDoList";


            return new MySqlConnection(conn_string.ToString());

          
        
            }
       
       
        
        
    
    }
}
