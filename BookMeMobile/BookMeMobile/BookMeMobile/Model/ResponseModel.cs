﻿namespace BookMeMobile.Model
{
    public class ResponseModel<T> : BaseResponseModel
    {
        public T Result { get; set; }
    }
}