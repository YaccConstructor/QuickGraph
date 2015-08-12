using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Diagnostics.Contracts;

namespace QuickGraph.Data
{
    public sealed class DataRelationEdge 
        : IEdge<DataTable>
    {
        private readonly DataRelation relation;
        public DataRelationEdge(DataRelation relation)
        {
            Contract.Requires(relation != null);

            this.relation = relation;
        }

        public DataRelation Relation
        {
            get { return this.relation; }
        }

        public DataTable Source
        {
            get { return this.relation.ParentTable;}
        }

        public DataTable Target
        {
            get { return this.relation.ChildTable; }
        }
    }
}
