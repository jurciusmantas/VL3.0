using System;
using System.Collections.Generic;
using VirtualLibAPI.Models.Entities;
using VirtualLibAPI.Services;
using VirtualLibrarity.DataWorkers;
using VirtualLibrarity.Interfaces;
using VirtualLibrarity.Models;

namespace VirtualLibAPI
{
    public class PostHandler : IPostHandler
    {
        private readonly IReader _fr;
        private readonly IWriter _fw;
        private readonly IRecognizer _rec;
        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;
        private readonly IBookService _bookService;
        public PostHandler(IReader fr, IWriter fw, IRecognizer rec, IRegisterService registerService, ILoginService loginService, IBookService bookService)
        {
            _fr = fr;
            _fw = fw;
            _rec = rec;
            _registerService = registerService;
            _loginService = loginService;
            _bookService = bookService;

        }
        public UserToLoginResponse HandlePost<F>(F face)
            where F : IFace
        {
            var ids = _fr.ReadInfo();
            if (ids.Count==0)
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
            { var user = _loginService.FaceRecognitionLogin(_rec.Recognize(faces64String, face.Image64String));
                if (user == null)
                {
                    return new UserToLoginResponse();
                }
                else
                {
                    return new UserToLoginResponse
                    {
                        UserInfo = new VirtualLibrarity.Models.Entities.User
                        {
                            Id = user.Id,
                            Firstname = user.FirstName,
                            Lastname = user.LastName,
                            Email = user.Email,

                        },
                        BorrowedBooks = _bookService.GetUsersBorrowedBooks(user.Id)
                    };
                }
            }
        }
        public int HandleRegisterPost(RegisterArgs regArgs)
        {
            var ids = _fr.ReadInfo();
            bool ok1, ok2;
            int id;
            if (ids == null)
                id = 1;
            else
                id = ids[ids.Count-1] + 1;
            ok1 = _fw.WriteFaceToFile(id, regArgs.Image);
            ok2 = _fw.WriteInfoFile(id);
            if (ok1 && ok2)
            {
                if (!_registerService.Register(regArgs.User))
                    return 0;
                else
                    return 1;
            }
            return Convert.ToInt16(Strings.GetString("errorCode"));
        }
        public bool DeleteUserFromFile(int userId)
        {
            var ids = _fr.ReadInfo();
            bool ok1, ok2;
            ids.Remove(userId);
            ok1 = _fw.WriteAllIdsIntoInfoFile(ids);
            ok2 = _fw.DeleteFile(userId);
            return ok1 & ok2;
        }

    }
}