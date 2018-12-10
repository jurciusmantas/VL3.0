using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VirtualLibAPI
{
    public class APIRecognizer:IRecognizer
    {
        private IRequest _req;
        private bool _isErrorHappened;

        public APIRecognizer(IRequest req)
        {
            _req = req;
        }

        public int Recognize(List<string> faces, string faceFromApp)
        {
            var result = Parallel.For(0, faces.Count,(i, state)  =>
            {
                FacePlusResponse resp = _req.MakeRequestAsync<FacePlusResponse>(faceFromApp, faces[i]);

                if (resp.Error_message != null)
                {
                    _isErrorHappened = true;
                    state.Break();
                    return;
                }
                else
                {
                    if (resp.Confidence > 80.000)
                    {
                        state.Break();
                        return;
                    }

                }
            });
            if (_isErrorHappened)
            {
                return -1;
            }
            else
            {
                if (result.IsCompleted)
                {
                    return 0;
                }
                else
                {
                    return (int)result.LowestBreakIteration + 1;
                }
            }

        }
    }
}