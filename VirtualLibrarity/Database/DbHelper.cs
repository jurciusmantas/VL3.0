using System;
using System.IO;
using Android.Content;
using Android.Database.Sqlite;

namespace VirtualLibrarity.Database
{
    public class DbHelper : SQLiteOpenHelper
    {
        private static string DB_PATH = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        private static string DB_NAME = "MyDB.db";
        private static int VERSION = 1;
        private Context _Context;

        public DbHelper(Context context):base(context, DB_NAME, null, VERSION)
        {
            _Context = context;
        }

        private string GetSQLDbPath()
        {
            return Path.Combine(DB_PATH, DB_NAME);
        }

        public override SQLiteDatabase WritableDatabase
        {
            get
            {
                return CreateSQLiteDB();
            }
        }

        private SQLiteDatabase CreateSQLiteDB()
        {
            SQLiteDatabase sqlDatabase = null;
            string path = GetSQLDbPath();
            Stream streamSQLite = null;
            FileStream streamWriter = null;
            Boolean isSQLiteInit = false;
            try
            {
                if (File.Exists(path))
                    isSQLiteInit = true;
                else
                {
                    streamSQLite = _Context.Resources.OpenRawResource(Resource.Raw.MyDB);
                    streamWriter = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    if (streamSQLite != null && streamWriter != null)
                    {
                        if (CopySQLiteDB(streamSQLite, streamWriter))
                            isSQLiteInit = true;

                    }
                }
                if (isSQLiteInit)
                    sqlDatabase = SQLiteDatabase.OpenDatabase(path, null, DatabaseOpenFlags.OpenReadonly);
            }
            catch { }
            return sqlDatabase;
        }

        private bool CopySQLiteDB(Stream streamSQLite, FileStream streamWriter)
        {
            bool isSucces = false;
            int length = 256;
            Byte[] buffer = new byte[length];
            try
            {
                int bytesRead = streamSQLite.Read(buffer, 0, length);
                while (bytesRead > 0)
                {
                    streamWriter.Write(buffer, 0, bytesRead);
                    bytesRead = streamSQLite.Read(buffer, 0, length);
                }
                isSucces = true;
            }
            catch { }
            finally
            {
                streamSQLite.Close();
                streamWriter.Close();
            }
            return isSucces;
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            throw new NotImplementedException();
        }
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            throw new NotImplementedException();
        }
    }
}