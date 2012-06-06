using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Driver.Extensions.EventListeners.Tests.Entities
{
    public class Employer : Entity, IInsertable, IModifieable, IRemovable
    {
        public string Name { get; set; }

        public IList<Employee> Employees { get; set; }

        #region Implementation of IInsertable

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        #endregion

        #region Implementation of IModifieable

        [BsonIgnoreIfNull]
        public string ModifiedBy { get; set; }

        [BsonIgnoreIfNull]
        public DateTime ModifiedOn { get; set; }

        #endregion

        #region Implementation of IRemovable

        [BsonIgnoreIfNull]
        public bool IsDeleted { get; set; }

        [BsonIgnoreIfNull]
        public string DeletedBy { get; set; }

        [BsonIgnoreIfNull]
        public DateTime DeletedOn { get; set; }

        #endregion
    }
}
