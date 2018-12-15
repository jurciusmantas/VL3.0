using System;
using System.Collections.Generic;
using System.Configuration;
using VirtualLibDatabase;
using VirtualLibDatabase.Entities;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Interfaces;
using VirtualLibrarity.Models;

namespace VirtualLibAPI
{
    public class PostHandler : IPostHandler
    {
        private IReader _fr;
        private IWriter _fw;
        private IRecognizer _rec;
        public PostHandler(IReader fr, IWriter fw, IRecognizer rec)
        {
            _fr = fr;
            _fw = fw;
            _rec = rec;
        }
        public UserToLoginResponse HandlePost<F>(F face)
            where F : IFace
        {
            int[] ids = _fr.ReadInfo();
            if (ids.Length==0)
                return new UserToLoginResponse
                {
                    ExceptionMessage = Strings.GetString("ex3Message"),
                };
            List<string> faces64String = _fr.ReadFaces(ids);
            if (faces64String == null)
                return new UserToLoginResponse
                {
                    ExceptionMessage = Strings.GetString("ex4Message"),
                };
            else
                return new UserToLoginResponse
                {
                    UserInfo = MigrationResolver.Login(_rec.Recognize(faces64String, face.Image64String)),
                    //BorrowedBooks = 
                };
        }
        public int HandleRegisterPost(RegisterArgs regArgs)
        {
            int[] ids = _fr.ReadInfo();
            bool ok1, ok2;
            int id;
            if (ids == null)
                id = 1;
            else
                id=ids.Length+1;
            ok1 = _fw.WriteFaceToFile(id, regArgs.Image);
            ok2 = _fw.WriteInfoFile(id);
            if (ok1 && ok2)
            {
                if (!MigrationResolver.Register(regArgs.User))
                    return 0;
            }
            return Convert.ToInt16(Strings.GetString("errorCode"));
        }

    }
}