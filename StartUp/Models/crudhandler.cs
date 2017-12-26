using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace StartUp.Models
{
    public class crudhandler : Configure
    {
        private SqlConnection con;
        private Dictionary<string,string> response;

        public crudhandler()
        {
            this.response = new Dictionary<string, string>();
            this.con = Configure.Connect();
        }
        private string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
        protected string delete(string tablename,Hashtable whereClause)
        {
            string querey = "delete from "+tablename+" where ";
            if(whereClause.Count != 0)
            {
                int index = 0;
                foreach (string key in whereClause.Keys)
                {
                    if(whereClause.Count - index == 1)
                    {
                        querey += key + " = " + whereClause[key]+" ";
                    }
                    else
                    {
                        querey += key + " = " + whereClause[key] + " and ";
                    }
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandText = querey;
                try
                {
                    this.con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if(rowsAffected > 0)
                    {
                        this.response.Add("status","success");
                        this.response.Add("rowsAffected",rowsAffected.ToString());
                        return JsonConvert.SerializeObject(this.response,Formatting.Indented);

                    }
                    else
                    {
                        this.response.Add("status", "success");
                        this.response.Add("rowsAffected", rowsAffected.ToString());
                        this.response.Add("result","no record found to the corresponding id");
                        return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                    }


                }catch(Exception ex)
                {
                    this.response.Add("status", "failed");
                    this.response.Add("rowsAffected","1");
                    this.response.Add("result",string.Format("{0}",ex.Message));
                    return JsonConvert.SerializeObject(this.response, Formatting.Indented);

                }finally
                {
                    this.con.Close();
                }


            }
            else
            {
                return "function parameter missing";
            }

            return null;
        }
        protected string update(string tablename,Hashtable column,Hashtable whereCondition)
        {
            int size,index;
            string querey = "update "+tablename+" set ";
            size = column.Count;
            index = 0;

            if (column.Count >= 1 && whereCondition.Count >= 1)
            {

                foreach (string key in column.Keys)
                {
                    if (size - index == 1)
                    {
                        querey += key + " = " + column[key]+" ";
                    }
                    else
                    {
                        querey += key + " = " + column[key] + " , ";
                    }
                    index++;
                }
                size = whereCondition.Count;
                index = 0;
                querey += " where ";
                foreach (string key in whereCondition.Keys)
                {
                    if (size - index == 1)
                    {
                        querey += key + " = " + whereCondition[key]+" ";
                    }
                    else
                    {
                        querey += key + " = " + whereCondition[key] + " and ";
                    }
                }

                SqlCommand cmd = new SqlCommand();
                try
                {
                    this.con.Open();
                    cmd.Connection = this.con;
                    cmd.CommandText = querey;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if(rowsAffected > 0)
                    {
                        this.response.Add("status","success");
                        this.response.Add("rowsAffected",rowsAffected.ToString());
                        return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                    }
                    else
                    {
                        this.response.Add("status", "failed");
                        this.response.Add("rowsAffected", rowsAffected.ToString());
                        return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                    }

                }catch(Exception ex)
                {
                    this.response.Add("status", "failed");
                    this.response.Add("rowsAffected", "0");
                    this.response.Add("result",string.Format("{0}",ex.Message));
                    return JsonConvert.SerializeObject(this.response, Formatting.Indented);

                }finally
                {
                    this.con.Close();
                }
            }
            else
            {
                return "paramter hashtable missing";
            }

            return null;
        }
        // simple insert 
        protected string insert(string tablename, List<Tuple<string, string>> Columns)
        {
            string querey = "insert into " + tablename + " (";
            if (Columns.Count > 0)
            {
                for (int i = 0; i < Columns.Count; i++)
                {
                    if (Columns.Count - i == 1)
                    {
                        querey += Columns[i].Item1 + " ";
                    }
                    else
                    {
                        querey += Columns[i].Item1 + ", ";
                    }
                }
                querey += ") values ( ";
                for (int i = 0; i < Columns.Count; i++)
                {
                    if (Columns.Count - i == 1)
                    {
                        querey += Columns[i].Item2 + " ";
                    }
                    else
                    {
                        querey += Columns[i].Item2 + ", ";
                    }
                }
                querey += ")";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandText = querey;
                try
                {
                    this.con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        this.response.Add("status", "success");
                        this.response.Add("rowsAffected", rowsAffected.ToString());
                        return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                    }
                    else
                    {
                        this.response.Add("status", "failed");
                        this.response.Add("rowsAffected", rowsAffected.ToString());
                        return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                    }
                }
                catch (Exception ex)
                {
                    this.response.Add("status", "failed");
                    this.response.Add("rowsAffected", "0");
                    this.response.Add("result", string.Format("{0}", ex.Message));
                    return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                }
                finally
                {
                    this.con.Close();
                }
            }
            else
            {
                return "missing function parameters";
            }
            return null;
        }
        // bulk insert
        protected string insert(string tablename,string[] Columns,Dictionary<int,List<string>> ColumnsValues)
        {
            string querey = "insert into "+tablename+"(";
            if(Columns.Length > 0 && ColumnsValues.Count > 0)
            {
                for (int i = 0; i < Columns.Length; i++)
                {
                    if(Columns.Length - i == 1)
                    {
                        querey += Columns[i] + " ";
                    }
                    else
                    {
                        querey += Columns[i] + ", ";
                    }
                }
                querey += ") ";
                int inner, outer;
                outer = 0;
                foreach (int index in ColumnsValues.Keys)
                {
                    if(ColumnsValues.Keys.Count - outer == 1)
                    {
                        querey += "select ";
                        inner = 0;
                        foreach (string item in ColumnsValues[index])
                        {
                            if (ColumnsValues[index].Count - inner == 1)
                            {
                                querey += item + " ";
                            }
                            else
                            {
                                querey += item + ", ";
                            }
                            inner++;
                        }
                    }
                    else
                    {
                        querey += "select ";
                        inner = 0;
                        foreach (string item in ColumnsValues[index])
                        {
                            if(ColumnsValues[index].Count - inner == 1)
                            {
                                querey += item + " ";
                            }
                            else
                            {
                                querey += item + ", ";
                            }
                            inner++;
                        }
                        querey += "union all ";
                    }
                    outer++;
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = this.con;
                cmd.CommandText = querey;
                try
                {
                    this.con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        this.response.Add("status", "success");
                        this.response.Add("rowsAffected", rowsAffected.ToString());
                        return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                    }
                    else
                    {
                        this.response.Add("status", "failed");
                        this.response.Add("rowsAffected", rowsAffected.ToString());
                        return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                    }
                }
                catch (Exception ex)
                {
                    this.response.Add("status", "failed");
                    this.response.Add("rowsAffected", "0");
                    this.response.Add("result", string.Format("{0}", ex.Message));
                    return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                }
                finally
                {
                    this.con.Close();
                }
                
            }
            else
            {
                return "missing function parameters";
            }
            return null;
        }

        protected string select(string tablename, List<Tuple<string, string>> whereClause = null, string[] selectParams = null)
        {
            string querey = "select ";

            if (tablename != null)
            {

                if (whereClause != null && selectParams != null)
                {
                    for (int i = 0; i < selectParams.Length; i++)
                    {
                        if (selectParams.Length - i == 1)
                        {
                            querey += selectParams[i] + " ";
                        }
                        else
                        {
                            querey += selectParams[i] + ", ";
                        }
                    }
                    querey += "from " + tablename + " where ";
                    for (int i = 0; i < whereClause.Count; i++)
                    {
                        if (whereClause.Count - i == 1)
                        {
                            querey += whereClause[i].Item1 + " = " + whereClause[i].Item2 + " ";
                        }
                        else
                        {
                            querey += whereClause[i].Item1 + " = " + whereClause[i].Item2 + " and ";
                        }
                    }
                }
                else if (selectParams != null && whereClause == null)
                {

                    for (int i = 0; i < selectParams.Length; i++)
                    {
                        if (selectParams.Length - i == 1)
                        {
                            querey += selectParams[i] + " ";
                        }
                        else
                        {
                            querey += selectParams[i] + ", ";
                        }
                    }
                    querey += " from " + tablename;

                }
                else if (selectParams == null && whereClause != null)
                {
                    querey += "* from " + tablename + " where ";
                    for (int i = 0; i < whereClause.Count; i++)
                    {
                        if (whereClause.Count - i == 1)
                        {
                            querey += whereClause[i].Item1 + " = " + whereClause[i].Item2 + " ";
                        }
                        else
                        {
                            querey += whereClause[i].Item1 + " = " + whereClause[i].Item2 + " and ";
                        }
                    }
                }
                else
                {
                    querey += " * from " + tablename;
                }
            }
            else
            {
                this.response.Add("status","failed");
                this.response.Add("rowsAffected","0");
                this.response.Add("result", "missing function parameters");
                return JsonConvert.SerializeObject(this.response,Formatting.Indented);
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.con;
            cmd.CommandText = querey;
            try
            {
                this.con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable t1 = new DataTable();
                if (dr.HasRows)
                {
                    t1.Load(dr);
                    this.response.Add("status","success");
                    this.response.Add("rowsAffected",t1.Rows.Count.ToString());
                    this.response.Add("result", this.DataTableToJSONWithStringBuilder(t1));
                    return JsonConvert.SerializeObject(this.response,Formatting.Indented);
                }
                else
                {
                    this.response.Add("status", "success");
                    this.response.Add("rowsAffected", "0");
                    this.response.Add("result", "No record found");
                    return JsonConvert.SerializeObject(this.response, Formatting.Indented);
                }
            }
            catch (Exception ex)
            {
                this.response.Add("status", "failed");
                this.response.Add("rowsAffected", "0");
                this.response.Add("result", string.Format("{0}", ex.Message));
                return JsonConvert.SerializeObject(this.response, Formatting.Indented);
            }
            finally
            {
                this.con.Close();
            }


            return null;
        }

        protected string executeQuerey(string querey)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.con;
            cmd.CommandText = querey;
            try
            {
                this.con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable t1 = new DataTable();
                if (dr.HasRows)
                {
                    t1.Load(dr);
                    this.response.Add("status", "success");
                    this.response.Add("rowsAffected", t1.Rows.Count.ToString());
                    this.response.Add("result", this.DataTableToJSONWithStringBuilder(t1));
                    return JsonConvert.SerializeObject(this.response, Formatting.Indented);

                }
                else
                {
                    this.response.Add("status","status");
                    this.response.Add("rowsAffected","0");
                    this.response.Add("result","not record found");
                }

            }
            catch(Exception ex)
            {
                this.response.Add("status","success");
                this.response.Add("rowsAffected","0");
                this.response.Add("result",string.Format("{0}",ex.Message));
                return JsonConvert.SerializeObject(this.response,Formatting.Indented);
            }
            finally
            {
                this.con.Close();
            }
            return null;
        }
        

    }
}