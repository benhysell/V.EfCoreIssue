using System;
using System.Collections.Generic;
using System.Text;

namespace V.EfCoreIssueNet50.Entities
{
    public class Organization
    {
        /// <summary>
        /// Key
        /// </summary>        
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public Guid HierarchyId { get; set; }

        public Hierarchy Hierarchy { get; set; }
    }
}
