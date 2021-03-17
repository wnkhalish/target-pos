using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System;
using System.Data;
using System.IO;
using UnityEngine.UI;

public class EditDB : MonoBehaviour
{
    [SerializeField]
    public EditSettings editSettings;
 

    private string conn, sqlQuery;
    IDbConnection dbconn;
    IDbCommand dbcmd;
    private IDataReader reader;


    string DatabaseName = "CustomTraining.s3db";

    void Start()
    {

        string filepath = Application.dataPath + "/Plugins/" + DatabaseName;

        conn = "URI=file:" + filepath;

        Debug.Log("Stablishing connection to: " + conn);
        dbconn = new SqliteConnection(conn);
        dbconn.Open();

        reader_function();
    }
    //Insert
    public void insert_button()
    {
        insert_function(editSettings.radiusSizeDropdown.options[editSettings.radiusSizeDropdown.value].text,editSettings.timeOnTargetDropdown.options[editSettings.timeOnTargetDropdown.value].text);
    }

    //Update
   public void Update_button()
    {
        update_function(editSettings.radiusSizeDropdown.options[editSettings.radiusSizeDropdown.value].text, editSettings.timeOnTargetDropdown.options[editSettings.timeOnTargetDropdown.value].text);
    }


    //Insert
    private void insert_function(string radiusSizeDropdown, string timeOnTargetDropdown)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("insert into Custom (name, Contact) values (\"{0}\",\"{1}\")", radiusSizeDropdown, timeOnTargetDropdown);
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }

        Debug.Log("Insert Done  ");

        reader_function();
    }

    private void reader_function()
    {

        string Namereaders, Contactreaders;
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            IDbCommand dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT  Name, Contact " + "FROM Custom";
            dbcmd.CommandText = sqlQuery;
            IDataReader reader = dbcmd.ExecuteReader();
            while (reader.Read())
            {

                Namereaders = reader.GetString(0);
                Contactreaders = reader.GetString(1);

                Debug.Log(" name =" + Namereaders + "Contact =" + Contactreaders);
            }
            reader.Close();
            reader = null;
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
        }
    }


    //Update 
    private void update_function(string update_name, string update_contact)
    {
        using (dbconn = new SqliteConnection(conn))
        {
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE Custom set name = @name ,contact = @contact where name = @name ");

            SqliteParameter P_update_name = new SqliteParameter("@name", update_name);
            SqliteParameter P_update_contact = new SqliteParameter("@contact", update_contact);

            dbcmd.Parameters.Add(P_update_name);
            dbcmd.Parameters.Add(P_update_contact);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbconn.Close();
        }

    }

}