//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GreenMachine.Layer.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plant
    {
        public Plant()
        {
            this.ParentOne = new HashSet<Plant>();
            this.ParentTwo = new HashSet<Plant>();
            this.PlantTrait = new HashSet<PlantTrait>();
        }
    
        public long PlantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<long> ParentOneId { get; set; }
        public Nullable<long> ParentTwoId { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
    
        public virtual ICollection<Plant> ParentOne { get; set; }
        public virtual ICollection<Plant> ParentTwo { get; set; }
        public virtual ICollection<PlantTrait> PlantTrait { get; set; }
    }
}