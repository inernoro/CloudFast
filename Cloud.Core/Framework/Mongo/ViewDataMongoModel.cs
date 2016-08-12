using System;
using System.Collections.Generic;
using Abp.Domain.Entities;

// ReSharper disable PossibleNullReferenceException

namespace Cloud.Framework.Mongo
{
    public sealed class ViewDataMongoModel : Entity<string>, IMongodbBase
    {
        public ViewDataMongoModel()
        {
            State = DataState.Unchanged;
            Id = Guid.NewGuid().ToString();
        }

        /*
         *顺序颠倒
         *参数调换
         *方法重命名
        */

        //  [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [ContentDisplay("创建时间")]
        public DateTime CreationTime { get; set; }

        [ContentDisplay("运行次数")]
        public int RunningTimes { get; set; }

        [ContentDisplay("存储当前接口信息")]
        public IEnumerable<OpenDocumentResponse> OpenDocument { get; set; }

        public DataState State { get; set; }

        public enum DataState
        {
            Delete,
            Insert,
            Change,
            Unchanged
        }
    }
}