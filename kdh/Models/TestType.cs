
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace kdh.Models
{

using System;
    using System.Collections.Generic;
    
public partial class TestType
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public TestType()
    {

        this.Results = new HashSet<Result>();

    }


    public System.Guid Id { get; set; }

    public string Category { get; set; }

    public string TestItem { get; set; }

    public double MaxReference { get; set; }

    public double MinReference { get; set; }

    public string Unit { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Result> Results { get; set; }

}

}
