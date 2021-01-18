using DBLib.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace DBLib.AppDBContext
{
    public class SampleDataSeeder : DropCreateDatabaseIfModelChanges<MockDBContext>
    {
        protected override void Seed(MockDBContext context)
        {
            Developer dev1 = new Developer()
            {
                FullName = "Sankalp Chauhan",
                UserId = "sankalp.chauhan",
                DevApps = new List<DevApp>()
                {
                    new DevApp()
                    {
                        AppName = "Company Management System",
                        AppEntitiys = new List<AppEntity>()
                        {
                            new AppEntity()
                            {
                                EntityName = "Employee",
                                EntityFields = new List<EntityField>()
                                {
                                    new EntityField()
                                    {
                                        FieldName = "FirstName",
                                        FieldType = "string"
                                    },
                                    new EntityField()
                                    {
                                         FieldName = "LastName",
                                        FieldType = "string"
                                    }
                                }
                            },
                            new AppEntity()
                            {
                                EntityName = "Department",
                                EntityFields = new List<EntityField>()
                                {
                                    new EntityField()
                                    {
                                        FieldName = "DepartmentName",
                                        FieldType = "string"
                                    }
                                }
                            }

                        }
                    },
                    new DevApp()
                    {
                        AppName = "Sample App",
                        AppEntitiys = new List<AppEntity>()
                        {
                            new AppEntity()
                            {
                                EntityName = "SqlConnection",
                                EntityFields = new List<EntityField>()
                                {
                                    new EntityField()
                                    {
                                        FieldName = "ConnString",
                                        FieldType = "string"
                                    },
                                    new EntityField()
                                    {
                                         FieldName = "ConnId",
                                        FieldType = "int"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            context.Developers.Add(dev1);
            base.Seed(context);
        }
    }
}
