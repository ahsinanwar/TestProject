//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Reporting.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CardType
    {
        public CardType()
        {
            this.Cards = new HashSet<Card>();
        }
    
        public byte CardTypeID { get; set; }
        public string CardTypeName { get; set; }
    
        public virtual ICollection<Card> Cards { get; set; }
    }
}
