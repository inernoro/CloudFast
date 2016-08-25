using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Abp.Dependency;
using Cloud.Framework.Assembly;
using Cloud.Framework.Dapper;
using Dapper;

namespace Cloud.Dapper.Framework
{
    public static class BottomDapper
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">Sql</param>
        /// <param name="obj">参数</param>
        /// <returns></returns>
        public static List<T> Query<T>(string sql, object obj = null)
        {
            DebuggerSql(sql, obj);
            using (IDbConnection conn = new SqlConnection(PersistentConfigurage.SlaveConnectionString))
            {
                var data = conn.Query<T>(sql, obj).ToList();
                return data;
            }
        }

        /// <summary>
        /// 列表查询 Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">Sql</param>
        /// <param name="obj">参数</param>
        /// <returns></returns>
        public static IEnumerable<T> QueryEnumerable<T>(string sql, object obj)
        {
            DebuggerSql(sql, obj);
            using (IDbConnection conn = new SqlConnection(PersistentConfigurage.SlaveConnectionString))
            {
                return conn.Query<T>(sql, obj);
            }
        }

        /// <summary>
        /// 查询，执行存储过程
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParentType"></typeparam>
        /// <param name="proc"></param>
        /// <param name="procParams"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryProc<T, TParentType>(string proc, TParentType procParams)
        {
            DebuggerSql(proc, procParams);
            using (IDbConnection conn = new SqlConnection(PersistentConfigurage.SlaveConnectionString))
            {
                return conn.Query<T>(proc, procParams, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        ///  存储过程查询，返回多个结果集的读取方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TParentType"></typeparam>
        /// <param name="proc"></param>
        /// <param name="procParams"></param>
        /// <param name="readResult"></param>
        /// <returns></returns>
        public static T QueryProc<T, TParentType>(string proc, TParentType procParams, Func<SqlMapper.GridReader, T> readResult)
        {
            DebuggerSql(proc, procParams);
            IDbConnection conn = null;
            SqlMapper.GridReader reader = null;
            try
            {
                conn = new SqlConnection(PersistentConfigurage.SlaveConnectionString);
                reader = conn.QueryMultiple(proc, procParams, commandType: CommandType.StoredProcedure);
                return readResult(reader);
            }
            finally
            {
                reader?.Dispose();
                conn?.Dispose();
            }
        }

        /// <summary>
        /// 查询，返回多个结果集的读取方式
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <typeparam name="TPType"></typeparam>
        /// <param name="proc">存储过程名</param>
        /// <param name="sqlParams"></param>
        /// <param name="readResult">对结果集的处理函数</param>
        /// <returns></returns>
        public static T QueryMultiple<T, TPType>(string proc, TPType sqlParams, Func<SqlMapper.GridReader, T> readResult)
        {
            DebuggerSql(proc, sqlParams);
            IDbConnection conn = null;
            SqlMapper.GridReader reader = null;
            try
            {
                conn = new SqlConnection(PersistentConfigurage.SlaveConnectionString);
                reader = conn.QueryMultiple(proc, sqlParams);
                return readResult(reader);
            }
            finally
            {
                reader?.Dispose();
                conn?.Dispose();
            }
        }


        /// <summary>
        /// 查询操作，需参数，返回第一行第一列结果
        /// </summary>
        /// <param name="sql"></param> 
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public static T QueryScalar<T>(string sql, object sqlParams = null)
        {
            DebuggerSql(sql, sqlParams);
            using (IDbConnection conn = new SqlConnection(PersistentConfigurage.SlaveConnectionString))
            {
                return conn.ExecuteScalar<T>(sql, sqlParams);
            }
        }

        /// <summary>
        /// 执行SQL，返回影响行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="sqlParams">参数</param>
        /// <returns></returns>
        public static int ExecuteSql(string sql, object sqlParams = null)
        {
            DebuggerSql(sql, sqlParams);
            using (IDbConnection conn = new SqlConnection(PersistentConfigurage.MasterConnectionString))
            {
                return conn.Execute(sql, sqlParams, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 执行PROC，返回影响行数
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="procParams">参数</param>
        /// <returns></returns>
        public static int ExecuteProc(string proc, object procParams = null)
        {
            DebuggerSql(proc, procParams);
            using (IDbConnection conn = new SqlConnection(PersistentConfigurage.MasterConnectionString))
            {
                return conn.Execute(proc, procParams, commandType: CommandType.StoredProcedure);
            }
        }

        public static void DebuggerSql(string sql, object parament)
        {
#if DEBUG
            try
            {
                IocManager.Instance.Resolve<INetWorkStrategy>().Send("SqlCode", new { sql, parament });
            }
            catch (Exception)
            {
                /**/
            }
#endif
        }
    }
}