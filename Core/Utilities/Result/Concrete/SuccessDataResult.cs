﻿using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result.Concrete
{
    public class SuccessDataResult<T> : DataResult<T> , ICountResult
    {
        public int TotalCount { get ; set ; }

        public SuccessDataResult(T data ) : base(data, true )
        {
        }
        public SuccessDataResult(T data,int count) : base(data, true)
        {
            TotalCount = count;
        }

        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
        }
        public SuccessDataResult(string message) : base(default, true, message)
        {
        }
       

    }
}
