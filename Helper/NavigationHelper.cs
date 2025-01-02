using SandboxRazor.Models.Identity;

namespace SandboxRazor.Helper
{
    public static class NavigationHelper
    {
        public static List<Navigation> DataInitializationSeedNavigations()
        {
            List<Navigation> navigationList = new List<Navigation>()
            {
                #region Heading 1 - Master Data, Transaksi, Persetujuan, Laporan

                    new Navigation()
                    {
                        ParentCode = null,
                        Icon = "fas fa-fw fa-cogs", // Icon for "Master Data"
                        Url = null,
                        Code = "H1-MASTER",
                        Name = "Master Data",
                        Index = 1
                    },
                    new Navigation()
                    {
                        ParentCode = null,
                        Icon = "fas fa-fw fa-exchange-alt", // Icon for "Transaksi"
                        Url = null,
                        Code = "H1-TRANS",
                        Name = "Transaksi",
                        Index = 2
                    },
                    new Navigation()
                    {
                        ParentCode = null,
                        Icon = "fas fa-fw fa-check-circle", // Icon for "Persetujuan"
                        Url = null,
                        Code = "H1-APPROVAL",
                        Name = "Persetujuan",
                        Index = 3
                    },
                    new Navigation()
                    {
                        ParentCode = null,
                        Icon = "fas fa-fw fa-chart-line", // Icon for "Laporan"
                        Url = null,
                        Code = "H1-REPORT",
                        Name = "Laporan",
                        Index = 4
                    },

                #endregion


                #region Heading 2 - Master Data
                
                    new Navigation()
                    {
                        ParentCode = "H1-MASTER",
                        Icon = "fas fa-fw fa-user-circle", // Icon for "Profil"
                        Url = null,
                        Code = "H2-MASTER-01",
                        Name = "Profil",
                        Index = 1
                    },
                    new Navigation()
                    {
                        ParentCode = "H1-MASTER",
                        Icon = "fas fa-fw fa-sitemap", // Icon for "Struktur Organisasi"
                        Url = null,
                        Code = "H2-MASTER-02",
                        Name = "Struktur Organisasi",
                        Index = 2
                    },
                    new Navigation()
                    {
                        ParentCode = "H1-MASTER",
                        Icon = "fas fa-fw fa-users-cog", // Icon for "Pengembangan SDM"
                        Url = null,
                        Code = "H2-MASTER-03",
                        Name = "Pengembangan SDM",
                        Index = 3
                    },
                    new Navigation()
                    {
                        ParentCode = "H1-MASTER",
                        Icon = "fas fa-fw fa-pills", // Icon for "Medis"
                        Url = null,
                        Code = "H2-MASTER-04",
                        Name = "Medis",
                        Index = 4
                    },
                    new Navigation()
                    {
                        ParentCode = "H1-MASTER",
                        Icon = "fas fa-fw fa-cogs", // Icon for Dynamics 365 functionality
                        Url = null,
                        Code = "H2-MASTER-05",
                        Name = "BC365 Dynamics",
                        Index = 5
                    },

                #endregion

                #region Heading 2 - Transaksi
                
                    new Navigation()
                    {
                        ParentCode = "H1-TRANS",
                        Icon = "fas fa-fw fa-money-check-alt", // Icon for "Penggajian"
                        Url = null,
                        Code = "H2-TRANS-01",
                        Name = "Penggajian",
                        Index = 1
                    },
                    new Navigation()
                    {
                        ParentCode = "H1-TRANS",
                        Icon = "fas fa-fw fa-capsules", // Icon for "Medis"
                        Url = null,
                        Code = "H2-TRANS-02",
                        Name = "Medis",
                        Index = 2
                    },

                #endregion

                #region Heading 2 - Persetujuan
                
                    new Navigation()
                    {
                        ParentCode = "H1-APPROVAL",
                        Icon = "fas fa-fw fa-check-circle", // Icon for "Konfigurasi Persetujuan"
                        Url = null,
                        Code = "H2-APPROVAL-01",
                        Name = "Konfigurasi Persetujuan",
                        Index = 1
                    },

                #endregion

                #region Heading 2 - Laporan
                
                    new Navigation()
                    {
                        ParentCode = "H1-REPORT",
                        Icon = "fas fa-fw fa-users-cog", // Icon for "Pengembangan SDM"
                        Url = null,
                        Code = "H2-REPORT-01",
                        Name = "Pengembangan SDM",
                        Index = 1
                    },
                    new Navigation()
                    {
                        ParentCode = "H1-REPORT",
                        Icon = "fas fa-fw fa-file-alt", // Icon for "Penggajian"
                        Url = null,
                        Code = "H2-REPORT-02",
                        Name = "Penggajian",
                        Index = 2
                    },

                #endregion


                #region Heading 3 - Master Data
                    
                    #region Heading 3 - Profil

                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-01",
                            Icon = "fas fa-fw fa-user-shield", // Icon for "Hak Akses"
                            Url = null,
                            Code = "H2-MASTER-01-01",
                            Name = "Hak Akses",
                            Index = 1
                        },
                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-01",
                            Icon = "fas fa-fw fa-users", // Icon for "Pengguna"
                            Url = "/Identity/ApplicationUserPages/Index",
                            Code = "H2-MASTER-01-02",
                            Name = "Pengguna",
                            Index = 2
                        },

	                #endregion

                    #region Heading 3 - Struktur Organisasi

                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-02",
                            Icon = "fas fa-fw fa-building", // Icon for "Perusahaan"
                            Url = "/Organization/CompanyPages/Index",
                            Code = "H2-MASTER-02-01",
                            Name = "Perusahaan",
                            Index = 1
                        },

                    #endregion

                    #region Heading 3 - Pengembangan SDM

                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-03",
                           Icon = "fas fa-fw fa-users", // Icon for "Karyawan"
                            Url = "/HumanResource/EmployeePages/Index",
                            Code = "H2-MASTER-03-01",
                            Name = "Karyawan",
                            Index = 1
                        },

                    #endregion

                    #region Heading 3 - Medis

                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-04",
                            Icon = "fas fa-fw fa-capsules", // Icon for "Medis"
                            Url = "/Pharmacy/MedicationPages/Index",
                            Code = "H2-MASTER-04-01",
                            Name = "Medis",
                            Index = 1
                        },
                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-04",
                            Icon = "fas fa-fw fa-truck-loading", // Icon for "Supplier"
                            Url = "/Pharmacy/SupplierPages/Index",
                            Code = "H2-MASTER-04-02",
                            Name = "Supplier",
                            Index = 2
                        },

                    #endregion

                    #region Heading 3 - BC365

                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-05",
                            Icon = "fas fa-fw fa-users", // Icon representing customer management
                            Url = "/BC365/CustomerPages/Index",
                            Code = "H2-MASTER-05-01",
                            Name = "Customer",
                            Index = 1
                        },

                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-05",
                            Icon = "fas fa-fw fa-file-invoice-dollar", // Icon for financial management
                            Url = "/BC365/FinancialPages/Index",
                            Code = "H2-MASTER-05-02",
                            Name = "Financials",
                            Index = 2
                        },

                        new Navigation()
                        {
                            ParentCode = "H2-MASTER-05",
                            Icon = "fas fa-fw fa-warehouse", // Icon for inventory or warehouse management
                            Url = "/BC365/InventoryPages/Index",
                            Code = "H2-MASTER-05-03",
                            Name = "Inventory",
                            Index = 3
                        },
                        
                    #endregion

                #endregion

                #region Heading 3 - Transaksi

                    #region Heading 3 - Penggajian

                        new Navigation()
                        {
                            ParentCode = "H2-TRANS-01",
                            Icon = "fas fa-fw fa-money-check-alt", // Icon for "Konfigurasi Penggajian"
                            Url = null,
                            Code = "H2-TRANS-01-01",
                            Name = "Konfigurasi Penggajian",
                            Index = 1
                        },

                    #endregion

                    #region Heading 3 - Medis

                        new Navigation()
                        {
                            ParentCode = "H2-TRANS-02",
                            Icon = "fas fa-fw fa-cash-register", // Icon for "Sale"
                            Url = "/Pharmacy/SalePages/Index",
                            Code = "H2-TRANS-02-01",
                            Name = "Sale",
                            Index = 1
                        },
                        new Navigation()
                        {
                            ParentCode = "H2-TRANS-02",
                            Icon = "fas fa-fw fa-hospital-alt", // Icon for "Transaksi Medis"
                            Url = "/Pharmacy/TransactionPages/Index",
                            Code = "H2-TRANS-02-02",
                            Name = "Transaksi Medis",
                            Index = 2
                        },

                    #endregion

                #endregion

            };
            return navigationList;
        }
    }
}