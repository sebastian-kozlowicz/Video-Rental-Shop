// <auto-generated />
namespace Video_Rental_Shop.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.2.0-61023")]
    public sealed partial class SetWillCascadeOnDeleteToTrueForCustomersTable : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(SetWillCascadeOnDeleteToTrueForCustomersTable));
        
        string IMigrationMetadata.Id
        {
            get { return "201908110953352_SetWillCascadeOnDeleteToTrueForCustomersTable"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}