using DBModels.Models;
using System.Collections.Generic;

namespace Mocker.Tests.Utility
{
    public static class MockDataClass
    {
        public static List<Developer> GetDevelopers()
        {
            List<Developer> developers = new List<Developer>();
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

            Developer dev2 = new Developer()
            {
                FullName = "Test User",
                UserId = "test.user",
                DevApps = new List<DevApp>()
                {
                    new DevApp()
                    {
                        AppName = "Sample App",
                        AppEntitiys = new List<AppEntity>()
                        {
                            new AppEntity()
                            {
                                EntityName = "en1",
                                EntityFields = new List<EntityField>()
                                {
                                    new EntityField()
                                    {
                                        FieldName = "fn1",
                                        FieldType = "string"
                                    },
                                    new EntityField()
                                    {
                                         FieldName = "n1",
                                        FieldType = "string"
                                    }
                                }
                            },
                            new AppEntity()
                            {
                                EntityName = "en2",
                                EntityFields = new List<EntityField>()
                                {
                                    new EntityField()
                                    {
                                        FieldName = "fn1",
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

            developers.Add(dev1);
            developers.Add(dev2);

            return developers;
        }
    }
}
